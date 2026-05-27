using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Controllers;

[Microsoft.AspNetCore.Authorization.Authorize]
public class AtendimentoController(
    ApplicationDbContext context,
    IAuthService authorizationService,
    UserManager<ApplicationUser> userManager,
    IAuditService auditService) : Controller
{

    // 1. LISTAGEM GERAL
    public async Task<IActionResult> Index()
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        // Alunos veem apenas seus atendimentos
        var atendimentos = await context.Atendimentos
            .Include(x => x.Paciente)
            .Include(x => x.Prontuario)
            .Where(a => User.IsInRole("Professor") || a.AlunoId == usuarioId)
            .AsNoTracking()
            .OrderByDescending(x => x.DataHoraInicio)
            .ToListAsync();

        return View(atendimentos);
    }

    // 2. DETALHES DO ATENDIMENTO
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        var atendimento = await context.Atendimentos
            .Include(a => a.Paciente)
            .Include(a => a.Prontuario)
            .Include(a => a.Evolucoes)
                .ThenInclude(e => e.CriadoPorUsuario) 
            .FirstOrDefaultAsync(m => m.Id == id);

        if (atendimento == null) return NotFound();

        // Validar autorização
        if (!await authorizationService.CanReadPacienteAsync(usuarioId, atendimento.PacienteId))
            return Forbid();

        return View(atendimento);
    }

    // 3. GET: CREATE
    public async Task<IActionResult> Create(Guid pacienteId)
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        // Validar autorização
        if (!await authorizationService.CanWritePacienteAsync(usuarioId, pacienteId))
            return Forbid();

        var paciente = await context.Pacientes
            .Include(p => p.Prontuario)
            .FirstOrDefaultAsync(p => p.Id == pacienteId);

        if (paciente == null) return NotFound("Paciente não encontrado.");

        if (paciente.Prontuario == null)
        {
            TempData["MensagemErro"] = "O paciente não possui um prontuário cadastrado.";
            return RedirectToAction("Details", "Paciente", new { id = pacienteId });
        }

        ViewBag.PacienteNome = paciente.NomeCompleto;
        ViewBag.ProntuarioNumero = paciente.Prontuario.NumeroProntuario;

        var atendimento = new Atendimento
        {
            PacienteId = paciente.Id,
            ProntuarioId = paciente.Prontuario.Id,
            DataHoraInicio = DateTime.Now,
            StatusAtendimento = StatusAtendimentoEnum.Agendado,
            AlunoId = usuarioId // Preenchendo automaticamente
        };

        PopulateEnums();
        return View(atendimento);
    }

    // 4. POST: CREATE
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PacienteId,ProntuarioId,TipoAtendimento,DataHoraInicio,DataHoraFim,StatusAtendimento,Observacoes")] Atendimento atendimento)
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        // Validar autorização
        if (!await authorizationService.CanWritePacienteAsync(usuarioId, atendimento.PacienteId))
            return Forbid();

        if (atendimento.DataHoraFim.HasValue && atendimento.DataHoraFim <= atendimento.DataHoraInicio)
        {
            ModelState.AddModelError("DataHoraFim", "A data de término deve ser posterior à data de início.");
        }

        LimparValidacoesNavegacao();

        if (ModelState.IsValid)
        {
            atendimento.AlunoId = usuarioId; // Definir o usuário logado
            context.Add(atendimento);
            await context.SaveChangesAsync();

            await AvaliarFrequenciaAsync(atendimento);

            // Registrar auditoria
            await auditService.LogAsync(
                usuarioId,
                TipoAcaoAuditoriaEnum.Insercao,
                nameof(Atendimento),
                atendimento.Id.ToString(),
                atendimento.PacienteId,
                atendimento.ProntuarioId,
                $"Atendimento criado: {atendimento.TipoAtendimento}");
            await auditService.SaveAuditAsync();

            TempData["MensagemSucesso"] = "Atendimento agendado com sucesso!";
            return RedirectToAction("Details", "Paciente", new { id = atendimento.PacienteId });
        }

        await LoadPatientInfoAsync(atendimento.PacienteId);
        PopulateEnums(atendimento.TipoAtendimento, atendimento.StatusAtendimento);
        return View(atendimento);
    }

    // 5. REGISTRAR EVOLUÇÃO (FINALIZAR SESSÃO)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegistrarEvolucao(int id, string relato)
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        var atendimento = await context.Atendimentos.FindAsync(id);
        
        if (atendimento == null) return NotFound();

        // Validar autorização
        if (!await authorizationService.CanWritePacienteAsync(usuarioId, atendimento.PacienteId))
            return Forbid();

        if (string.IsNullOrWhiteSpace(relato))
        {
            TempData["MensagemErro"] = "O relato da evolução não pode estar vazio.";
            return RedirectToAction(nameof(Details), new { id = id });
        }

        var novaEvolucao = new EvolucaoAtendimentoModel
        {
            AtendimentoId = id,
            TextoEvolucao = relato,
            DataEvolucao = DateTime.Now,
            CriadoPorUsuarioId = usuarioId // Registrar quem criou
        };

        atendimento.StatusAtendimento = StatusAtendimentoEnum.Realizado;
        atendimento.DataHoraFim = DateTime.Now;
        
        context.EvolucoesAtendimento.Add(novaEvolucao);
        context.Update(atendimento);
        
        await context.SaveChangesAsync();

        // Registrar auditoria
        await auditService.LogAsync(
            usuarioId,
            TipoAcaoAuditoriaEnum.Insercao,
            nameof(EvolucaoAtendimentoModel),
            novaEvolucao.Id.ToString(),
            atendimento.PacienteId,
            atendimento.ProntuarioId,
            "Evolução de atendimento registrada");
        await auditService.SaveAuditAsync();

        TempData["MensagemSucesso"] = "Atendimento finalizado!";
        return RedirectToAction(nameof(Details), new { id = id });
    }

    // 6. ADICIONAR EVOLUÇÃO MANUAL
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddEvolucao(int atendimentoId, string textoEvolucao, DateTime dataEvolucao)
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        var atendimento = await context.Atendimentos.FindAsync(atendimentoId);
        if (atendimento == null) return NotFound();

        // Validar autorização
        if (!await authorizationService.CanWritePacienteAsync(usuarioId, atendimento.PacienteId))
            return Forbid();

        if (string.IsNullOrWhiteSpace(textoEvolucao))
        {
            TempData["MensagemErro"] = "O texto da evolução não pode estar vazio.";
            return RedirectToAction("Details", new { id = atendimentoId });
        }

        var evolucao = new EvolucaoAtendimentoModel
        {
            AtendimentoId = atendimentoId,
            TextoEvolucao = textoEvolucao,
            DataEvolucao = dataEvolucao,
            CriadoPorUsuarioId = usuarioId // Registrar quem criou
        };

        context.EvolucoesAtendimento.Add(evolucao);
        await context.SaveChangesAsync();

        // Registrar auditoria
        await auditService.LogAsync(
            usuarioId,
            TipoAcaoAuditoriaEnum.Insercao,
            nameof(EvolucaoAtendimentoModel),
            evolucao.Id.ToString(),
            atendimento.PacienteId,
            atendimento.ProntuarioId,
            "Evolução adicional registrada");
        await auditService.SaveAuditAsync();

        TempData["MensagemSucesso"] = "Evolução adicionada!";
        return RedirectToAction("Details", new { id = atendimentoId });
    }

    // MÉTODOS AUXILIARES
    private void PopulateEnums(TipoAtendimentoEnum? tipo = null, StatusAtendimentoEnum? status = null)
    {
        ViewData["TipoAtendimento"] = new SelectList(Enum.GetValues(typeof(TipoAtendimentoEnum)), tipo);
        ViewData["StatusAtendimento"] = new SelectList(Enum.GetValues(typeof(StatusAtendimentoEnum)), status);
    }

    private async Task LoadPatientInfoAsync(Guid pacienteId)
    {
        var paciente = await context.Pacientes.Include(x => x.Prontuario).AsNoTracking().FirstOrDefaultAsync(x => x.Id == pacienteId);
        ViewBag.PacienteNome = paciente?.NomeCompleto ?? "Não encontrado";
        ViewBag.ProntuarioNumero = paciente?.Prontuario?.NumeroProntuario ?? "N/A";
    }

    private async Task AvaliarFrequenciaAsync(Atendimento atendimento)
    {
        if (atendimento.StatusAtendimento != StatusAtendimentoEnum.FaltaPaciente && atendimento.StatusAtendimento != StatusAtendimentoEnum.FaltaAluno)
            return;

        if (atendimento.FaltaJustificada)
            return;

        var faltasRecentes = await context.Atendimentos
            .Where(x => x.PacienteId == atendimento.PacienteId &&
                        (x.StatusAtendimento == StatusAtendimentoEnum.FaltaPaciente || x.StatusAtendimento == StatusAtendimentoEnum.FaltaAluno))
            .OrderByDescending(x => x.DataHoraInicio)
            .Take(3)
            .ToListAsync();

        var faltasNaoJustificadas = faltasRecentes.Count(x => !x.FaltaJustificada);
        var faltasConsecutivas = 0;

        foreach (var falta in faltasRecentes.OrderByDescending(x => x.DataHoraInicio))
        {
            if (!falta.FaltaJustificada)
                faltasConsecutivas++;
            else
                break;
        }

        if (faltasNaoJustificadas >= 3 || faltasConsecutivas >= 2)
        {
            var prontuario = await context.Prontuarios.FirstOrDefaultAsync(p => p.Id == atendimento.ProntuarioId);
            if (prontuario == null)
                return;

            if (prontuario.SituacaoProntuario != SituacaoProntuarioEnum.InativoDesligado)
            {
                prontuario.SituacaoProntuario = SituacaoProntuarioEnum.InativoDesligado;
                context.Update(prontuario);
                await context.SaveChangesAsync();
                TempData["MensagemAlertaFaltas"] = "O prontuário foi marcado como Inativo/Desligado devido a faltas injustificadas acumuladas ou consecutivas.";
            }
        }
    }

    private void LimparValidacoesNavegacao()
    {
        ModelState.Remove("Aluno");
        ModelState.Remove("Paciente");
        ModelState.Remove("Prontuario");
        ModelState.Remove("Evolucoes");
        ModelState.Remove("CriadoPorUsuario");
    }
}
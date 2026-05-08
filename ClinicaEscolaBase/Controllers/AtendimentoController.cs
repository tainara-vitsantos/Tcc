using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ClinicaEscolaBase.Controllers;

public class AtendimentoController : Controller
{
    private readonly ApplicationDbContext _context;

    public AtendimentoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // LISTAGEM GERAL
    public async Task<IActionResult> Index()
    {
        var atendimentos = await _context.Atendimentos
            .Include(x => x.Paciente)
            .Include(x => x.Prontuario)
            .AsNoTracking()
            .OrderByDescending(x => x.DataHoraInicio)
            .ToListAsync();

        return View(atendimentos);
    }

    // GET: CREATE
    public async Task<IActionResult> Create(Guid pacienteId)
    {
        var paciente = await _context.Pacientes
            .Include(p => p.Prontuario)
            .FirstOrDefaultAsync(p => p.Id == pacienteId);

        if (paciente == null) return NotFound("Paciente não encontrado.");

        if (paciente.Prontuario == null)
        {
            TempData["MensagemErro"] = "O paciente não possui um prontuário cadastrado. Crie o prontuário primeiro.";
            return RedirectToAction("Details", "Paciente", new { id = pacienteId });
        }

        ViewBag.PacienteNome = paciente.NomeCompleto;
        ViewBag.ProntuarioNumero = paciente.Prontuario.NumeroProntuario;

        var atendimento = new Atendimento
        {
            PacienteId = paciente.Id,
            ProntuarioId = paciente.Prontuario.Id,
            DataHoraInicio = DateTime.Now,
            StatusAtendimento = StatusAtendimento.Agendado,
            AlunoId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""
        };

        PopulateEnums();
        return View(atendimento);
    }

    // POST: CREATE
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PacienteId,ProntuarioId,TipoAtendimento,DataHoraInicio,DataHoraFim,StatusAtendimento,Observacoes,AlunoId")] Atendimento atendimento)
    {
        // Regra de Negócio: Data Fim deve ser posterior à Início
        if (atendimento.DataHoraFim.HasValue && atendimento.DataHoraFim <= atendimento.DataHoraInicio)
        {
            ModelState.AddModelError("DataHoraFim", "A data de término deve ser posterior à data de início.");
        }

        if (string.IsNullOrEmpty(atendimento.AlunoId))
        {
            atendimento.AlunoId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        }

        // Limpeza de validações automáticas de objetos complexos
        LimparValidacoesNavegacao();

        if (ModelState.IsValid)
        {
            try 
            {
                _context.Add(atendimento);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Atendimento agendado com sucesso!";
                return RedirectToAction("Details", "Paciente", new { id = atendimento.PacienteId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao salvar no banco: " + ex.Message);
            }
        }

        // Repopular dados se houver erro
        await LoadPatientInfoAsync(atendimento.PacienteId);
        PopulateEnums(atendimento.TipoAtendimento, atendimento.StatusAtendimento);
        return View(atendimento);
    }

    // ADICIONAR EVOLUÇÃO (DENTRO DE DETAILS)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddEvolucao(int atendimentoId, string textoEvolucao, DateTime dataEvolucao)
    {
        if (string.IsNullOrWhiteSpace(textoEvolucao))
        {
            TempData["MensagemErro"] = "O texto da evolução não pode estar vazio.";
            return RedirectToAction("Details", new { id = atendimentoId });
        }

        var atendimento = await _context.Atendimentos.FindAsync(atendimentoId);
        if (atendimento == null) return NotFound();

        var evolucao = new EvolucaoAtendimento
        {
            AtendimentoId = atendimentoId,
            TextoEvolucao = textoEvolucao,
            DataEvolucao = dataEvolucao,
            CriadoPorUsuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""
        };

        _context.EvolucoesAtendimento.Add(evolucao);
        await _context.SaveChangesAsync();

        TempData["MensagemSucesso"] = "Evolução adicionada com sucesso!";
        return RedirectToAction("Details", new { id = atendimentoId });
    }

    // MÉTODOS AUXILIARES (DRY - Don't Repeat Yourself)
    private void PopulateEnums(TipoAtendimento? tipo = null, StatusAtendimento? status = null)
    {
        ViewData["TipoAtendimento"] = new SelectList(Enum.GetValues(typeof(TipoAtendimento)), tipo);
        ViewData["StatusAtendimento"] = new SelectList(Enum.GetValues(typeof(StatusAtendimento)), status);
    }

    private async Task LoadPatientInfoAsync(Guid pacienteId)
    {
        var paciente = await _context.Pacientes
            .Include(x => x.Prontuario)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == pacienteId);

        ViewBag.PacienteNome = paciente?.NomeCompleto ?? "Não encontrado";
        ViewBag.ProntuarioNumero = paciente?.Prontuario?.NumeroProntuario ?? "N/A";
    }

    private void LimparValidacoesNavegacao()
    {
        ModelState.Remove("Aluno");
        ModelState.Remove("Paciente");
        ModelState.Remove("Prontuario");
        ModelState.Remove("DocumentosClinicos");
        ModelState.Remove("Evolucoes");
    }

    private bool AtendimentoExists(int id)
    {
        return _context.Atendimentos.Any(x => x.Id == id);
    }
}
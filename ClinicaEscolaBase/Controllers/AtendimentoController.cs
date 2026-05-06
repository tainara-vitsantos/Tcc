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

    public async Task<IActionResult> Create(Guid pacienteId)
{
    // Buscamos o paciente e incluímos o prontuário na mesma consulta
    var paciente = await _context.Pacientes
        .Include(p => p.Prontuario)
        .FirstOrDefaultAsync(p => p.Id == pacienteId);

    // Verificação 1: Paciente existe?
    if (paciente == null) return NotFound("Paciente não encontrado.");

    // Verificação 2: Prontuário existe?
    if (paciente.Prontuario == null)
    {
        TempData["Error"] = "O paciente não possui um prontuário cadastrado. Crie o prontuário primeiro.";
        return RedirectToAction("Details", "Paciente", new { id = pacienteId });
    }

    // Preparando o objeto para a View
    ViewBag.PacienteNome = paciente.NomeCompleto;
    ViewBag.ProntuarioNumero = paciente.Prontuario.NumeroProntuario;

    var atendimento = new Atendimento
    {
        PacienteId = paciente.Id,
        ProntuarioId = paciente.Prontuario.Id, // Aqui não dará mais erro de int vs Guid
        DataHoraInicio = DateTime.Now,
        StatusAtendimento = StatusAtendimento.Agendado,
        // AlunoId deve vir do sistema de login ou ser preenchido aqui
        AlunoId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""
    };

    PopulateEnums();
    return View(atendimento);
}

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PacienteId,ProntuarioId,TipoAtendimento,DataHoraInicio,DataHoraFim,StatusAtendimento,Observacoes,AlunoId")] Atendimento atendimento)
    {
        // Se o AlunoId veio vazio da View, tentamos pegar do User logado novamente
        if (string.IsNullOrEmpty(atendimento.AlunoId))
        {
            atendimento.AlunoId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        }

        // REMOVER VALIDAÇÕES QUE IMPEDEM O SALVAMENTO
        ModelState.Remove("Aluno");
        ModelState.Remove("Paciente");
        ModelState.Remove("Prontuario");
        ModelState.Remove("DocumentosClinicos");
        ModelState.Remove("Evolucoes");

        if (ModelState.IsValid)
        {
            try 
            {
                _context.Add(atendimento);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Atendimento agendado com sucesso!";
                return RedirectToAction("Details", "Paciente", new { id = atendimento.PacienteId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao salvar no banco: " + ex.Message);
            }
        }

        // Se deu erro, recarrega os dados da tela
        await LoadPatientInfoAsync(atendimento.PacienteId, atendimento.ProntuarioId);
        PopulateEnums();
        return View(atendimento);
    }

    private void PopulateEnums()
    {
        ViewData["TipoAtendimento"] = new SelectList(Enum.GetValues(typeof(TipoAtendimento)));
        ViewData["StatusAtendimento"] = new SelectList(Enum.GetValues(typeof(StatusAtendimento)));
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var atendimento = await _context.Atendimentos
            .Include(x => x.Paciente)
            .Include(x => x.Prontuario)
            .Include(x => x.Aluno)
            .Include(x => x.Supervisor)
            .Include(x => x.Evolucoes)
                .ThenInclude(e => e.CriadoPorUsuario)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (atendimento == null)
        {
            return NotFound();
        }

        return View(atendimento);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddEvolucao(int atendimentoId, string textoEvolucao, DateTime dataEvolucao)
    {
        var atendimento = await _context.Atendimentos.FindAsync(atendimentoId);
        if (atendimento == null)
        {
            return NotFound();
        }

        var evolucao = new EvolucaoAtendimento
        {
            AtendimentoId = atendimentoId,
            TextoEvolucao = textoEvolucao,
            DataEvolucao = dataEvolucao,
            CriadoPorUsuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? ""
        };

        _context.EvolucoesAtendimento.Add(evolucao);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Evolução adicionada com sucesso!";
        return RedirectToAction("Details", new { id = atendimentoId });
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var atendimento = await _context.Atendimentos.FindAsync(id.Value);
        if (atendimento == null)
        {
            return NotFound();
        }

        PopulateEnums();
        return View(atendimento);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,PacienteId,ProntuarioId,TipoAtendimento,DataHoraInicio,DataHoraFim,StatusAtendimento,Observacoes")] Atendimento atendimento)
    {
        if (id != atendimento.Id)
        {
            return BadRequest();
        }

        // Verificar se há evoluções para permitir status 'Realizado'
        if (atendimento.StatusAtendimento == StatusAtendimento.Realizado)
        {
            var hasEvolucoes = await _context.EvolucoesAtendimento.AnyAsync(e => e.AtendimentoId == id);
            if (!hasEvolucoes)
            {
                ModelState.AddModelError("StatusAtendimento", "Não é possível marcar como 'Realizado' sem evoluções registradas.");
            }
        }

        ModelState.Remove("Aluno");
        ModelState.Remove("Paciente");
        ModelState.Remove("Prontuario");
        ModelState.Remove("DocumentosClinicos");
        ModelState.Remove("Evolucoes");

        if (!ModelState.IsValid)
        {
            PopulateEnums();
            return View(atendimento);
        }

        try
        {
            _context.Update(atendimento);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AtendimentoExists(atendimento.Id))
            {
                return NotFound();
            }
            throw;
        }

        return RedirectToAction("Details", "Paciente", new { id = atendimento.PacienteId });
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var atendimento = await _context.Atendimentos
            .Include(x => x.Paciente)
            .Include(x => x.Prontuario)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (atendimento == null)
        {
            return NotFound();
        }

        return View(atendimento);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var atendimento = await _context.Atendimentos.FindAsync(id);
        if (atendimento != null)
        {
            var pacienteId = atendimento.PacienteId;
            _context.Atendimentos.Remove(atendimento);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Paciente", new { id = pacienteId });
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadPatientInfoAsync(Guid pacienteId, int prontuarioId)
    {
        var paciente = await _context.Pacientes
            .Include(x => x.Prontuario)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == pacienteId);

        ViewBag.PacienteNome = paciente?.NomeCompleto ?? "Não encontrado";
        ViewBag.ProntuarioNumero = paciente?.Prontuario?.NumeroProntuario ?? "N/A";
    }

    private async Task<bool> ValidateForeignKeysAsync(Atendimento atendimento)
    {
        var pacienteExists = await _context.Pacientes.AnyAsync(x => x.Id == atendimento.PacienteId);
        if (!pacienteExists)
        {
            ModelState.AddModelError(nameof(atendimento.PacienteId), "Paciente inválido.");
        }

        var prontuarioExists = await _context.Prontuarios.AnyAsync(x => x.Id == atendimento.ProntuarioId && x.PacienteId == atendimento.PacienteId);
        if (!prontuarioExists)
        {
            ModelState.AddModelError(nameof(atendimento.ProntuarioId), "Prontuário inválido ou não pertence ao paciente selecionado.");
        }

        return ModelState.IsValid;
    }

    private bool AtendimentoExists(int id)
    {
        return _context.Atendimentos.Any(x => x.Id == id);
    }
}
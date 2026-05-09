using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Controllers;

[Authorize]
public class ProntuarioController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly AuthorizationService _authorizationService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AuditService _auditService;

    public ProntuarioController(
        ApplicationDbContext context,
        AuthorizationService authorizationService,
        UserManager<ApplicationUser> userManager,
        AuditService auditService)
    {
        _context = context;
        _authorizationService = authorizationService;
        _userManager = userManager;
        _auditService = auditService;
    }

    // LISTAGEM GERAL
    public async Task<IActionResult> Index()
    {
        var usuarioId = _userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        // Alunos veem apenas prontuários de pacientes vinculados
        var acessibleIds = await _authorizationService.GetAcessiblePacienteIdsAsync(usuarioId);

        var prontuarios = await _context.Prontuarios
            .Include(x => x.Paciente)
            .Where(p => acessibleIds.Contains(p.PacienteId))
            .AsNoTracking()
            .OrderBy(x => x.NumeroProntuario)
            .ToListAsync();

        return View(prontuarios);
    }

    // DETALHES DO PRONTUÁRIO
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var usuarioId = _userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        var prontuario = await _context.Prontuarios
            .Include(x => x.Paciente)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (prontuario == null) return NotFound();

        // Validar autorização
        if (!await _authorizationService.CanReadPacienteAsync(usuarioId, prontuario.PacienteId))
            return Forbid();

        // Registrar auditoria
        await _auditService.LogVisualizacaoProntuarioAsync(usuarioId, prontuario.Id, prontuario.PacienteId);
        await _auditService.SaveAuditAsync();

        return View(prontuario);
    }

    // GET: CREATE (Suporta criação direta via PacienteId)
    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> Create(Guid? pacienteId)
    {
        if (pacienteId.HasValue)
        {
            var paciente = await _context.Pacientes.FindAsync(pacienteId.Value);
            if (paciente != null)
            {
                ViewBag.PacienteNome = paciente.NomeCompleto;
                var novoProntuario = new Prontuario 
                { 
                    PacienteId = pacienteId.Value,
                    DataPrimeiraConsulta = DateTime.Now,
                    SituacaoProntuario = SituacaoProntuario.Ativo
                };
                return View(novoProntuario);
            }
        }

        await PopulatePacientesDropDownList();
        return View();
    }

    // POST: CREATE
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> Create(Prontuario prontuario)
    {
        // Limpa validações de navegação para evitar erros no ModelState
        ModelState.Remove("Paciente");
        ModelState.Remove("Atendimentos");

        if (ModelState.IsValid)
        {
            try 
            {
                var usuarioId = _userManager.GetUserId(User);
                if (usuarioId == null) return Unauthorized();

                _context.Add(prontuario);
                await _context.SaveChangesAsync();

                // Registrar auditoria
                await _auditService.LogAsync(
                    usuarioId,
                    TipoAcaoAuditoria.Insercao,
                    nameof(Prontuario),
                    prontuario.Id.ToString(),
                    prontuario.PacienteId,
                    prontuario.Id,
                    $"Prontuário {prontuario.NumeroProntuario} criado");
                await _auditService.SaveAuditAsync();
                
                TempData["MensagemSucesso"] = "Prontuário aberto com sucesso! Agora você já pode agendar atendimentos.";
                
                // Redireciona para a ficha do paciente para seguir o fluxo
                return RedirectToAction("Details", "Paciente", new { id = prontuario.PacienteId });
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Erro ao salvar o prontuário. Verifique se o número do prontuário já existe.";
            }
        }

        await PopulatePacientesDropDownList(prontuario.PacienteId);
        return View(prontuario);
    }

    // GET: EDIT
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var usuarioId = _userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        var prontuario = await _context.Prontuarios
            .Include(p => p.Paciente)
            .FirstOrDefaultAsync(p => p.Id == id.Value);

        if (prontuario == null) return NotFound();

        // Validar autorização
        if (!await _authorizationService.CanWritePacienteAsync(usuarioId, prontuario.PacienteId))
            return Forbid();

        ViewBag.PacienteNome = prontuario.Paciente?.NomeCompleto;
        return View(prontuario);
    }

    // POST: EDIT
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Prontuario prontuario)
    {
        if (id != prontuario.Id) return BadRequest();

        var usuarioId = _userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        // Validar autorização
        if (!await _authorizationService.CanWritePacienteAsync(usuarioId, prontuario.PacienteId))
            return Forbid();

        ModelState.Remove("Paciente");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(prontuario);
                await _context.SaveChangesAsync();

                // Registrar auditoria
                await _auditService.LogAsync(
                    usuarioId,
                    TipoAcaoAuditoria.Atualizacao,
                    nameof(Prontuario),
                    prontuario.Id.ToString(),
                    prontuario.PacienteId,
                    prontuario.Id,
                    "Prontuário atualizado");
                await _auditService.SaveAuditAsync();

                TempData["MensagemSucesso"] = "Prontuário atualizado com sucesso.";
                return RedirectToAction("Details", "Paciente", new { id = prontuario.PacienteId });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProntuarioExists(prontuario.Id)) return NotFound();
                throw;
            }
        }
        return View(prontuario);
    }

    // GET: DELETE
    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var prontuario = await _context.Prontuarios
            .Include(x => x.Paciente)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (prontuario == null) return NotFound();

        return View(prontuario);
    }

    // POST: DELETE
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Professor")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var usuarioId = _userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        var prontuario = await _context.Prontuarios.FindAsync(id);
        if (prontuario != null)
        {
            var pacienteId = prontuario.PacienteId;
            _context.Prontuarios.Remove(prontuario);
            await _context.SaveChangesAsync();

            // Registrar auditoria
            await _auditService.LogAsync(
                usuarioId,
                TipoAcaoAuditoria.ExclusaoLogica,
                nameof(Prontuario),
                prontuario.Id.ToString(),
                prontuario.PacienteId,
                prontuario.Id,
                "Prontuário excluído");
            await _auditService.SaveAuditAsync();

            TempData["MensagemSucesso"] = "Prontuário excluído com sucesso.";
            return RedirectToAction("Details", "Paciente", new { id = pacienteId });
        }

        return RedirectToAction(nameof(Index));
    }

    // MÉTODOS AUXILIARES
    private async Task PopulatePacientesDropDownList(Guid? selectedPacienteId = null)
    {
        // Só mostra pacientes que AINDA NÃO possuem prontuário
        var pacientesQuery = _context.Pacientes
            .AsNoTracking()
            .Where(p => !_context.Prontuarios.Any(pr => pr.PacienteId == p.Id))
            .OrderBy(p => p.NomeCompleto);

        ViewData["PacienteId"] = new SelectList(await pacientesQuery.ToListAsync(), "Id", "NomeCompleto", selectedPacienteId);
    }

    private bool ProntuarioExists(int id)
    {
        return _context.Prontuarios.Any(x => x.Id == id);
    }
}
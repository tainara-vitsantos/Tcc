using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Services;
using ClinicaEscolaBase.ViewModels;
using ClinicaEscolaBase.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Controllers;

[Authorize]
public class DocumentoClinicoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly AuthorizationService _authorizationService;
    private readonly AuditService _auditService;
    private readonly UserManager<ApplicationUser> _userManager;

    public DocumentoClinicoController(
        ApplicationDbContext context,
        AuthorizationService authorizationService,
        AuditService auditService,
        UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _authorizationService = authorizationService;
        _auditService = auditService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var usuarioId = _userManager.GetUserId(User) ?? "";
        var acessibleIds = await _authorizationService.GetAcessiblePacienteIdsAsync(usuarioId);

        var documentos = await _context.DocumentosClinicos
            .Include(x => x.Paciente)
            .Include(x => x.Prontuario)
            .Include(x => x.CriadoPorUsuario)
            .Where(x => acessibleIds.Contains(x.PacienteId))
            .OrderByDescending(x => x.DataDocumento)
            .AsNoTracking()
            .ToListAsync();

        return View(documentos);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var usuarioId = _userManager.GetUserId(User) ?? "";

        var documento = await _context.DocumentosClinicos
            .Include(x => x.Paciente)
            .Include(x => x.Prontuario)
            .Include(x => x.Atendimento)
            .Include(x => x.CriadoPorUsuario)
            .Include(x => x.Anexos)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (documento == null) return NotFound();

        if (!await _authorizationService.CanReadPacienteAsync(usuarioId, documento.PacienteId))
            return Forbid();

        ViewBag.CanWrite = await _authorizationService.CanWritePacienteAsync(usuarioId, documento.PacienteId);
        return View(documento);
    }

    [HttpGet]
    public async Task<IActionResult> CreateAnamneseAdulto(Guid pacienteId)
    {
        var usuarioId = _userManager.GetUserId(User) ?? "";
        if (!await _authorizationService.CanWritePacienteAsync(usuarioId, pacienteId))
        {
            return Forbid();
        }

        var paciente = await _context.Pacientes
            .Include(p => p.Prontuario)
            .FirstOrDefaultAsync(p => p.Id == pacienteId);

        if (paciente == null || !paciente.Ativo)
        {
            return NotFound();
        }

        if (paciente.Prontuario == null)
        {
            TempData["MensagemErro"] = "O paciente ainda não possui prontuário. Abra o prontuário antes de criar uma anamnese.";
            return RedirectToAction("Details", "Paciente", new { id = pacienteId });
        }

        var viewModel = new AnamneseAdultoViewModel
        {
            PacienteId = pacienteId,
            PacienteNome = paciente.NomeCompleto,
            ProntuarioId = paciente.Prontuario.Id
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAnamneseAdulto(AnamneseAdultoViewModel viewModel)
    {
        var usuarioId = _userManager.GetUserId(User) ?? "";
        if (!await _authorizationService.CanWritePacienteAsync(usuarioId, viewModel.PacienteId))
        {
            return Forbid();
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var prontuario = await _context.Prontuarios.FirstOrDefaultAsync(p => p.PacienteId == viewModel.PacienteId);
        if (prontuario == null)
        {
            ModelState.AddModelError(string.Empty, "O paciente não possui um prontuário cadastrado.");
            return View(viewModel);
        }

        var documento = new DocumentoClinico
        {
            PacienteId = viewModel.PacienteId,
            ProntuarioId = prontuario.Id,
            AtendimentoId = viewModel.AtendimentoId,
            TipoDocumento = TipoDocumentoClinico.AnamneseAdulto,
            StatusDocumento = StatusDocumentoClinico.Rascunho,
            DataDocumento = DateTime.UtcNow,
            CriadoPorUsuarioId = usuarioId,
            Observacoes = System.Text.Json.JsonSerializer.Serialize(viewModel)
        };

        _context.DocumentosClinicos.Add(documento);
        await _context.SaveChangesAsync();

        await _auditService.LogCriacaoDocumentoAsync(
            usuarioId,
            documento.Id,
            viewModel.PacienteId,
            documento.ProntuarioId,
            TipoDocumentoClinico.AnamneseAdulto);

        return RedirectToAction("Details", "Paciente", new { id = viewModel.PacienteId });
    }

    [HttpGet]
    public async Task<IActionResult> CreateAnamneseAdolescente(Guid pacienteId)
    {
        var usuarioId = _userManager.GetUserId(User) ?? "";
        if (!await _authorizationService.CanWritePacienteAsync(usuarioId, pacienteId))
        {
            return Forbid();
        }

        var paciente = await _context.Pacientes
            .Include(p => p.Prontuario)
            .FirstOrDefaultAsync(p => p.Id == pacienteId);

        if (paciente == null || !paciente.Ativo)
        {
            return NotFound();
        }

        if (paciente.Prontuario == null)
        {
            TempData["MensagemErro"] = "O paciente ainda não possui prontuário. Abra o prontuário antes de criar uma anamnese.";
            return RedirectToAction("Details", "Paciente", new { id = pacienteId });
        }

        var viewModel = new AnamneseAdolescenteViewModel
        {
            PacienteId = pacienteId,
            PacienteNome = paciente.NomeCompleto,
            ProntuarioId = paciente.Prontuario.Id
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateAnamneseAdolescente(AnamneseAdolescenteViewModel viewModel)
    {
        var usuarioId = _userManager.GetUserId(User) ?? "";
        if (!await _authorizationService.CanWritePacienteAsync(usuarioId, viewModel.PacienteId))
        {
            return Forbid();
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var prontuario = await _context.Prontuarios.FirstOrDefaultAsync(p => p.PacienteId == viewModel.PacienteId);
        if (prontuario == null)
        {
            ModelState.AddModelError(string.Empty, "O paciente não possui um prontuário cadastrado.");
            return View(viewModel);
        }

        var documento = new DocumentoClinico
        {
            PacienteId = viewModel.PacienteId,
            ProntuarioId = prontuario.Id,
            AtendimentoId = viewModel.AtendimentoId,
            TipoDocumento = TipoDocumentoClinico.AnamneseAdolescente,
            StatusDocumento = StatusDocumentoClinico.Rascunho,
            DataDocumento = DateTime.UtcNow,
            CriadoPorUsuarioId = usuarioId,
            Observacoes = System.Text.Json.JsonSerializer.Serialize(viewModel)
        };

        _context.DocumentosClinicos.Add(documento);
        await _context.SaveChangesAsync();

        await _auditService.LogCriacaoDocumentoAsync(
            usuarioId,
            documento.Id,
            viewModel.PacienteId,
            documento.ProntuarioId,
            TipoDocumentoClinico.AnamneseAdolescente);

        return RedirectToAction("Details", "Paciente", new { id = viewModel.PacienteId });
    }
}
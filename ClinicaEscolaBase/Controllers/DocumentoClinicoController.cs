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

    [HttpGet]
    public async Task<IActionResult> CreateAnamneseAdulto(Guid pacienteId)
    {
        var usuarioId = _userManager.GetUserId(User) ?? "";
        if (!await _authorizationService.CanWritePacienteAsync(usuarioId, pacienteId))
        {
            return Forbid();
        }

        var paciente = await _context.Pacientes.FindAsync(pacienteId);
        if (paciente == null || !paciente.Ativo)
        {
            return NotFound();
        }

        var viewModel = new AnamneseAdultoViewModel
        {
            PacienteId = pacienteId,
            PacienteNome = paciente.NomeCompleto
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

        var documento = new DocumentoClinico
        {
            PacienteId = viewModel.PacienteId,
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
            null, // ProntuarioId if applicable
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

        var paciente = await _context.Pacientes.FindAsync(pacienteId);
        if (paciente == null || !paciente.Ativo)
        {
            return NotFound();
        }

        var viewModel = new AnamneseAdolescenteViewModel
        {
            PacienteId = pacienteId,
            PacienteNome = paciente.NomeCompleto
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

        var documento = new DocumentoClinico
        {
            PacienteId = viewModel.PacienteId,
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
            null,
            TipoDocumentoClinico.AnamneseAdolescente);

        return RedirectToAction("Details", "Paciente", new { id = viewModel.PacienteId });
    }
}
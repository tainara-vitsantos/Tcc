using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Services;
using ClinicaEscolaBase.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Controllers;

[Authorize]
public class TermosController(
    ApplicationDbContext context,
    AuthorizationService authorizationService,
    AuditService auditService,
    UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet]
    public async Task<IActionResult> UploadContratoPsicoterapiaIndividual(Guid pacienteId)
    {
        var usuarioId = userManager.GetUserId(User) ?? string.Empty;
        if (!await authorizationService.CanWritePacienteAsync(usuarioId, pacienteId))
            return Forbid();

        var paciente = await context.Pacientes.Include(p => p.Prontuario).FirstOrDefaultAsync(p => p.Id == pacienteId);
        if (paciente == null || paciente.Prontuario == null)
            return NotFound();

        var viewModel = new TermoUploadViewModel
        {
            PacienteId = pacienteId,
            PacienteNome = paciente.NomeCompleto,
            ProntuarioId = paciente.Prontuario.Id
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadContratoPsicoterapiaIndividual(TermoUploadViewModel viewModel)
    {
        var usuarioId = userManager.GetUserId(User) ?? string.Empty;
        if (!await authorizationService.CanWritePacienteAsync(usuarioId, viewModel.PacienteId))
            return Forbid();

        if (viewModel.Arquivo == null)
        {
            ModelState.AddModelError("Arquivo", "O arquivo é obrigatório.");
        }
        else
        {
            var extension = Path.GetExtension(viewModel.Arquivo.FileName).ToLowerInvariant();
            if (extension != ".pdf")
                ModelState.AddModelError("Arquivo", "Apenas arquivos PDF são permitidos.");

            if (viewModel.Arquivo.Length == 0 || viewModel.Arquivo.Length > 10 * 1024 * 1024)
                ModelState.AddModelError("Arquivo", "O arquivo deve ter entre 1 byte e 10MB.");
        }

        if (!ModelState.IsValid)
            return View(viewModel);

        var paciente = await context.Pacientes.Include(p => p.Prontuario).FirstOrDefaultAsync(p => p.Id == viewModel.PacienteId);
        if (paciente == null || paciente.Prontuario == null)
            return NotFound();

        var documento = new DocumentoClinico
        {
            PacienteId = viewModel.PacienteId,
            ProntuarioId = paciente.Prontuario.Id,
            TipoDocumento = TipoDocumentoClinico.TermoPsicoterapiaIndividual,
            StatusDocumento = StatusDocumentoClinico.Rascunho,
            DataDocumento = DateTime.UtcNow,
            CriadoPorUsuarioId = usuarioId,
            Observacoes = "Contrato de Psicoterapia Individual enviado como anexo."
        };

        context.DocumentosClinicos.Add(documento);
        await context.SaveChangesAsync();

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "termos");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"termo-psicoterapia-{Guid.NewGuid()}{Path.GetExtension(viewModel.Arquivo.FileName)}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await viewModel.Arquivo.CopyToAsync(fileStream);
        }

        var anexo = new Anexo
        {
            DocumentoClinicoId = documento.Id,
            NomeOriginal = viewModel.Arquivo.FileName,
            NomeArmazenado = uniqueFileName,
            Extensao = Path.GetExtension(viewModel.Arquivo.FileName),
            MimeType = viewModel.Arquivo.ContentType,
            TamanhoBytes = viewModel.Arquivo.Length,
            CaminhoArquivo = $"/uploads/termos/{uniqueFileName}",
            EnviadoPorUsuarioId = usuarioId,
            DataUpload = DateTime.UtcNow
        };

        context.Anexos.Add(anexo);
        await context.SaveChangesAsync();

        await auditService.LogAsync(usuarioId, TipoAcaoAuditoria.Insercao, nameof(Anexo), anexo.Id.ToString(), viewModel.PacienteId, documento.ProntuarioId, "Contrato de Psicoterapia Individual enviado");
        await auditService.SaveAuditAsync();

        TempData["MensagemSucesso"] = "Contrato de Psicoterapia Individual enviado com sucesso.";
        return RedirectToAction("Details", "Paciente", new { id = viewModel.PacienteId });
    }

    [HttpGet]
    public async Task<IActionResult> UploadAutorizacaoMenores(Guid pacienteId)
    {
        var usuarioId = userManager.GetUserId(User) ?? string.Empty;
        if (!await authorizationService.CanWritePacienteAsync(usuarioId, pacienteId))
            return Forbid();

        var paciente = await context.Pacientes.Include(p => p.Prontuario).FirstOrDefaultAsync(p => p.Id == pacienteId);
        if (paciente == null || paciente.Prontuario == null)
            return NotFound();

        var viewModel = new TermoUploadViewModel
        {
            PacienteId = pacienteId,
            PacienteNome = paciente.NomeCompleto,
            ProntuarioId = paciente.Prontuario.Id
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadAutorizacaoMenores(TermoUploadViewModel viewModel)
    {
        var usuarioId = userManager.GetUserId(User) ?? string.Empty;
        if (!await authorizationService.CanWritePacienteAsync(usuarioId, viewModel.PacienteId))
            return Forbid();

        if (viewModel.Arquivo == null)
        {
            ModelState.AddModelError("Arquivo", "O arquivo é obrigatório.");
        }
        else
        {
            var extension = Path.GetExtension(viewModel.Arquivo.FileName).ToLowerInvariant();
            if (extension != ".pdf")
                ModelState.AddModelError("Arquivo", "Apenas arquivos PDF são permitidos.");

            if (viewModel.Arquivo.Length == 0 || viewModel.Arquivo.Length > 10 * 1024 * 1024)
                ModelState.AddModelError("Arquivo", "O arquivo deve ter entre 1 byte e 10MB.");
        }

        if (!ModelState.IsValid)
            return View(viewModel);

        var paciente = await context.Pacientes.Include(p => p.Prontuario).FirstOrDefaultAsync(p => p.Id == viewModel.PacienteId);
        if (paciente == null || paciente.Prontuario == null)
            return NotFound();

        var documento = new DocumentoClinico
        {
            PacienteId = viewModel.PacienteId,
            ProntuarioId = paciente.Prontuario.Id,
            TipoDocumento = TipoDocumentoClinico.TermoAutorizacaoMenor,
            StatusDocumento = StatusDocumentoClinico.Rascunho,
            DataDocumento = DateTime.UtcNow,
            CriadoPorUsuarioId = usuarioId,
            Observacoes = "Autorização de Menores enviada como anexo."
        };

        context.DocumentosClinicos.Add(documento);
        await context.SaveChangesAsync();

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "termos");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"termo-autorizacao-menor-{Guid.NewGuid()}{Path.GetExtension(viewModel.Arquivo.FileName)}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);
        await using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await viewModel.Arquivo.CopyToAsync(fileStream);
        }

        var anexo = new Anexo
        {
            DocumentoClinicoId = documento.Id,
            NomeOriginal = viewModel.Arquivo.FileName,
            NomeArmazenado = uniqueFileName,
            Extensao = Path.GetExtension(viewModel.Arquivo.FileName),
            MimeType = viewModel.Arquivo.ContentType,
            TamanhoBytes = viewModel.Arquivo.Length,
            CaminhoArquivo = $"/uploads/termos/{uniqueFileName}",
            EnviadoPorUsuarioId = usuarioId,
            DataUpload = DateTime.UtcNow
        };

        context.Anexos.Add(anexo);
        await context.SaveChangesAsync();

        await auditService.LogAsync(usuarioId, TipoAcaoAuditoria.Insercao, nameof(Anexo), anexo.Id.ToString(), viewModel.PacienteId, documento.ProntuarioId, "Autorização de Menores enviada");
        await auditService.SaveAuditAsync();

        TempData["MensagemSucesso"] = "Autorização de Menores enviada com sucesso.";
        return RedirectToAction("Details", "Paciente", new { id = viewModel.PacienteId });
    }
}

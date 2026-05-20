using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Services;
using ClinicaEscolaBase.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Controllers;

[Authorize]
public class AnexoController(
    ApplicationDbContext context,
    AuthorizationService authorizationService,
    AuditService auditService,
    UserManager<ApplicationUser> userManager) : Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(int documentoId, IFormFile arquivo)
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        if (arquivo == null || arquivo.Length == 0)
        {
            return BadRequest("Arquivo vazio ou não fornecido.");
        }

        // Validate file type (only PDF for now)
        var allowedExtensions = new[] { ".pdf" };
        var extension = Path.GetExtension(arquivo.FileName).ToLower();
        if (!allowedExtensions.Contains(extension))
        {
            return BadRequest("Apenas arquivos PDF são permitidos.");
        }

        // Validate file size (max 10MB)
        if (arquivo.Length > 10 * 1024 * 1024)
        {
            return BadRequest("Arquivo muito grande. Máximo 10MB.");
        }

        // Check if documento exists and user has access
        var documento = await context.DocumentosClinicos
            .Include(d => d.Paciente)
            .FirstOrDefaultAsync(d => d.Id == documentoId && d.Ativo);

        if (documento == null)
        {
            return NotFound("Documento não encontrado.");
        }

        if (!await authorizationService.CanWritePacienteAsync(usuarioId, documento.PacienteId))
        {
            return Forbid();
        }

        // Create uploads directory if not exists
        var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(uploadsDir))
        {
            Directory.CreateDirectory(uploadsDir);
        }

        // Generate unique filename
        var uniqueFileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(uploadsDir, uniqueFileName);

        // Save file
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        // Create Anexo record
        var anexo = new Anexo
        {
            DocumentoClinicoId = documentoId,
            NomeOriginal = arquivo.FileName,
            NomeArmazenado = uniqueFileName,
            CaminhoArquivo = $"/uploads/{uniqueFileName}",
            TamanhoBytes = arquivo.Length,
            MimeType = arquivo.ContentType,
            EnviadoPorUsuarioId = usuarioId,
            DataUpload = DateTime.UtcNow
        };

        context.Anexos.Add(anexo);
        await context.SaveChangesAsync();

        // Audit
        await auditService.LogAsync(
            usuarioId,
            TipoAcaoAuditoria.Insercao,
            "Anexo",
            anexo.Id.ToString(),
            documento.PacienteId,
            documento.ProntuarioId,
            $"Anexo '{arquivo.FileName}' enviado para documento {documentoId}");
        await auditService.SaveAuditAsync();

        return Json(new
        {
            success = true,
            message = "Anexo enviado com sucesso.",
            anexoId = anexo.Id,
            nomeArquivo = anexo.NomeOriginal
        });
    }

    [HttpGet]
    public async Task<IActionResult> Download(int id)
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        var anexo = await context.Anexos
            .Include(a => a.DocumentoClinico)
            .FirstOrDefaultAsync(a => a.Id == id && a.Ativo);

        if (anexo == null)
        {
            return NotFound();
        }

        if (!await authorizationService.CanReadPacienteAsync(usuarioId, anexo.DocumentoClinico.PacienteId))
        {
            return Forbid();
        }

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", anexo.CaminhoArquivo.TrimStart('/'));
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("Arquivo não encontrado no servidor.");
        }

        // Audit download
        await auditService.LogAsync(
            usuarioId,
            TipoAcaoAuditoria.Visualizacao,
            "Anexo",
            anexo.Id.ToString(),
            anexo.DocumentoClinico.PacienteId,
            anexo.DocumentoClinico.ProntuarioId,
            $"Download do anexo '{anexo.NomeOriginal}'" );
        await auditService.SaveAuditAsync();

        var memory = new MemoryStream();
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        return File(memory, anexo.MimeType ?? "application/octet-stream", anexo.NomeOriginal);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        var anexo = await context.Anexos
            .Include(a => a.DocumentoClinico)
            .FirstOrDefaultAsync(a => a.Id == id && a.Ativo);

        if (anexo == null)
        {
            return NotFound();
        }

        if (!await authorizationService.CanWritePacienteAsync(usuarioId, anexo.DocumentoClinico.PacienteId))
        {
            return Forbid();
        }

        // Soft delete
        anexo.Ativo = false;
        anexo.DataAtualizacao = DateTime.UtcNow;

        await context.SaveChangesAsync();

        // Delete physical file
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", anexo.CaminhoArquivo.TrimStart('/'));
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }

        // Audit
        await auditService.LogAsync(
            usuarioId,
            TipoAcaoAuditoria.ExclusaoLogica,
            "Anexo",
            anexo.Id.ToString(),
            anexo.DocumentoClinico.PacienteId,
            anexo.DocumentoClinico.ProntuarioId,
            $"Anexo '{anexo.NomeOriginal}' removido");
        await auditService.SaveAuditAsync();

        return Json(new { success = true, message = "Anexo removido com sucesso." });
    }
}
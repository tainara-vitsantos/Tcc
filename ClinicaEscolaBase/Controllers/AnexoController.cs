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
public class AnexoController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly AuthorizationService _authorizationService;
    private readonly AuditService _auditService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AnexoController(
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(int documentoId, IFormFile arquivo)
    {
        var usuarioId = _userManager.GetUserId(User);
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
        var documento = await _context.DocumentosClinicos
            .Include(d => d.Paciente)
            .FirstOrDefaultAsync(d => d.Id == documentoId && d.Ativo);

        if (documento == null)
        {
            return NotFound("Documento não encontrado.");
        }

        if (!await _authorizationService.CanWritePacienteAsync(usuarioId, documento.PacienteId))
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

        _context.Anexos.Add(anexo);
        await _context.SaveChangesAsync();

        // Audit
        await _auditService.LogAsync(
            usuarioId,
            TipoAcaoAuditoria.Insercao,
            "Anexo",
            anexo.Id.ToString(),
            documento.PacienteId,
            documento.ProntuarioId,
            $"Anexo '{arquivo.FileName}' enviado para documento {documentoId}");
        await _auditService.SaveAuditAsync();

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
        var usuarioId = _userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        var anexo = await _context.Anexos
            .Include(a => a.DocumentoClinico)
            .FirstOrDefaultAsync(a => a.Id == id && a.Ativo);

        if (anexo == null)
        {
            return NotFound();
        }

        if (!await _authorizationService.CanReadPacienteAsync(usuarioId, anexo.DocumentoClinico.PacienteId))
        {
            return Forbid();
        }

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", anexo.CaminhoArquivo.TrimStart('/'));
        if (!System.IO.File.Exists(filePath))
        {
            return NotFound("Arquivo não encontrado no servidor.");
        }

        // Audit download
        await _auditService.LogAsync(
            usuarioId,
            TipoAcaoAuditoria.Visualizacao,
            "Anexo",
            anexo.Id.ToString(),
            anexo.DocumentoClinico.PacienteId,
            anexo.DocumentoClinico.ProntuarioId,
            $"Download do anexo '{anexo.NomeOriginal}'" );
        await _auditService.SaveAuditAsync();

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
        var usuarioId = _userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        var anexo = await _context.Anexos
            .Include(a => a.DocumentoClinico)
            .FirstOrDefaultAsync(a => a.Id == id && a.Ativo);

        if (anexo == null)
        {
            return NotFound();
        }

        if (!await _authorizationService.CanWritePacienteAsync(usuarioId, anexo.DocumentoClinico.PacienteId))
        {
            return Forbid();
        }

        // Soft delete
        anexo.Ativo = false;
        anexo.DataAtualizacao = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        // Delete physical file
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", anexo.CaminhoArquivo.TrimStart('/'));
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }

        // Audit
        await _auditService.LogAsync(
            usuarioId,
            TipoAcaoAuditoria.ExclusaoLogica,
            "Anexo",
            anexo.Id.ToString(),
            anexo.DocumentoClinico.PacienteId,
            anexo.DocumentoClinico.ProntuarioId,
            $"Anexo '{anexo.NomeOriginal}' removido");
        await _auditService.SaveAuditAsync();

        return Json(new { success = true, message = "Anexo removido com sucesso." });
    }
}
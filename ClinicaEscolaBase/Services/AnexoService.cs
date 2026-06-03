using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Dtos;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ClinicaEscolaBase.Services;

public class AnexoService(
    ApplicationDbContext context,
    IAuthService authorizationService,
    IAuditService auditService,
    IWebHostEnvironment environment,
    ILogger<AnexoService> logger) : IAnexoService
{
    private const long MaxFileSizeBytes = 10 * 1024 * 1024;
    private static readonly string[] AllowedExtensions = [".pdf"];
    private const string UploadsFolderName = "uploads";

    public async Task<AnexoOperationResultDto<AnexoResponseDto>> UploadAsync(
        AnexoUploadDto request,
        CancellationToken cancellationToken = default)
    {
        if (request.Arquivo == null || request.Arquivo.Length == 0)
        {
            return BadRequest<AnexoResponseDto>("Arquivo vazio ou não fornecido.");
        }

        var extension = Path.GetExtension(request.Arquivo.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
        {
            return BadRequest<AnexoResponseDto>("Apenas arquivos PDF são permitidos.");
        }

        if (request.Arquivo.Length > MaxFileSizeBytes)
        {
            return BadRequest<AnexoResponseDto>("Arquivo muito grande. Máximo 10MB.");
        }

        var documento = await context.DocumentosClinicos
            .FirstOrDefaultAsync(d => d.Id == request.DocumentoId && d.Ativo, cancellationToken);

        if (documento == null)
        {
            return NotFound<AnexoResponseDto>("Documento não encontrado.");
        }

        if (!await authorizationService.CanWritePacienteAsync(request.UsuarioId, documento.PacienteId))
        {
            return Forbid<AnexoResponseDto>("Você não tem permissão para anexar arquivo a este documento.");
        }

        var uploadsDirectory = GetUploadsDirectory();
        Directory.CreateDirectory(uploadsDirectory);

        var originalFileName = Path.GetFileName(request.Arquivo.FileName);
        var storageFileName = $"{Guid.NewGuid():N}{extension}";
        var filePath = GetSafeUploadFilePath(uploadsDirectory, storageFileName);

        try
        {
            await using (var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                await request.Arquivo.CopyToAsync(stream, cancellationToken);
            }

            var anexo = new Anexo
            {
                DocumentoClinicoId = request.DocumentoId,
                NomeOriginal = originalFileName,
                NomeArmazenado = storageFileName,
                Extensao = extension,
                CaminhoArquivo = $"/{UploadsFolderName}/{storageFileName}",
                TamanhoBytes = request.Arquivo.Length,
                MimeType = string.IsNullOrWhiteSpace(request.Arquivo.ContentType)
                    ? "application/octet-stream"
                    : request.Arquivo.ContentType,
                EnviadoPorUsuarioId = request.UsuarioId,
                DataUpload = DateTime.UtcNow
            };

            context.Anexos.Add(anexo);
            await context.SaveChangesAsync(cancellationToken);

            await auditService.LogAsync(
                request.UsuarioId,
                TipoAcaoAuditoria.Insercao,
                nameof(Anexo),
                anexo.Id.ToString(),
                documento.PacienteId,
                documento.ProntuarioId,
                $"Anexo '{originalFileName}' enviado para documento {request.DocumentoId}");
            await auditService.SaveAuditAsync();

            var response = new AnexoResponseDto(
                anexo.Id,
                anexo.DocumentoClinicoId,
                anexo.NomeOriginal,
                anexo.NomeArmazenado,
                anexo.Extensao,
                anexo.MimeType,
                anexo.TamanhoBytes,
                anexo.CaminhoArquivo,
                anexo.EnviadoPorUsuarioId,
                anexo.DataUpload);

            return Ok("Anexo enviado com sucesso.", response);
        }
        catch
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            logger.LogError("Falha ao processar upload do anexo para o documento {DocumentoId}.", request.DocumentoId);
            throw;
        }
    }

    public async Task<AnexoOperationResultDto<AnexoDownloadResultDto>> DownloadAsync(
        AnexoDownloadDto request,
        CancellationToken cancellationToken = default)
    {
        var anexo = await context.Anexos
            .Include(a => a.DocumentoClinico)
            .FirstOrDefaultAsync(a => a.Id == request.AnexoId && a.Ativo, cancellationToken);

        if (anexo == null)
        {
            return NotFound<AnexoDownloadResultDto>("Anexo não encontrado.");
        }

        if (!await authorizationService.CanReadPacienteAsync(request.UsuarioId, anexo.DocumentoClinico.PacienteId))
        {
            return Forbid<AnexoDownloadResultDto>("Você não tem permissão para acessar este anexo.");
        }

        var filePath = GetSafeExistingUploadFilePath(anexo.CaminhoArquivo);
        if (filePath == null || !File.Exists(filePath))
        {
            return NotFound<AnexoDownloadResultDto>("Arquivo não encontrado no servidor.");
        }

        await auditService.LogAsync(
            request.UsuarioId,
            TipoAcaoAuditoria.Visualizacao,
            nameof(Anexo),
            anexo.Id.ToString(),
            anexo.DocumentoClinico.PacienteId,
            anexo.DocumentoClinico.ProntuarioId,
            $"Download do anexo '{anexo.NomeOriginal}'");
        await auditService.SaveAuditAsync();

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var download = new AnexoDownloadResultDto(
            stream,
            anexo.MimeType ?? "application/octet-stream",
            anexo.NomeOriginal);

        return Ok("Download liberado.", download);
    }

    public async Task<AnexoOperationResultDto> DeleteAsync(
        AnexoDeleteDto request,
        CancellationToken cancellationToken = default)
    {
        var anexo = await context.Anexos
            .Include(a => a.DocumentoClinico)
            .FirstOrDefaultAsync(a => a.Id == request.AnexoId && a.Ativo, cancellationToken);

        if (anexo == null)
        {
            return NotFound("Anexo não encontrado.");
        }

        if (!await authorizationService.CanWritePacienteAsync(request.UsuarioId, anexo.DocumentoClinico.PacienteId))
        {
            return Forbid("Você não tem permissão para remover este anexo.");
        }

        anexo.Ativo = false;
        anexo.DataAtualizacao = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        var filePath = GetSafeExistingUploadFilePath(anexo.CaminhoArquivo);
        if (filePath != null && File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        await auditService.LogAsync(
            request.UsuarioId,
            TipoAcaoAuditoria.ExclusaoLogica,
            nameof(Anexo),
            anexo.Id.ToString(),
            anexo.DocumentoClinico.PacienteId,
            anexo.DocumentoClinico.ProntuarioId,
            $"Anexo '{anexo.NomeOriginal}' removido");
        await auditService.SaveAuditAsync();

        return Ok("Anexo removido com sucesso.");
    }

    private string GetUploadsDirectory()
    {
        var webRootPath = environment.WebRootPath ?? Path.Combine(environment.ContentRootPath, "wwwroot");
        return Path.Combine(webRootPath, UploadsFolderName);
    }

    private string GetSafeUploadFilePath(string uploadsDirectory, string fileName)
    {
        var safeFileName = Path.GetFileName(fileName);
        var fullUploadsDirectory = Path.GetFullPath(uploadsDirectory);
        var fullFilePath = Path.GetFullPath(Path.Combine(fullUploadsDirectory, safeFileName));

        if (!fullFilePath.StartsWith(fullUploadsDirectory, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Caminho de arquivo inválido.");
        }

        return fullFilePath;
    }

    private string? GetSafeExistingUploadFilePath(string? caminhoArquivo)
    {
        if (string.IsNullOrWhiteSpace(caminhoArquivo))
        {
            return null;
        }

        var uploadsDirectory = GetUploadsDirectory();
        var safeFileName = Path.GetFileName(caminhoArquivo);
        if (string.IsNullOrWhiteSpace(safeFileName))
        {
            return null;
        }

        return GetSafeUploadFilePath(uploadsDirectory, safeFileName);
    }

    private static AnexoOperationResultDto Ok(string message)
        => new(true, HttpStatusCode.OK, message);

    private static AnexoOperationResultDto<T> Ok<T>(string message, T data)
        => new(true, HttpStatusCode.OK, message, data);

    private static AnexoOperationResultDto<T> BadRequest<T>(string message)
        => new(false, HttpStatusCode.BadRequest, message, default);

    private static AnexoOperationResultDto<T> NotFound<T>(string message)
        => new(false, HttpStatusCode.NotFound, message, default);

    private static AnexoOperationResultDto<T> Forbid<T>(string message)
        => new(false, HttpStatusCode.Forbidden, message, default);

    private static AnexoOperationResultDto BadRequest(string message)
        => new(false, HttpStatusCode.BadRequest, message);

    private static AnexoOperationResultDto NotFound(string message)
        => new(false, HttpStatusCode.NotFound, message);

    private static AnexoOperationResultDto Forbid(string message)
        => new(false, HttpStatusCode.Forbidden, message);
}
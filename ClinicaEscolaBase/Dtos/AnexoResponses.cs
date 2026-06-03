using System.Net;

namespace ClinicaEscolaBase.Dtos;

public record AnexoResponseDto(
    int Id,
    int DocumentoClinicoId,
    string NomeOriginal,
    string NomeArmazenado,
    string? Extensao,
    string? MimeType,
    long TamanhoBytes,
    string CaminhoArquivo,
    string EnviadoPorUsuarioId,
    DateTime DataUpload);

public record AnexoDownloadResultDto(
    Stream Content,
    string ContentType,
    string FileName);

public record AnexoOperationResultDto(
    bool Success,
    HttpStatusCode StatusCode,
    string Message);

public record AnexoOperationResultDto<T>(
    bool Success,
    HttpStatusCode StatusCode,
    string Message,
    T? Data);
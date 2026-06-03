using Microsoft.AspNetCore.Http;

namespace ClinicaEscolaBase.Dtos;

public record AnexoUploadDto(int DocumentoId, IFormFile Arquivo, string UsuarioId);

public record AnexoDownloadDto(int AnexoId, string UsuarioId);

public record AnexoDeleteDto(int AnexoId, string UsuarioId);
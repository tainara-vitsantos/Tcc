using ClinicaEscolaBase.Dtos;

namespace ClinicaEscolaBase.Services.Interfaces;

public interface IAnexoService
{
    Task<AnexoOperationResultDto<AnexoResponseDto>> UploadAsync(
        AnexoUploadDto request,
        CancellationToken cancellationToken = default);

    Task<AnexoOperationResultDto<AnexoDownloadResultDto>> DownloadAsync(
        AnexoDownloadDto request,
        CancellationToken cancellationToken = default);

    Task<AnexoOperationResultDto> DeleteAsync(
        AnexoDeleteDto request,
        CancellationToken cancellationToken = default);
}
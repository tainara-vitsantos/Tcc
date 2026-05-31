
namespace ClinicaEscolaBase.Models;

public class AnexoModel : EntityBase
{
    public string NomeOriginal { get; set; } = string.Empty;
    public string NomeArmazenado { get; set; } = string.Empty;
    public string? Extensao { get; set; }
    public string? MimeType { get; set; }
    public long TamanhoBytes { get; set; }
    public string CaminhoArquivo { get; set; } = string.Empty;
    public string? HashArquivo { get; set; }
    public string EnviadoPorUsuarioId { get; set; } = string.Empty;
    public DateTime DataUpload { get; set; } = DateTime.UtcNow;
    public int IdDocumentoClinico { get; set; }
    public DocumentoClinicoModel DocumentoClinico { get; set; } = null!;
    public ApplicationUserModel EnviadoPorUsuario { get; set; } = null!;
}

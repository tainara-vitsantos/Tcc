
namespace ClinicaEscolaBase.Models;

public class EvolucaoAtendimentoModel : EntityBase
{
    public int DocumentoClinicoId { get; set; }
    public int? AtendimentoId { get; set; }
    public string CriadoPorUsuarioId { get; set; } = string.Empty;
    public DateTime DataEvolucao { get; set; } = DateTime.UtcNow;
    public string TextoEvolucao { get; set; } = string.Empty;
    public DocumentoClinicoModel DocumentoClinico { get; set; } = null!;
    public AtendimentoModel? Atendimento { get; set; }   
    public ApplicationUserModel CriadoPorUsuario { get; set; } = null!;
}

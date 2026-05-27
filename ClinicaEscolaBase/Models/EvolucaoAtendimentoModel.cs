
namespace ClinicaEscolaBase.Models;

public class EvolucaoAtendimentoModel : EntityBase
{
    public int DocumentoClinicoId { get; set; }
    public int? AtendimentoId { get; set; }
    public DateTime DataEvolucao { get; set; } = DateTime.UtcNow;
    public string TextoEvolucao { get; set; } = string.Empty;
    public string CriadoPorUsuarioId { get; set; } = string.Empty;
    public string? SupervisorId { get; set; }
    public string? AssinaturaAluno { get; set; }
    public string? AssinaturaSupervisor { get; set; }

    public DocumentoClinicoModel DocumentoClinico { get; set; } = null!;
    public AtendimentoModel? Atendimento { get; set; }
    public ApplicationUserModel CriadoPorUsuario { get; set; } = null!;
    public ApplicationUserModel? Supervisor { get; set; }
}

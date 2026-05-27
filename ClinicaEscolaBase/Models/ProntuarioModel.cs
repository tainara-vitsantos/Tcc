using ClinicaEscolaBase.Enums;

namespace ClinicaEscolaBase.Models;

public class ProntuarioModel : EntityBase
{
    public Guid PacienteId { get; set; }
    public string NumeroProntuario { get; set; } = string.Empty;
    public DateTime? DataPrimeiraConsulta { get; set; }
    public SituacaoProntuarioEnum SituacaoProntuario { get; set; } = SituacaoProntuarioEnum.Ativo;
    public string? ObservacoesGerais { get; set; }

    public PacienteModel Paciente { get; set; } = null!;
    public ICollection<AtendimentoModel> Atendimentos { get; set; } = new List<AtendimentoModel>();
    public ICollection<DocumentoClinicoModel> DocumentosClinicos { get; set; } = new List<DocumentoClinicoModel>();
    public ICollection<AuditoriaModel> Auditorias { get; set; } = new List<AuditoriaModel>();
}


using ClinicaEscolaBase.Enums;

namespace ClinicaEscolaBase.Models;

public class AtendimentoModel : EntityBase
{
    public int ProntuarioId { get; set; }
    public Guid PacienteId { get; set; }
    public string AlunoId { get; set; } = string.Empty;
    public string? SupervisorId { get; set; }
    public TipoAtendimentoEnum TipoAtendimento { get; set; }
    public DateTime DataHoraInicio { get; set; }
    public DateTime? DataHoraFim { get; set; }
    public StatusAtendimentoEnum StatusAtendimento { get; set; } = StatusAtendimentoEnum.Agendado;
    public bool FaltaJustificada { get; set; }
    public string? Observacoes { get; set; }

    public ProntuarioModel Prontuario { get; set; } = null!;
    public PacienteModel Paciente { get; set; } = null!;
    public ApplicationUserModel Aluno { get; set; } = null!;
    public ApplicationUserModel? Supervisor { get; set; }
    public ICollection<DocumentoClinicoModel> DocumentosClinicos { get; set; } = new List<DocumentoClinicoModel>();
    public ICollection<EvolucaoAtendimentoModel> Evolucoes { get; set; } = new List<EvolucaoAtendimentoModel>();
}

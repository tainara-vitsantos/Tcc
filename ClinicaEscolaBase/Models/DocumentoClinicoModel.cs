using ClinicaEscolaBase.Enums;

namespace ClinicaEscolaBase.Models;

public class DocumentoClinicoModel : EntityBase
{
    public int ProntuarioId { get; set; }
    public Guid PacienteId { get; set; }
    public int? AtendimentoId { get; set; }
    public TipoDocumentoClinicoEnum TipoDocumento { get; set; }
    public string CriadoPorUsuarioId { get; set; } = string.Empty;
    public string? SupervisorId { get; set; }
    public StatusDocumentoClinicoEnum StatusDocumento { get; set; } = StatusDocumentoClinicoEnum.Rascunho;
    public int Versao { get; set; } = 1;
    public DateTime DataDocumento { get; set; } = DateTime.UtcNow;
    public DateTime? FinalizadoEm { get; set; }
    public string? Observacoes { get; set; }
    public bool ExcluidoLogicamente { get; set; }

    public ProntuarioModel Prontuario { get; set; } = null!;
    public PacienteModel Paciente { get; set; } = null!;
    public AtendimentoModel? Atendimento { get; set; }
    public ApplicationUserModel CriadoPorUsuario { get; set; } = null!;
    public ApplicationUserModel? Supervisor { get; set; }
    public DocumentoIdentificacaoPaciente? DocumentoIdentificacaoPaciente { get; set; }
    public AnamneseAdultoModel? AnamneseAdulto { get; set; }
    public AnamneseAdolescenteModel? AnamneseAdolescente { get; set; }
    public PlantaoPsicologicoModel? PlantaoPsicologico { get; set; }
    public TermoPsicoterapiaIndividualModel? TermoPsicoterapiaIndividual { get; set; }
    public TermoAutorizacaoMenorModel? TermoAutorizacaoMenor { get; set; }
    public TermoCompromissoInformatizacaoModel? TermoCompromissoInformatizacao { get; set; }
    public ICollection<EvolucaoAtendimentoModel> Evolucoes { get; set; } = new List<EvolucaoAtendimentoModel>();
    public ICollection<AnexoModel> Anexos { get; set; } = new List<AnexoModel>();
}

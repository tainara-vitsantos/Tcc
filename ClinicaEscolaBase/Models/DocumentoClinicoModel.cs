using ClinicaEscolaBase.Enums;

namespace ClinicaEscolaBase.Models;

public class DocumentoClinicoModel : EntityBase
{
    public int ProntuarioId { get; set; }
    public int PacienteId { get; set; }
    public int? AtendimentoId { get; set; }    
    public string CriadoPorUsuarioId { get; set; } = string.Empty;   
    public TipoDocumentoClinicoEnum TipoDocumentoClinico { get; set; }
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
}

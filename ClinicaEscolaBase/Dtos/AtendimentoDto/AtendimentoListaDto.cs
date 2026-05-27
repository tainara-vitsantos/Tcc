namespace ClinicaEscolaBase.Dtos.AtendimentoDto;

public record AtendimentoListaDto{
      public int ProntuarioId { get; set; }
    public Guid PacienteId { get; set; }
    public string AlunoId { get; set; } = string.Empty;
    public string? SupervisorId { get; set; }
    public TipoAtendimentoEnum TipoAtendimento { get; set; }
}
public record AtendimentoDto{
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
}
public record EvolucaoAtendimentoDto{
   public int DocumentoClinicoId { get; set; }
    public int? AtendimentoId { get; set; }
    public DateTime DataEvolucao { get; set; } = DateTime.UtcNow;
    public string TextoEvolucao { get; set; } = string.Empty;
    public string CriadoPorUsuarioId { get; set; } = string.Empty;
    public string? SupervisorId { get; set; }
    public string? AssinaturaAluno { get; set; }
    public string? AssinaturaSupervisor { get; set; }
}

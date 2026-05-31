using ClinicaEscolaBase.Enums;

namespace ClinicaEscolaBase.Models;

public class ProntuarioModel : EntityBase
{
   public int PacienteId { get; set; }
    public PacienteModel Paciente { get; set; } = null!;
    public string NumeroProntuario { get; set; } = string.Empty;
    public DateTime? DataPrimeiraConsulta { get; set; }
    public SituacaoProntuarioEnum SituacaoProntuario { get; set; } = SituacaoProntuarioEnum.Ativo;
    public string? ObservacoesGerais { get; set; }

    // Coleções
    public ICollection<TratamentoAnteriorModel> TratamentosAnteriores { get; set; } = [];
       public ICollection<AtendimentoModel> Atendimentos { get; set; } = [];
    public ICollection<DocumentoClinicoModel> DocumentosClinicos { get; set; } = [];
    public ICollection<AuditoriaModel> Auditorias { get; set; } = [];
}

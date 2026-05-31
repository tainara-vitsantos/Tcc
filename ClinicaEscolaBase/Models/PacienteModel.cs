namespace ClinicaEscolaBase.Models;

public class PacienteModel : EntityBase
{
  public string NomeCompleto { get; set; } = string.Empty;  
    public string? Telefone { get; set; }
    public string? TelefoneRecado { get; set; }
    public string? Sexo { get; set; }
    public string? Naturalidade { get; set; }
    public string? EstadoNascimento { get; set; }
    public string? Escolaridade { get; set; }
    public string? Profissao { get; set; }    
    public string? RG { get; set; }
    public string? CPF { get; set; }
    public string? EstadoCivil { get; set; }
    public string? Religiao { get; set; }
    public Endereco? Endereco { get; set; } 
    public DateTime? DataNascimento { get; set; }
    public int? Idade => DataNascimento.HasValue 
        ? DateTime.Today.Year - DataNascimento.Value.Year - (DateTime.Today < DataNascimento.Value.AddYears(DateTime.Today.Year - DataNascimento.Value.Year) ? 1 : 0) 
        : null;
    public int FamiliarResponsavelId { get; set; } 
    public InfoFamiliarModel? FamiliarResponsavel { get; set; }
    public ProntuarioModel Prontuario { get; set; } = null!;
    public ICollection<InfoFamiliarModel> Familiares { get; set; } = [];
    public ICollection<AtendimentoModel> Atendimentos { get; set; } = [];
    public ICollection<DocumentoClinicoModel> DocumentosClinicos { get; set; } = [];
    public ICollection<TratamentoAnteriorModel> TratamentosAnteriores { get; set; } = [];
    public ICollection<AuditoriaModel> Auditorias { get; set; } = [];
}

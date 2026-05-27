

namespace ClinicaEscolaBase.Models;

public class PacienteModel
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataAtualizacao { get; set; }
    public bool Ativo { get; set; } = true;
    public string NomeCompleto { get; set; } = string.Empty;
    public DateTime? DataNascimento { get; set; }
    public int? Idade { get; set; }
    public string? Sexo { get; set; }
    public string? Naturalidade { get; set; }
    public string? EstadoNascimento { get; set; }
    public string? Escolaridade { get; set; }
    public string? Profissao { get; set; }
    public string? RG { get; set; }
    public string? CPF { get; set; }
    public string? EstadoCivil { get; set; }
    public string? Religiao { get; set; }
    public string? EnderecoLogradouro { get; set; }
    public string? EnderecoNumero { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? CEP { get; set; }
    public string? Telefone { get; set; }
    public string? TelefoneRecado { get; set; }
    public string? NomePai { get; set; }
    public string? NomeMae { get; set; }
    public string? NomeResponsavel { get; set; }
    public string? GrauParentescoResponsavel { get; set; }
    public bool TratamentoPsicologico { get; set; }
    public bool TratamentoNeurologico { get; set; }
    public bool TratamentoPsiquiatrico { get; set; }
    public bool TratamentoCardiologico { get; set; }
    public bool Internacao { get; set; }
    public string? MotivoInternacao { get; set; }
    public string? Observacoes { get; set; }

    public ProntuarioModel? Prontuario { get; set; }
    public ICollection<TratamentoAnteriorPacienteModel> TratamentosAnteriores { get; set; } = new List<TratamentoAnteriorPacienteModel>();
    public ICollection<ResponsavelLegalModel> ResponsaveisLegais { get; set; } = new List<ResponsavelLegalModel>();
    public ICollection<VinculoAlunoPacienteModel> VinculosAluno { get; set; } = new List<VinculoAlunoPacienteModel>();
    public ICollection<AtendimentoModel> Atendimentos { get; set; } = new List<AtendimentoModel>();
    public ICollection<DocumentoClinicoModel> DocumentosClinicos { get; set; } = new List<DocumentoClinicoModel>();
    public ICollection<AuditoriaModel> Auditorias { get; set; } = new List<AuditoriaModel>();
}

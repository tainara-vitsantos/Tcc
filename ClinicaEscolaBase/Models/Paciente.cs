using System;
using System.Collections.Generic;

using System;
using System.Collections.Generic;

namespace ClinicaEscolaBase.Models;

public class Paciente
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
    public bool TratamentoPsicologico { get; set; }
    public bool TratamentoNeurologico { get; set; }
    public bool TratamentoPsiquiatrico { get; set; }
    public bool TratamentoCardiologico { get; set; }
    public bool Internacao { get; set; }
    public string? MotivoInternacao { get; set; }
    public string? Observacoes { get; set; }

    public Prontuario? Prontuario { get; set; }
    public ICollection<TratamentoAnteriorPaciente> TratamentosAnteriores { get; set; } = new List<TratamentoAnteriorPaciente>();
    public ICollection<ResponsavelLegal> ResponsaveisLegais { get; set; } = new List<ResponsavelLegal>();
    public ICollection<VinculoAlunoPaciente> VinculosAluno { get; set; } = new List<VinculoAlunoPaciente>();
    public ICollection<Atendimento> Atendimentos { get; set; } = new List<Atendimento>();
    public ICollection<DocumentoClinico> DocumentosClinicos { get; set; } = new List<DocumentoClinico>();
    public ICollection<Auditoria> Auditorias { get; set; } = new List<Auditoria>();
}

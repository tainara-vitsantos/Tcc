using ClinicaEscolaBase.Enums;

namespace ClinicaEscolaBase.Dtos.PacienteDto;

public record UsuarioListaDto(
    Guid Id, 
    string NomeCompleto, 
    string? TelefoneRecado, 
    string? NomeResponsavel
);

public record PacienteDetalhesDto(
    Guid Id,
    string NomeCompleto,
    string ProntuarioId,
    string? TelefoneRecado,
    DateTime DataCriacao, // Pode receber um valor padrão no construtor se necessário
    DateTime? DataAtualizacao,
    bool Ativo,
    DateTime? DataNascimento,
    int? Idade,
    string? Sexo,
    string? Naturalidade,
    string? EstadoNascimento,
    string? Escolaridade,
    string? Profissao,
    string? RG,
    string? CPF,
    string? EstadoCivil,
    string? Religiao,
    string? EnderecoLogradouro,
    string? EnderecoNumero,
    string? Bairro,
    string? Cidade,
    string? Estado,
    string? CEP,
    string? Telefone,
    string? NomePai,
    string? NomeMae,
    string? NomeResponsavel,
    string? GrauParentescoResponsavel,
    bool TratamentoPsicologico,
    bool TratamentoNeurologico,
    bool TratamentoPsiquiatrico,
    bool TratamentoCardiologico,
    bool Internacao,
    string? MotivoInternacao,
    string? Observacoes
);

public record TratamentoAnteriorDto(
    Guid PacienteId,
    TipoTratamentoAnteriorEnum TipoTratamento,
    bool PossuiHistorico,
    string? MotivoInternacao,
    string? Observacoes
);

public record ResponsavelLegalDto(
    string NomeCompleto,
    string? RG,
    string? CPF,
    string? GrauParentesco,
    string? Telefone,
    string? Email,
    string? Endereco,
    bool ResponsavelPrincipal
);
public record PacienteCadastroDto{}
public record PacienteAtualizacaoDto{}


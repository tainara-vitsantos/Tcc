namespace ClinicaEscolaBase.Services.Interfaces;

/// <summary>
/// Contrato para o serviço de validação de autorização de acesso aos pacientes.
/// Garante a segurança acadêmica do sistema.
/// </summary>
public interface IAuthorizationService
{
    /// <summary>
    /// Verifica se um usuário (Aluno) tem permissão de leitura para um paciente.
    /// </summary>
    Task<bool> CanReadPacienteAsync(string usuarioId, Guid pacienteId);

    /// <summary>
    /// Verifica se um usuário (Aluno) tem permissão de escrita para um paciente.
    /// </summary>
    Task<bool> CanWritePacienteAsync(string usuarioId, Guid pacienteId);

    /// <summary>
    /// Retorna todos os IDs dos pacientes aos quais o usuário possui acesso.
    /// </summary>
    Task<List<Guid>> GetAcessiblePacienteIdsAsync(string usuarioId);
}
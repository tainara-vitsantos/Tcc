
using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IProntuarioRepository
{
	/// <summary>
	/// Obtém a listagem de prontuários ativos para leitura, sem rastreamento de mudanças pelo Entity Framework.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que representa a operação de consulta e retorna uma sequência de prontuários ativos.
	/// </returns>
	Task<IEnumerable<ProntuarioModel>> GetAllAsync();

	/// <summary>
	/// Busca um prontuário pelo identificador primário para leitura, sem rastreamento de mudanças pelo Entity Framework.
	/// </summary>
	/// <param name="id">Identificador do prontuário herdado de <see cref="EntityBase"/>.</param>
	/// <returns>
	/// Uma tarefa assíncrona que representa a operação de busca e retorna o prontuário encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<ProntuarioModel?> GetByIdAsync(int id);

	/// <summary>
	/// Busca o prontuário vinculado a um paciente específico para leitura, sem rastreamento de mudanças pelo Entity Framework.
	/// </summary>
	/// <param name="pacienteId">Identificador do paciente ao qual o prontuário está associado.</param>
	/// <returns>
	/// Uma tarefa assíncrona que representa a operação de busca e retorna o prontuário vinculado ao paciente ou <c>null</c> quando não existir.
	/// </returns>
	Task<ProntuarioModel?> GetByPacienteIdAsync(Guid pacienteId);

	/// <summary>
	/// Busca um prontuário pelo identificador carregando o paciente e os atendimentos relacionados.
	/// </summary>
	/// <param name="id">Identificador do prontuário herdado de <see cref="EntityBase"/>.</param>
	/// <returns>
	/// Uma tarefa assíncrona que representa a operação de consulta com detalhes e retorna o prontuário com navegações carregadas ou <c>null</c> quando não existir.
	/// </returns>
	Task<ProntuarioModel?> GetWithDetailsByIdAsync(int id);

	/// <summary>
	/// Cria um novo prontuário no banco de dados.
	/// </summary>
	/// <param name="prontuario">Entidade de prontuário que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que representa a operação de criação e retorna o prontuário persistido com os valores atualizados pelo banco de dados.
	/// </returns>
	Task<ProntuarioModel> CreateAsync(ProntuarioModel prontuario);

	/// <summary>
	/// Atualiza um prontuário existente no banco de dados.
	/// </summary>
	/// <param name="prontuario">Entidade de prontuário contendo o estado atualizado.</param>
	/// <returns>
	/// Uma tarefa assíncrona que representa a operação de atualização e retorna o prontuário atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<ProntuarioModel?> UpdateAsync(ProntuarioModel prontuario);

	/// <summary>
	/// Executa a exclusão lógica de um prontuário, marcando sua situação como inativa/desligada.
	/// </summary>
	/// <param name="id">Identificador do prontuário herdado de <see cref="EntityBase"/>.</param>
	/// <returns>
	/// Uma tarefa assíncrona que representa a operação de exclusão lógica e retorna <c>true</c> quando a alteração foi aplicada com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int id);
}
using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IEvolucaoAtendimentoRepository
{
	/// <summary>
	/// Obtém todas as evoluções de atendimento cadastradas no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de evoluções de atendimento encontradas.
	/// </returns>
	Task<IEnumerable<EvolucaoAtendimentoModel>> GetAllAsync();

	/// <summary>
	/// Busca uma evolução de atendimento pelo identificador herdado de <see cref="EntityBase"/>.
	/// </summary>
	/// <param name="id">Identificador único da evolução de atendimento.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a evolução encontrada ou <c>null</c> quando não existir.
	/// </returns>
	Task<EvolucaoAtendimentoModel?> GetByIdAsync(int id);

	/// <summary>
	/// Lista o histórico de evoluções vinculadas a um documento clínico específico.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de evoluções associadas ao documento clínico informado.
	/// </returns>
	Task<IEnumerable<EvolucaoAtendimentoModel>> GetByDocumentoClinicoIdAsync(int documentoClinicoId);

	/// <summary>
	/// Busca a evolução de atendimento vinculada a um atendimento específico.
	/// </summary>
	/// <param name="atendimentoId">Identificador do atendimento usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de evoluções associadas ao atendimento informado.
	/// </returns>
	Task<IEnumerable<EvolucaoAtendimentoModel>> GetByAtendimentoIdAsync(int atendimentoId);

	/// <summary>
	/// Cria uma nova evolução de atendimento no banco de dados.
	/// </summary>
	/// <param name="evolucao">Entidade de evolução que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a evolução persistida com os valores gravados pelo banco de dados.
	/// </returns>
	Task<EvolucaoAtendimentoModel> AddAsync(EvolucaoAtendimentoModel evolucao);

	/// <summary>
	/// Atualiza uma evolução de atendimento existente no banco de dados.
	/// </summary>
	/// <param name="evolucao">Entidade de evolução contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a evolução atualizada ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<EvolucaoAtendimentoModel?> UpdateAsync(EvolucaoAtendimentoModel evolucao);

	/// <summary>
	/// Remove uma evolução de atendimento do banco de dados.
	/// </summary>
	/// <param name="id">Identificador único da evolução de atendimento.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int id);
}
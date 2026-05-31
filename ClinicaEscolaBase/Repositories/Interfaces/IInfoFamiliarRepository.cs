using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IInfoFamiliarRepository
{
	/// <summary>
	/// Obtém todos os familiares cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de familiares encontrados.
	/// </returns>
	Task<IEnumerable<InfoFamiliarModel>> GetAllAsync();

	/// <summary>
	/// Busca um familiar pelo identificador herdado de <see cref="EntityBase"/>.
	/// </summary>
	/// <param name="id">Identificador único do familiar</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna um familiar
	/// </returns>
	Task<InfoFamiliarModel> GetByIdAsync(int id);

	/// <summary>
	/// Busca o familiar responsável legal, pelo identificador herdado de <see cref="EntityBase"/>.
	/// </summary>
	/// <param name="id">Identificador único do responsável legal.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o responsável legal encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<InfoFamiliarModel?> GetResponsavelByIdAsync(int id, bool responsavel=true);

	/// <summary>
	/// Lista todos os familiares vinculados a um paciente específico.
	/// </summary>
	/// <param name="pacienteId">Identificador do paciente usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de familiares associados ao paciente informado.
	/// </returns>
	Task<IEnumerable<InfoFamiliarModel>> GetByPacienteIdAsync(Guid pacienteId);


	/// <summary>
	/// Cria um novo familiar no banco de dados.
	/// </summary>
	/// <param name="familiar">Entidade de familiar que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o familiar persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<InfoFamiliarModel> AddAsync(InfoFamiliarModel familiar);

	/// <summary>
	/// Atualiza um familiar existente no banco de dados.
	/// </summary>
	/// <param name="familiar">Entidade de familiar contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o familiar atualizado 
	/// </returns>
	Task<InfoFamiliarModel> UpdateAsync(InfoFamiliarModel familiar);

	/// <summary>
	/// Remove um familiar do banco de dados.
	/// </summary>
	/// <param name="id">Identificador único do responsável legal.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int id);

	//add metodo para alterar familiar responsavel
}
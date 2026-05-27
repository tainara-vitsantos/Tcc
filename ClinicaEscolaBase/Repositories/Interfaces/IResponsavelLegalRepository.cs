using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IResponsavelLegalRepository
{
	/// <summary>
	/// Obtém todos os responsáveis legais cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de responsáveis legais encontrados.
	/// </returns>
	Task<IEnumerable<ResponsavelLegal>> GetAllAsync();

	/// <summary>
	/// Busca um responsável legal pelo identificador herdado de <see cref="EntityBase"/>.
	/// </summary>
	/// <param name="id">Identificador único do responsável legal.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o responsável legal encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<ResponsavelLegal?> GetByIdAsync(int id);

	/// <summary>
	/// Lista todos os responsáveis legais vinculados a um paciente específico.
	/// </summary>
	/// <param name="pacienteId">Identificador do paciente usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de responsáveis legais associados ao paciente informado.
	/// </returns>
	Task<IEnumerable<ResponsavelLegal>> GetByPacienteIdAsync(Guid pacienteId);

	/// <summary>
	/// Busca o responsável legal principal vinculado a um paciente específico.
	/// </summary>
	/// <param name="pacienteId">Identificador do paciente usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o responsável legal principal do paciente informado ou <c>null</c> quando não existir.
	/// </returns>
	Task<ResponsavelLegal?> GetResponsavelPrincipalByPacienteIdAsync(Guid pacienteId);

	/// <summary>
	/// Cria um novo responsável legal no banco de dados.
	/// </summary>
	/// <param name="responsavelLegal">Entidade de responsável legal que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o responsável legal persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<ResponsavelLegal> AddAsync(ResponsavelLegal responsavelLegal);

	/// <summary>
	/// Atualiza um responsável legal existente no banco de dados.
	/// </summary>
	/// <param name="responsavelLegal">Entidade de responsável legal contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o responsável legal atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<ResponsavelLegal?> UpdateAsync(ResponsavelLegal responsavelLegal);

	/// <summary>
	/// Remove um responsável legal do banco de dados.
	/// </summary>
	/// <param name="id">Identificador único do responsável legal.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int id);
}
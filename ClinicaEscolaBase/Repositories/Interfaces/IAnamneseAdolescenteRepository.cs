using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IAnamneseAdolescenteRepository
{
	/// <summary>
	/// Obtém todas as anamneses de adolescentes cadastradas no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de anamneses de adolescentes encontradas.
	/// </returns>
	Task<IEnumerable<AnamneseAdolescente>> GetAllAsync();

	/// <summary>
	/// Busca uma anamnese de adolescente pelo identificador do documento clínico.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado à anamnese.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a anamnese encontrada ou <c>null</c> quando não existir.
	/// </returns>
	Task<AnamneseAdolescente?> GetByIdAsync(int documentoClinicoId);

	/// <summary>
	/// Cria uma nova anamnese de adolescente no banco de dados.
	/// </summary>
	/// <param name="anamneseAdolescente">Entidade que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a anamnese persistida com os dados gravados pelo banco.
	/// </returns>
	Task<AnamneseAdolescente> AddAsync(AnamneseAdolescente anamneseAdolescente);

	/// <summary>
	/// Atualiza uma anamnese de adolescente existente no banco de dados.
	/// </summary>
	/// <param name="anamneseAdolescente">Entidade com os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a anamnese atualizada ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<AnamneseAdolescente?> UpdateAsync(AnamneseAdolescente anamneseAdolescente);

	/// <summary>
	/// Remove uma anamnese de adolescente do banco de dados.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado à anamnese.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int documentoClinicoId);
}
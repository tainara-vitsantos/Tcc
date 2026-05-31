using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IAnamneseAdultoRepository
{
	/// <summary>
	/// Obtém todas as anamneses de adultos cadastradas no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de anamneses de adultos encontradas.
	/// </returns>
	Task<IEnumerable<AnamneseAdultoModel>> GetAllAsync();

	/// <summary>
	/// Busca uma anamnese de adulto pelo identificador do documento clínico.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado à anamnese.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a anamnese encontrada ou <c>null</c> quando não existir.
	/// </returns>
	Task<AnamneseAdultoModel> GetByIdAsync(int documentoClinicoId);

	/// <summary>
	/// Cria uma nova anamnese de adulto no banco de dados.
	/// </summary>
	/// <param name="anamneseAdulto">Entidade que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a anamnese persistida com os dados gravados pelo banco.
	/// </returns>
	Task<AnamneseAdultoModel> AddAsync(AnamneseAdultoModel anamneseAdulto);

	/// <summary>
	/// Atualiza uma anamnese de adulto existente no banco de dados.
	/// </summary>
	/// <param name="anamneseAdulto">Entidade com os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a anamnese atualizada ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<AnamneseAdultoModel> UpdateAsync(AnamneseAdultoModel anamneseAdulto);

	/// <summary>
	/// Remove uma anamnese de adulto do banco de dados.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado à anamnese.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int documentoClinicoId);
}
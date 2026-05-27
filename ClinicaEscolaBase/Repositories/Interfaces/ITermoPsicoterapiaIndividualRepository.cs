using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface ITermoPsicoterapiaIndividualRepository
{
	/// <summary>
	/// Obtém todos os termos de psicoterapia individual cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de termos de psicoterapia individual encontrados.
	/// </returns>
	Task<IEnumerable<TermoPsicoterapiaIndividual>> GetAllAsync();

	/// <summary>
	/// Busca um termo de psicoterapia individual pelo identificador do documento clínico.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado ao termo.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<TermoPsicoterapiaIndividual?> GetByIdAsync(int documentoClinicoId);

	/// <summary>
	/// Cria um novo termo de psicoterapia individual no banco de dados.
	/// </summary>
	/// <param name="termo">Entidade de termo de psicoterapia individual que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<TermoPsicoterapiaIndividual> AddAsync(TermoPsicoterapiaIndividual termo);

	/// <summary>
	/// Atualiza um termo de psicoterapia individual existente no banco de dados.
	/// </summary>
	/// <param name="termo">Entidade de termo de psicoterapia individual contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<TermoPsicoterapiaIndividual?> UpdateAsync(TermoPsicoterapiaIndividual termo);

	/// <summary>
	/// Remove um termo de psicoterapia individual do banco de dados.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado ao termo.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int documentoClinicoId);
}
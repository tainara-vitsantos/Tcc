using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface ITermoAutorizacaoMenorRepository
{
	/// <summary>
	/// Obtém todos os termos de autorização de menor cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de termos de autorização de menor encontrados.
	/// </returns>
	Task<IEnumerable<TermoAutorizacaoMenor>> GetAllAsync();

	/// <summary>
	/// Busca um termo de autorização de menor pelo identificador do documento clínico.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado ao termo.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<TermoAutorizacaoMenor?> GetByIdAsync(int documentoClinicoId);

	/// <summary>
	/// Lista os termos de autorização de menor vinculados a um responsável legal específico.
	/// </summary>
	/// <param name="responsavelLegalId">Identificador do responsável legal usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de termos associados ao responsável legal informado.
	/// </returns>
	Task<IEnumerable<TermoAutorizacaoMenor>> GetByResponsavelLegalIdAsync(int responsavelLegalId);

	/// <summary>
	/// Cria um novo termo de autorização de menor no banco de dados.
	/// </summary>
	/// <param name="termo">Entidade de termo de autorização de menor que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<TermoAutorizacaoMenor> AddAsync(TermoAutorizacaoMenor termo);

	/// <summary>
	/// Atualiza um termo de autorização de menor existente no banco de dados.
	/// </summary>
	/// <param name="termo">Entidade de termo de autorização de menor contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<TermoAutorizacaoMenor?> UpdateAsync(TermoAutorizacaoMenor termo);

	/// <summary>
	/// Remove um termo de autorização de menor do banco de dados.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado ao termo.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int documentoClinicoId);
}
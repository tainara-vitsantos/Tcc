using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface ITermoResponsabilidadeEstagiarioRepository
{
	/// <summary>
	/// Obtém todos os termos de responsabilidade de estagiário cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de termos de responsabilidade de estagiário encontrados.
	/// </returns>
	Task<IEnumerable<TermoResponsabilidadeEstagiario>> GetAllAsync();

	/// <summary>
	/// Busca um termo de responsabilidade de estagiário pelo identificador herdado de <see cref="EntityBase"/>.
	/// </summary>
	/// <param name="id">Identificador único do termo de responsabilidade de estagiário.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<TermoResponsabilidadeEstagiario?> GetByIdAsync(int id);

	/// <summary>
	/// Lista os termos de responsabilidade de um estagiário específico.
	/// </summary>
	/// <param name="estagiarioId">Identificador do usuário estagiário usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de termos associados ao estagiário informado.
	/// </returns>
	Task<IEnumerable<TermoResponsabilidadeEstagiario>> GetByEstagiarioIdAsync(string estagiarioId);

	/// <summary>
	/// Busca o termo mais recente ou ativo assinado por um estagiário específico.
	/// </summary>
	/// <param name="estagiarioId">Identificador do usuário estagiário usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo ativo ou mais recente do estagiário informado, ou <c>null</c> quando não existir.
	/// </returns>
	Task<TermoResponsabilidadeEstagiario?> GetAtivoByEstagiarioIdAsync(string estagiarioId);

	/// <summary>
	/// Cria um novo termo de responsabilidade de estagiário no banco de dados.
	/// </summary>
	/// <param name="termo">Entidade de termo de responsabilidade de estagiário que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<TermoResponsabilidadeEstagiario> AddAsync(TermoResponsabilidadeEstagiario termo);

	/// <summary>
	/// Atualiza um termo de responsabilidade de estagiário existente no banco de dados.
	/// </summary>
	/// <param name="termo">Entidade de termo de responsabilidade de estagiário contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<TermoResponsabilidadeEstagiario?> UpdateAsync(TermoResponsabilidadeEstagiario termo);

	/// <summary>
	/// Remove um termo de responsabilidade de estagiário do banco de dados.
	/// </summary>
	/// <param name="id">Identificador único do termo de responsabilidade de estagiário.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int id);
}
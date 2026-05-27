using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IAuditoriaRepository
{
	/// <summary>
	/// Registra um novo log de auditoria no banco de dados.
	/// </summary>
	/// <param name="auditoria">Entidade de auditoria que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o log de auditoria persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<Auditoria> AddAsync(Auditoria auditoria);

	/// <summary>
	/// Busca um log de auditoria pelo identificador herdado de <see cref="EntityBase"/>.
	/// </summary>
	/// <param name="id">Identificador único do log de auditoria.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o log encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<Auditoria?> GetByIdAsync(int id);

	/// <summary>
	/// Lista os logs de auditoria realizados por um usuário específico.
	/// </summary>
	/// <param name="usuarioId">Identificador do usuário que executou as ações auditadas.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de logs vinculados ao usuário informado.
	/// </returns>
	Task<IEnumerable<Auditoria>> GetByUsuarioIdAsync(string usuarioId);

	/// <summary>
	/// Lista os logs de auditoria ocorridos em um intervalo de datas.
	/// </summary>
	/// <param name="inicio">Data e hora inicial do intervalo a ser consultado.</param>
	/// <param name="fim">Data e hora final do intervalo a ser consultado.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de logs registrados no período informado.
	/// </returns>
	Task<IEnumerable<Auditoria>> GetByPeriodoAsync(DateTime inicio, DateTime fim);

	/// <summary>
	/// Lista o histórico de auditoria de uma entidade e de um registro específico.
	/// </summary>
	/// <param name="entidade">Nome da entidade auditada.</param>
	/// <param name="registroId">Identificador do registro auditado.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de logs relacionados à entidade e ao registro informados.
	/// </returns>
	Task<IEnumerable<Auditoria>> GetByEntidadeERegistroAsync(string entidade, string registroId);
}
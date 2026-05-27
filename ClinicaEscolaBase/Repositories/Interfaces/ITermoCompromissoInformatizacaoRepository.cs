using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface ITermoCompromissoInformatizacaoRepository
{
	/// <summary>
	/// Obtém todos os termos de compromisso de informatização cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de termos de compromisso de informatização encontrados.
	/// </returns>
	Task<IEnumerable<TermoCompromissoInformatizacaoModel>> GetAllAsync();

	/// <summary>
	/// Busca um termo de compromisso de informatização pelo identificador do documento clínico.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado ao termo.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<TermoCompromissoInformatizacaoModel?> GetByIdAsync(int documentoClinicoId);

	/// <summary>
	/// Lista os termos de compromisso de informatização assinados por um estagiário específico.
	/// </summary>
	/// <param name="estagiarioId">Identificador do usuário estagiário usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de termos assinados pelo estagiário informado.
	/// </returns>
	Task<IEnumerable<TermoCompromissoInformatizacaoModel>> GetByEstagiarioIdAsync(string estagiarioId);

	/// <summary>
	/// Lista os termos de compromisso de informatização vinculados a um paciente específico.
	/// </summary>
	/// <param name="pacienteId">Identificador do paciente usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de termos associados ao paciente informado.
	/// </returns>
	Task<IEnumerable<TermoCompromissoInformatizacaoModel>> GetByPacienteIdAsync(Guid pacienteId);

	/// <summary>
	/// Cria um novo termo de compromisso de informatização no banco de dados.
	/// </summary>
	/// <param name="termo">Entidade de termo de compromisso de informatização que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<TermoCompromissoInformatizacaoModel> AddAsync(TermoCompromissoInformatizacaoModel termo);

	/// <summary>
	/// Atualiza um termo de compromisso de informatização existente no banco de dados.
	/// </summary>
	/// <param name="termo">Entidade de termo de compromisso de informatização contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o termo atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<TermoCompromissoInformatizacaoModel?> UpdateAsync(TermoCompromissoInformatizacaoModel termo);

	/// <summary>
	/// Remove um termo de compromisso de informatização do banco de dados.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado ao termo.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int documentoClinicoId);
}
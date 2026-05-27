using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IDocumentoClinicoRepository
{
	/// <summary>
	/// Obtém todos os documentos clínicos que não foram excluídos logicamente.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de documentos clínicos ativos.
	/// </returns>
	Task<IEnumerable<DocumentoClinico>> GetAllAtivosAsync();

	/// <summary>
	/// Busca um documento clínico ativo pelo identificador herdado de <see cref="EntityBase"/>.
	/// </summary>
	/// <param name="id">Identificador único do documento clínico.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o documento encontrado ou <c>null</c> quando não existir ou estiver excluído logicamente.
	/// </returns>
	Task<DocumentoClinico?> GetByIdAsync(int id);

	/// <summary>
	/// Lista os documentos clínicos ativos de um prontuário específico.
	/// </summary>
	/// <param name="prontuarioId">Identificador do prontuário ao qual os documentos estão vinculados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de documentos clínicos ativos do prontuário informado.
	/// </returns>
	Task<IEnumerable<DocumentoClinico>> GetByProntuarioIdAsync(int prontuarioId);

	/// <summary>
	/// Lista os documentos clínicos ativos de acordo com o status informado.
	/// </summary>
	/// <param name="status">Status do documento clínico a ser aplicado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de documentos clínicos ativos com o status informado.
	/// </returns>
	Task<IEnumerable<DocumentoClinico>> GetByStatusAsync(StatusDocumentoClinicoEnum status);

	/// <summary>
	/// Cria um novo documento clínico no banco de dados.
	/// </summary>
	/// <param name="documento">Entidade de documento clínico que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o documento clínico persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<DocumentoClinico> AddAsync(DocumentoClinico documento);

	/// <summary>
	/// Atualiza um documento clínico existente no banco de dados.
	/// </summary>
	/// <param name="documento">Entidade de documento clínico contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o documento clínico atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<DocumentoClinico?> UpdateAsync(DocumentoClinico documento);

	/// <summary>
	/// Executa a exclusão lógica de um documento clínico.
	/// </summary>
	/// <param name="id">Identificador único do documento clínico.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão lógica for aplicada com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int id);
}
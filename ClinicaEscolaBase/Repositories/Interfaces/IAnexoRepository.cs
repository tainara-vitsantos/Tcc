using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IAnexoRepository
{
	/// <summary>
	/// Obtém todos os anexos cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de anexos encontrados.
	/// </returns>
	Task<IEnumerable<Anexo>> GetAllAsync();

	/// <summary>
	/// Busca um anexo pelo identificador primário herdado de <see cref="EntityBase"/>.
	/// </summary>
	/// <param name="id">Identificador único do anexo.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o anexo encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<Anexo?> GetByIdAsync(int id);

	/// <summary>
	/// Lista todos os anexos associados a um documento clínico específico.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico que será usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de anexos vinculados ao documento clínico informado.
	/// </returns>
	Task<IEnumerable<Anexo>> GetByDocumentoClinicoIdAsync(int documentoClinicoId);

	/// <summary>
	/// Cria um novo anexo no banco de dados.
	/// </summary>
	/// <param name="anexo">Entidade de anexo que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o anexo persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<Anexo> AddAsync(Anexo anexo);

	/// <summary>
	/// Atualiza um anexo existente no banco de dados.
	/// </summary>
	/// <param name="anexo">Entidade de anexo contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o anexo atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<Anexo?> UpdateAsync(Anexo anexo);

	/// <summary>
	/// Remove um anexo do banco de dados.
	/// </summary>
	/// <param name="id">Identificador único do anexo.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int id);
}
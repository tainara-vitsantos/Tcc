using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IDocIdentificacaoPacienteRepository
{
	/// <summary>
	/// Obtém todos os documentos de identificação de pacientes cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de documentos de identificação de pacientes encontrados.
	/// </returns>
	Task<IEnumerable<DocumentoIdentificacaoPaciente>> GetAllAsync();

	/// <summary>
	/// Busca um documento de identificação de paciente pelo identificador do documento clínico.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado ao registro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o documento encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<DocumentoIdentificacaoPaciente?> GetByIdAsync(int documentoClinicoId);

	/// <summary>
	/// Cria um novo documento de identificação de paciente no banco de dados.
	/// </summary>
	/// <param name="docIdentificacao">Entidade de documento de identificação que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o documento persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<DocumentoIdentificacaoPaciente> AddAsync(DocumentoIdentificacaoPaciente docIdentificacao);

	/// <summary>
	/// Atualiza um documento de identificação de paciente existente no banco de dados.
	/// </summary>
	/// <param name="docIdentificacao">Entidade de documento de identificação contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o documento atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<DocumentoIdentificacaoPaciente?> UpdateAsync(DocumentoIdentificacaoPaciente docIdentificacao);

	/// <summary>
	/// Remove um documento de identificação de paciente do banco de dados.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado ao registro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int documentoClinicoId);
}
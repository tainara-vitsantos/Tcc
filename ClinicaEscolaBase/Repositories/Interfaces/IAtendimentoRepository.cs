using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IAtendimentoRepository
{
	/// <summary>
	/// Obtém todos os atendimentos cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de atendimentos encontrados.
	/// </returns>
	Task<IEnumerable<Atendimento>> GetAllAsync();

	/// <summary>
	/// Busca um atendimento pelo identificador herdado de <see cref="EntityBase"/>.
	/// </summary>
	/// <param name="id">Identificador único do atendimento.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o atendimento encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<Atendimento?> GetByIdAsync(int id);

	/// <summary>
	/// Lista os atendimentos de um aluno específico.
	/// </summary>
	/// <param name="alunoId">Identificador do aluno responsável pelo atendimento.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de atendimentos do aluno informado.
	/// </returns>
	Task<IEnumerable<Atendimento>> GetByAlunoIdAsync(string alunoId);

	/// <summary>
	/// Lista os atendimentos de um paciente específico.
	/// </summary>
	/// <param name="pacienteId">Identificador do paciente que será usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de atendimentos do paciente informado.
	/// </returns>
	Task<IEnumerable<Atendimento>> GetByPacienteIdAsync(Guid pacienteId);

	/// <summary>
	/// Filtra os atendimentos por status.
	/// </summary>
	/// <param name="status">Status do atendimento a ser aplicado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de atendimentos com o status informado.
	/// </returns>
	Task<IEnumerable<Atendimento>> GetByStatusAsync(StatusAtendimentoEnum status);

	/// <summary>
	/// Cria um novo atendimento no banco de dados.
	/// </summary>
	/// <param name="atendimento">Entidade de atendimento que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o atendimento persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<Atendimento> AddAsync(Atendimento atendimento);

	/// <summary>
	/// Atualiza um atendimento existente no banco de dados.
	/// </summary>
	/// <param name="atendimento">Entidade de atendimento contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o atendimento atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<Atendimento?> UpdateAsync(Atendimento atendimento);

	/// <summary>
	/// Remove um atendimento do banco de dados.
	/// </summary>
	/// <param name="id">Identificador único do atendimento.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int id);
}
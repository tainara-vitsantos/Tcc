using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface ITratamentoAnteriorPacienteRepository
{
	/// <summary>
	/// Obtém todos os tratamentos anteriores de pacientes cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de tratamentos anteriores encontrados.
	/// </returns>
	Task<IEnumerable<TratamentoAnteriorPaciente>> GetAllAsync();

	/// <summary>
	/// Busca um tratamento anterior pelo identificador herdado de <see cref="EntityBase"/>.
	/// </summary>
	/// <param name="id">Identificador único do tratamento anterior do paciente.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o tratamento encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<TratamentoAnteriorPaciente?> GetByIdAsync(int id);

	/// <summary>
	/// Lista o histórico completo de tratamentos anteriores de um paciente específico.
	/// </summary>
	/// <param name="pacienteId">Identificador do paciente usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de tratamentos anteriores associados ao paciente informado.
	/// </returns>
	Task<IEnumerable<TratamentoAnteriorPaciente>> GetByPacienteIdAsync(Guid pacienteId);

	/// <summary>
	/// Lista apenas os registros de internação de um paciente específico.
	/// </summary>
	/// <param name="pacienteId">Identificador do paciente usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de tratamentos anteriores com histórico de internação do paciente informado.
	/// </returns>
	Task<IEnumerable<TratamentoAnteriorPaciente>> GetInternacoesByPacienteIdAsync(Guid pacienteId);

	/// <summary>
	/// Cria um novo tratamento anterior de paciente no banco de dados.
	/// </summary>
	/// <param name="tratamentoAnterior">Entidade de tratamento anterior que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o tratamento anterior persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<TratamentoAnteriorPaciente> AddAsync(TratamentoAnteriorPaciente tratamentoAnterior);

	/// <summary>
	/// Atualiza um tratamento anterior de paciente existente no banco de dados.
	/// </summary>
	/// <param name="tratamentoAnterior">Entidade de tratamento anterior contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o tratamento atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<TratamentoAnteriorPaciente?> UpdateAsync(TratamentoAnteriorPaciente tratamentoAnterior);

	/// <summary>
	/// Remove um tratamento anterior de paciente do banco de dados.
	/// </summary>
	/// <param name="id">Identificador único do tratamento anterior do paciente.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int id);
}
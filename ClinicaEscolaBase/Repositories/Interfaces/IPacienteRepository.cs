using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IPacienteRepository
{
	/// <summary>
	/// Retorna todos os pacientes ativos em modo somente leitura.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de pacientes ativos.
	/// </returns>
	Task<IEnumerable<Paciente>> GetAllAsync();

	/// <summary>
	/// Busca um paciente ativo pelo identificador em modo somente leitura.
	/// </summary>
	/// <param name="id">Identificador único do paciente.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o paciente encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<Paciente?> GetByIdAsync(Guid id);

	/// <summary>
	/// Busca um paciente ativo pelo identificador carregando os detalhes relacionados.
	/// </summary>
	/// <param name="id">Identificador único do paciente.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o paciente com os relacionamentos carregados ou <c>null</c> quando não existir.
	/// </returns>
	Task<Paciente?> GetByIdWithDetailsAsync(Guid id);

	/// <summary>
	/// Cria um novo paciente garantindo a data de criação em UTC.
	/// </summary>
	/// <param name="paciente">Entidade de paciente que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o paciente persistido com os valores atualizados pelo banco de dados.
	/// </returns>
	Task<Paciente> CreateAsync(Paciente paciente);

	/// <summary>
	/// Atualiza um paciente existente e registra a data da última alteração em UTC.
	/// </summary>
	/// <param name="paciente">Entidade de paciente contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o paciente atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<Paciente?> UpdateAsync(Paciente paciente);

	/// <summary>
	/// Remove logicamente um paciente, marcando-o como inativo.
	/// </summary>
	/// <param name="id">Identificador único do paciente.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão lógica for aplicada com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(Guid id);
}
using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IVinculoAlunoPacienteRepository
{
	/// <summary>
	/// Obtém todos os vínculos de aluno e paciente cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de vínculos encontrados.
	/// </returns>
	Task<IEnumerable<VinculoAlunoPacienteModel>> GetAllAsync();

	/// <summary>
	/// Busca um vínculo de aluno e paciente pelo identificador herdado de <see cref="EntityBase"/>.
	/// </summary>
	/// <param name="id">Identificador único do vínculo.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o vínculo encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<VinculoAlunoPacienteModel?> GetByIdAsync(int id);

	/// <summary>
	/// Lista todos os vínculos de um aluno específico.
	/// </summary>
	/// <param name="alunoId">Identificador do aluno usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de vínculos associados ao aluno informado.
	/// </returns>
	Task<IEnumerable<VinculoAlunoPacienteModel>> GetByAlunoIdAsync(string alunoId);

	/// <summary>
	/// Lista todos os vínculos de um paciente específico.
	/// </summary>
	/// <param name="pacienteId">Identificador do paciente usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de vínculos associados ao paciente informado.
	/// </returns>
	Task<IEnumerable<VinculoAlunoPacienteModel>> GetByPacienteIdAsync(Guid pacienteId);

	/// <summary>
	/// Busca um vínculo ativo entre aluno e paciente.
	/// </summary>
	/// <param name="alunoId">Identificador do aluno usado no filtro.</param>
	/// <param name="pacienteId">Identificador do paciente usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o vínculo ativo encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<VinculoAlunoPacienteModel?> GetVinculoAtivoAsync(string alunoId, Guid pacienteId);

	/// <summary>
	/// Cria um novo vínculo de aluno e paciente no banco de dados.
	/// </summary>
	/// <param name="vinculo">Entidade de vínculo que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o vínculo persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<VinculoAlunoPacienteModel> AddAsync(VinculoAlunoPacienteModel vinculo);

	/// <summary>
	/// Atualiza um vínculo de aluno e paciente existente no banco de dados.
	/// </summary>
	/// <param name="vinculo">Entidade de vínculo contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o vínculo atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<VinculoAlunoPacienteModel?> UpdateAsync(VinculoAlunoPacienteModel vinculo);

	/// <summary>
	/// Remove um vínculo de aluno e paciente do banco de dados.
	/// </summary>
	/// <param name="id">Identificador único do vínculo.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int id);
}
using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IPlantaoPsicologicoRepository
{
	/// <summary>
	/// Obtém todos os registros de plantão psicológico cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de plantões psicológicos encontrados.
	/// </returns>
	Task<IEnumerable<PlantaoPsicologicoModel>> GetAllAsync();

	/// <summary>
	/// Busca um plantão psicológico pelo identificador do documento clínico.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado ao plantão psicológico.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o plantão psicológico encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<PlantaoPsicologicoModel?> GetByIdAsync(int documentoClinicoId);

	/// <summary>
	/// Cria um novo registro de plantão psicológico no banco de dados.
	/// </summary>
	/// <param name="plantaoPsicologico">Entidade de plantão psicológico que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o plantão psicológico persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<PlantaoPsicologicoModel> AddAsync(PlantaoPsicologicoModel plantaoPsicologico);

	/// <summary>
	/// Atualiza um registro de plantão psicológico existente no banco de dados.
	/// </summary>
	/// <param name="plantaoPsicologico">Entidade de plantão psicológico contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o plantão psicológico atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<PlantaoPsicologicoModel?> UpdateAsync(PlantaoPsicologicoModel plantaoPsicologico);

	/// <summary>
	/// Remove um plantão psicológico do banco de dados.
	/// </summary>
	/// <param name="documentoClinicoId">Identificador do documento clínico associado ao plantão psicológico.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(int documentoClinicoId);
}
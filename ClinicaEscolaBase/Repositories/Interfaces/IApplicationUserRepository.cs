using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;

namespace ClinicaEscolaBase.Repositories.Interfaces;

public interface IApplicationUserRepository
{
	/// <summary>
	/// Obtém todos os usuários cadastrados no banco de dados.
	/// </summary>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de usuários encontrados.
	/// </returns>
	Task<IEnumerable<ApplicationUser>> GetAllAsync();

	/// <summary>
	/// Busca um usuário pelo identificador.
	/// </summary>
	/// <param name="id">Identificador único do usuário.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o usuário encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<ApplicationUser?> GetByIdAsync(string id);

	/// <summary>
	/// Busca um usuário pelo CPF.
	/// </summary>
	/// <param name="cpf">CPF do usuário a ser localizado.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o usuário encontrado ou <c>null</c> quando não existir.
	/// </returns>
	Task<ApplicationUser?> GetByCpfAsync(string cpf);

	/// <summary>
	/// Lista os usuários de um determinado tipo/perfil.
	/// </summary>
	/// <param name="tipoUsuario">Tipo de usuário que será usado no filtro.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna a lista de usuários do perfil informado.
	/// </returns>
	Task<IEnumerable<ApplicationUser>> GetByTipoUsuarioAsync(TipoUsuario tipoUsuario);

	/// <summary>
	/// Cria um novo usuário no banco de dados.
	/// </summary>
	/// <param name="usuario">Entidade de usuário que será persistida.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o usuário persistido com os valores gravados pelo banco de dados.
	/// </returns>
	Task<ApplicationUser> AddAsync(ApplicationUser usuario);

	/// <summary>
	/// Atualiza um usuário existente no banco de dados.
	/// </summary>
	/// <param name="usuario">Entidade de usuário contendo os dados atualizados.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna o usuário atualizado ou <c>null</c> quando o registro não for encontrado.
	/// </returns>
	Task<ApplicationUser?> UpdateAsync(ApplicationUser usuario);

	/// <summary>
	/// Remove um usuário do banco de dados.
	/// </summary>
	/// <param name="id">Identificador único do usuário.</param>
	/// <returns>
	/// Uma tarefa assíncrona que retorna <c>true</c> quando a exclusão for concluída com sucesso; caso contrário, retorna <c>false</c>.
	/// </returns>
	Task<bool> DeleteAsync(string id);
}
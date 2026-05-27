using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class ApplicationUserRepository(ApplicationDbContext AppDbContext) : IApplicationUserRepository
{
	public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
	{
		return await AppDbContext.Users
			.AsNoTracking()
			.Where(usuario => usuario.Ativo)
			.OrderBy(usuario => usuario.NomeCompleto)
			.ToListAsync();
	}

	public async Task<ApplicationUser?> GetByIdAsync(string id)
	{
		return await AppDbContext.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(usuario => usuario.Id == id && usuario.Ativo);
	}

	public async Task<ApplicationUser?> GetByCpfAsync(string cpf)
	{
		return await AppDbContext.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(usuario => usuario.Cpf == cpf && usuario.Ativo);
	}

	public async Task<IEnumerable<ApplicationUser>> GetByTipoUsuarioAsync(TipoUsuarioEnum tipoUsuario)
	{
		return await AppDbContext.Users
			.AsNoTracking()
			.Where(usuario => usuario.TipoUsuario == tipoUsuario && usuario.Ativo)
			.OrderBy(usuario => usuario.NomeCompleto)
			.ToListAsync();
	}

	public async Task<ApplicationUser> AddAsync(ApplicationUser usuario)
	{
		usuario.Ativo = true;

		await AppDbContext.Users.AddAsync(usuario);
		await AppDbContext.SaveChangesAsync();

		return usuario;
	}

	public async Task<ApplicationUser?> UpdateAsync(ApplicationUser usuario)
	{
		var existente = await AppDbContext.Users
			.FirstOrDefaultAsync(item => item.Id == usuario.Id);

		if (existente is null)
		{
			return null;
		}

		AppDbContext.Entry(existente).CurrentValues.SetValues(usuario);
		existente.Id = usuario.Id;

		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(string id)
	{
		var linhasAfetadas = await AppDbContext.Users
			.Where(usuario => usuario.Id == id)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class AnamneseAdolescenteRepository(ApplicationDbContext AppDbContext) : IAnamneseAdolescenteRepository
{
	public async Task<IEnumerable<AnamneseAdolescente>> GetAllAsync()
	{
		return await AppDbContext.AnamnesesAdolescente
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<AnamneseAdolescente?> GetByIdAsync(int documentoClinicoId)
	{
		return await AppDbContext.AnamnesesAdolescente
			.AsNoTracking()
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == documentoClinicoId);
	}

	public async Task<AnamneseAdolescente> AddAsync(AnamneseAdolescente anamneseAdolescente)
	{
		AppDbContext.AnamnesesAdolescente.Add(anamneseAdolescente);
		await AppDbContext.SaveChangesAsync();

		return anamneseAdolescente;
	}

	public async Task<AnamneseAdolescente?> UpdateAsync(AnamneseAdolescente anamneseAdolescente)
	{
		var existente = await AppDbContext.AnamnesesAdolescente
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == anamneseAdolescente.DocumentoClinicoId);

		if (existente is null)
		{
			return null;
		}

		AppDbContext.Entry(existente).CurrentValues.SetValues(anamneseAdolescente);
		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int documentoClinicoId)
	{
		var existente = await AppDbContext.AnamnesesAdolescente
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == documentoClinicoId);

		if (existente is null)
		{
			return false;
		}

		AppDbContext.AnamnesesAdolescente.Remove(existente);
		await AppDbContext.SaveChangesAsync();

		return true;
	}
}
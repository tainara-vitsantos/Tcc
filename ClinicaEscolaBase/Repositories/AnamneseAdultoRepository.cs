using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class AnamneseAdultoRepository(ApplicationDbContext AppDbContext) : IAnamneseAdultoRepository
{
	public async Task<IEnumerable<AnamneseAdulto>> GetAllAsync()
	{
		return await AppDbContext.AnamnesesAdulto
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<AnamneseAdulto?> GetByIdAsync(int documentoClinicoId)
	{
		return await AppDbContext.AnamnesesAdulto
			.AsNoTracking()
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == documentoClinicoId);
	}

	public async Task<AnamneseAdulto> AddAsync(AnamneseAdulto anamneseAdulto)
	{
		AppDbContext.AnamnesesAdulto.Add(anamneseAdulto);
		await AppDbContext.SaveChangesAsync();

		return anamneseAdulto;
	}

	public async Task<AnamneseAdulto?> UpdateAsync(AnamneseAdulto anamneseAdulto)
	{
		var existente = await AppDbContext.AnamnesesAdulto
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == anamneseAdulto.DocumentoClinicoId);

		if (existente is null)
		{
			return null;
		}

		AppDbContext.Entry(existente).CurrentValues.SetValues(anamneseAdulto);
		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int documentoClinicoId)
	{
		var existente = await AppDbContext.AnamnesesAdulto
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == documentoClinicoId);

		if (existente is null)
		{
			return false;
		}

		AppDbContext.AnamnesesAdulto.Remove(existente);
		await AppDbContext.SaveChangesAsync();

		return true;
	}
}
using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class PlantaoPsicologicoRepository(ApplicationDbContext AppDbContext) : IPlantaoPsicologicoRepository
{
	public async Task<IEnumerable<PlantaoPsicologicoModel>> GetAllAsync()
	{
		return await AppDbContext.PlantaoPsicologico
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<PlantaoPsicologicoModel?> GetByIdAsync(int documentoClinicoId)
	{
		return await AppDbContext.PlantaoPsicologico
			.AsNoTracking()
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == documentoClinicoId);
	}

	public async Task<PlantaoPsicologicoModel> AddAsync(PlantaoPsicologicoModel plantaoPsicologico)
	{
		await AppDbContext.PlantaoPsicologico.AddAsync(plantaoPsicologico);
		await AppDbContext.SaveChangesAsync();

		return plantaoPsicologico;
	}

	public async Task<PlantaoPsicologicoModel?> UpdateAsync(PlantaoPsicologicoModel plantaoPsicologico)
	{
		var existente = await AppDbContext.PlantaoPsicologico
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == plantaoPsicologico.DocumentoClinicoId);

		if (existente is null)
		{
			return null;
		}

		AppDbContext.Entry(existente).CurrentValues.SetValues(plantaoPsicologico);
		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int documentoClinicoId)
	{
		var linhasAfetadas = await AppDbContext.PlantaoPsicologico
			.Where(item => item.DocumentoClinicoId == documentoClinicoId)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class TermoPsicoterapiaIndividualRepository(ApplicationDbContext AppDbContext) : ITermoPsicoterapiaIndividualRepository
{
	public async Task<IEnumerable<TermoPsicoterapiaIndividualModel>> GetAllAsync()
	{
		return await AppDbContext.TermosPsicoterapiaIndividual
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<TermoPsicoterapiaIndividualModel?> GetByIdAsync(int documentoClinicoId)
	{
		return await AppDbContext.TermosPsicoterapiaIndividual
			.AsNoTracking()
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == documentoClinicoId);
	}

	public async Task<TermoPsicoterapiaIndividualModel> AddAsync(TermoPsicoterapiaIndividualModel termo)
	{
		await AppDbContext.TermosPsicoterapiaIndividual.AddAsync(termo);
		await AppDbContext.SaveChangesAsync();

		return termo;
	}

	public async Task<TermoPsicoterapiaIndividualModel?> UpdateAsync(TermoPsicoterapiaIndividualModel termo)
	{
		var existente = await AppDbContext.TermosPsicoterapiaIndividual
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == termo.DocumentoClinicoId);

		if (existente is null)
		{
			return null;
		}

		AppDbContext.Entry(existente).CurrentValues.SetValues(termo);
		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int documentoClinicoId)
	{
		var linhasAfetadas = await AppDbContext.TermosPsicoterapiaIndividual
			.Where(item => item.DocumentoClinicoId == documentoClinicoId)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class TermoAutorizacaoMenorRepository(ApplicationDbContext AppDbContext) : ITermoAutorizacaoMenorRepository
{
	public async Task<IEnumerable<TermoAutorizacaoMenor>> GetAllAsync()
	{
		return await AppDbContext.TermosAutorizacaoMenor
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<TermoAutorizacaoMenor?> GetByIdAsync(int documentoClinicoId)
	{
		return await AppDbContext.TermosAutorizacaoMenor
			.AsNoTracking()
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == documentoClinicoId);
	}

	public async Task<IEnumerable<TermoAutorizacaoMenor>> GetByResponsavelLegalIdAsync(int responsavelLegalId)
	{
		return await AppDbContext.TermosAutorizacaoMenor
			.AsNoTracking()
			.Where(item => item.ResponsavelLegalId == responsavelLegalId)
			.ToListAsync();
	}

	public async Task<TermoAutorizacaoMenor> AddAsync(TermoAutorizacaoMenor termo)
	{
		await AppDbContext.TermosAutorizacaoMenor.AddAsync(termo);
		await AppDbContext.SaveChangesAsync();

		return termo;
	}

	public async Task<TermoAutorizacaoMenor?> UpdateAsync(TermoAutorizacaoMenor termo)
	{
		var existente = await AppDbContext.TermosAutorizacaoMenor
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
		var linhasAfetadas = await AppDbContext.TermosAutorizacaoMenor
			.Where(item => item.DocumentoClinicoId == documentoClinicoId)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
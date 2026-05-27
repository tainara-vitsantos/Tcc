using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class TermoResponsabilidadeEstagiarioRepository(ApplicationDbContext AppDbContext) : ITermoResponsabilidadeEstagiarioRepository
{
	public async Task<IEnumerable<TermoResponsabilidadeEstagiario>> GetAllAsync()
	{
		return await AppDbContext.TermosResponsabilidadeEstagiario
			.AsNoTracking()
			.OrderByDescending(item => item.DataAssinatura)
			.ThenByDescending(item => item.Id)
			.ToListAsync();
	}

	public async Task<TermoResponsabilidadeEstagiario?> GetByIdAsync(int id)
	{
		return await AppDbContext.TermosResponsabilidadeEstagiario
			.AsNoTracking()
			.FirstOrDefaultAsync(item => item.Id == id);
	}

	public async Task<IEnumerable<TermoResponsabilidadeEstagiario>> GetByEstagiarioIdAsync(string estagiarioId)
	{
		return await AppDbContext.TermosResponsabilidadeEstagiario
			.AsNoTracking()
			.Where(item => item.EstagiarioUsuarioId == estagiarioId)
			.OrderByDescending(item => item.DataAssinatura)
			.ThenByDescending(item => item.Id)
			.ToListAsync();
	}

	public async Task<TermoResponsabilidadeEstagiario?> GetAtivoByEstagiarioIdAsync(string estagiarioId)
	{
		return await AppDbContext.TermosResponsabilidadeEstagiario
			.AsNoTracking()
			.Where(item => item.EstagiarioUsuarioId == estagiarioId && item.Ativo)
			.OrderByDescending(item => item.DataAssinatura)
			.ThenByDescending(item => item.Id)
			.FirstOrDefaultAsync();
	}

	public async Task<TermoResponsabilidadeEstagiario> AddAsync(TermoResponsabilidadeEstagiario termo)
	{
		termo.DataCriacao = DateTime.UtcNow;
		termo.Ativo = true;

		await AppDbContext.TermosResponsabilidadeEstagiario.AddAsync(termo);
		await AppDbContext.SaveChangesAsync();

		return termo;
	}

	public async Task<TermoResponsabilidadeEstagiario?> UpdateAsync(TermoResponsabilidadeEstagiario termo)
	{
		var existente = await AppDbContext.TermosResponsabilidadeEstagiario
			.FirstOrDefaultAsync(item => item.Id == termo.Id && item.Ativo);

		if (existente is null)
		{
			return null;
		}

		var dataCriacaoOriginal = existente.DataCriacao;

		AppDbContext.Entry(existente).CurrentValues.SetValues(termo);
		existente.DataCriacao = dataCriacaoOriginal;
		existente.DataAtualizacao = DateTime.UtcNow;

		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var linhasAfetadas = await AppDbContext.TermosResponsabilidadeEstagiario
			.Where(item => item.Id == id)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
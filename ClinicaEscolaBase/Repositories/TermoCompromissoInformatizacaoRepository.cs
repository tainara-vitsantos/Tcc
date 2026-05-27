using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class TermoCompromissoInformatizacaoRepository(ApplicationDbContext AppDbContext) : ITermoCompromissoInformatizacaoRepository
{
	public async Task<IEnumerable<TermoCompromissoInformatizacao>> GetAllAsync()
	{
		return await AppDbContext.TermosCompromissoInformatizacao
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<TermoCompromissoInformatizacao?> GetByIdAsync(int documentoClinicoId)
	{
		return await AppDbContext.TermosCompromissoInformatizacao
			.AsNoTracking()
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == documentoClinicoId);
	}

	public async Task<IEnumerable<TermoCompromissoInformatizacao>> GetByEstagiarioIdAsync(string estagiarioId)
	{
		return await AppDbContext.TermosCompromissoInformatizacao
			.AsNoTracking()
			.Where(item => item.EstagiarioUsuarioId == estagiarioId)
			.ToListAsync();
	}

	public async Task<IEnumerable<TermoCompromissoInformatizacao>> GetByPacienteIdAsync(Guid pacienteId)
	{
		return await AppDbContext.TermosCompromissoInformatizacao
			.AsNoTracking()
			.Where(item => item.PacienteId == pacienteId)
			.ToListAsync();
	}

	public async Task<TermoCompromissoInformatizacao> AddAsync(TermoCompromissoInformatizacao termo)
	{
		await AppDbContext.TermosCompromissoInformatizacao.AddAsync(termo);
		await AppDbContext.SaveChangesAsync();

		return termo;
	}

	public async Task<TermoCompromissoInformatizacao?> UpdateAsync(TermoCompromissoInformatizacao termo)
	{
		var existente = await AppDbContext.TermosCompromissoInformatizacao
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
		var linhasAfetadas = await AppDbContext.TermosCompromissoInformatizacao
			.Where(item => item.DocumentoClinicoId == documentoClinicoId)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
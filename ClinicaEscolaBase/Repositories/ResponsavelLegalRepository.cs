using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class ResponsavelLegalRepository(ApplicationDbContext AppDbContext) : IResponsavelLegalRepository
{
	public async Task<IEnumerable<ResponsavelLegal>> GetAllAsync()
	{
		return await AppDbContext.ResponsaveisLegais
			.AsNoTracking()
			.Include(responsavel => responsavel.Paciente)
			.OrderBy(responsavel => responsavel.NomeCompleto)
			.ToListAsync();
	}

	public async Task<ResponsavelLegal?> GetByIdAsync(int id)
	{
		return await AppDbContext.ResponsaveisLegais
			.AsNoTracking()
			.Include(responsavel => responsavel.Paciente)
			.FirstOrDefaultAsync(responsavel => responsavel.Id == id);
	}

	public async Task<IEnumerable<ResponsavelLegal>> GetByPacienteIdAsync(Guid pacienteId)
	{
		return await AppDbContext.ResponsaveisLegais
			.AsNoTracking()
			.Where(responsavel => responsavel.PacienteId == pacienteId)
			.Include(responsavel => responsavel.Paciente)
			.OrderByDescending(responsavel => responsavel.ResponsavelPrincipal)
			.ThenBy(responsavel => responsavel.NomeCompleto)
			.ToListAsync();
	}

	public async Task<ResponsavelLegal?> GetResponsavelPrincipalByPacienteIdAsync(Guid pacienteId)
	{
		return await AppDbContext.ResponsaveisLegais
			.AsNoTracking()
			.Include(responsavel => responsavel.Paciente)
			.FirstOrDefaultAsync(responsavel => responsavel.PacienteId == pacienteId && responsavel.ResponsavelPrincipal);
	}

	public async Task<ResponsavelLegal> AddAsync(ResponsavelLegal responsavelLegal)
	{
		responsavelLegal.DataCriacao = DateTime.UtcNow;
		responsavelLegal.Ativo = true;

		await AppDbContext.ResponsaveisLegais.AddAsync(responsavelLegal);
		await AppDbContext.SaveChangesAsync();

		return responsavelLegal;
	}

	public async Task<ResponsavelLegal?> UpdateAsync(ResponsavelLegal responsavelLegal)
	{
		var existente = await AppDbContext.ResponsaveisLegais
			.FirstOrDefaultAsync(item => item.Id == responsavelLegal.Id && item.Ativo);

		if (existente is null)
		{
			return null;
		}

		var dataCriacaoOriginal = existente.DataCriacao;

		AppDbContext.Entry(existente).CurrentValues.SetValues(responsavelLegal);
		existente.DataCriacao = dataCriacaoOriginal;
		existente.DataAtualizacao = DateTime.UtcNow;

		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var linhasAfetadas = await AppDbContext.ResponsaveisLegais
			.Where(responsavel => responsavel.Id == id)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
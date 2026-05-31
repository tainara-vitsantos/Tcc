using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class TratamentoAnteriorPacienteRepository(ApplicationDbContext AppDbContext) : ITratamentoAnteriorPacienteRepository
{
	public async Task<IEnumerable<TratamentoAnteriorModel>> GetAllAsync()
	{
		return await AppDbContext.TratamentosAnterioresPaciente
			.AsNoTracking()
			.Include(tratamentoAnterior => tratamentoAnterior.Paciente)
			.OrderByDescending(tratamentoAnterior => tratamentoAnterior.Id)
			.ToListAsync();
	}

	public async Task<TratamentoAnteriorModel?> GetByIdAsync(int id)
	{
		return await AppDbContext.TratamentosAnterioresPaciente
			.AsNoTracking()
			.Include(tratamentoAnterior => tratamentoAnterior.Paciente)
			.FirstOrDefaultAsync(tratamentoAnterior => tratamentoAnterior.Id == id);
	}

	public async Task<IEnumerable<TratamentoAnteriorModel>> GetByPacienteIdAsync(int pacienteId)
	{
		return await AppDbContext.TratamentosAnterioresPaciente
			.AsNoTracking()
			.Where(tratamentoAnterior => tratamentoAnterior.PacienteId == pacienteId)
			.Include(tratamentoAnterior => tratamentoAnterior.Paciente)
			.OrderByDescending(tratamentoAnterior => tratamentoAnterior.Id)
			.ToListAsync();
	}

	public async Task<IEnumerable<TratamentoAnteriorModel>> GetInternacoesByPacienteIdAsync(int pacienteId)
	{
		return await AppDbContext.TratamentosAnterioresPaciente
			.AsNoTracking()
			.Where(tratamentoAnterior =>
				tratamentoAnterior.PacienteId == pacienteId &&
				!string.IsNullOrWhiteSpace(tratamentoAnterior.MotivoInternacao))
			.Include(tratamentoAnterior => tratamentoAnterior.Paciente)
			.OrderByDescending(tratamentoAnterior => tratamentoAnterior.Id)
			.ToListAsync();
	}

	public async Task<TratamentoAnteriorModel> AddAsync(TratamentoAnteriorModel tratamentoAnterior)
	{
		tratamentoAnterior.DataCriacao = DateTime.UtcNow;
		tratamentoAnterior.Ativo = true;

		await AppDbContext.TratamentosAnterioresPaciente.AddAsync(tratamentoAnterior);
		await AppDbContext.SaveChangesAsync();

		return tratamentoAnterior;
	}

	public async Task<TratamentoAnteriorModel?> UpdateAsync(TratamentoAnteriorModel tratamentoAnterior)
	{
		var existente = await AppDbContext.TratamentosAnterioresPaciente
			.FirstOrDefaultAsync(item => item.Id == tratamentoAnterior.Id && item.Ativo);

		if (existente is null)
		{
			return null;
		}

		var dataCriacaoOriginal = existente.DataCriacao;

		AppDbContext.Entry(existente).CurrentValues.SetValues(tratamentoAnterior);
		existente.DataCriacao = dataCriacaoOriginal;
		existente.DataAtualizacao = DateTime.UtcNow;

		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var linhasAfetadas = await AppDbContext.TratamentosAnterioresPaciente
			.Where(tratamentoAnterior => tratamentoAnterior.Id == id)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class PacienteRepository(ApplicationDbContext context) : IPacienteRepository
{
	public async Task<IEnumerable<Paciente>> GetAllAsync()
	{
		return await context.Pacientes
			.AsNoTracking()
			.Where(paciente => paciente.Ativo)
			.ToListAsync();
	}

	public async Task<Paciente?> GetByIdAsync(Guid id)
	{
		return await context.Pacientes
			.AsNoTracking()
			.FirstOrDefaultAsync(paciente => paciente.Id == id && paciente.Ativo);
	}

	public async Task<Paciente?> GetByIdWithDetailsAsync(Guid id)
	{
		return await context.Pacientes
			.AsNoTracking()
			.Include(paciente => paciente.Prontuario)
			.Include(paciente => paciente.TratamentosAnteriores)
			.FirstOrDefaultAsync(paciente => paciente.Id == id && paciente.Ativo);
	}

	public async Task<Paciente> CreateAsync(Paciente paciente)
	{
		paciente.DataCriacao = DateTime.UtcNow;
		paciente.Ativo = true;

		context.Pacientes.Add(paciente);
		await context.SaveChangesAsync();

		return paciente;
	}

	public async Task<Paciente?> UpdateAsync(Paciente paciente)
	{
		var existente = await context.Pacientes
			.FirstOrDefaultAsync(item => item.Id == paciente.Id && item.Ativo);

		if (existente is null)
		{
			return null;
		}

		context.Entry(existente).CurrentValues.SetValues(paciente);
		existente.DataCriacao = paciente.DataCriacao;
		existente.DataAtualizacao = DateTime.UtcNow;

		await context.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(Guid id)
	{
		var paciente = await context.Pacientes
			.FirstOrDefaultAsync(item => item.Id == id && item.Ativo);

		if (paciente is null)
		{
			return false;
		}

		paciente.Ativo = false;
		paciente.DataAtualizacao = DateTime.UtcNow;

		await context.SaveChangesAsync();

		return true;
	}
}
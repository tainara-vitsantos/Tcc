
using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class ProntuarioRepository(ApplicationDbContext context) : IProntuarioRepository
{
	public async Task<IEnumerable<Prontuario>> GetAllAsync()
	{
		return await context.Prontuarios
			.AsNoTracking()
			.Where(prontuario => prontuario.SituacaoProntuario != SituacaoProntuario.InativoDesligado)
			.OrderBy(prontuario => prontuario.NumeroProntuario)
			.ToListAsync();
	}

	public async Task<Prontuario?> GetByIdAsync(int id)
	{
		return await context.Prontuarios
			.AsNoTracking()
			.FirstOrDefaultAsync(prontuario => prontuario.Id == id && prontuario.SituacaoProntuario != SituacaoProntuario.InativoDesligado);
	}

	public async Task<Prontuario?> GetByPacienteIdAsync(Guid pacienteId)
	{
		return await context.Prontuarios
			.AsNoTracking()
			.FirstOrDefaultAsync(prontuario => prontuario.PacienteId == pacienteId && prontuario.SituacaoProntuario != SituacaoProntuario.InativoDesligado);
	}

	public async Task<Prontuario?> GetWithDetailsByIdAsync(int id)
	{
		return await context.Prontuarios
			.AsNoTracking()
			.Include(prontuario => prontuario.Paciente)
			.Include(prontuario => prontuario.Atendimentos)
			.FirstOrDefaultAsync(prontuario => prontuario.Id == id && prontuario.SituacaoProntuario != SituacaoProntuario.InativoDesligado);
	}

	public async Task<Prontuario> CreateAsync(Prontuario prontuario)
	{
		prontuario.DataCriacao = DateTime.UtcNow;

		context.Prontuarios.Add(prontuario);
		await context.SaveChangesAsync();

		return prontuario;
	}

	public async Task<Prontuario?> UpdateAsync(Prontuario prontuario)
	{
		var existente = await context.Prontuarios
			.FirstOrDefaultAsync(item => item.Id == prontuario.Id && item.SituacaoProntuario != SituacaoProntuario.InativoDesligado);

		if (existente is null)
		{
			return null;
		}

		context.Entry(existente).CurrentValues.SetValues(prontuario);
		existente.DataCriacao = prontuario.DataCriacao;
		existente.DataAtualizacao = DateTime.UtcNow;

		await context.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var prontuario = await context.Prontuarios
			.FirstOrDefaultAsync(item => item.Id == id && item.SituacaoProntuario != SituacaoProntuario.InativoDesligado);

		if (prontuario is null)
		{
			return false;
		}

		prontuario.SituacaoProntuario = SituacaoProntuario.InativoDesligado;
		prontuario.DataAtualizacao = DateTime.UtcNow;

		await context.SaveChangesAsync();

		return true;
	}
}
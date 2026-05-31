
using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class ProntuarioRepository(ApplicationDbContext context) : IProntuarioRepository
{
	public async Task<IEnumerable<ProntuarioModel>> GetAllAsync()
	{
		return await context.Prontuarios
			.AsNoTracking()
			.Where(prontuario => prontuario.SituacaoProntuario != SituacaoProntuarioEnum.InativoDesligado)
			.OrderBy(prontuario => prontuario.NumeroProntuario)
			.ToListAsync();
	}

	public async Task<ProntuarioModel?> GetByIdAsync(int id)
	{
		return await context.Prontuarios
			.AsNoTracking()
			.FirstOrDefaultAsync(prontuario => prontuario.Id == id && prontuario.SituacaoProntuario != SituacaoProntuarioEnum.InativoDesligado);
	}

	public async Task<ProntuarioModel?> GetByPacienteIdAsync(int pacienteId)
	{
		return await context.Prontuarios
			.AsNoTracking()
			.FirstOrDefaultAsync(prontuario => prontuario.PacienteId == pacienteId && prontuario.SituacaoProntuario != SituacaoProntuarioEnum.InativoDesligado);
	}

	public async Task<ProntuarioModel?> GetWithDetailsByIdAsync(int id)
	{
		return await context.Prontuarios
			.AsNoTracking()
			.Include(prontuario => prontuario.Paciente)
			.Include(prontuario => prontuario.Atendimentos)
			.FirstOrDefaultAsync(prontuario => prontuario.Id == id && prontuario.SituacaoProntuario != SituacaoProntuarioEnum.InativoDesligado);
	}

	public async Task<ProntuarioModel> CreateAsync(ProntuarioModel prontuario)
	{
		prontuario.DataCriacao = DateTime.UtcNow;

		context.Prontuarios.Add(prontuario);
		await context.SaveChangesAsync();

		return prontuario;
	}

	public async Task<ProntuarioModel?> UpdateAsync(ProntuarioModel prontuario)
	{
		var existente = await context.Prontuarios
			.FirstOrDefaultAsync(item => item.Id == prontuario.Id && item.SituacaoProntuario != SituacaoProntuarioEnum.InativoDesligado);

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
			.FirstOrDefaultAsync(item => item.Id == id && item.SituacaoProntuario != SituacaoProntuarioEnum.InativoDesligado);

		if (prontuario is null)
		{
			return false;
		}

		prontuario.SituacaoProntuario = SituacaoProntuarioEnum.InativoDesligado;
		prontuario.DataAtualizacao = DateTime.UtcNow;

		await context.SaveChangesAsync();

		return true;
	}
}
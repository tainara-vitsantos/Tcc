using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class AtendimentoRepository(ApplicationDbContext AppDbContext) : IAtendimentoRepository
{
	public async Task<IEnumerable<Atendimento>> GetAllAsync()
	{
		return await AppDbContext.Atendimentos
			.AsNoTracking()
			.Include(atendimento => atendimento.Prontuario)
			.Include(atendimento => atendimento.Paciente)
			.Include(atendimento => atendimento.Aluno)
			.Include(atendimento => atendimento.Supervisor)
			.OrderByDescending(atendimento => atendimento.DataHoraInicio)
			.ToListAsync();
	}

	public async Task<Atendimento?> GetByIdAsync(int id)
	{
		return await AppDbContext.Atendimentos
			.AsNoTracking()
			.Include(atendimento => atendimento.Prontuario)
			.Include(atendimento => atendimento.Paciente)
			.Include(atendimento => atendimento.Aluno)
			.Include(atendimento => atendimento.Supervisor)
			.FirstOrDefaultAsync(atendimento => atendimento.Id == id);
	}

	public async Task<IEnumerable<Atendimento>> GetByAlunoIdAsync(string alunoId)
	{
		return await AppDbContext.Atendimentos
			.AsNoTracking()
			.Where(atendimento => atendimento.AlunoId == alunoId)
			.Include(atendimento => atendimento.Prontuario)
			.Include(atendimento => atendimento.Paciente)
			.Include(atendimento => atendimento.Aluno)
			.Include(atendimento => atendimento.Supervisor)
			.OrderByDescending(atendimento => atendimento.DataHoraInicio)
			.ToListAsync();
	}

	public async Task<IEnumerable<Atendimento>> GetByPacienteIdAsync(Guid pacienteId)
	{
		return await AppDbContext.Atendimentos
			.AsNoTracking()
			.Where(atendimento => atendimento.PacienteId == pacienteId)
			.Include(atendimento => atendimento.Prontuario)
			.Include(atendimento => atendimento.Paciente)
			.Include(atendimento => atendimento.Aluno)
			.Include(atendimento => atendimento.Supervisor)
			.OrderByDescending(atendimento => atendimento.DataHoraInicio)
			.ToListAsync();
	}

	public async Task<IEnumerable<Atendimento>> GetByStatusAsync(StatusAtendimentoEnum status)
	{
		return await AppDbContext.Atendimentos
			.AsNoTracking()
			.Where(atendimento => atendimento.StatusAtendimento == status)
			.Include(atendimento => atendimento.Prontuario)
			.Include(atendimento => atendimento.Paciente)
			.Include(atendimento => atendimento.Aluno)
			.Include(atendimento => atendimento.Supervisor)
			.OrderByDescending(atendimento => atendimento.DataHoraInicio)
			.ToListAsync();
	}

	public async Task<Atendimento> AddAsync(Atendimento atendimento)
	{
		atendimento.DataCriacao = DateTime.UtcNow;
		atendimento.Ativo = true;

		await AppDbContext.Atendimentos.AddAsync(atendimento);
		await AppDbContext.SaveChangesAsync();

		return atendimento;
	}

	public async Task<Atendimento?> UpdateAsync(Atendimento atendimento)
	{
		var existente = await AppDbContext.Atendimentos
			.FirstOrDefaultAsync(item => item.Id == atendimento.Id);

		if (existente is null)
		{
			return null;
		}

		var dataCriacaoOriginal = existente.DataCriacao;

		AppDbContext.Entry(existente).CurrentValues.SetValues(atendimento);
		existente.DataCriacao = dataCriacaoOriginal;
		existente.DataAtualizacao = DateTime.UtcNow;

		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var linhasAfetadas = await AppDbContext.Atendimentos
			.Where(atendimento => atendimento.Id == id)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
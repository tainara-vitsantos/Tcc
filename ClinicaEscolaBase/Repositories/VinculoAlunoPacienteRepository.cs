using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class VinculoAlunoPacienteRepository(ApplicationDbContext AppDbContext) : IVinculoAlunoPacienteRepository
{
	public async Task<IEnumerable<VinculoAlunoPaciente>> GetAllAsync()
	{
		return await AppDbContext.VinculosAlunoPaciente
			.AsNoTracking()
			.Include(vinculo => vinculo.Paciente)
			.Include(vinculo => vinculo.Aluno)
			.Include(vinculo => vinculo.LiberadoPorUsuario)
			.Include(vinculo => vinculo.RevogadoPorUsuario)
			.OrderByDescending(vinculo => vinculo.DataLiberacao)
			.ToListAsync();
	}

	public async Task<VinculoAlunoPaciente?> GetByIdAsync(int id)
	{
		return await AppDbContext.VinculosAlunoPaciente
			.AsNoTracking()
			.Include(vinculo => vinculo.Paciente)
			.Include(vinculo => vinculo.Aluno)
			.Include(vinculo => vinculo.LiberadoPorUsuario)
			.Include(vinculo => vinculo.RevogadoPorUsuario)
			.FirstOrDefaultAsync(vinculo => vinculo.Id == id);
	}

	public async Task<IEnumerable<VinculoAlunoPaciente>> GetByAlunoIdAsync(string alunoId)
	{
		return await AppDbContext.VinculosAlunoPaciente
			.AsNoTracking()
			.Where(vinculo => vinculo.AlunoId == alunoId)
			.Include(vinculo => vinculo.Paciente)
			.Include(vinculo => vinculo.Aluno)
			.Include(vinculo => vinculo.LiberadoPorUsuario)
			.Include(vinculo => vinculo.RevogadoPorUsuario)
			.OrderByDescending(vinculo => vinculo.DataLiberacao)
			.ToListAsync();
	}

	public async Task<IEnumerable<VinculoAlunoPaciente>> GetByPacienteIdAsync(Guid pacienteId)
	{
		return await AppDbContext.VinculosAlunoPaciente
			.AsNoTracking()
			.Where(vinculo => vinculo.PacienteId == pacienteId)
			.Include(vinculo => vinculo.Paciente)
			.Include(vinculo => vinculo.Aluno)
			.Include(vinculo => vinculo.LiberadoPorUsuario)
			.Include(vinculo => vinculo.RevogadoPorUsuario)
			.OrderByDescending(vinculo => vinculo.DataLiberacao)
			.ToListAsync();
	}

	public async Task<VinculoAlunoPaciente?> GetVinculoAtivoAsync(string alunoId, Guid pacienteId)
	{
		return await AppDbContext.VinculosAlunoPaciente
			.AsNoTracking()
			.Include(vinculo => vinculo.Paciente)
			.Include(vinculo => vinculo.Aluno)
			.Include(vinculo => vinculo.LiberadoPorUsuario)
			.Include(vinculo => vinculo.RevogadoPorUsuario)
			.Where(vinculo =>
				vinculo.AlunoId == alunoId &&
				vinculo.PacienteId == pacienteId &&
				vinculo.StatusVinculo == StatusVinculo.Ativo &&
				vinculo.DataRevogacao == null)
			.OrderByDescending(vinculo => vinculo.DataLiberacao)
			.FirstOrDefaultAsync();
	}

	public async Task<VinculoAlunoPaciente> AddAsync(VinculoAlunoPaciente vinculo)
	{
		vinculo.DataCriacao = DateTime.UtcNow;
		vinculo.Ativo = true;

		await AppDbContext.VinculosAlunoPaciente.AddAsync(vinculo);
		await AppDbContext.SaveChangesAsync();

		return vinculo;
	}

	public async Task<VinculoAlunoPaciente?> UpdateAsync(VinculoAlunoPaciente vinculo)
	{
		var existente = await AppDbContext.VinculosAlunoPaciente
			.FirstOrDefaultAsync(item => item.Id == vinculo.Id && item.Ativo);

		if (existente is null)
		{
			return null;
		}

		var dataCriacaoOriginal = existente.DataCriacao;

		AppDbContext.Entry(existente).CurrentValues.SetValues(vinculo);
		existente.DataCriacao = dataCriacaoOriginal;
		existente.DataAtualizacao = DateTime.UtcNow;

		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var linhasAfetadas = await AppDbContext.VinculosAlunoPaciente
			.Where(vinculo => vinculo.Id == id)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
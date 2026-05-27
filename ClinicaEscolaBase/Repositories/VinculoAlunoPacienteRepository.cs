using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class VinculoAlunoPacienteRepository(ApplicationDbContext AppDbContext) : IVinculoAlunoPacienteRepository
{
	public async Task<IEnumerable<VinculoAlunoPacienteModel>> GetAllAsync()
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

	public async Task<VinculoAlunoPacienteModel?> GetByIdAsync(int id)
	{
		return await AppDbContext.VinculosAlunoPaciente
			.AsNoTracking()
			.Include(vinculo => vinculo.Paciente)
			.Include(vinculo => vinculo.Aluno)
			.Include(vinculo => vinculo.LiberadoPorUsuario)
			.Include(vinculo => vinculo.RevogadoPorUsuario)
			.FirstOrDefaultAsync(vinculo => vinculo.Id == id);
	}

	public async Task<IEnumerable<VinculoAlunoPacienteModel>> GetByAlunoIdAsync(string alunoId)
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

	public async Task<IEnumerable<VinculoAlunoPacienteModel>> GetByPacienteIdAsync(Guid pacienteId)
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

	public async Task<VinculoAlunoPacienteModel?> GetVinculoAtivoAsync(string alunoId, Guid pacienteId)
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
				vinculo.StatusVinculo == StatusVinculoEnum.Ativo &&
				vinculo.DataRevogacao == null)
			.OrderByDescending(vinculo => vinculo.DataLiberacao)
			.FirstOrDefaultAsync();
	}

	public async Task<VinculoAlunoPacienteModel> AddAsync(VinculoAlunoPacienteModel vinculo)
	{
		vinculo.DataCriacao = DateTime.UtcNow;
		vinculo.Ativo = true;

		await AppDbContext.VinculosAlunoPaciente.AddAsync(vinculo);
		await AppDbContext.SaveChangesAsync();

		return vinculo;
	}

	public async Task<VinculoAlunoPacienteModel?> UpdateAsync(VinculoAlunoPacienteModel vinculo)
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
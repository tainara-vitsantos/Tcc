using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class AuditoriaRepository(ApplicationDbContext AppDbContext) : IAuditoriaRepository
{
	public async Task<Auditoria> AddAsync(Auditoria auditoria)
	{
		auditoria.DataHora = DateTime.UtcNow;

		await AppDbContext.Auditorias.AddAsync(auditoria);
		await AppDbContext.SaveChangesAsync();

		return auditoria;
	}

	public async Task<Auditoria?> GetByIdAsync(int id)
	{
		return await AppDbContext.Auditorias
			.AsNoTracking()
			.Include(auditoria => auditoria.Usuario)
			.Include(auditoria => auditoria.Paciente)
			.Include(auditoria => auditoria.Prontuario)
			.FirstOrDefaultAsync(auditoria => auditoria.Id == id);
	}

	public async Task<IEnumerable<Auditoria>> GetByUsuarioIdAsync(string usuarioId)
	{
		return await AppDbContext.Auditorias
			.AsNoTracking()
			.Where(auditoria => auditoria.UsuarioId == usuarioId)
			.Include(auditoria => auditoria.Usuario)
			.Include(auditoria => auditoria.Paciente)
			.Include(auditoria => auditoria.Prontuario)
			.OrderByDescending(auditoria => auditoria.DataHora)
			.ToListAsync();
	}

	public async Task<IEnumerable<Auditoria>> GetByPeriodoAsync(DateTime inicio, DateTime fim)
	{
		return await AppDbContext.Auditorias
			.AsNoTracking()
			.Where(auditoria => auditoria.DataHora >= inicio && auditoria.DataHora <= fim)
			.Include(auditoria => auditoria.Usuario)
			.Include(auditoria => auditoria.Paciente)
			.Include(auditoria => auditoria.Prontuario)
			.OrderByDescending(auditoria => auditoria.DataHora)
			.ToListAsync();
	}

	public async Task<IEnumerable<Auditoria>> GetByEntidadeERegistroAsync(string entidade, string registroId)
	{
		return await AppDbContext.Auditorias
			.AsNoTracking()
			.Where(auditoria => auditoria.Entidade == entidade && auditoria.RegistroId == registroId)
			.Include(auditoria => auditoria.Usuario)
			.Include(auditoria => auditoria.Paciente)
			.Include(auditoria => auditoria.Prontuario)
			.OrderByDescending(auditoria => auditoria.DataHora)
			.ToListAsync();
	}
}
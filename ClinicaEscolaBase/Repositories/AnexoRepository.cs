using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class AnexoRepository(ApplicationDbContext AppDbContext) : IAnexoRepository
{
	public async Task<IEnumerable<Anexo>> GetAllAsync()
	{
		return await AppDbContext.Anexos
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<Anexo?> GetByIdAsync(int id)
	{
		return await AppDbContext.Anexos
			.AsNoTracking()
			.FirstOrDefaultAsync(anexo => anexo.Id == id);
	}

	public async Task<IEnumerable<Anexo>> GetByDocumentoClinicoIdAsync(int documentoClinicoId)
	{
		return await AppDbContext.Anexos
			.AsNoTracking()
			.Where(anexo => anexo.DocumentoClinicoId == documentoClinicoId)
			.OrderBy(anexo => anexo.NomeOriginal)
			.ToListAsync();
	}

	public async Task<Anexo> AddAsync(Anexo anexo)
	{
		anexo.DataUpload = DateTime.UtcNow;

		AppDbContext.Anexos.Add(anexo);
		await AppDbContext.SaveChangesAsync();

		return anexo;
	}

	public async Task<Anexo?> UpdateAsync(Anexo anexo)
	{
		var existente = await AppDbContext.Anexos
			.FirstOrDefaultAsync(item => item.Id == anexo.Id);

		if (existente is null)
		{
			return null;
		}

		var dataUploadOriginal = existente.DataUpload;

		AppDbContext.Entry(existente).CurrentValues.SetValues(anexo);
		existente.DataUpload = dataUploadOriginal;

		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var linhasAfetadas = await AppDbContext.Anexos
			.Where(anexo => anexo.Id == id)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
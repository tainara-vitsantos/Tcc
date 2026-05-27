using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class DocumentoClinicoRepository(ApplicationDbContext AppDbContext) : IDocumentoClinicoRepository
{
	public async Task<IEnumerable<DocumentoClinico>> GetAllAtivosAsync()
	{
		return await AppDbContext.DocumentosClinicos
			.AsNoTracking()
			.Where(documento => !documento.ExcluidoLogicamente)
			.Include(documento => documento.Prontuario)
			.Include(documento => documento.Paciente)
			.Include(documento => documento.Atendimento)
			.Include(documento => documento.CriadoPorUsuario)
			.Include(documento => documento.Supervisor)
			.OrderByDescending(documento => documento.DataDocumento)
			.ToListAsync();
	}

	public async Task<DocumentoClinico?> GetByIdAsync(int id)
	{
		return await AppDbContext.DocumentosClinicos
			.AsNoTracking()
			.Include(documento => documento.Prontuario)
			.Include(documento => documento.Paciente)
			.Include(documento => documento.Atendimento)
			.Include(documento => documento.CriadoPorUsuario)
			.Include(documento => documento.Supervisor)
			.FirstOrDefaultAsync(documento => documento.Id == id && !documento.ExcluidoLogicamente);
	}

	public async Task<IEnumerable<DocumentoClinico>> GetByProntuarioIdAsync(int prontuarioId)
	{
		return await AppDbContext.DocumentosClinicos
			.AsNoTracking()
			.Where(documento => documento.ProntuarioId == prontuarioId && !documento.ExcluidoLogicamente)
			.Include(documento => documento.Prontuario)
			.Include(documento => documento.Paciente)
			.Include(documento => documento.Atendimento)
			.Include(documento => documento.CriadoPorUsuario)
			.Include(documento => documento.Supervisor)
			.OrderByDescending(documento => documento.DataDocumento)
			.ToListAsync();
	}

	public async Task<IEnumerable<DocumentoClinico>> GetByStatusAsync(StatusDocumentoClinico status)
	{
		return await AppDbContext.DocumentosClinicos
			.AsNoTracking()
			.Where(documento => documento.StatusDocumento == status && !documento.ExcluidoLogicamente)
			.Include(documento => documento.Prontuario)
			.Include(documento => documento.Paciente)
			.Include(documento => documento.Atendimento)
			.Include(documento => documento.CriadoPorUsuario)
			.Include(documento => documento.Supervisor)
			.OrderByDescending(documento => documento.DataDocumento)
			.ToListAsync();
	}

	public async Task<DocumentoClinico> AddAsync(DocumentoClinico documento)
	{
		documento.DataCriacao = DateTime.UtcNow;
		documento.DataDocumento = documento.DataDocumento == default ? DateTime.UtcNow : documento.DataDocumento;
		documento.ExcluidoLogicamente = false;
		documento.Ativo = true;

		await AppDbContext.DocumentosClinicos.AddAsync(documento);
		await AppDbContext.SaveChangesAsync();

		return documento;
	}

	public async Task<DocumentoClinico?> UpdateAsync(DocumentoClinico documento)
	{
		var existente = await AppDbContext.DocumentosClinicos
			.FirstOrDefaultAsync(item => item.Id == documento.Id && !item.ExcluidoLogicamente);

		if (existente is null)
		{
			return null;
		}

		var dataCriacaoOriginal = existente.DataCriacao;
		var dataDocumentoOriginal = existente.DataDocumento;
		var excluidoLogicamenteOriginal = existente.ExcluidoLogicamente;

		AppDbContext.Entry(existente).CurrentValues.SetValues(documento);
		existente.DataCriacao = dataCriacaoOriginal;
		existente.DataDocumento = dataDocumentoOriginal;
		existente.ExcluidoLogicamente = excluidoLogicamenteOriginal;
		existente.DataAtualizacao = DateTime.UtcNow;

		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var documento = await AppDbContext.DocumentosClinicos
			.FirstOrDefaultAsync(item => item.Id == id && !item.ExcluidoLogicamente);

		if (documento is null)
		{
			return false;
		}

		documento.ExcluidoLogicamente = true;
		documento.DataAtualizacao = DateTime.UtcNow;

		await AppDbContext.SaveChangesAsync();

		return true;
	}
}
using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class DocIdentificacaoPacienteRepository(ApplicationDbContext AppDbContext) : IDocIdentificacaoPacienteRepository
{
	public async Task<IEnumerable<DocumentoIdentificacaoPaciente>> GetAllAsync()
	{
		return await AppDbContext.DocumentosIdentificacaoPaciente
			.AsNoTracking()
			.ToListAsync();
	}

	public async Task<DocumentoIdentificacaoPaciente?> GetByIdAsync(int documentoClinicoId)
	{
		return await AppDbContext.DocumentosIdentificacaoPaciente
			.AsNoTracking()
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == documentoClinicoId);
	}

	public async Task<DocumentoIdentificacaoPaciente> AddAsync(DocumentoIdentificacaoPaciente docIdentificacao)
	{
		await AppDbContext.DocumentosIdentificacaoPaciente.AddAsync(docIdentificacao);
		await AppDbContext.SaveChangesAsync();

		return docIdentificacao;
	}

	public async Task<DocumentoIdentificacaoPaciente?> UpdateAsync(DocumentoIdentificacaoPaciente docIdentificacao)
	{
		var existente = await AppDbContext.DocumentosIdentificacaoPaciente
			.FirstOrDefaultAsync(item => item.DocumentoClinicoId == docIdentificacao.DocumentoClinicoId);

		if (existente is null)
		{
			return null;
		}

		AppDbContext.Entry(existente).CurrentValues.SetValues(docIdentificacao);
		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int documentoClinicoId)
	{
		var linhasAfetadas = await AppDbContext.DocumentosIdentificacaoPaciente
			.Where(item => item.DocumentoClinicoId == documentoClinicoId)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
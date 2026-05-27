using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class EvolucaoAtendimentoRepository(ApplicationDbContext AppDbContext) : IEvolucaoAtendimentoRepository
{
	public async Task<IEnumerable<EvolucaoAtendimento>> GetAllAsync()
	{
		return await AppDbContext.EvolucoesAtendimento
			.AsNoTracking()
			.Include(evolucao => evolucao.DocumentoClinico)
			.Include(evolucao => evolucao.Atendimento)
			.Include(evolucao => evolucao.CriadoPorUsuario)
			.Include(evolucao => evolucao.Supervisor)
			.OrderByDescending(evolucao => evolucao.DataEvolucao)
			.ToListAsync();
	}

	public async Task<EvolucaoAtendimento?> GetByIdAsync(int id)
	{
		return await AppDbContext.EvolucoesAtendimento
			.AsNoTracking()
			.Include(evolucao => evolucao.DocumentoClinico)
			.Include(evolucao => evolucao.Atendimento)
			.Include(evolucao => evolucao.CriadoPorUsuario)
			.Include(evolucao => evolucao.Supervisor)
			.FirstOrDefaultAsync(evolucao => evolucao.Id == id);
	}

	public async Task<IEnumerable<EvolucaoAtendimento>> GetByDocumentoClinicoIdAsync(int documentoClinicoId)
	{
		return await AppDbContext.EvolucoesAtendimento
			.AsNoTracking()
			.Where(evolucao => evolucao.DocumentoClinicoId == documentoClinicoId)
			.Include(evolucao => evolucao.DocumentoClinico)
			.Include(evolucao => evolucao.Atendimento)
			.Include(evolucao => evolucao.CriadoPorUsuario)
			.Include(evolucao => evolucao.Supervisor)
			.OrderByDescending(evolucao => evolucao.DataEvolucao)
			.ToListAsync();
	}

	public async Task<IEnumerable<EvolucaoAtendimento>> GetByAtendimentoIdAsync(int atendimentoId)
	{
		return await AppDbContext.EvolucoesAtendimento
			.AsNoTracking()
			.Where(evolucao => evolucao.AtendimentoId == atendimentoId)
			.Include(evolucao => evolucao.DocumentoClinico)
			.Include(evolucao => evolucao.Atendimento)
			.Include(evolucao => evolucao.CriadoPorUsuario)
			.Include(evolucao => evolucao.Supervisor)
			.OrderByDescending(evolucao => evolucao.DataEvolucao)
			.ToListAsync();
	}

	public async Task<EvolucaoAtendimento> AddAsync(EvolucaoAtendimento evolucao)
	{
		evolucao.DataCriacao = DateTime.UtcNow;
		evolucao.DataEvolucao = evolucao.DataEvolucao == default ? DateTime.UtcNow : evolucao.DataEvolucao;
		evolucao.Ativo = true;

		await AppDbContext.EvolucoesAtendimento.AddAsync(evolucao);
		await AppDbContext.SaveChangesAsync();

		return evolucao;
	}

	public async Task<EvolucaoAtendimento?> UpdateAsync(EvolucaoAtendimento evolucao)
	{
		var existente = await AppDbContext.EvolucoesAtendimento
			.FirstOrDefaultAsync(item => item.Id == evolucao.Id);

		if (existente is null)
		{
			return null;
		}

		var dataCriacaoOriginal = existente.DataCriacao;

		AppDbContext.Entry(existente).CurrentValues.SetValues(evolucao);
		existente.DataCriacao = dataCriacaoOriginal;
		existente.DataAtualizacao = DateTime.UtcNow;

		await AppDbContext.SaveChangesAsync();

		return existente;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var linhasAfetadas = await AppDbContext.EvolucoesAtendimento
			.Where(evolucao => evolucao.Id == id)
			.ExecuteDeleteAsync();

		return linhasAfetadas > 0;
	}
}
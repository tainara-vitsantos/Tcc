using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Repositories;

public class EvolucaoAtendimentoRepository(ApplicationDbContext AppDbContext) : IEvolucaoAtendimentoRepository
{
	public async Task<IEnumerable<EvolucaoAtendimentoModel>> GetAllAsync()
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

	public async Task<EvolucaoAtendimentoModel?> GetByIdAsync(int id)
	{
		return await AppDbContext.EvolucoesAtendimento
			.AsNoTracking()
			.Include(evolucao => evolucao.DocumentoClinico)
			.Include(evolucao => evolucao.Atendimento)
			.Include(evolucao => evolucao.CriadoPorUsuario)
			.Include(evolucao => evolucao.Supervisor)
			.FirstOrDefaultAsync(evolucao => evolucao.Id == id);
	}

	public async Task<IEnumerable<EvolucaoAtendimentoModel>> GetByDocumentoClinicoIdAsync(int documentoClinicoId)
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

	public async Task<IEnumerable<EvolucaoAtendimentoModel>> GetByAtendimentoIdAsync(int atendimentoId)
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

	public async Task<EvolucaoAtendimentoModel> AddAsync(EvolucaoAtendimentoModel evolucao)
	{
		evolucao.DataCriacao = DateTime.UtcNow;
		evolucao.DataEvolucao = evolucao.DataEvolucao == default ? DateTime.UtcNow : evolucao.DataEvolucao;
		evolucao.Ativo = true;

		await AppDbContext.EvolucoesAtendimento.AddAsync(evolucao);
		await AppDbContext.SaveChangesAsync();

		return evolucao;
	}

	public async Task<EvolucaoAtendimentoModel?> UpdateAsync(EvolucaoAtendimentoModel evolucao)
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
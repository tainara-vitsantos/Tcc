using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;

namespace ClinicaEscolaBase.Repositories;

public class EvolucaoAtendimentoRepository(ApplicationDbContext AppDbContext) : IEvolucaoAtendimentoRepository
{
    public Task<EvolucaoAtendimentoModel> AddAsync(EvolucaoAtendimentoModel evolucao)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<EvolucaoAtendimentoModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<EvolucaoAtendimentoModel>> GetByAtendimentoIdAsync(int atendimentoId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<EvolucaoAtendimentoModel>> GetByDocumentoClinicoIdAsync(int documentoClinicoId)
    {
        throw new NotImplementedException();
    }

    public Task<EvolucaoAtendimentoModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<EvolucaoAtendimentoModel?> UpdateAsync(EvolucaoAtendimentoModel evolucao)
    {
        throw new NotImplementedException();
    }
}
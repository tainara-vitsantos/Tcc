using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;

namespace ClinicaEscolaBase.Repositories;

public class TermoAutorizacaoMenorRepository(ApplicationDbContext AppDbContext) : ITermoAutorizacaoMenorRepository
{
    public Task<TermoAutorizacaoMenorModel> AddAsync(TermoAutorizacaoMenorModel termo)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int documentoClinicoId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TermoAutorizacaoMenorModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TermoAutorizacaoMenorModel?> GetByIdAsync(int documentoClinicoId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TermoAutorizacaoMenorModel>> GetByResponsavelLegalIdAsync(int responsavelLegalId)
    {
        throw new NotImplementedException();
    }

    public Task<TermoAutorizacaoMenorModel?> UpdateAsync(TermoAutorizacaoMenorModel termo)
    {
        throw new NotImplementedException();
    }
}
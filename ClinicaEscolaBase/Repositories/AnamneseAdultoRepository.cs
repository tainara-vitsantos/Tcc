using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;

namespace ClinicaEscolaBase.Repositories;

public class AnamneseAdultoRepository(ApplicationDbContext AppDbContext) : IAnamneseAdultoRepository
{
    public Task<AnamneseAdultoModel> AddAsync(AnamneseAdultoModel anamneseAdulto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int documentoClinicoId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AnamneseAdultoModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<AnamneseAdultoModel> GetByIdAsync(int documentoClinicoId)
    {
        throw new NotImplementedException();
    }

    public Task<AnamneseAdultoModel> UpdateAsync(AnamneseAdultoModel anamneseAdulto)
    {
        throw new NotImplementedException();
    }
}
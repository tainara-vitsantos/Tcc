using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;

namespace ClinicaEscolaBase.Repositories;

public class AuditoriaRepository(ApplicationDbContext AppDbContext) : IAuditoriaRepository
{
	
    public Task<AuditoriaModel> AddAsync(AuditoriaModel auditoria)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AuditoriaModel>> GetByEntidadeERegistroAsync(string entidade, string registroId)
    {
        throw new NotImplementedException();
    }

    public Task<AuditoriaModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AuditoriaModel>> GetByPeriodoAsync(DateTime inicio, DateTime fim)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AuditoriaModel>> GetByUsuarioIdAsync(string usuarioId)
    {
        throw new NotImplementedException();
    }
}
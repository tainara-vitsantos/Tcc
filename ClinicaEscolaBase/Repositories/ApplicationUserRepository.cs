using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;

namespace ClinicaEscolaBase.Repositories;

public class ApplicationUserRepository(ApplicationDbContext AppDbContext) : IApplicationUserRepository
{
    public Task<ApplicationUserModel> AddAsync(ApplicationUserModel usuario)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ApplicationUserModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUserModel> GetByCpfAsync(string cpf)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUserModel> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ApplicationUserModel>> GetByTipoUsuarioAsync(TipoUsuarioEnum tipoUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUserModel> UpdateAsync(ApplicationUserModel usuario)
    {
        throw new NotImplementedException();
    }
}
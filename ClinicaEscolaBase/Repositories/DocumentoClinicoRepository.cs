using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Repositories.Interfaces;

namespace ClinicaEscolaBase.Repositories;

public class DocumentoClinicoRepository(ApplicationDbContext AppDbContext) : IDocumentoClinicoRepository
{
    public Task<DocumentoClinicoModel> AddAsync(DocumentoClinicoModel documento)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DocumentoClinicoModel>> GetAllAtivosAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DocumentoClinicoModel?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DocumentoClinicoModel>> GetByProntuarioIdAsync(int prontuarioId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DocumentoClinicoModel>> GetByStatusAsync(StatusDocumentoClinicoEnum status)
    {
        throw new NotImplementedException();
    }

    public Task<DocumentoClinicoModel?> UpdateAsync(DocumentoClinicoModel documento)
    {
        throw new NotImplementedException();
    }
}
using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Repositories.Interfaces;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Enums;

namespace ClinicaEscolaBase.Repositories;

public class AtendimentoRepository(ApplicationDbContext AppDbContext) : IAtendimentoRepository
{
	
    public Task<AtendimentoModel> AddAsync(AtendimentoModel atendimento)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AtendimentoModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AtendimentoModel>> GetByAlunoIdAsync(string alunoId)
    {
        throw new NotImplementedException();
    }

    public Task<AtendimentoModel> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AtendimentoModel>> GetByPacienteIdAsync(int pacienteId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AtendimentoModel>> GetByStatusAsync(StatusAtendimentoEnum status)
    {
        throw new NotImplementedException();
    }

    public Task<AtendimentoModel> UpdateAsync(AtendimentoModel atendimento)
    {
        throw new NotImplementedException();
    }
}
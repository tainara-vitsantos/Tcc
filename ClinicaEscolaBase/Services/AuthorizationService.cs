using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Services;

/// <summary>
/// Serviço para validar autorização de acesso aos pacientes.
/// Implementa a segurança acadêmica: Um Aluno só pode visualizar ou editar dados de um Paciente 
/// se houver um vínculo ativo e liberado.
/// </summary>
public class AuthorizationService
{
    private readonly ApplicationDbContext _context;

    public AuthorizationService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Verifica se um usuário (Aluno) tem permissão de leitura para um paciente.
    /// Professores sempre têm acesso. Alunos precisam de vínculo ativo e permissão.
    /// </summary>
    public async Task<bool> CanReadPacienteAsync(string usuarioId, Guid pacienteId)
    {
        // Verificar se é professor (acesso total)
        var usuario = await _context.Users.FindAsync(usuarioId);
        if (usuario == null) return false;

        // Buscar Role de Professor uma única vez
        var professorRoleId = await _context.Roles
            .Where(r => r.Name == "Professor")
            .Select(r => r.Id)
            .FirstOrDefaultAsync();

        var isProfessor = usuario.NomeCompleto.Contains("Professor") || 
            (professorRoleId != null && 
             await _context.UserRoles.AnyAsync(ur => 
                ur.UserId == usuarioId && ur.RoleId == professorRoleId));

        if (isProfessor)
            return true;

        // Para alunos, verificar vínculo ativo
        return await _context.VinculosAlunoPaciente
            .AnyAsync(v => 
                v.AlunoId == usuarioId && 
                v.PacienteId == pacienteId && 
                v.StatusVinculo == StatusVinculo.Ativo && 
                v.PermiteLeitura && 
                v.Ativo);
    }

    /// <summary>
    /// Verifica se um usuário (Aluno) tem permissão de escrita para um paciente.
    /// Professores sempre têm acesso. Alunos precisam de vínculo ativo e permissão.
    /// </summary>
    public async Task<bool> CanWritePacienteAsync(string usuarioId, Guid pacienteId)
    {
        // Verificar se é professor (acesso total)
        var usuario = await _context.Users.FindAsync(usuarioId);
        if (usuario == null) return false;

        // Buscar Role de Professor uma única vez
        var professorRoleId = await _context.Roles
            .Where(r => r.Name == "Professor")
            .Select(r => r.Id)
            .FirstOrDefaultAsync();

        var isProfessor = usuario.NomeCompleto.Contains("Professor") || 
            (professorRoleId != null && 
             await _context.UserRoles.AnyAsync(ur => 
                ur.UserId == usuarioId && ur.RoleId == professorRoleId));

        if (isProfessor)
            return true;

        // Para alunos, verificar vínculo ativo
        return await _context.VinculosAlunoPaciente
            .AnyAsync(v => 
                v.AlunoId == usuarioId && 
                v.PacienteId == pacienteId && 
                v.StatusVinculo == StatusVinculo.Ativo && 
                v.PermiteEscrita && 
                v.Ativo);
    }

    /// <summary>
    /// Retorna todos os pacientes que um usuário tem acesso.
    /// Professores veem todos. Alunos veem apenas vinculados.
    /// </summary>
    public async Task<List<Guid>> GetAcessiblePacienteIdsAsync(string usuarioId)
    {
        var usuario = await _context.Users.FindAsync(usuarioId);
        if (usuario == null) return new List<Guid>();

        // Buscar Role de Professor uma única vez
        var professorRoleId = await _context.Roles
            .Where(r => r.Name == "Professor")
            .Select(r => r.Id)
            .FirstOrDefaultAsync();

        // Verificar se é professor
        var isProfessor = usuario.NomeCompleto.Contains("Professor") || 
            (professorRoleId != null && 
             await _context.UserRoles.AnyAsync(ur => 
                ur.UserId == usuarioId && ur.RoleId == professorRoleId));

        if (isProfessor)
        {
            return await _context.Pacientes.Select(p => p.Id).ToListAsync();
        }

        // Alunos: retornar pacientes com vínculo ativo
        return await _context.VinculosAlunoPaciente
            .Where(v => 
                v.AlunoId == usuarioId && 
                v.StatusVinculo == StatusVinculo.Ativo && 
                v.Ativo)
            .Select(v => v.PacienteId)
            .ToListAsync();
    }
}

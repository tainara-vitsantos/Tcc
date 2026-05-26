using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Dtos;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Services;

/// <summary>
/// Serviço para validar autorização de acesso aos pacientes.
/// Implementa a segurança acadêmica: Um Aluno só pode visualizar ou editar dados de um Paciente 
/// se houver um vínculo ativo e liberado.
/// </summary>
public class AuthService(ApplicationDbContext context,
             UserManager<ApplicationUser> userManager, // Injetado para facilitar checagem de Roles/Rotas
             SignInManager<ApplicationUser> signInManager,
             ILogger<AuthService> logger
             ) : IAuthService
{

    private bool? _isProfessorCache; // Variável de estado para fazer cache do resultado durante o ciclo de vida (Scoped) da requisição
    /// <summary>
    /// Verifica se um usuário (Aluno) tem permissão de leitura para um paciente.
    /// Professores sempre têm acesso. Alunos precisam de vínculo ativo e permissão.
    /// </summary>
    public async Task<bool> CanReadPacienteAsync(string usuarioId, Guid pacienteId)
    {
        // Verificar se é professor (acesso total)
      if (await IsProfessorAsync(usuarioId)) 
            return true;

        // Para alunos, verifica vínculo ativo com permissão de leitura
        return await context.VinculosAlunoPaciente
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
      if (await IsProfessorAsync(usuarioId)) 
            return true;

        // Para alunos, verificar vínculo ativo
        return await context.VinculosAlunoPaciente
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
        // Verificar se é professor
       if (await IsProfessorAsync(usuarioId))
        {
            return await context.Pacientes.Select(p => p.Id).ToListAsync();
        }

        // Alunos: retornar pacientes com vínculo ativo
       return await context.VinculosAlunoPaciente
            .Where(v =>
                v.AlunoId == usuarioId &&
                v.StatusVinculo == StatusVinculo.Ativo &&
                v.Ativo)
            .Select(v => v.PacienteId)
            .ToListAsync();
    }
     public async Task Logout(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await signInManager.SignOutAsync();

        logger.LogInformation("Usuário realizou logout e a sessão foi encerrada.");
    }

public async Task<DashboardAlunoDto> GetDashboardAlunoAsync(string usuarioId)
{
    var inicioHoje = DateTime.Today;
    var fimHoje = inicioHoje.AddDays(1);

    // Reaproveita a lógica de IDs acessíveis que já criamos!
    var acessiblePacienteIds = await GetAcessiblePacienteIdsAsync(usuarioId);

        var dto = new DashboardAlunoDto
        {
            MeusPacientes = acessiblePacienteIds.Count,

            // Executa os contadores no banco
            MeusAtendimentosAgendados = await context.Atendimentos
                .CountAsync(a => a.AlunoId == usuarioId && a.StatusAtendimento == StatusAtendimento.Agendado),

            MeusAtendimentosRealizados = await context.Atendimentos
                .CountAsync(a => a.AlunoId == usuarioId && a.StatusAtendimento == StatusAtendimento.Realizado),

            MeusAtendimentosHoje = await context.Atendimentos
                .CountAsync(a => a.AlunoId == usuarioId && a.DataHoraInicio >= inicioHoje && a.DataHoraInicio < fimHoje),

            // Busca as listagens usando AsNoTracking
            MeusProximosAtendimentos = await context.Atendimentos
                .Include(a => a.Paciente)
                .Where(a => a.AlunoId == usuarioId && a.StatusAtendimento == StatusAtendimento.Agendado)
                .OrderBy(a => a.DataHoraInicio)
                .Take(10)
                .AsNoTracking()
                .ToListAsync(),

            MeusPacientesRecentes = await context.Atendimentos
                .Include(a => a.Paciente)
                .Where(a => a.AlunoId == usuarioId)
                .OrderByDescending(a => a.DataHoraInicio)
                .Take(5)
                .AsNoTracking()
                .ToListAsync()
        };

        return dto;
}

#region Metodos Privados
/// <summary>
    /// Verifica se o usuário é professor, utilizando cache local por requisição (Scoped)
    /// e uma consulta otimizada via JOIN implícito.
    /// </summary>
    private async Task<bool> IsProfessorAsync(string usuarioId)
    {
      if (_isProfessorCache.HasValue)
    {
        return _isProfessorCache.Value;
    }

    var usuario = await userManager.FindByIdAsync(usuarioId);
    if (usuario == null)
    {
        _isProfessorCache = false;
    }
    else
    {
        _isProfessorCache = await userManager.IsInRoleAsync(usuario, "Professor");
    }

    return _isProfessorCache.Value;
    }
#endregion
}

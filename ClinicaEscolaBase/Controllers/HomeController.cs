using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;
using ClinicaEscolaBase.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ClinicaEscolaBase.Controllers;

public class HomeController(
    ILogger<HomeController> logger,
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    AuthorizationService authorizationService) : Controller
{
    public async Task<IActionResult> Index()
    {
        // Se não está autenticado, mostra página pública
        if (!User.Identity?.IsAuthenticated ?? true)
        {
            return View("IndexPublico");
        }

        // Dashboard diferenciado por Role
        if (User.IsInRole("Professor"))
        {
            return await DashboardProfessor();
        }
        else if (User.IsInRole("Aluno"))
        {
            return await DashboardAluno();
        }

        // Fallback
        return View();
    }

    /// <summary>
    /// Dashboard do Professor: Visão global com estatísticas gerais.
    /// </summary>
    private async Task<IActionResult> DashboardProfessor()
    {
        // Estatísticas Globais
        var totalPacientes = await context.Pacientes.CountAsync();
        var totalProntuariosAtivos = await context.Prontuarios
            .CountAsync(p => p.SituacaoProntuario == SituacaoProntuario.Ativo);
        var atendimentosHoje = await context.Atendimentos
            .CountAsync(a => a.DataHoraInicio.Date == DateTime.Today);
        var atendimentosRealizados = await context.Atendimentos
            .CountAsync(a => a.StatusAtendimento == StatusAtendimento.Realizado);

        ViewBag.TotalPacientes = totalPacientes;
        ViewBag.TotalProntuariosAtivos = totalProntuariosAtivos;
        ViewBag.AtendimentosHoje = atendimentosHoje;
        ViewBag.AtendimentosRealizados = atendimentosRealizados;

        // Próximos atendimentos (global)
        var proximosAtendimentos = await context.Atendimentos
            .Include(a => a.Paciente)
            .Include(a => a.Aluno)
            .Where(a => a.StatusAtendimento == StatusAtendimento.Agendado)
            .OrderBy(a => a.DataHoraInicio)
            .Take(10)
            .ToListAsync();

        // Atividade recente de auditoria
        var auditoriasRecentes = await context.Auditorias
            .Include(a => a.Usuario)
            .Include(a => a.Paciente)
            .OrderByDescending(a => a.DataHora)
            .Take(20)
            .ToListAsync();

        ViewBag.AuditoriasRecentes = auditoriasRecentes;
        ViewBag.IsProfessor = true;

        return View("Index", proximosAtendimentos);
    }

    /// <summary>
    /// Dashboard do Aluno: Visão pessoal apenas de seus pacientes e atendimentos.
    /// </summary>
    private async Task<IActionResult> DashboardAluno()
    {
        var usuarioId = userManager.GetUserId(User);
        if (usuarioId == null) return Unauthorized();

        // Estatísticas pessoais do aluno
        var acessiblePacienteIds = await authorizationService.GetAcessiblePacienteIdsAsync(usuarioId);

        var meusPacientes = acessiblePacienteIds.Count;
        var meusAtendimentosAgendados = await context.Atendimentos
            .CountAsync(a => a.AlunoId == usuarioId && a.StatusAtendimento == StatusAtendimento.Agendado);
        var meusAtendimentosRealizados = await context.Atendimentos
            .CountAsync(a => a.AlunoId == usuarioId && a.StatusAtendimento == StatusAtendimento.Realizado);
        var meusAtendimentosHoje = await context.Atendimentos
            .CountAsync(a => a.AlunoId == usuarioId && a.DataHoraInicio.Date == DateTime.Today);

        ViewBag.MeusPacientes = meusPacientes;
        ViewBag.MeusAtendimentosAgendados = meusAtendimentosAgendados;
        ViewBag.MeusAtendimentosRealizados = meusAtendimentosRealizados;
        ViewBag.MeusAtendimentosHoje = meusAtendimentosHoje;

        // Meus próximos atendimentos
        var meusProximosAtendimentos = await context.Atendimentos
            .Include(a => a.Paciente)
            .Where(a => a.AlunoId == usuarioId && a.StatusAtendimento == StatusAtendimento.Agendado)
            .OrderBy(a => a.DataHoraInicio)
            .Take(10)
            .ToListAsync();

        // Meus pacientes recentemente atendidos
        var meusPacientesRecentes = await context.Atendimentos
            .Include(a => a.Paciente)
            .Where(a => a.AlunoId == usuarioId)
            .OrderByDescending(a => a.DataHoraInicio)
            .Take(5)
            .ToListAsync();

        ViewBag.MeusPacientesRecentes = meusPacientesRecentes;
        ViewBag.IsAluno = true;

        return View("Index", meusProximosAtendimentos);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
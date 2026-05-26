using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClinicaEscolaBase.Models;
using ClinicaEscolaBase.Data;
using ClinicaEscolaBase.Enums;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ClinicaEscolaBase.Services.Interfaces;
using ClinicaEscolaBase.Dtos;

namespace ClinicaEscolaBase.Controllers;

public class HomeController(
    ApplicationDbContext context,
    UserManager<ApplicationUser> userManager,
    IAuthService authorizationService) : Controller
{
    public async Task<IActionResult> Index()
    {
        // Se não está autenticado, mostra página pública
        if (User.Identity?.IsAuthenticated != true)
        {
            return View("Index");
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
// Otimização de data: define os limites do dia atual fora da query
        var inicioHoje = DateTime.Today;
        var fimHoje = inicioHoje.AddDays(1);

        // Estatísticas Globais
        var totalPacientes = await context.Pacientes.CountAsync();
        var totalProntuariosAtivos = await context.Prontuarios
            .CountAsync(p => p.SituacaoProntuario == SituacaoProntuario.Ativo);
        
        // Uso de intervalo `>=` e `<` aproveita os índices do banco de dados muito melhor do que .Date
        var atendimentosHoje = await context.Atendimentos
            .CountAsync(a => a.DataHoraInicio >= inicioHoje && a.DataHoraInicio < fimHoje);
            
        var atendimentosRealizados = await context.Atendimentos
            .CountAsync(a => a.StatusAtendimento == StatusAtendimento.Realizado);

        ViewBag.TotalPacientes = totalPacientes;
        ViewBag.TotalProntuariosAtivos = totalProntuariosAtivos;
        ViewBag.AtendimentosHoje = atendimentosHoje;
        ViewBag.AtendimentosRealizados = atendimentosRealizados;

        // Próximos atendimentos (global) - Traz apenas as colunas necessárias para a tabela da View
        var proximosAtendimentos = await context.Atendimentos
            .Include(a => a.Paciente)
            .Include(a => a.Aluno)
            .Where(a => a.StatusAtendimento == StatusAtendimento.Agendado)
            .OrderBy(a => a.DataHoraInicio)
            .Take(10)
            .AsNoTracking() // Melhora performance pois esses dados são apenas para exibição (leitura)
            .ToListAsync();

        // Atividade recente de auditoria
        var auditoriasRecentes = await context.Auditorias
            .Include(a => a.Usuario)
            .Include(a => a.Paciente)
            .OrderByDescending(a => a.DataHora)
            .Take(20)
            .AsNoTracking()
            .ToListAsync();

        ViewBag.AuditoriasRecentes = auditoriasRecentes;
        ViewBag.IsProfessor = true;

        return View("Dashboard", proximosAtendimentos);
    }

    /// <summary>
    /// Dashboard do Aluno: Visão pessoal apenas de seus pacientes e atendimentos.
    /// </summary>
    private async Task<IActionResult> DashboardAluno()
    {
    var usuarioId = userManager.GetUserId(User);
    if (usuarioId == null) return Unauthorized();

    // Uma única linha chama o serviço e resolve toda a dor de cabeça
    DashboardAlunoDto dadosDashboard = await authorizationService.GetDashboardAlunoAsync(usuarioId);
   
    ViewBag.IsAluno = true;

    // Passamos a lista de próximos atendimentos para a View, igualzinho estava antes
    return View("Dashboard", dadosDashboard);
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
     [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await authorizationService.Logout(cancellationToken);
        return RedirectToAction("Index");
    }    
}
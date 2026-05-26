using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClinicaEscolaBase.Models;

using Microsoft.AspNetCore.Identity;
using ClinicaEscolaBase.Services.Interfaces;
using ClinicaEscolaBase.Dtos;
using AutoMapper;
using ClinicaEscolaBase.ViewModels;

namespace ClinicaEscolaBase.Controllers;

public class HomeController(
    UserManager<ApplicationUser> userManager,
    IAuthService authorizationService,
    IMapper mapper) : Controller
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
// Chama o serviço para obter o DTO fortemente tipado
   DashboardProfessorDto dadosProfessor = await authorizationService.GetDashboardProfessorAsync();

    // O AutoMapper faz toda a mágica de envelopamento em uma única linha:
    var viewModel = mapper.Map<DashboardViewModel>(dadosProfessor);

    return View("Dashboard", viewModel);
    }

    /// <summary>
    /// Dashboard do Aluno: Visão pessoal apenas de seus pacientes e atendimentos.
    /// </summary>
    private async Task<IActionResult> DashboardAluno()
    {
    var usuarioId = userManager.GetUserId(User);
    if (usuarioId == null) return Unauthorized();

    DashboardAlunoDto dadosAluno = await authorizationService.GetDashboardAlunoAsync(usuarioId);

    // Mapeia o DTO do aluno diretamente para o ViewModel unificado:
    var viewModel = mapper.Map<DashboardViewModel>(dadosAluno);

    return View("Dashboard", viewModel);
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
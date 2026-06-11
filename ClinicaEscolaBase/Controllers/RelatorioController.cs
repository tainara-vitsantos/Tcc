using Microsoft.AspNetCore.Mvc;

namespace ClinicaEscolaBase.Controllers
{
    public class RelatorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Atendimentos()
        {
            return View();
        }

        public IActionResult Pacientes()
        {
            return View();
        }

        public IActionResult Prontuarios()
        {
            return View();
        }

        public IActionResult Academico()
        {
            return View();
        }

        public IActionResult Financeiro()
        {
            return View();
        }
    }
}
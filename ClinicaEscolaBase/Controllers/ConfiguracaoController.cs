using Microsoft.AspNetCore.Mvc;

namespace ClinicaEscolaBase.Controllers
{
    public class ConfiguracaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
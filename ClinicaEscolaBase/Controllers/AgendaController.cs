using Microsoft.AspNetCore.Mvc;

namespace ClinicaEscolaBase.Controllers
{
    public class AgendaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
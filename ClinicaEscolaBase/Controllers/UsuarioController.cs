using Microsoft.AspNetCore.Mvc;

namespace ClinicaEscolaBase.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
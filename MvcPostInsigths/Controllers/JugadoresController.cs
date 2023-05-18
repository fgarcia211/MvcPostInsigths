using Microsoft.AspNetCore.Mvc;

namespace MvcPostInsigths.Controllers
{
    public class JugadoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

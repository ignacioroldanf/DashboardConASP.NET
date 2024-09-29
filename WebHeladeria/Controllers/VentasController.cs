using Microsoft.AspNetCore.Mvc;

namespace WebHeladeria.Controllers
{
    public class VentasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

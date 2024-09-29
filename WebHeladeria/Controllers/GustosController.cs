using Microsoft.AspNetCore.Mvc;
using WebHeladeria.Models;

namespace WebHeladeria.Controllers
{
    public class GustosController : Controller
    {
        private readonly HeladeriaContext _context;

        public GustosController (HeladeriaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var gustos = _context.Gustos.ToList();
            return View(gustos);
        }
    }
}

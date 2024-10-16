using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHeladeria.Models;

namespace WebHeladeria.Controllers
{
    public class LocalidadesController : Controller
    {
        private readonly HeladeriaContext _context;

        public LocalidadesController(HeladeriaContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            // Obtener las sucursales de manera asíncrona
            var localidades = await _context.Localidades.ToListAsync();

            // Enviar los datos a la vista
            return View(localidades);
        }
    }
}

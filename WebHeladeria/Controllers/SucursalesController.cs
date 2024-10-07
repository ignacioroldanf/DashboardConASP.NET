using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHeladeria.Models;

namespace WebHeladeria.Controllers
{
    public class SucursalesController : Controller
    {
        private readonly HeladeriaContext _context;
        
        public SucursalesController(HeladeriaContext context)
        {
            _context = context;
        }
        
        
        
        public async Task<IActionResult> Index()
        {
            // Obtener las sucursales de manera asíncrona
            var sucursales = await _context.Sucursales.ToListAsync();

            // Enviar los datos a la vista
            return View(sucursales);
        }
    }
}

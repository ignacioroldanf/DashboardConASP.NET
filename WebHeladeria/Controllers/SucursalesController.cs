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

            var sucursales = _context.Sucursales.ToList(); // Obteniendo todas las sucursales
            return View(sucursales); // Enviando  datos a la vista


            //return View(await _context.Sucursales.ToListAsync());
        }
    }
}

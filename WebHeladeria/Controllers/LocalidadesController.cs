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
            var localidades = await _context.Localidades.ToListAsync();

            // Enviar los datos a la vista
            return View(localidades);
        }
        public IActionResult GustosPorLocalidad(int idLocalidad)
        {
            var localidad = _context.Localidades
                .Where(l => l.IdLocalidad == idLocalidad)
                .Select(l => new
                {
                    IdLocalidad = l.IdLocalidad,
                    NombreLocalidad = l.NombreLocalidad,
                    GustosMasVendidos = _context.VentasDetalles
                        .Where(v => v.IdVentaNavigation.IdSucursalNavigation.Localidad == l.IdLocalidad)
                        .GroupBy(v => v.IdGustoNavigation.NombreGusto)
                        .Select(g => g.Key) // solo el nombre del gusto
                        .OrderByDescending(g => g.Count()) // cuenta para ordenar
                        .Take(5)
                        .ToList()
                })
                .FirstOrDefault(); // Asegúrate de obtener solo una localidad


            return View(Index); // envía una lista con la localidad
        }


    }
}

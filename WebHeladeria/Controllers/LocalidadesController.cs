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
            int KilosObjetivo = 10;

            var localidad = _context.Localidades
                .Where(l => l.IdLocalidad == idLocalidad)
                .Select(l => new
                {
                    IdLocalidad = l.IdLocalidad,
                    NombreLocalidad = l.NombreLocalidad,
                    GustosMasVendidos = _context.VentasDetalles
                        .Where(v => v.IdVentaNavigation.IdSucursalNavigation.IdLocalidad == l.IdLocalidad)
                        .GroupBy(v => v.IdGustoNavigation.NombreGusto)
                        .Select(g => g.Key) 
                        .OrderByDescending(g => g.Count()) 
                        .Take(5)
                        .ToList(),

                    KilosVendidos = _context.VentasDetalles
                    .Where(v => v.IdVentaNavigation.IdSucursalNavigation.IdLocalidad == l.IdLocalidad)
                    .Sum(v => v.Cantidad),

                    EstadoObjetivo = _context.VentasDetalles
                    .Where(v => v.IdVentaNavigation.IdSucursalNavigation.IdLocalidad == l.IdLocalidad)
                    .Sum(v => v.Cantidad) > KilosObjetivo ? "Objetivo Completo" : "Objetivo Incompleto"
                })
                .FirstOrDefault(); 

            return View(Index); 
        }


    }
    }

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
            var localidades = await _context.Localidades
                .Select(l => new
                {
                    l.IdLocalidad,
                    l.NombreLocalidad,
                    GustosMasVendidos = _context.VentasDetalles
                        .Where(v => v.IdVentaNavigation.IdSucursalNavigation.IdLocalidad == l.IdLocalidad)
                        .GroupBy(v => v.IdGustoNavigation.NombreGusto)
                        .OrderByDescending(g => g.Sum(v => v.Cantidad))
                        .Select(g => g.Key)
                        .Take(5) //top 5
                        .ToList(),
                    KilosVendidos = _context.VentasDetalles
                        .Where(v => v.IdVentaNavigation.IdSucursalNavigation.IdLocalidad == l.IdLocalidad)
                        .Sum(v => v.Cantidad),
                    SemaforoEstado = _context.VentasDetalles
                        .Where(v => v.IdVentaNavigation.IdSucursalNavigation.IdLocalidad == l.IdLocalidad)
                        .Sum(v => v.Cantidad) >= 15 ? "verde" :
                            _context.VentasDetalles
                            .Where(v => v.IdVentaNavigation.IdSucursalNavigation.IdLocalidad == l.IdLocalidad)
                            .Sum(v => v.Cantidad) >= 10 ? "amarillo" : "rojo"
                })
                .ToListAsync();

            return View(localidades);
        }

    }
    }

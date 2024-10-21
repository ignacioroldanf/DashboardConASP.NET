using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHeladeria.Models;

public class SucursalesController : Controller
{
    private readonly HeladeriaContext _context;

    // Inyección de dependencias en el constructor
    public SucursalesController(HeladeriaContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IActionResult> Index()
    {
        // Obtener todas las sucursales de manera asíncrona
        var sucursales = await _context.Sucursales.ToListAsync();
        return View(sucursales);
    }

    public IActionResult SucursalesPorLocalidad(int idLocalidad)
    {
        // Obtener las sucursales filtradas por la localidad seleccionada
        var sucursales = _context.Sucursales
            .Where(s => s.Localidad == idLocalidad)
            .Select(s => new
            {
                Sucursal = s,
                GustosMasVendidos = _context.VentasDetalles
                    .Where(v => v.IdVentaNavigation.IdSucursal == s.IdSucursal)
                    .GroupBy(v => v.IdGustoNavigation.NombreGusto)
                    .Select(g => new
                    {
                        Gusto = g.Key,
                        CantidadVendida = g.Sum(v => v.Cantidad)
                    })
                    .OrderByDescending(g => g.CantidadVendida)
                    .Take(5) // Obtener el top 5 de gustos más vendidos
                    .ToList()
            })
            .ToList();

        // Obtener el nombre de la localidad para mostrar en la vista
        ViewBag.NombreLocalidad = _context.Localidades
            .Where(l => l.IdLocalidad == idLocalidad)
            .Select(l => l.NombreLocalidad)
            .FirstOrDefault();

        return View(sucursales);
    }

}

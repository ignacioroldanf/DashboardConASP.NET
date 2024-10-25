using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHeladeria.Models;

public class SucursalesController : Controller
{
    private readonly HeladeriaContext _context;

    public SucursalesController(HeladeriaContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IActionResult> Index()
    {
        var sucursales = await _context.Sucursales.ToListAsync();
        return View(sucursales);
    }

    public IActionResult SucursalesPorLocalidad(int idLocalidad)
    {

        var sucursal = _context.Sucursales
            .Where(s => s.IdLocalidad == idLocalidad)
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
                    .Take(5) //top 5
                    .ToList(),

                    KilosVendidos = _context.VentasDetalles
                    .Where(v => v.IdVentaNavigation.IdSucursal == s.IdSucursal)
                    .Sum(v => v.Cantidad),

                    SemaforoEstado = _context.VentasDetalles
                    .Where(v => v.IdVentaNavigation.IdSucursal == s.IdSucursal)
                    .Sum(v => v.Cantidad) >= 10 ? "verde"
                    : _context.VentasDetalles
                    .Where(v => v.IdVentaNavigation.IdSucursal == s.IdSucursal)
                    .Sum(v => v.Cantidad) >= 7 ? "amarillo"
                    : "rojo"

            })
            .ToList();


        ViewBag.NombreLocalidad = _context.Localidades
            .Where(l => l.IdLocalidad == idLocalidad)
            .Select(l => l.NombreLocalidad)
            .FirstOrDefault();

        return View(sucursal);
    }

}

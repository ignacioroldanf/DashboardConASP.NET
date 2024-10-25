using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHeladeria.Models;

public class GustosController : Controller
{
    private readonly HeladeriaContext _context;

    public GustosController(HeladeriaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var gustos = await _context.Gustos.ToListAsync();
        return View(gustos);
    }

    public IActionResult GustosPorSucursal(int idSucursal)
    {
        if (idSucursal <= 0)
        {
            return NotFound();
        }

        int KilosObjetivo = 5; // Ajustar según tu necesidad

        var gustosVendidos = _context.VentasDetalles
            .Include(vd => vd.IdGustoNavigation)
            .Include(vd => vd.IdVentaNavigation)
            .Where(vd => vd.IdVentaNavigation.IdSucursal == idSucursal)
            .Select(vd => new
            {
                IdGusto = vd.IdGustoNavigation.IdGusto,
                NombreGusto = vd.IdGustoNavigation.NombreGusto,
                CantidadVendida = vd.Cantidad,
                SemaforoEstado = vd.Cantidad >= 5 ? "verde" :
                                 vd.Cantidad == 4 ? "amarillo" :
                                 "rojo"
            })
            .ToList();

        var sucursal = _context.Sucursales.FirstOrDefault(s => s.IdSucursal == idSucursal);

        if (sucursal == null)
        {
            return NotFound();
        }

        ViewBag.NombreSucursal = sucursal.CalleSucursal;
        ViewBag.IdLocalidad = sucursal.IdLocalidad;

        return View(gustosVendidos);
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHeladeria.Models;

namespace WebHeladeria.Controllers
{
    public class GustosController : Controller
    {
        private readonly HeladeriaContext _context;

        public GustosController(HeladeriaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var gustos = _context.Gustos.ToList();
            return View(gustos);
        }

        public IActionResult GustosPorSucursal(int idSucursal)
        {
            // Verificar que el idSucursal sea válido
            if (idSucursal <= 0)
            {
                return NotFound(); // Manejo de error si el id es inválido
            }

            // Obtener los gustos vendidos en la sucursal seleccionada
            var gustosVendidos = _context.VentasDetalles
                .Include(vd => vd.IdGustoNavigation) // Incluir el Gusto
                .Include(vd => vd.IdVentaNavigation) // Incluir la Venta
                .Where(vd => vd.IdVentaNavigation.IdSucursal == idSucursal) // Filtrar por sucursal
                .ToList();

            // Obtener el nombre de la sucursal para mostrar en la vista
            var sucursal = _context.Sucursales.FirstOrDefault(s => s.IdSucursal == idSucursal);

            // Verificar si se encontró la sucursal
            if (sucursal == null)
            {
                return NotFound(); // Manejo de error si no se encuentra la sucursal
            }

            ViewBag.NombreSucursal = sucursal.CalleSucursal; // O el nombre que desees mostrar

            return View(gustosVendidos);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using WebHeladeria.Servicios.Contrato;

namespace WebHeladeria.Controllers
{
    public class ReporteController : Controller
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        public async Task<IActionResult> Index()
        {
            var reportes = await _reporteService.ObtenerReportesAsync();
            return View(reportes);
        }
    }
}

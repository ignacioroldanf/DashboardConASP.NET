using Microsoft.AspNetCore.Mvc;
using WebHeladeria.Servicios.Contrato;

namespace WebHeladeria.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
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

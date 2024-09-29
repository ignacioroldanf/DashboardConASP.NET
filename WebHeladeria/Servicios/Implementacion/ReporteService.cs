using WebHeladeria.Models;
using WebHeladeria.Servicios.Contrato;

namespace WebHeladeria.Servicios.Implementacion
{
    public class ReporteService : IReporteService
    {
        private readonly HeladeriaContext _context;

        public ReporteService(HeladeriaContext context)
        {
            _context = context;
        }

        public async Task<List<Reporte>> ObtenerReportesAsync()
        {
            return await _context.ObtenerReporteAsync();
        }
    }
}

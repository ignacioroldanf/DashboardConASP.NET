using WebHeladeria.Models;

namespace WebHeladeria.Servicios.Contrato
{
    public interface IReporteService
    {
        Task<List<Reporte>> ObtenerReportesAsync();
    }

}

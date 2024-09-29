using System;
using System.Collections.Generic;

namespace WebHeladeria.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public DateOnly? Fecha { get; set; }

    public int? IdSucursal { get; set; }

    public virtual Sucursale? IdSucursalNavigation { get; set; }

    public virtual ICollection<VentasDetalle> VentasDetalles { get; set; } = new List<VentasDetalle>();
}

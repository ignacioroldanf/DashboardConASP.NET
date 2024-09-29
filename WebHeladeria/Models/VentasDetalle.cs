using System;
using System.Collections.Generic;

namespace WebHeladeria.Models;

public partial class VentasDetalle
{
    public int IdVenta { get; set; }

    public int IdGusto { get; set; }

    public int Cantidad { get; set; }

    public virtual Gusto IdGustoNavigation { get; set; } = null!;

    public virtual Venta IdVentaNavigation { get; set; } = null!;
}

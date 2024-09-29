using System;
using System.Collections.Generic;

namespace WebHeladeria.Models;

public partial class Gusto
{
    public int IdGusto { get; set; }

    public string? NombreGusto { get; set; }

    public string? DescripGusto { get; set; }

    public virtual ICollection<VentasDetalle> VentasDetalles { get; set; } = new List<VentasDetalle>();
}

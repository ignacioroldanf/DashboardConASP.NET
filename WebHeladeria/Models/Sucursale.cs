using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHeladeria.Models;

public partial class Sucursale
{
    public int IdSucursal { get; set; }

    public int? Localidad { get; set; }

    public string? CalleSucursal { get; set; }

    public int? NroSucursal { get; set; }

    public virtual Localidade? LocalidadNavigation { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
    
    [NotMapped]
    public List<string> GustosMasVendidos { get; set; } = new List<string>();

}

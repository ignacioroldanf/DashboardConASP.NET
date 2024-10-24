﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebHeladeria.Models;

public partial class Localidade
{
    public int IdLocalidad { get; set; }

    public string? NombreLocalidad { get; set; }

    public virtual ICollection<Sucursale> Sucursales { get; set; } = new List<Sucursale>();


    [NotMapped]
    public List<string> GustosMasVendidos { get; set; } = new List<string>();

    [NotMapped]
    public decimal KilosVendidos { get; set; }

    [NotMapped]
    public string EstadoObjetivo { get; set; }
}

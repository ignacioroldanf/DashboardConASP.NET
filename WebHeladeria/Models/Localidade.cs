﻿using System;
using System.Collections.Generic;

namespace WebHeladeria.Models;

public partial class Localidade
{
    public int IdLocalidad { get; set; }

    public string? NombreLocalidad { get; set; }

    public virtual ICollection<Sucursale> Sucursales { get; set; } = new List<Sucursale>();
}

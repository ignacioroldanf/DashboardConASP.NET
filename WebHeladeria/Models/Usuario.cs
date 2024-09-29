using System;
using System.Collections.Generic;

namespace WebHeladeria.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? ContraUsuario { get; set; }
}

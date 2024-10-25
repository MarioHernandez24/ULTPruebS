﻿using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class UnidadMedida
{
    public int IdUnidad { get; set; }

    public string NombreUnidad { get; set; } = null!;

    public string? Simbolo { get; set; }

    public string? Descripcion { get; set; }

    public string? TipoUnidad { get; set; }

    public virtual ICollection<Producto1> Producto1s { get; set; } = new List<Producto1>();
}

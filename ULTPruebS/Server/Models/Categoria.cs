using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<Producto1> Producto1s { get; } = new List<Producto1>();
    public virtual ICollection<Inventario> Inventarios { get; } = new List<Inventario>();

}

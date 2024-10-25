using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class Proveedor
{
    public int IdProveedor { get; set; }

    public string NombreProveedor { get; set; } = null!;

    public string? DireccionEmpresa { get; set; }

    public string? Telefono { get; set; }

    public string? RazonSocial { get; set; }

    public string? NumeroRuc { get; set; }

    public virtual ICollection<Compra> Compras { get; } = new List<Compra>();

    public virtual ICollection<Producto1> Producto1s { get; } = new List<Producto1>();
}

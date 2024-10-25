using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public DateTime FechaCompra { get; set; }

    public int IdProveedor { get; set; }

    public decimal TotalCompra { get; set; }

    public string? Cliente { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; } = new List<DetalleCompra>();

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual ICollection<Reporte> Reportes { get; } = new List<Reporte>();
}

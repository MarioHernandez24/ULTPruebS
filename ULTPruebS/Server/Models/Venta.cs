using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public string? Cliente { get; set; }

    public decimal? Total { get; set; }

    public DateTime FechaVenta { get; set; }

    public decimal? MontoPagado { get; set; }

    public decimal? Cambio { get; set; }

    public decimal? DescuentoTotal { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual ICollection<Factura1> Factura1s { get; } = new List<Factura1>();

    public virtual ICollection<Factura> Facturas { get; } = new List<Factura>();

    public virtual ICollection<Reporte> Reportes { get; } = new List<Reporte>();
}

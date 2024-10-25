using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class Factura1
{
    public int IdFactura1 { get; set; }

    public int IdVenta { get; set; }

    public string NumeroFactura { get; set; } = null!;

    public DateTime FechaEmision { get; set; }

    public decimal TotalFactura { get; set; }

    public decimal Iva { get; set; }

    public decimal Subtotal { get; set; }

    public decimal? DescuentoTotal { get; set; }

    public virtual Venta IdVentaNavigation { get; set; } = null!;
}

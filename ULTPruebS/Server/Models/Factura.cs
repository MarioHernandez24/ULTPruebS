using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateTime FechaFactura { get; set; }

    public decimal MontoTotal { get; set; }

    public string MontoPago { get; set; } = null!;

    public string EstadoFactura { get; set; } = null!;

    public string? DetallesFactura { get; set; }

    public int IdVenta { get; set; }

    public virtual Venta IdVentaNavigation { get; set; } = null!;
}

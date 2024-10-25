using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class Reporte
{
    public int IdReporte { get; set; }

    public string TipoReporte { get; set; } = null!;

    public string Periodo { get; set; } = null!;

    public string DetallesReporte { get; set; } = null!;

    public int? IdVenta { get; set; }

    public int? IdCompra { get; set; }

    public string? Cliente { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Iva { get; set; }

    public decimal? Descuento { get; set; }

    public decimal? Total { get; set; }

    public decimal? MontoPagado { get; set; }

    public decimal? Cambio { get; set; }

    public string TipoTransaccion { get; set; } = null!;

    public string? MetodoPago { get; set; }

    public virtual Compra? IdCompraNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}

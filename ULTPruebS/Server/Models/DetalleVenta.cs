using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class DetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public int? Cantidad { get; set; }

    public decimal? SubTotal { get; set; }

    public int? IdProducto1 { get; set; }

    public decimal? Iva { get; set; }

    public decimal TasaIva { get; set; }

    public decimal? Total { get; set; }

    public decimal? Descuento { get; set; }

    public virtual Producto1? IdProducto1Navigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}

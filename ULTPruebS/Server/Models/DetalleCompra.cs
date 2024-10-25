using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class DetalleCompra
{
    public int IdDetalleCompra { get; set; }

    public int Cantidad { get; set; }

    public decimal SubtotalCompra { get; set; }

    public int IdCompra { get; set; }

    public int? IdProducto { get; set; }

    public virtual Compra IdCompraNavigation { get; set; } = null!;

    public virtual Producto1? IdProductoNavigation { get; set; }
}

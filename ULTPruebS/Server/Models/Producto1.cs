using ULTPruebS.Server.Models;
using System;
using System.Collections.Generic;

namespace ULTPruebS.Server.Models;

public partial class Producto1
{
    public int IdProducto1 { get; set; }

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    public decimal? PrecioCompra { get; set; }

    public decimal? PrecioVenta { get; set; }

    public string? Caracteristicas { get; set; }

    public bool Estado { get; set; }

    public string? Detalle { get; set; }

    public int? Stock { get; set; }

    public int? IdUnidad { get; set; }

    public int? StockMinimo { get; set; }

    public decimal Ganancia { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdProveedor { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;

    public virtual UnidadMedida IdUnidadNavigation { get; set; } = null!;




    // Colecciones de otras entidades relacionadas
    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
    public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
}

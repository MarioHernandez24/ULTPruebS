using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTPruebS.Shared
{
    public class DetalleVentaDTO
    {

        public int IdDetalleVenta { get; set; } // Llave primaria del detalle de venta
        public int? IdVenta { get; set; } // Llave foránea de la venta
        public int? Cantidad { get; set; } // Cantidad de productos vendidos
        public decimal? SubTotal { get; set; } // Subtotal de la venta
        public int? IdProducto1 { get; set; } // Llave foránea del producto
        public decimal? Iva { get; set; } // IVA calculado
        public decimal TasaIva { get; set; } // Tasa de IVA aplicada
        public decimal? Total { get; set; } // Total de la venta (con IVA y descuentos)
        public decimal? Descuento { get; set; } // Descuento aplicado
        

        public virtual Producto1DTO Producto1 { get; set; }
    }
}

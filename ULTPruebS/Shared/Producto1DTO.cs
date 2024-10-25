using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTPruebS.Shared
{
    public class Producto1DTO
    {
        public int IdProducto1 { get; set; } // Identificador único del producto
        public string? Nombre { get; set; } // Nombre del producto
        public decimal? Precio { get; set; } // Precio del producto (opcional)
        public decimal PrecioCompra { get; set; } // Precio al que se compra el producto
        public decimal PrecioVenta { get; set; } // Precio al que se vende el producto
        public string? Caracteristicas { get; set; } // Características adicionales del producto
        public bool Estado { get; set; } // Estado del producto (activo o inactivo)
        public string? Detalle { get; set; } // Descripción detallada del producto
        public int Stock { get; set; } // Cantidad en inventario
        public int IdUnidad { get; set; } // Identificador de la unidad de medida
        public int StockMinimo { get; set; } // Stock mínimo permitido

        public decimal Ganancia
        {
            get
            {
                return PrecioVenta - PrecioCompra;
            }
        }
        public int IdCategoria { get; set; } // Identificador de la categoría del producto
        public int IdProveedor { get; set; } // Identificador del proveedor del producto
        
    }
}

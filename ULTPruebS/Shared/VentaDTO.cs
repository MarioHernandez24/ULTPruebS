using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTPruebS.Shared
{
    public  class VentaDTO
    {
        public int IdVenta { get; set; } // Llave primaria
        public string? Cliente { get; set; } // Cliente (máx. 50 caracteres)
        public decimal? Total { get; set; } // Total de la venta
        public decimal DescuentoTotal { get; set; } // Descuento aplicado a la venta
        public decimal? MontoPagado { get; set; } // Monto pagado por el cliente
        public decimal Cambio { get; set; } // Cambio entregado al cliente
        public DateTime FechaVenta { get; set; } // Fecha de la venta
        public virtual ICollection<DetalleVentaDTO> DetalleVenta { get; set; }
    }
}

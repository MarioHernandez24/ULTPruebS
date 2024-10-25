using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ULTPruebS.Server.Models;
using ULTPruebS.Shared;

namespace ULTPruebS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly FerreteriaDContext _dbContext;

        public VentaController(FerreteriaDContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(VentaDTO ventaDto)
        {
            try
            {
                // Crear la venta desde el DTO
                var mdVenta = new Venta
                {
                    Cliente = ventaDto.Cliente,
                    Total = ventaDto.Total,
                    DescuentoTotal = ventaDto.DescuentoTotal,
                    MontoPagado = ventaDto.MontoPagado,
                    Cambio = ventaDto.Cambio,
                    FechaVenta = ventaDto.FechaVenta
                };

                // Crear los detalles de la venta
                var mdDetalleVenta = new List<DetalleVenta>();

                foreach (var item in ventaDto.DetalleVenta)
                {
                    mdDetalleVenta.Add(new DetalleVenta
                    {
                        IdProducto1 = item.Producto1.IdProducto1,
                        Cantidad = item.Cantidad,
                        SubTotal = item.SubTotal,
                        Iva = item.Iva,
                        TasaIva = item.TasaIva,
                        Total = item.Total,
                        Descuento = item.Descuento
                    });
                }

                // Asignar los detalles a la venta
                mdVenta.DetalleVenta = mdDetalleVenta;

                // Guardar la venta en la base de datos
                _dbContext.Venta.Add(mdVenta);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ULTPruebS.Server.Models;
using ULTPruebS.Shared; // Asegúrate de que los DTOs están aquí
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;

namespace ULTPruebS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly FerreteriaDContext _dbContext;

        public ProductoController(FerreteriaDContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<Producto1DTO>>();

            try
            {
                var listaProducto1DTO = await _dbContext.Producto1s
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdUnidadNavigation)
                .Select(item => new Producto1DTO
                {
                  IdProducto1 = item.IdProducto1,
                  Nombre = item.Nombre != null ? item.Nombre : "Producto desconocido",
                 // Cantidad = item.Cantidad.HasValue ? item.Cantidad.Value : 0,
                  PrecioCompra = item.PrecioCompra.HasValue ? item.PrecioCompra.Value : 0.00m,
                  PrecioVenta = item.PrecioVenta.HasValue ? item.PrecioVenta.Value : 0.00m,
                  //Ganancia = item.Ganancia.HasValue ? item.Ganancia.Value : 0.00m,
                  Caracteristicas = item.Caracteristicas != null ? item.Caracteristicas : "Sin características",
                  Estado = item.Estado,
                  Detalle = item.Detalle != null ? item.Detalle : "Sin detalles",
                  IdUnidad = item.IdUnidad.HasValue ? item.IdUnidad.Value : 1,
                  IdCategoria = item.IdCategoria.HasValue ? item.IdCategoria.Value : 4,
                  Stock = item.Stock.HasValue ? item.Stock.Value : 0,
                  StockMinimo = item.StockMinimo.HasValue ? item.StockMinimo.Value : 0
                }).ToListAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaProducto1DTO;
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<Producto1DTO>();

            try
            {
                var dbProducto1 = await _dbContext.Producto1s
                    .Include(p => p.IdCategoriaNavigation) // Incluye Categoria
                    .Include(p => p.IdUnidadNavigation)    // Incluye UnidadMedida
                    .FirstOrDefaultAsync(x => x.IdProducto1 == id);

                if (dbProducto1 != null)
                {
                    var producto1DTO = new Producto1DTO
                    {
                        IdProducto1 = dbProducto1.IdProducto1,
                        Nombre = dbProducto1.Nombre != null ? dbProducto1.Nombre : "Producto1 desconocido",
                      //  Cantidad = dbProducto.Cantidad.HasValue ? dbProducto.Cantidad.Value : 0,
                        PrecioCompra = dbProducto1.PrecioCompra.HasValue ? dbProducto1.PrecioCompra.Value : 0.00m,
                        PrecioVenta = dbProducto1.PrecioVenta.HasValue ? dbProducto1.PrecioVenta.Value : 0.00m,
                        //Ganancia = dbProducto.Ganancia.HasValue ? dbProducto.Ganancia.Value : 0.00m,
                        Caracteristicas = dbProducto1.Caracteristicas != null ? dbProducto1.Caracteristicas : "Sin características",
                        Estado = dbProducto1 .Estado,
                        Detalle = dbProducto1.Detalle != null ? dbProducto1.Detalle : "Sin detalles",
                        IdUnidad = dbProducto1.IdUnidad.HasValue ? dbProducto1.IdUnidad.Value : 1,
                        IdCategoria = dbProducto1.IdCategoria.HasValue ? dbProducto1.IdCategoria.Value : 4,
                        //IdProveedor = dbProducto1.IdProveedor.HasValue ? dbProducto1.IdProveedor.Value : 4,
                        Stock = dbProducto1.Stock.HasValue ? dbProducto1.Stock.Value : 0,
                        StockMinimo = dbProducto1.StockMinimo.HasValue ? dbProducto1.StockMinimo.Value : 0
                    };

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = producto1DTO;
                    return Ok(responseApi);
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Producto no encontrado";
                    return NotFound(responseApi);
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(Producto1DTO producto1Dto)
        {
            var responseApi = new ResponseAPI<int>();

            if (!ModelState.IsValid)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Datos del producto no válidos";
                return BadRequest(responseApi);
            }

            try
            {
                var dbProducto1 = new Producto1
                {
                    Nombre = producto1Dto.Nombre,
                   // Cantidad = productoDto.Cantidad,
                    PrecioCompra = producto1Dto.PrecioCompra,
                    PrecioVenta = producto1Dto.PrecioVenta,
                    Ganancia = producto1Dto.Ganancia,
                    Caracteristicas = producto1Dto.Caracteristicas,
                    Estado = producto1Dto.Estado,
                    Detalle = producto1Dto.Detalle,
                    IdUnidad = producto1Dto.IdUnidad,
                    IdCategoria = producto1Dto.IdCategoria,
                    Stock = producto1Dto.Stock,
                    StockMinimo = producto1Dto.StockMinimo
                };

                _dbContext.Producto1s.Add(dbProducto1);
                await _dbContext.SaveChangesAsync();

                // Verificar stock después de guardar
                responseApi.Mensaje = VerificarStockMinimo(dbProducto1);
                responseApi.EsCorrecto = true;
                responseApi.Valor = dbProducto1.IdProducto1;
                return CreatedAtAction(nameof(Buscar), new { id = dbProducto1.IdProducto1 }, responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                //return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
                return StatusCode(500, $"Error: {ex.Message} - StackTrace: {ex.StackTrace}");

            }
        }


        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar([FromBody] Producto1DTO producto1Dto, int id)
        {
            var responseApi = new ResponseAPI<int>();

            if (!ModelState.IsValid)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Datos del producto no válidos";
                return BadRequest(responseApi);
            }

            try
            {
                var dbProducto1 = await _dbContext.Producto1s.FirstOrDefaultAsync(e => e.IdProducto1 == id);

                if (dbProducto1 != null)
                {
                    dbProducto1.Nombre = producto1Dto.Nombre;
                  //  dbProducto.Cantidad = productoDto.Cantidad;
                    dbProducto1.PrecioCompra = producto1Dto.PrecioCompra;
                    dbProducto1.PrecioVenta = producto1Dto.PrecioVenta;
                    dbProducto1.Ganancia = producto1Dto.Ganancia;
                    dbProducto1.Caracteristicas = producto1Dto.Caracteristicas;
                    dbProducto1.Estado = producto1Dto.Estado;
                    dbProducto1.Detalle = producto1Dto.Detalle;
                    dbProducto1.IdUnidad = producto1Dto.IdUnidad;
                    dbProducto1.IdCategoria = producto1Dto.IdCategoria;
                    dbProducto1.Stock = producto1Dto.Stock;
                    dbProducto1.StockMinimo = producto1Dto.StockMinimo;

                    _dbContext.Producto1s.Update(dbProducto1);
                    await _dbContext.SaveChangesAsync();

                    // Verificar stock después de editar
                    responseApi.Mensaje = VerificarStockMinimo(dbProducto1);


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProducto1.IdProducto1;
                    return Ok(responseApi);
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Producto no encontrado";
                    return NotFound(responseApi);
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }

        private string VerificarStockMinimo(Producto1 producto1)
        {
            if (producto1.Stock <= producto1.StockMinimo)
            {
                // Lógica para enviar el aviso. Puede ser una notificación, un log, o un correo.
                // Aquí puedes definir cómo quieres que se envíe el aviso, por ejemplo:
                Console.WriteLine($"El producto '{producto1.Nombre}' ha alcanzado su stock mínimo.");
            }
            return string.Empty;
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProducto1 = await _dbContext.Producto1s.FirstOrDefaultAsync(e => e.IdProducto1 == id);

                if (dbProducto1 != null)
                {
                    _dbContext.Producto1s.Remove(dbProducto1);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    return Ok(responseApi);
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Producto no encontrado";
                    return NotFound(responseApi);
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }
    }
}

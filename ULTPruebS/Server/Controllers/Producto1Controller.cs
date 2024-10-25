using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ULTPruebS.Server.Models;
using ULTPruebS.Shared;

namespace ULTPruebS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Producto1Controller : ControllerBase
    {
        private readonly FerreteriaDContext _dbContext;

        public Producto1Controller(FerreteriaDContext dbContext)
        {
            _dbContext = dbContext; ;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var lista = new List<Producto1DTO>();

            foreach (var item in await _dbContext.Producto1s.ToListAsync())
            {
                lista.Add(new Producto1DTO
                {
                    IdProducto1 = item.IdProducto1,
                    Nombre = item.Nombre,
                    Precio = item.Precio
                });
            }

            return Ok(lista);
        }
    }
}

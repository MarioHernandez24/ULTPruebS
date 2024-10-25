
using ULTPruebS.Shared;
using System.Net.Http.Json;

namespace ULTPruebS.Client.Servicio
{
    public class Producto1Service : IProducto1Service
    {
        private readonly HttpClient _http;

        public Producto1Service(HttpClient http)
        {
            _http = http;
        }


        public async Task<List<Producto1DTO>> Lista()
        {
            var lista = new List<Producto1DTO>();

            lista = await _http.GetFromJsonAsync<List<Producto1DTO>>("api/Producto1");

            return lista!;
        }
    }
}

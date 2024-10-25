using System.Net.Http.Json;
using ULTPruebS.Shared;
using static System.Net.WebRequestMethods;
using System.Threading.Tasks;
using System.Net.Http;


namespace ULTPruebS.Client.Service
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Obtiene la lista de productos desde la API.
        /// </summary>
        public async Task<List<Producto1DTO>> Lista()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<ResponseAPI<List<Producto1DTO>>>("api/producto/lista");

                if (result?.EsCorrecto == true)
                {
                    return result.Valor ?? new List<Producto1DTO>(); // Manejo de posibles valores nulos
                }
                else
                {
                    throw new Exception(result?.Mensaje ?? "Error desconocido");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la lista de productos: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Busca un producto por su ID.
        /// </summary>
        public async Task<Producto1DTO> Buscar(int id)
        {
            try
            {
                //var product= await _httpClient.GetFromJsonAsync<ProductoDTO>($"api/Producto/Buscar/{id}");

                var result = await _httpClient.GetFromJsonAsync<ResponseAPI<Producto1DTO>>($"api/Producto/Buscar/{id}");

                if (result?.EsCorrecto == true)
                {
                    return result.Valor ?? throw new Exception("Categoría no encontrada"); // Manejar posibles valores nulos
                }
                else
                {
                    throw new Exception(result?.Mensaje ?? "Error desconocido");
                }
                //int i = 0;
                // return result;
            }
            catch (HttpRequestException ex)
            {
                // Manejar excepción y registrar error
                // Aquí podrías loggear el error o mostrar un mensaje de usuario
                throw new Exception($"Error al buscar el producto con ID {id}.", ex);
            }
        }

        /// <summary>
        /// Guarda un nuevo producto.
        /// </summary>
        public async Task<int> Guardar(Producto1DTO producto)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync("api/Producto/Guardar", producto);
                var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

                if (response?.EsCorrecto == true)
                {
                    return response.Valor;
                }
                else
                {
                    // Manejar la respuesta no exitosa
                    throw new Exception(response?.Mensaje ?? "Error desconocido");
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejar excepción y registrar error
                // Aquí podrías loggear el error o mostrar un mensaje de usuario
                throw new Exception("Error al guardar el producto.", ex);
            }
        }

        /// <summary>
        /// Edita un producto existente.
        /// </summary>
        public async Task<int> Editar(Producto1DTO producto)
        {
            try
            {
                var result = await _httpClient.PutAsJsonAsync($"api/Producto/Editar/{producto.IdProducto1}", producto);
                var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

                if (response?.EsCorrecto == true)
                {
                    return response.Valor;
                }
                else
                {
                    // Manejar la respuesta no exitosa
                    throw new Exception(response?.Mensaje ?? "Error desconocido");
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejar excepción y registrar error
                // Aquí podrías loggear el error o mostrar un mensaje de usuario
                throw new Exception("Error al editar el producto.", ex);
            }
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Producto/Eliminar/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                // Manejar excepción y registrar error
                // Aquí podrías loggear el error o mostrar un mensaje de usuario
                throw new Exception($"Error al eliminar el producto con ID {id}.", ex);
            }
        }
    }
}

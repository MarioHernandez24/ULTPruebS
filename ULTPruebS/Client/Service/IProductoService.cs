using ULTPruebS.Shared;

namespace ULTPruebS.Client.Service
{
    public interface IProductoService
    {
        Task<List<Producto1DTO>> Lista();

        /// <summary>
        /// Busca un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto.</param>
        /// <returns>Detalles del producto.</returns>
        Task<Producto1DTO> Buscar(int id);

        /// <summary>
        /// Guarda un nuevo producto.
        /// </summary>
        /// <param name="producto">Datos del producto a guardar.</param>
        /// <returns>ID del producto guardado.</returns>
        Task<int> Guardar(Producto1DTO producto);

        /// <summary>
        /// Edita un producto existente.
        /// </summary>
        /// <param name="producto">Datos del producto a editar.</param>
        /// <returns>ID del producto editado.</returns>
        Task<int> Editar(Producto1DTO producto);

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a eliminar.</param>
        /// <returns>Indica si la eliminación fue exitosa.</returns>
        Task<bool> Eliminar(int id);
    }
}


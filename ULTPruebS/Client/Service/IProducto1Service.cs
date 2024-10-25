using ULTPruebS.Shared;

namespace ULTPruebS.Client.Servicio
{
    public interface IProducto1Service
    {
        Task<List<Producto1DTO>> Lista();
    }
}

using ULTPruebS.Shared;
namespace ULTPruebS.Client.Service
{
    public interface IProveedorService
    {
        Task<List<ProveedorDTO>> Lista();
        Task<ProveedorDTO> Buscar(int id);
        Task<int> Guardar(ProveedorDTO proveedor);
        Task<int> Editar(ProveedorDTO proveedor);
        Task<bool> Eliminar(int id);

    }
}

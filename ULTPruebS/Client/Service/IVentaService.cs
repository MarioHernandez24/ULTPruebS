using ULTPruebS.Shared;

namespace ULTPruebS.Client.Servicio
{
    public interface IVentaService
    {
        Task<bool> Guardar(VentaDTO ventaDto);
    }
}

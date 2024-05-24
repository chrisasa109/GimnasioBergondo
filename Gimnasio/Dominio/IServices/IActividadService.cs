using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IServices
{
    public interface IActividadService
    {
        Task<bool> AgregarActividad(ActividadDTO actividadFront);
        Task<List<ActividadDTO>> ObtenerTodasActividadesDisponibles();
    }
}

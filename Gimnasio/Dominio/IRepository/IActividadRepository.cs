using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IRepository
{
    public interface IActividadRepository
    {
        Task<bool> AgregarActividad(ActividadDTO actividadFront);
        Task<List<ActividadDTO>> ObtenerTodasActividadesDisponibles();
        Task<ActividadDTO> ObtenerActividadPorId(int id);
    }
}

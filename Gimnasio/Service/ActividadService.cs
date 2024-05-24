using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;

namespace Gimnasio.Service
{
    public class ActividadService : IActividadService
    {
        private readonly IActividadRepository _IActividadRepository;
        public ActividadService(IActividadRepository iActividadRepository)
        {
            _IActividadRepository = iActividadRepository;
        }

        public Task<bool> AgregarActividad(ActividadDTO actividadFront)
        {
            return _IActividadRepository.AgregarActividad(actividadFront);
        }

        public async Task<List<ActividadDTO>> ObtenerTodasActividadesDisponibles()
        {
            return await _IActividadRepository.ObtenerTodasActividadesDisponibles();
        }
    }
}

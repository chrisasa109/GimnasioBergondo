using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;

namespace Gimnasio.Service
{
    public class UsuarioActividadService : IUsuarioActividadService
    {
        private readonly IUsuarioActividadRepository _IUsuarioActividadRepository;
        private readonly IActividadRepository _IActividadRepository;
        private readonly SessionService _sessionService;
        public UsuarioActividadService(IUsuarioActividadRepository iUsuarioActividadRepository, SessionService sessionService, IActividadRepository actividadRepository)
        {
            _IUsuarioActividadRepository = iUsuarioActividadRepository;
            _sessionService = sessionService;
            _IActividadRepository= actividadRepository;
        }

        public async Task<List<UsuarioActividadDTO>> ObtenerActividadesDelUsuarioPorId(int? id)
        {
            if (id == null)
            {
                id = _sessionService.ObtenerUsuario().Id;
            }
            List<UsuarioActividadDTO>lista = await _IUsuarioActividadRepository.ObtenerActividadesDelUsuarioPorId(id);
            if(lista.Count>0)
            {
                foreach (UsuarioActividadDTO item in lista)
                {
                    item.Actividad = await _IActividadRepository.ObtenerActividadPorId(item.Id);
                }
            }
            return lista;
        }
    }
}

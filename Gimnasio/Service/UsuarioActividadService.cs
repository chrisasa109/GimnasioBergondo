using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;
using Gimnasio.Transporte.UsuarioActividad;

namespace Gimnasio.Service
{
    public class UsuarioActividadService : IUsuarioActividadService
    {
        private readonly IUsuarioActividadRepository _IUsuarioActividadRepository;
        private readonly IActividadRepository _IActividadRepository;
        private readonly SessionService _sessionService;
        private readonly IUsuarioRepository _IUsuarioRepository;
        public UsuarioActividadService(IUsuarioRepository usuariorepository,IUsuarioActividadRepository iUsuarioActividadRepository, SessionService sessionService, IActividadRepository actividadRepository)
        {
            _IUsuarioActividadRepository = iUsuarioActividadRepository;
            _sessionService = sessionService;
            _IActividadRepository = actividadRepository;
            _IUsuarioRepository = usuariorepository;
        }

        public async Task<bool> ApuntarUsuarioActividad(ModeloUserAct modelo)
        {
            UsuarioActividadDTO front = new()
            {
                ActividadId = modelo.ActividadId,
                Notas = modelo.Notas,
                UsuarioId = _sessionService.ObtenerUsuario().Id
            };
            return await _IUsuarioActividadRepository.ApuntarUsuarioActividad(front);
        }

        public async Task<bool> ComprobarUsuarioApuntadoActividad(int actividadId)
        {
            int idUsuario = _sessionService.ObtenerUsuario().Id;
            return await _IUsuarioActividadRepository.ComrobarUsuarioApuntadoActividad(actividadId, idUsuario);
        }

        public async Task<bool> EliminarUsuarioActividad(int eliminar)
        {
            return await _IUsuarioActividadRepository.EliminarUsuarioActividad(eliminar);
        }

        public async Task<bool> GuardarCambios(UsuarioActividadDTO usAc)
        {
            return await _IUsuarioActividadRepository.GuardarCambios(usAc);
        }

        public async Task<List<UsuarioActividadDTO>> ObtenerActividadesDelUsuarioPorId(int? id)
        {
            if (id == null)
            {
                id = _sessionService.ObtenerUsuario().Id;
            }
            List<UsuarioActividadDTO> lista = await _IUsuarioActividadRepository.ObtenerActividadesDelUsuarioPorId(id);
            if (lista.Count > 0)
            {
                foreach (UsuarioActividadDTO item in lista)
                {
                    item.Actividad = await _IActividadRepository.ObtenerActividadPorId(item.ActividadId);
                }
            }
            return lista;
        }

        public async Task<UsuarioActividadDTO> ObtenerDetalleActividad(int id)
        {
            UsuarioActividadDTO usuarioActividadDTO = await _IUsuarioActividadRepository.ObtenerDetalleActividad(id);
            usuarioActividadDTO.Actividad = await _IActividadRepository.ObtenerActividadPorId(usuarioActividadDTO.ActividadId);
            return usuarioActividadDTO;
        }

        public async Task<List<UsuarioActividadDTO>> ObtenerListaActividades()
        {
            List<UsuarioActividadDTO> lista = await _IUsuarioActividadRepository.ObtenerListadoActividadesUsuario();
            foreach (var item in lista)
            {
                item.Actividad = await _IActividadRepository.ObtenerActividadPorId(item.ActividadId);
            }
            return lista;
        }

        public async Task<List<UsuarioActividadDTO>> ObtenerUsuariosDeActividad(int idActividad)
        {
            List<UsuarioActividadDTO>lista = await _IUsuarioActividadRepository.ObtenerListado(idActividad);
            foreach (var item in lista)
            {
                item._usuario = await _IUsuarioRepository.ConsultarUsuarioActividad(item.UsuarioId);
            }
            return lista;
        }
    }
}

using Gimnasio.Transporte;
using Gimnasio.Transporte.UsuarioActividad;

namespace Gimnasio.Dominio.IServices
{
    public interface IUsuarioActividadService
    {
        Task<bool> ApuntarUsuarioActividad(ModeloUserAct modelo);
        Task<bool> ComprobarUsuarioApuntadoActividad(int actividadId);
        Task<bool> EliminarUsuarioActividad(int eliminar);
        Task<bool> GuardarCambios(UsuarioActividadDTO usAc);
        Task<List<UsuarioActividadDTO>> ObtenerActividadesDelUsuarioPorId(int? id);
        Task<UsuarioActividadDTO> ObtenerDetalleActividad(int id);
        Task<List<UsuarioActividadDTO>> ObtenerListaActividades();
        Task<List<UsuarioActividadDTO>> ObtenerUsuariosDeActividad(int idActividad);
    }
}

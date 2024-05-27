using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IRepository
{
    public interface IUsuarioActividadRepository
    {
        Task<bool> ApuntarUsuarioActividad(UsuarioActividadDTO front);
        Task<bool> ComrobarUsuarioApuntadoActividad(int actividadId, int idUsuario);
        Task<bool> EliminarUsuarioActividad(int id);
        Task<bool> GuardarCambios(UsuarioActividadDTO usAc);
        Task<List<UsuarioActividadDTO>> ObtenerActividadesDelUsuarioPorId(int? id);
        Task<UsuarioActividadDTO> ObtenerDetalleActividad(int id);
        Task<List<UsuarioActividadDTO>> ObtenerListado(int idActividad);
        Task<List<UsuarioActividadDTO>> ObtenerListadoActividadesUsuario();
    }
}

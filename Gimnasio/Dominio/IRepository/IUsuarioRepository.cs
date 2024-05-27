using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IRepository
{
    public interface IUsuarioRepository
    {
        Task<bool> CambiarRol(int idUsuario, string rolFronted);
        Task<UsuarioDTO> ConsultarUsuario(int? id);
        Task<UsuarioDTO?> ConsultarUsuarioActividad(int usuarioId);
        Task<bool> EliminarUsuario(int? v);
        Task<bool> ModificarUsuario(UsuarioDTO usuario);
        Task<List<UsuarioDTO>> ObtenerListaUsuarios();
        Task<bool> RegistrarUsuario(UsuarioDTO usuarioFront);
    }
}

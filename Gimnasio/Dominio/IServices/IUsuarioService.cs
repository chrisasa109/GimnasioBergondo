using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IServices
{
    public interface IUsuarioService
    {
        Task<bool> CambioRol(int idUsuario, string rolFronted);
        Task<UsuarioDTO> ConsultarUsuario(int? id);
        Task<bool> EliminarUsuario(int? v);
        Task<bool> ModificarUsuario(UsuarioDTO usuario);
        Task<List<UsuarioDTO>> ObtenerListaUsuarios();
        Task<bool> RegistrarUsuario(UsuarioDTO usuarioFront);
    }
}

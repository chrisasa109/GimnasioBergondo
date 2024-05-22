using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IServices
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> ConsultarUsuario(int id);
        Task<bool> ModificarUsuario(UsuarioDTO usuario);
        Task<bool> RegistrarUsuario(UsuarioDTO usuarioFront);
    }
}

using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IRepository
{
    public interface IUsuarioRepository
    {
        Task<UsuarioDTO> ConsultarUsuario(int id);
        Task<bool> ModificarUsuario(UsuarioDTO usuario);
        Task<bool> RegistrarUsuario(UsuarioDTO usuarioFront);
    }
}

using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;

namespace Gimnasio.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _IUsuarioRepository;
        public UsuarioService(IUsuarioRepository UsuarioRepository)
        {
            _IUsuarioRepository = UsuarioRepository;
        }

        public async Task<bool> CambioRol(int idUsuario, string rolFronted)
        {
            return await _IUsuarioRepository.CambiarRol(idUsuario, rolFronted);
        }

        public async Task<UsuarioDTO> ConsultarUsuario(int? id)
        {
            return await _IUsuarioRepository.ConsultarUsuario(id);
        }

        public async Task<bool> EliminarUsuario(int? v)
        {
            return await _IUsuarioRepository.EliminarUsuario(v);
        }

        public async Task<bool> ModificarUsuario(UsuarioDTO usuario)
        {
            return await _IUsuarioRepository.ModificarUsuario(usuario);
        }

        public async Task<List<UsuarioDTO>> ObtenerListaUsuarios()
        {
            return await _IUsuarioRepository.ObtenerListaUsuarios();
        }

        public async Task<bool> RegistrarUsuario(UsuarioDTO usuarioFront)
        {
            return await _IUsuarioRepository.RegistrarUsuario(usuarioFront);
        }
    }
}

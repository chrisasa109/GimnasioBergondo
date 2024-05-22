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
        public async Task<UsuarioDTO> ConsultarUsuario(int id)
        {
            return await _IUsuarioRepository.ConsultarUsuario(id);
        }

        public async Task<bool> ModificarUsuario(UsuarioDTO usuario)
        {
            return await _IUsuarioRepository.ModificarUsuario(usuario);
        }

        public async Task<bool> RegistrarUsuario(UsuarioDTO usuarioFront)
        {
            return await _IUsuarioRepository.RegistrarUsuario(usuarioFront);
        }
    }
}

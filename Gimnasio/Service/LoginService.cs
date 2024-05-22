using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;

namespace Gimnasio.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _ILoginRepository;
        public LoginService(ILoginRepository ILoginRepository)
        {
            _ILoginRepository = ILoginRepository;
        }

        public async Task<bool> IniciarSesion(LoginDTO login)
        {
            return await _ILoginRepository.IniciarSesion(login);
        }
    }
}

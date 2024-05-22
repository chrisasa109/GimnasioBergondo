using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IServices
{
    public interface ILoginService
    {
        Task<bool> IniciarSesion(LoginDTO login);
    }
}

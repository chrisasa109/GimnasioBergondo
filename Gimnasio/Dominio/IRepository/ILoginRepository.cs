using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IRepository
{
    public interface ILoginRepository
    {
        Task<bool> IniciarSesion(LoginDTO login);
    }
}

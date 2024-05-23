using Gimnasio.Models;
using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IRepository
{
    public interface ILoginRepository
    {
        Task<bool> ExisteUsuario(LoginDTO login);
        Task<Usuario> ObtenerUsuarioPorEmail(string email);
    }
}

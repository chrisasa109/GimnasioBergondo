using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IServices
{
    public interface ILoginService
    {
        void CerrarSesion(HttpContext httpContext);
        Task<bool> ExisteUsuario(LoginDTO login);
        Task<bool> IniciarSesion(LoginDTO login, HttpContext httpContext);
    }
}

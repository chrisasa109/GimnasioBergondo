using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Models;
using Gimnasio.Transporte;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Gimnasio.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _ILoginRepository;
        public LoginService(ILoginRepository ILoginRepository)
        {
            _ILoginRepository = ILoginRepository;
        }

        public async void CerrarSesion(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> ExisteUsuario(LoginDTO login)
        {
            return await _ILoginRepository.ExisteUsuario(login);
        }

        public async Task<bool> IniciarSesion(LoginDTO login, HttpContext httpContext)
        {
            Usuario usuarioF = await _ILoginRepository.ObtenerUsuarioPorEmail(login.Email);
            if (usuarioF == null)
            {
                return false;
            }

            var passHasher = new PasswordHasher<Usuario>();
            if (passHasher.VerifyHashedPassword(usuarioF, usuarioF.Password, login.Password) == PasswordVerificationResult.Success)
            {
                var claims = new List<Claim>{
                new(ClaimTypes.Name, usuarioF.Nombre),
                new(ClaimTypes.Role, usuarioF.Rol.ToString()),
                new("Id", usuarioF.Id.ToString())
                };
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

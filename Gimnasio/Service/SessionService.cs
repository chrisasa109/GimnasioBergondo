using Gimnasio.Dates;
using Gimnasio.Models;

namespace Gimnasio.Service
{
    public class SessionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Usuario? ObtenerUsuario()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
            if (int.TryParse(userIdClaim, out int userId))
            {
                var usuario = _context.Usuario.FirstOrDefault(x => x.Id == userId);
                return usuario;
            }
            return null;
        }

        public bool EsAdministrador()
        {
            return _httpContextAccessor.HttpContext.User.IsInRole("ADMINISTRADOR");
        }

        public bool EsTrabajador()
        {
            return _httpContextAccessor.HttpContext.User.IsInRole("TRABAJADOR");
        }

        public bool EsCliente()
        {
            return _httpContextAccessor.HttpContext.User.IsInRole("CLIENTE");
        }
    }
}

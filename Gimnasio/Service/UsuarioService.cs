using Gimnasio.Dates;
using Gimnasio.Models;

namespace Gimnasio.Service
{
    public class UsuarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
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
    }
}

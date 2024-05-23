using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Models;
using Gimnasio.Transporte;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Persistence
{
    public class LoginRepository : ILoginRepository
    {
        private ApplicationDbContext _context;
        public LoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Usuario> ObtenerUsuarioPorEmail(string email)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<bool> ExisteUsuario(LoginDTO login)
        {
            Usuario usuarioF = await ObtenerUsuarioPorEmail(login.Email);
            return usuarioF != null;
        }

    }
}

using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Service;
using Gimnasio.Transporte;

namespace Gimnasio.Persistence
{
    public class LoginRepository : ILoginRepository
    {
        private ApplicationDbContext _context;
        private SessionService _SessionService;
        public LoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<bool> IniciarSesion(LoginDTO login)
        {
            //
        }
    }
}

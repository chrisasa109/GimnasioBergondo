using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Transporte;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Persistence
{
    public class UsuarioActividadRepository : IUsuarioActividadRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioActividadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioActividadDTO>> ObtenerActividadesDelUsuarioPorId(int? id)
        {
            List<UsuarioActividadDTO> lista = [];
            lista = await (from t in _context.UsuarioActividad
                           where t.UsuarioId == id
                           select new UsuarioActividadDTO
                           {
                               ActividadId = t.ActividadId,
                               Id = t.Id,
                               Notas = t.Notas,
                               UsuarioId = t.UsuarioId
                           }).ToListAsync();
            return lista;
        }
    }
}

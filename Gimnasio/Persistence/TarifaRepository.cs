using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Transporte;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Persistence
{
    public class TarifaRepository : ITarifaRepository
    {
        private readonly ApplicationDbContext _context;
        public TarifaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<TarifaDTO>> ConsultaTarifas()
        {
            var tarifas= await (from t in _context.Tarifa
                               select new TarifaDTO
                               {
                                   Id = t.Id,
                                   Nombre = t.Nombre,
                                   Descripcion = t.Descripcion,
                                   Precio = t.Precio
                               })
                               .OrderBy(t => t.Precio)
                               .ToListAsync();
            return tarifas;
        }
    }
}

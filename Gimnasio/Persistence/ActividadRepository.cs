using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Transporte;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Persistence
{
    public class ActividadRepository : IActividadRepository
    {
        private readonly ApplicationDbContext _context;
        public ActividadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AgregarActividad(ActividadDTO actividadFront)
        {
            try
            {
                _context.Actividad.Add(new Models.Actividad
                {
                    Descripcion = actividadFront.Descripcion,
                    Duracion = actividadFront.Duracion,
                    CapacidadMaxima = actividadFront.CapacidadMaxima,
                    FechaHora = actividadFront.FechaHora
                });
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ActividadDTO> ObtenerActividadPorId(int id)
        {
            ActividadDTO actividad = await (from t in _context.Actividad
                                                   where t.Id == id
                                                   select new ActividadDTO
                                                   {
                                                       Id = t.Id,
                                                       CapacidadMaxima = t.CapacidadMaxima,
                                                       Descripcion = t.Descripcion,
                                                       Duracion=t.Duracion,
                                                       FechaHora=t.FechaHora
                                                   }).FirstOrDefaultAsync();
            return actividad;
        }

        public async Task<List<ActividadDTO>> ObtenerTodasActividadesDisponibles()
        {

            List<ActividadDTO> lista = [];
            lista = await (from t in _context.Actividad
                           where t.CapacidadMaxima > 0
                           select new ActividadDTO
                           {
                               CapacidadMaxima = t.CapacidadMaxima,
                               Descripcion = t.Descripcion,
                               Duracion = t.Duracion,
                               FechaHora = t.FechaHora,
                               Id = t.Id,
                           }).ToListAsync();
            return lista;
        }
    }
}

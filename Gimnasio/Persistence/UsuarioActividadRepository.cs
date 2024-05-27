using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Models;
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

        public async Task<bool> ApuntarUsuarioActividad(UsuarioActividadDTO front)
        {
            try
            {
                await _context.UsuarioActividad.AddAsync(new Models.UsuarioActividad
                {
                    UsuarioId = front.UsuarioId,
                    ActividadId = front.ActividadId,
                    Notas = front.Notas
                });
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ComrobarUsuarioApuntadoActividad(int actividadId, int idUsuario)
        {
            return await _context.UsuarioActividad.AnyAsync(ua => ua.ActividadId == actividadId && ua.UsuarioId == idUsuario);
        }

        public async Task<bool> EliminarUsuarioActividad(int id)
        {
            try
            {
                UsuarioActividad usuarioActividad = await _context.UsuarioActividad.FirstAsync(ua => ua.Id == id);
                _ = _context.UsuarioActividad.Remove(usuarioActividad);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> GuardarCambios(UsuarioActividadDTO usAc)
        {
            try
            {
                UsuarioActividad usuarioActividad = await _context.UsuarioActividad.FirstAsync(u => u.Id == usAc.Id);
                usuarioActividad.Notas = usAc.Notas;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
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

        public async Task<UsuarioActividadDTO> ObtenerDetalleActividad(int id)
        {
            UsuarioActividadDTO usuarioactividad = await (from t in _context.UsuarioActividad
                                                          where t.Id == id
                                                          select new UsuarioActividadDTO
                                                          {
                                                              ActividadId = t.ActividadId,
                                                              Id = t.Id,
                                                              Notas = t.Notas,
                                                              UsuarioId = t.UsuarioId
                                                          }).FirstAsync();
            return usuarioactividad;
        }

        public async Task<List<UsuarioActividadDTO>> ObtenerListado(int idActividad)
        {
            List<UsuarioActividadDTO> lista = await (from t in _context.UsuarioActividad
                                                     where t.ActividadId == idActividad
                                                     select new UsuarioActividadDTO
                                                     {
                                                         Id = t.Id,
                                                         ActividadId = t.ActividadId,
                                                         UsuarioId = t.UsuarioId
                                                     }).ToListAsync();
            return lista;
        }

        public async Task<List<UsuarioActividadDTO>> ObtenerListadoActividadesUsuario()
        {
            List<UsuarioActividadDTO> lista = await (from t in _context.UsuarioActividad
                                                     group t by t.ActividadId into g
                                                     select new UsuarioActividadDTO
                                                     {
                                                         ActividadId = g.Key
                                                     }
                                                     ).ToListAsync();

            return lista;
        }
    }
}

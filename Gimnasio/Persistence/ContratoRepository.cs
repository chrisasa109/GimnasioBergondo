using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Models;
using Gimnasio.Service;
using Gimnasio.Transporte;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Persistence
{
    public class ContratoRepository : IContratoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly SessionService _UsuarioSesion;
        public ContratoRepository(ApplicationDbContext context, SessionService sessionService)
        {
            _context = context;
            _UsuarioSesion = sessionService;
        }

        public ContratoDTO ComprobarContrato(int idUsuario)
        {
            ContratoDTO contrato = _context.Contrato
                .Where(t => t.UsuarioId == idUsuario)
                .Select(t => new ContratoDTO
                {
                    Comentarios = t.Comentarios,
                    FechaFin = t.FechaFin,
                    FechaInicio = t.FechaInicio,
                    FormaPago = (ContratoDTO.Metodo)t.FormaPago
                })
                .FirstOrDefault();
            return contrato;
        }

        public async Task<bool> ContratoActivado(int usuarioId)
        {
            bool result = await _context.Contrato.AnyAsync(c => c.UsuarioId == usuarioId && DateOnly.FromDateTime(DateTime.Now) < c.FechaFin);
            return result;
        }

        public async Task<ContratoDTO> ContratoPrevio(int tarifaId)
        {
            Usuario user = _UsuarioSesion.ObtenerUsuario();

            TarifaDTO tarifa = await (from t in _context.Tarifa
                                      where t.Id == tarifaId
                                      select new TarifaDTO
                                      {
                                          Nombre = t.Nombre,
                                          Descripcion = t.Descripcion,
                                          Precio = t.Precio,
                                      }).FirstOrDefaultAsync();

            ContratoDTO modelo = new ContratoDTO
            {
                UsuarioId = user.Id,
                _usuario = new UsuarioDTO
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    FechaNacimiento = user.FechaNacimiento,
                    Apellidos = user.Apellidos,
                    DNI = user.DNI,
                    Direccion = user.Direccion,
                    Poblacion = user.Poblacion,
                    Email = user.Email,
                    Telefono = user.Telefono
                },
                FechaInicio = DateOnly.FromDateTime(DateTime.Now),
                FechaFin = DateOnly.FromDateTime(DateTime.Now).AddMonths(1),
                TarifaID = tarifaId,
                _tarifa = tarifa
            };

            return modelo;
        }

        public async Task<bool> ContratoVigente()
        {
            bool result = await _context.Contrato.AnyAsync(c => c.UsuarioId == _UsuarioSesion.ObtenerUsuario().Id && DateOnly.FromDateTime(DateTime.Now) < c.FechaFin);
            return result;
        }

        public async Task<bool> GuardarCambiosNotas(string comentario)
        {
            try
            {
                Contrato? contrato = _context.Contrato.FirstOrDefault(c => c.UsuarioId == _UsuarioSesion.ObtenerUsuario().Id);
                contrato.Comentarios = comentario;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ContratoDTO> ObtenerContrato()
        {
            int usuarioId = _UsuarioSesion.ObtenerUsuario().Id;

            ContratoDTO contrato = await _context.Contrato
                .Where(t => t.UsuarioId == usuarioId)
                .Include(t => t._usuario)
                .Select(t => new ContratoDTO
                {
                    Comentarios = t.Comentarios,
                    FechaFin = t.FechaFin,
                    FechaInicio = t.FechaInicio,
                    FormaPago = (ContratoDTO.Metodo)t.FormaPago,
                    _usuario = new UsuarioDTO
                    {
                        Nombre = t._usuario.Nombre,
                        Apellidos = t._usuario.Apellidos,
                        DNI = t._usuario.DNI,
                        FechaNacimiento = t._usuario.FechaNacimiento,
                        Direccion = t._usuario.Direccion,
                        Poblacion = t._usuario.Poblacion,
                        Telefono = t._usuario.Telefono,
                        Email = t._usuario.Email,
                    }
                })
                .FirstOrDefaultAsync();
            return contrato;
        }

        public async Task<bool> RealizarContrato(ContratoDTO contratoFronted)
        {
            try
            {
                Contrato contract = new()
                {
                    Comentarios = contratoFronted.Comentarios,
                    FechaFin = contratoFronted.FechaFin,
                    FechaInicio = contratoFronted.FechaInicio,
                    FormaPago = (Contrato.Metodo)contratoFronted.FormaPago,
                    UsuarioId = contratoFronted.UsuarioId,
                    TarifaID = contratoFronted.TarifaID
                };
                _context.Contrato.AddAsync(contract);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}

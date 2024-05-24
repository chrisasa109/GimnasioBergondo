using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Models;
using Gimnasio.Service;
using Gimnasio.Transporte;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Gimnasio.Persistence
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly SessionService _sessionService;
        public PedidoRepository(ApplicationDbContext context, SessionService sessionService)
        {
            _context = context;
            _sessionService = sessionService;
        }

        public async Task<List<PedidoDTO>> ObtenerListaPedidosPorUsuario([Optional] int idUsuario)
        {
            if (idUsuario == 0) { idUsuario = _sessionService.ObtenerUsuario().Id; }
            List<PedidoDTO> listado = [];
            listado = await (from t in _context.Pedido
                             where t.UsuarioID == idUsuario
                             select new PedidoDTO
                             {
                                 Id = t.Id,
                                 UsuarioID = t.UsuarioID,
                                 Fecha = t.Fecha,
                                 FormaPago = (PedidoDTO.Metodo)t.FormaPago
                             }).ToListAsync();
            return listado;
        }

        public async Task<PedidoDTO> ObtenerPedidoPOrId(int idPedido)
        {
            try
            {
                PedidoDTO pedido = await (from t in _context.Pedido
                                          where t.Id == idPedido
                                          select new PedidoDTO
                                          {
                                              Id = t.Id,
                                              UsuarioID = t.UsuarioID,
                                              Fecha = t.Fecha,
                                              FormaPago = (PedidoDTO.Metodo)t.FormaPago
                                          }).FirstAsync();
                return pedido;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<PedidoDTO>> ObtenerTodosPedidos()
        {
            try
            {
                List<PedidoDTO> pedido = await(from t in _context.Pedido
                                         select new PedidoDTO
                                         {
                                             Id = t.Id,
                                             UsuarioID = t.UsuarioID,
                                             Fecha = t.Fecha,
                                             FormaPago = (PedidoDTO.Metodo)t.FormaPago
                                         }).ToListAsync();
                return pedido;
            }
            catch
            {
                return null;
            }
        }
    }
}

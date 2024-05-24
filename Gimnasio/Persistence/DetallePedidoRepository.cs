using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Models;
using Gimnasio.Transporte;
using Microsoft.EntityFrameworkCore;
using static Gimnasio.Models.Pedido;

namespace Gimnasio.Persistence
{
    public class DetallePedidoRepository : IDetallePedidoRepository
    {
        private readonly ApplicationDbContext _context;
        public DetallePedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DetallePedidoDTO>> ObtenerDetallePorPedidoId(int idPedido)
        {
            List<DetallePedidoDTO> lista = [];
            lista = await (from t in _context.DetallePedido
                           where t.PedidoId == idPedido
                           select new DetallePedidoDTO
                           {
                               Id = t.PedidoId,
                               PedidoId = t.PedidoId,
                               ProductoId = t.ProductoId,
                               Cantidad = t.Cantidad
                           }).ToListAsync();
            return lista;
        }

        public async Task<bool> ProcesarPedido(int idUsuario, PedidoDTO.Metodo metodoPago)
        {
            var metodoPagoString = metodoPago.ToString();
            if (Enum.TryParse(metodoPagoString, true, out Metodo metodoPagoPedido))
            {
                var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    Pedido pedido = new Pedido { Fecha = DateTime.Now, UsuarioID = idUsuario, FormaPago = (Pedido.Metodo)metodoPagoPedido };
                    _context.Pedido.Add(pedido);
                    await _context.SaveChangesAsync();
                    var carritos = _context.Carrito.Where(c => c.UsuarioId == idUsuario).ToList();
                    foreach (var carrito in carritos)
                    {
                        var nuevoDetalle = new DetallePedido
                        {
                            ProductoId = carrito.ProductoId,
                            Cantidad = carrito.Cantidad,
                            PedidoId = pedido.Id
                        };

                        _context.DetallePedido.Add(nuevoDetalle);
                        Producto producto = _context.Producto.FirstOrDefault(p => p.Id == nuevoDetalle.ProductoId);
                        producto.Stock -= nuevoDetalle.Cantidad;
                        _context.Carrito.Remove(carrito);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}

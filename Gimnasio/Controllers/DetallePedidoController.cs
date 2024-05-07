using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Gimnasio.Models.Pedido;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class DetallePedidoController : Controller
    {
        public DetallePedidoController(ApplicationDbContext context) { _context = context; }
        private readonly ApplicationDbContext _context;
        private Usuario ObtenerDatosPorCookies()
        {
            _ = int.TryParse(User.FindFirst("Id")?.Value, out int userId);
            var usuario = _context.Usuario.FirstOrDefault(x => x.Id == userId);
            return usuario;
        }

        public async Task<IActionResult> ProcesarPedido([FromBody] string formaPago)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                if (Enum.TryParse(formaPago, true, out Metodo metodoPago))
                {
                    var usuarioId = ObtenerDatosPorCookies().Id;

                    var nuevoPedido = new Pedido
                    {
                        Fecha = DateTime.Now,
                        UsuarioID = usuarioId,
                        FormaPago = metodoPago
                    };

                    _context.Pedido.Add(nuevoPedido);
                    await _context.SaveChangesAsync();

                    var carritos = _context.Carrito.Where(c => c.UsuarioId == usuarioId).ToList();

                    foreach (var carrito in carritos)
                    {
                        var nuevoDetalle = new DetallePedido
                        {
                            ProductoId = carrito.ProductoId,
                            Cantidad = carrito.Cantidad,
                            PedidoId = nuevoPedido.Id
                        };

                        _context.DetallePedido.Add(nuevoDetalle);
                        Producto producto = _context.Producto.FirstOrDefault(p => p.Id == nuevoDetalle.ProductoId);
                        producto.Stock-= nuevoDetalle.Cantidad;
                        _context.Carrito.Remove(carrito);
                    }

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    TempData["pedidoCompletado"] = "El pedido ha sido realizado correctamente.";

                    return Ok("Pedido procesado correctamente");
                }
                else
                {
                    return BadRequest("Método de pago no válido");
                }
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error al procesar el pedido: {ex.Message}");
            }
        }

        public IActionResult Index()
        {
            List<Pedido> pedidos = _context.Pedido.Where(p => p.UsuarioID == ObtenerDatosPorCookies().Id).ToList();
            List<DetallePedido> listaDetallePedido = [];
            if (pedidos.Count != 0)
            {
                foreach (var item in pedidos)
                {
                    List<DetallePedido> lista = _context.DetallePedido.Where(d => d.PedidoId == item.Id).ToList();
                    listaDetallePedido.AddRange(lista);
                }
            }
            else
            {
                TempData["infoPedidos"] = "No has realizado ningún pedido anteriormente.";
            }
            return View(listaDetallePedido);
        }
    }
}

using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        public PedidoController(ApplicationDbContext context) { _context = context; }
        private readonly ApplicationDbContext _context;
        private Usuario ObtenerDatosPorCookies()
        {
            _ = int.TryParse(User.FindFirst("Id")?.Value, out int userId);
            var usuario = _context.Usuario.FirstOrDefault(x => x.Id == userId);
            return usuario;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Pedido> lista = _context.Pedido.OrderByDescending(x=>x.Id).Where(p => p.UsuarioID == ObtenerDatosPorCookies().Id).Include(p => p.DetallesPedido).ToList();
            foreach (Pedido item in lista)
            {
                double precio = 0;
                foreach (DetallePedido item1 in item.DetallesPedido)
                {
                    item1._producto = _context.Producto.FirstOrDefault(x => x.Id == item1.ProductoId);
                    precio += item1._producto.Precio * item1.Cantidad;

                }
                item.PrecioTotal = precio;
            }
            return View(lista);
        }
        [HttpGet]
        public IActionResult Detalles(int IdPedido)
        {
            Pedido? pedido = _context.Pedido.FirstOrDefault(p => p.Id == IdPedido);
            if (pedido == null)
            {
                return NotFound();
            }
            else
            {
                pedido.DetallesPedido = _context.DetallePedido.Where(d => d.PedidoId == pedido.Id).ToList();
                pedido._usuario = _context.Usuario.FirstOrDefault(u => u.Id == pedido.UsuarioID);
                foreach (var item in pedido.DetallesPedido)
                {
                    item._producto = _context.Producto.FirstOrDefault(p => p.Id == item.ProductoId);
                }
                return View(pedido);
            }
        }
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public IActionResult IndexGestion()
        {
            List<Pedido> lista = _context.Pedido.OrderByDescending(x => x.Id).Include(p => p.DetallesPedido).ToList();
            foreach (Pedido item in lista)
            {
                double precio = 0;
                foreach (DetallePedido item1 in item.DetallesPedido)
                {
                    item1._producto = _context.Producto.FirstOrDefault(x => x.Id == item1.ProductoId);
                    precio += item1._producto.Precio * item1.Cantidad;

                }
                item._usuario = _context.Usuario.FirstOrDefault(u => u.Id == item.UsuarioID);
                item.PrecioTotal = precio;
            }
            return View("~/Views/Pedido/Index.cshtml", lista);
        }
    }
}
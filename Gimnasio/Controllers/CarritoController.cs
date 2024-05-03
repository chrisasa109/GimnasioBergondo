using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CarritoController(ApplicationDbContext context) { _context = context; }
        private Usuario ObtenerDatosPorCookies()
        {
            _ = int.TryParse(User.FindFirst("Id")?.Value, out int userId);
            var usuario = _context.Usuario.FirstOrDefault(x => x.Id == userId);
            return usuario;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(int productoId)
        {
            int idUsuario = ObtenerDatosPorCookies().Id;
            Carrito carritoExiste = _context.Carrito.FirstOrDefault(c => c.UsuarioId == idUsuario && c.ProductoId == productoId);
            if (carritoExiste != null)
            {
                carritoExiste.Cantidad += 1;
            }else
            {
                Carrito carrito = new Carrito
                {
                    UsuarioId = idUsuario,
                    ProductoId = productoId,
                    Cantidad = 1
                };
                await _context.Carrito.AddAsync(carrito);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Carrito");
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Carrito> carrito = [.. _context.Carrito.Where(c => c.UsuarioId == ObtenerDatosPorCookies().Id)
                .Include(a => a._producto)];
            return View(carrito);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            Carrito? fila = _context.Carrito.FirstOrDefault(f => f.Id == id);
            if (fila != null)
            {
                _context.Carrito.Remove(fila);
                await _context.SaveChangesAsync();
                TempData["eliminarProducto"] = "El producto se ha eliminado del carrito correctamente.";
                return Json(new { success = true });
            }
            TempData["eliminarProducto"] = "El producto no se ha podido eliminar del carrito.";
            return Json(new { success = false });
        }

        public class EnvioCambios
        {
            public required List<int> IdsCarrito { get; set; }
            public required List<int> Cantidades { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios([FromBody]  EnvioCambios modelo)
        {
            if(modelo != null && modelo.IdsCarrito!=null && modelo.Cantidades!=null && modelo.IdsCarrito.Count == modelo.Cantidades.Count)
            {
                for(int i = 0; i < modelo.IdsCarrito.Count; i++)
                {
                    int carritoId = modelo.IdsCarrito[i];
                    int cantidad = modelo.Cantidades[i];
                    Carrito carrito = _context.Carrito.FirstOrDefault(a => a.Id == carritoId);
                    if(carrito != null)
                    {
                        carrito.Cantidad = cantidad;
                        _context.Update(carrito);
                        await _context.SaveChangesAsync();
                    }
                }
                TempData["exitoGuardarCambios"] = "Los cambios se han guardado correctamente.";
                return Ok();
            }
            else
            {
                TempData["errorGuardarCambios"] = "Se ha producido un error en el momento de guardar los cambios.";
                return BadRequest();
            }
        }
    }
}

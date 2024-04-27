using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gimnasio.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CarritoController(ApplicationDbContext context) { _context = context; }
        private Usuario ObtenerDatosPorCookies()
        {
            int.TryParse(User.FindFirst("Id")?.Value, out int userId);
            var usuario = _context.Usuario.FirstOrDefault(x => x.Id == userId);
            return usuario;
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(int productoId)
        {
            Carrito carrito = new Carrito
            {
                UsuarioId = ObtenerDatosPorCookies().Id,
                ProductoId = productoId,
                Cantidad = 1
            };
            await _context.Carrito.AddAsync(carrito);
            await _context.SaveChangesAsync();
            return View(Index);
        }
        */
    }
}

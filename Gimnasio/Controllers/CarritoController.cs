using Gimnasio.Dates;
using Gimnasio.Dominio.IServices;
using Gimnasio.Models;
using Gimnasio.Service;
using Gimnasio.Transporte;
using Gimnasio.Transporte.Carrito;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ICarritoService _ICarritoService;
        public CarritoController(ICarritoService carritoService, ApplicationDbContext context)
        {
            _context = context;
            _ICarritoService = carritoService;
        }

        [HttpPost]
        public async Task<ActionResult> Agregar([FromBody] ProductoCarrito carro)
        {
            try
            {
                bool carroDTO = await _ICarritoService.AgregarCarro(carro);
                if (carroDTO)
                {
                    TempData["agregadoCorrectamente"] = "El producto se ha agregado correctamente al carrito.";
                }
                else
                {
                    TempData["malAgregado"] = "El producto no se ha podido agregar correctamente al carrito";
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<CarritoDTO> carrito = await _ICarritoService.ObtenerCarritoUsuario();
            return View(carrito);
        }

        [HttpDelete]
        public async Task<ActionResult> Eliminar(int id)
        {
            bool resultado = await _ICarritoService.EliminarProductoCarrito(id);
            if (resultado)
            {
                TempData["eliminarProducto"] = "El producto se ha eliminado del carrito correctamente.";
                return Json(new { success = true });
            }
            else
            {
                TempData["eliminarProducto"] = "El producto no se ha podido eliminar del carrito.";
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GuardarCambios([FromBody] EnvioCambios modelo)
        {
            if (modelo != null && modelo.IdsCarrito != null && modelo.Cantidades != null && modelo.IdsCarrito.Count == modelo.Cantidades.Count)
            {
                for (int i = 0; i < modelo.IdsCarrito.Count; i++)
                {
                    int carritoId = modelo.IdsCarrito[i];
                    int cantidad = modelo.Cantidades[i];
                    Carrito carrito = _context.Carrito.FirstOrDefault(a => a.Id == carritoId);
                    if (carrito != null)
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

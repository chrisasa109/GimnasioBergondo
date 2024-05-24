using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;
using Gimnasio.Transporte.Carrito;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class CarritoController : Controller
    {
        private readonly ICarritoService _ICarritoService;
        public CarritoController(ICarritoService carritoService)
        {
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
        public async Task<ActionResult> GuardarCambios([FromBody] EnvioCambios modelo)
        {
            if (modelo != null && modelo.IdsCarrito != null && modelo.Cantidades != null && modelo.IdsCarrito.Count == modelo.Cantidades.Count)
            {
                bool result = await _ICarritoService.ActualizarCarrito(modelo);
                if (result)
                {
                    TempData["exitoGuardarCambios"] = "Los cambios se han guardado correctamente.";
                    return Ok();
                }
            }
            TempData["errorGuardarCambios"] = "Se ha producido un error en el momento de guardar los cambios.";
            return BadRequest();
        }

    }
}

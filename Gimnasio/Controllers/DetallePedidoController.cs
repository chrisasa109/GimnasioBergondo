using Gimnasio.Dates;
using Gimnasio.Dominio.IServices;
using Gimnasio.Models;
using Gimnasio.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Gimnasio.Transporte.PedidoDTO;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class DetallePedidoController : Controller
    {
        private readonly IDetallePedidoService _IDetallePedidoService;
        public DetallePedidoController(IDetallePedidoService detallePedidoService)
        {
            _IDetallePedidoService = detallePedidoService;
        }

        public async Task<ActionResult> ProcesarPedido([FromBody] string formaPago)
        {

            try
            {
                if (Enum.TryParse(formaPago, true, out Metodo metodoPago))
                {
                    bool realizado = await _IDetallePedidoService.RealizarPedido(metodoPago);
                    if (realizado)
                    {
                        TempData["pedidoCompletado"] = "El pedido ha sido realizado correctamente.";
                        return Ok("Pedido procesado correctamente.");
                    }
                    else
                    {
                        return BadRequest("No se ha podido procesar el pedido");
                    }
                }
                else
                {
                    return BadRequest("Método de pago no válido");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al procesar el pedido: {ex.Message}");
            }
        }
    }
}

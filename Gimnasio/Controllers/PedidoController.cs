using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly IPedidoService _IPedidoService;
        public PedidoController(IPedidoService pedidoService)
        {
            _IPedidoService = pedidoService;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<PedidoDTO> lista = await _IPedidoService.ObtenerListaPedidosPorUsuario();
            return View(lista);
        }
        [HttpGet]
        public async Task<ActionResult> Detalles(int IdPedido)
        {
            PedidoDTO? pedido = await _IPedidoService.ObtenerDatosPedido(IdPedido);
            if (pedido == null)
            {
                return NotFound();
            }
            else
            {
                return View(pedido);
            }
        }
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public async Task<ActionResult> IndexGestion()
        {
            List<PedidoDTO> lista = await _IPedidoService.ObtenerTodosPedidos();
            return View("~/Views/Pedido/Index.cshtml", lista);
        }
    }
}
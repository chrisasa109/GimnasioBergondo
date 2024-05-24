using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;

namespace Gimnasio.Service
{
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly IDetallePedidoRepository _IDetallePedidoRepository;
        private readonly SessionService _SessionService;
        public DetallePedidoService(IDetallePedidoRepository iDetallePedidoRepository, SessionService sessionService)
        {
            _IDetallePedidoRepository = iDetallePedidoRepository;
            _SessionService = sessionService;
        }

        public async Task<bool> RealizarPedido(PedidoDTO.Metodo metodoPago)
        {
            int idUsuario = _SessionService.ObtenerUsuario().Id;
            bool respuesta = await _IDetallePedidoRepository.ProcesarPedido(idUsuario, metodoPago);
            return respuesta;
        }
    }
}

using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;

namespace Gimnasio.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _IPedidoRepository;
        private readonly IProductoRepository _IProductoRepository;
        private readonly IUsuarioRepository _IUsuarioRepository;
        private readonly IDetallePedidoRepository _IDetallePedidoRepository;
        public PedidoService(IPedidoRepository iPedidoRepository, IProductoRepository productoRepository, IDetallePedidoRepository detallePedidoRepository, IUsuarioRepository usuarioRepository)
        {
            _IPedidoRepository = iPedidoRepository;
            _IDetallePedidoRepository = detallePedidoRepository;
            _IProductoRepository = productoRepository;
            _IUsuarioRepository = usuarioRepository;
        }

        public async Task<PedidoDTO?> ObtenerDatosPedido(int idPedido)
        {
            PedidoDTO pedido = await _IPedidoRepository.ObtenerPedidoPOrId(idPedido);
            pedido._usuario = await _IUsuarioRepository.ConsultarUsuario(pedido.UsuarioID);
            pedido.DetallesPedido = await _IDetallePedidoRepository.ObtenerDetallePorPedidoId(idPedido);
            foreach (DetallePedidoDTO item in pedido.DetallesPedido)
            {
                item._producto = await _IProductoRepository.ObtenerProducto(item.ProductoId);
            }
            return pedido;
        }

        public async Task<List<PedidoDTO>> ObtenerListaPedidosPorUsuario()
        {
            List<PedidoDTO> lista = [];
            lista = await _IPedidoRepository.ObtenerListaPedidosPorUsuario();
            lista = await CompletarDatosListaPedidos(lista);
            return lista;
        }

        public async Task<List<PedidoDTO>> ObtenerTodosPedidos()
        {
            List<PedidoDTO> lista = await _IPedidoRepository.ObtenerTodosPedidos();
            lista = await CompletarDatosListaPedidos(lista);
            return lista;
        }

        private async Task<List<PedidoDTO>> CompletarDatosListaPedidos(List<PedidoDTO> lista)
        {
            foreach (PedidoDTO item in lista)
            {
                double precio = 0;
                item.DetallesPedido = await _IDetallePedidoRepository.ObtenerDetallePorPedidoId(item.Id);
                foreach (DetallePedidoDTO item1 in item.DetallesPedido)
                {
                    item1._producto = await _IProductoRepository.ObtenerProducto(item1.ProductoId);
                    precio += item1._producto.Precio * item1.Cantidad;
                }
                item.PrecioTotal = precio;
            }
            return lista;
        }
    }
}

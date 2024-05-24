using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IServices
{
    public interface IPedidoService
    {
        Task<PedidoDTO?> ObtenerDatosPedido(int idPedido);
        Task<List<PedidoDTO>> ObtenerListaPedidosPorUsuario();
        Task<List<PedidoDTO>> ObtenerTodosPedidos();
    }
}

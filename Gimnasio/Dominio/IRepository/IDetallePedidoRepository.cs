using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IRepository
{
    public interface IDetallePedidoRepository
    {
        Task<List<DetallePedidoDTO>> ObtenerDetallePorPedidoId(int id);
        Task<bool> ProcesarPedido(int idUsuario, PedidoDTO.Metodo metodoPago);
    }
}

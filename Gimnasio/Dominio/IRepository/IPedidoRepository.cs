using Gimnasio.Transporte;
using System.Runtime.InteropServices;

namespace Gimnasio.Dominio.IRepository
{
    public interface IPedidoRepository
    {
        Task<List<PedidoDTO>> ObtenerListaPedidosPorUsuario([Optional] int idUsuario);
        Task<PedidoDTO> ObtenerPedidoPOrId(int idPedido);
        Task<List<PedidoDTO>> ObtenerTodosPedidos();
    }
}

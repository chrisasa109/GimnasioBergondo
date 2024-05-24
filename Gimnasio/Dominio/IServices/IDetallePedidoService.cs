using Gimnasio.Models;
using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IServices
{
    public interface IDetallePedidoService
    {
        Task<bool> RealizarPedido(PedidoDTO.Metodo metodoPago);
    }
}

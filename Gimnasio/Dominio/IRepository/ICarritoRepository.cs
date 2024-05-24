using Gimnasio.Transporte;
using Gimnasio.Transporte.Carrito;

namespace Gimnasio.Dominio.IRepository
{
    public interface ICarritoRepository
    {
        Task<bool> ActualizarCarrito(CarritoDTO encontrado, int cantidad);
        Task<bool> AgregarCantidad(CarritoDTO? carrito, ProductoCarrito carro);
        Task<bool> AgregarTupla(ProductoCarrito? carro, int idUsuario);
        Task<CarritoDTO?> BuscarTupla(ProductoCarrito carro, int id);
        Task<bool> EliminarFila(CarritoDTO fila);
        Task<List<CarritoDTO>> ObtenerCarritoUsuario(int id);
        Task<CarritoDTO> ObtenerFila(int id);
    }
}

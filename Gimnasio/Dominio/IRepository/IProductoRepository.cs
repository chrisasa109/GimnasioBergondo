using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IRepository
{
    public interface IProductoRepository
    {
        Task<bool> ActualizarProducto(ProductoDTO almacenado);
        Task<bool> AgregarProducto(ProductoDTO producto);
        Task<ProductoDTO> ObtenerProducto(int idProducto);
        Task<List<ProductoDTO>> ObtenerTodosProductos();
        Task<List<ProductoDTO>> ObtenerTodosProductosDisponibles();
    }
}

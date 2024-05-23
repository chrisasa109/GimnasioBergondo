using Gimnasio.Transporte;

namespace Gimnasio.Dominio.IServices
{
    public interface IProductoService
    {
        Task<bool> ActualizarProducto(ProductoDTO almacenado);
        Task<bool> AgregarProducto(ProductoDTO producto);
        Task<ProductoDTO> CrearProductoDTO(string nombre, double precio, int stock, IFormFile foto);
        Task<ProductoDTO> ObtenerProducto(int idProducto);
        Task<List<ProductoDTO>> ObtenerTodosProductos();
        Task<List<ProductoDTO>> ObtenerTodosProductosDisponibles();
    }
}

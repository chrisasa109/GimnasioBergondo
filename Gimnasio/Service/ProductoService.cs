using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;

namespace Gimnasio.Service
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _IProductoRepository;
        public ProductoService(IProductoRepository iProductoRepository)
        {
            _IProductoRepository = iProductoRepository;
        }

        public async Task<bool> ActualizarProducto(ProductoDTO almacenado)
        {
            return await _IProductoRepository.ActualizarProducto(almacenado);
        }

        public async Task<bool> AgregarProducto(ProductoDTO producto)
        {
            return await _IProductoRepository.AgregarProducto(producto);
        }

        public async Task<ProductoDTO> CrearProductoDTO(string nombre, double precio, int stock, IFormFile foto)
        {
            ProductoDTO producto = new()
            {
                Nombre = nombre,
                Precio = precio,
                Stock = stock
            };

            if (foto != null && foto.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await foto.CopyToAsync(memoryStream);
                    producto.Foto = memoryStream.ToArray();
                }
            }
            return producto;
        }

        public async Task<ProductoDTO> ObtenerProducto(int idProducto)
        {
            return await _IProductoRepository.ObtenerProducto(idProducto);
        }

        public async Task<List<ProductoDTO>> ObtenerTodosProductos()
        {
            return await _IProductoRepository.ObtenerTodosProductos();
        }

        public async Task<List<ProductoDTO>> ObtenerTodosProductosDisponibles()
        {
            return await _IProductoRepository.ObtenerTodosProductosDisponibles();
        }
    }
}

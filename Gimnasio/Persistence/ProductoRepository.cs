using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Models;
using Gimnasio.Transporte;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Persistence
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        private async Task<List<ProductoDTO>> TodosProductos()
        {
            return await (from t in _context.Producto
                                             select new ProductoDTO
                                             {
                                                 Id = t.Id,
                                                 Nombre = t.Nombre,
                                                 Precio = t.Precio,
                                                 Stock = t.Stock,
                                                 Foto = t.Foto
                                             }).ToListAsync();
        }

        public async Task<List<ProductoDTO>> ObtenerTodosProductos()
        {
            List<ProductoDTO> result = await TodosProductos();
            result.Where(p => p.Stock > 0);
            return result;
        }

        public async Task<List<ProductoDTO>> ObtenerTodosProductosDisponibles()
        {
            var result = await TodosProductos();
            return result.Where(p => p.Stock > 0).ToList();
        }

        public async Task<ProductoDTO> ObtenerProducto(int idProducto)
        {
            ProductoDTO producto = await (from t in _context.Producto
                                          where t.Id == idProducto
                                          select new ProductoDTO
                                          {
                                              Id = t.Id,
                                              Nombre = t.Nombre,
                                              Precio = t.Precio,
                                              Stock = t.Stock,
                                              Foto = t.Foto
                                          }).FirstOrDefaultAsync();
            return producto;
        }

        public async Task<bool> AgregarProducto(ProductoDTO producto)
        {
            try
            {
                Producto prod = new()
                {
                    Nombre = producto.Nombre,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    Foto = producto.Foto
                };
                await _context.Producto.AddAsync(prod);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ActualizarProducto(ProductoDTO almacenado)
        {
            try
            {
                Producto prod = await _context.Producto.FirstAsync(p => p.Id == almacenado.Id);
                prod.Nombre = almacenado.Nombre;
                prod.Precio = almacenado.Precio;
                prod.Stock = almacenado.Stock;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

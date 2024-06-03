using Gimnasio.Dates;
using Gimnasio.Dominio.IRepository;
using Gimnasio.Models;
using Gimnasio.Transporte;
using Gimnasio.Transporte.Carrito;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Persistence
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductoRepository _IProductoRepository;
        public CarritoRepository(ApplicationDbContext context, IProductoRepository productoRepository)
        {
            _context = context;
            _IProductoRepository = productoRepository;
        }

        public async Task<bool> ActualizarCarrito(CarritoDTO encontrado, int cantidad)
        {
            try
            {
                Carrito? carrito = await _context.Carrito.FirstOrDefaultAsync(c => c.Id == encontrado.Id);
                if (cantidad != null)
                {
                    carrito.Cantidad = cantidad;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AgregarCantidad(CarritoDTO? carrito, ProductoCarrito carro)
        {
            try
            {
                Carrito carr = _context.Carrito.Where(c => c.Id == carrito.Id).FirstOrDefault();
                carr.Cantidad += carro.Cantidad;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AgregarTupla(ProductoCarrito? carro, int idUsuario)
        {
            if (carro == null)
            {
                return false;
            }

            try
            {
                _context.Carrito.Add(new Carrito
                {
                    ProductoId = carro.idProducto,
                    UsuarioId = idUsuario,
                    Cantidad = carro.Cantidad
                });

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<CarritoDTO?> BuscarTupla(ProductoCarrito carro, int id)
        {
            var carrito = await (from t in _context.Carrito
                                 where t.UsuarioId == id && t.ProductoId == carro.idProducto
                                 select new CarritoDTO
                                 {
                                     Id = t.Id,
                                     ProductoId = t.ProductoId,
                                     UsuarioId = t.UsuarioId,
                                     Cantidad = carro.Cantidad
                                 }).FirstOrDefaultAsync();
            if (carrito == null)
            {
                return null;
            }
            return carrito;
        }

        public async Task<bool> EliminarFila(CarritoDTO fila)
        {
            try
            {
                _context.Carrito.Remove(await _context.Carrito.FirstAsync(c => c.Id == fila.Id));
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public async Task<List<CarritoDTO>> ObtenerCarritoUsuario(int id)
        {
            List<CarritoDTO> carrito = await (from t in _context.Carrito
                                              where t.UsuarioId == id
                                              select new CarritoDTO
                                              {
                                                  Id = t.Id,
                                                  UsuarioId = t.UsuarioId,
                                                  ProductoId = t.ProductoId,
                                                  Cantidad = t.Cantidad,
                                              }).ToListAsync();
            foreach (CarritoDTO carritoDTO in carrito)
            {
                carritoDTO._producto = await _IProductoRepository.ObtenerProducto(carritoDTO.ProductoId);
            }
            return carrito;
        }

        public async Task<CarritoDTO> ObtenerFila(int id)
        {
            CarritoDTO carrito = await (from t in _context.Carrito
                                        where t.Id == id
                                        select new CarritoDTO
                                        {
                                            Id = t.Id,
                                            UsuarioId = t.UsuarioId,
                                            ProductoId = t.ProductoId,
                                            Cantidad = t.Cantidad
                                        }).FirstAsync();
            if (carrito == null)
            {
                return null;
            }
            else
            {
                return carrito;
            }
        }
    }
}

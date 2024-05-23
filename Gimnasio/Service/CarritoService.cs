using Gimnasio.Dominio.IRepository;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;
using Gimnasio.Transporte.Carrito;

namespace Gimnasio.Service
{
    public class CarritoService : ICarritoService
    {
        private ICarritoRepository _ICarritoRepository;
        private SessionService _SessionService;
        public CarritoService(ICarritoRepository icarritoRepository, SessionService sessionService)
        {
            _ICarritoRepository = icarritoRepository;
            _SessionService = sessionService;

        }

        public async Task<bool> AgregarCarro(ProductoCarrito carro)
        {
            int idUsuario = _SessionService.ObtenerUsuario().Id;
            CarritoDTO? carrito = await _ICarritoRepository.BuscarTupla(carro, idUsuario);
            bool realizado;
            if (carrito != null)
            {
                realizado = await _ICarritoRepository.AgregarCantidad(carrito, carro);
            }
            else
            {
                realizado = await _ICarritoRepository.AgregarTupla(carro, idUsuario);
            }
            return realizado;
        }

        public async Task<bool> EliminarProductoCarrito(int id)
        {
            CarritoDTO fila = await _ICarritoRepository.ObtenerFila(id);
            if(fila == null)
            {
                return false;
            }
            else
            {
                bool result = await _ICarritoRepository.EliminarFila(fila);
                if(result)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<List<CarritoDTO>> ObtenerCarritoUsuario()
        {
            return await _ICarritoRepository.ObtenerCarritoUsuario(_SessionService.ObtenerUsuario().Id);

        }

        
    }
}

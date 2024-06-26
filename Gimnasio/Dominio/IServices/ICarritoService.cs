﻿using Gimnasio.Transporte;
using Gimnasio.Transporte.Carrito;

namespace Gimnasio.Dominio.IServices
{
    public interface ICarritoService
    {
        Task<bool> ActualizarCarrito(EnvioCambios modelo);
        Task<bool> AgregarCarro(ProductoCarrito carro);
        Task<bool> EliminarProductoCarrito(int id);
        Task<List<CarritoDTO>> ObtenerCarritoUsuario();
        
    }
}

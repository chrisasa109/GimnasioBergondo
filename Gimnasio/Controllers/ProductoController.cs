using Gimnasio.Dates;
using Gimnasio.Dominio.IServices;
using Gimnasio.Models;
using Gimnasio.Transporte;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _IProductoService;
        public ProductoController(IProductoService productoService)
        {
            _IProductoService = productoService;
        }

        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        [HttpPost]
        public async Task<ActionResult> Create(string nombre, double precio, int stock, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                ProductoDTO producto = await _IProductoService.CrearProductoDTO(nombre, precio, stock, foto);
                bool resultado = await _IProductoService.AgregarProducto(producto);
                if (resultado)
                {
                    TempData["ProductoBienCreado"] = "El producto se ha creado correctamente";
                }
                else
                {
                    TempData["ProductoMalCreado"] = "No se ha podido crear el producto";
                }
                return RedirectToAction("Detalles", "Producto");
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            List<ProductoDTO> productos = await _IProductoService.ObtenerTodosProductosDisponibles();
            return View(productos);
        }
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public async Task<ActionResult> Detalles()
        {
            List<ProductoDTO> productos = await _IProductoService.ObtenerTodosProductos();
            return View(productos);
        }
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public async Task<ActionResult> Editar(int idProducto)
        {
            ProductoDTO prod = await _IProductoService.ObtenerProducto(idProducto);
            return PartialView("~/Views/Shared/_EditarProducto.cshtml", prod);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id,Nombre, Precio,Stock")] ProductoDTO prod)
        {
            try
            {
                ProductoDTO almacenado = await _IProductoService.ObtenerProducto(prod.Id);
                if (almacenado == null)
                {
                    TempData["NoEncontrado"] = "No se ha podido encontrar el producto.";
                }
                bool actualizado = await _IProductoService.ActualizarProducto(prod);
                if (actualizado)
                {
                    TempData["Actualizado"] = "El producto se ha actualizado correctamente.";
                }
                else
                {
                    TempData["NoActualizado"] = "No se ha podido actualizar el producto";
                }
                return RedirectToAction("Detalles", "Producto");
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Error al actualizar el producto.");
            }

        }
    }
}

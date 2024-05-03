﻿using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductoController(ApplicationDbContext context) { _context = context; }

        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        [HttpPost]
        public async Task<IActionResult> Create(string nombre, double precio, int stock, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                var producto = new Producto
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

                _context.Producto.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var productos = _context.Producto.Where(p => p.Stock > 0).ToList();
            return View(productos);
        }
        [HttpGet]
        public IActionResult MostrarImagen(int id)
        {
            var producto = _context.Producto.FirstOrDefault(p => p.Id == id);
            if (producto != null && producto.Foto != null)
            {
                return File(producto.Foto, "image/jpeg");
            }
            else
            {
                // Manejar el caso en que la imagen no se encuentra
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Detalles()
        {
            return View(_context.Producto.ToList());
        }
    }
}

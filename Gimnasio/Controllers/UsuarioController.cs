using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuarioController(ApplicationDbContext context) {  _context = context; }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro([Bind("Nombre,DNI,Apellidos,FechaNacimiento,Direccion,Poblacion,Telefono,Email,Password,ConfirmPassword,FotoFronted")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Password = new PasswordHasher<Usuario>().HashPassword(usuario, usuario.Password);
                if (usuario.FotoFronted != null && usuario.FotoFronted.Length > 0)
                {
                    using var memoryStream = new MemoryStream();
                    await usuario.FotoFronted.CopyToAsync(memoryStream);
                    usuario.Foto = memoryStream.ToArray();
                }
                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();
                TempData["iniciarSesion"] = "Debes de iniciar sesión para acceder a la plataforma.";
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Modificacion()
        {
            Usuario user = ObtenerDatosPorCookies();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificacion([Bind("Id,Nombre,DNI,Apellidos,FechaNacimiento,Direccion,Poblacion,Telefono,Email,FotoFronted")] Usuario usuario)
        {
            var usuarioExistente = await _context.Usuario.FindAsync(usuario.Id);

            if (usuarioExistente == null)
            {
                return NotFound();
            }
            if (usuario.FotoFronted != null && usuario.FotoFronted.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await usuario.FotoFronted.CopyToAsync(memoryStream);
                    usuario.Foto = memoryStream.ToArray();
                    usuarioExistente.Foto = usuario.Foto;
                }
            }
            // Excluir estos dos campos de ser actualizados
            usuarioExistente.Rol = usuarioExistente.Rol; 
            usuarioExistente.Password = usuarioExistente.Password; 

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.DNI = usuario.DNI;
            usuarioExistente.Apellidos = usuario.Apellidos;
            usuarioExistente.FechaNacimiento = usuario.FechaNacimiento;
            usuarioExistente.Direccion = usuario.Direccion;
            usuarioExistente.Poblacion = usuario.Poblacion;
            usuarioExistente.Telefono = usuario.Telefono;
            usuarioExistente.Email = usuario.Email;
            

            _context.Update(usuarioExistente);
            await _context.SaveChangesAsync();

            TempData["cambioCorrecto"] = "Los cambios se han guardado correctamente";
            return RedirectToAction("Detalles", "Usuario");
        }

        [HttpGet]
        public IActionResult Detalles()
        {
            Usuario user = ObtenerDatosPorCookies();
            return View(user);
        }

        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                int? userIdFromCookie = ObtenerDatosPorCookies().Id;
                id = userIdFromCookie;
            }
            Usuario? usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == id);

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarUsuario(int? id)
        {
            if (id == null)
            {
                id = ObtenerDatosPorCookies().Id;
            }
            Usuario? us = await _context.Usuario.FindAsync(id);
            if(us != null)
            {
                _context.Usuario.Remove(us);
                await _context.SaveChangesAsync();
                return RedirectToAction("Salir", "Login");
            }
            return RedirectToAction("Index", "Home");
        }
        
        private Usuario ObtenerDatosPorCookies()
        {
            int.TryParse(User.FindFirst("Id")?.Value, out int userId);
            var usuario = _context.Usuario.FirstOrDefault(x => x.Id == userId);
            return usuario;
        }

    }
}

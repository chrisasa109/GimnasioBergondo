using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Registro([Bind("Nombre,DNI,Apellidos,FechaNacimiento,Direccion,Poblacion,Telefono,Email,Password,ConfirmPassword")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.Password = new PasswordHasher<Usuario>().HashPassword(usuario, usuario.Password);
                _context.Usuario.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> Modificacion([Bind("Id,Nombre,DNI,Apellidos,FechaNacimiento,Direccion,Poblacion,Telefono,Email")] Usuario usuario)
        {
            var usuarioExistente = await _context.Usuario.FindAsync(usuario.Id);

            if (usuarioExistente == null)
            {
                return NotFound();
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

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Detalles()
        {
            Usuario user = ObtenerDatosPorCookies();
            return View(user);
        }

        private Usuario ObtenerDatosPorCookies()
        {
            int.TryParse(User.FindFirst("Id")?.Value, out int userId);
            var usuario = _context.Usuario.FirstOrDefault(x => x.Id == userId);
            return usuario;
        }
    }
}

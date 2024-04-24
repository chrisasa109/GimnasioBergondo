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
            int.TryParse(User.FindFirst("Id")?.Value, out int userId);
            var cliente = _context.Usuario.FirstOrDefault(x => x.Id == userId);
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificacion([Bind("Id,Nombre,DNI,Apellidos,FechaNacimiento,Direccion,Poblacion,Telefono,Email")] Usuario usuario)
        {
            // Buscar el usuario existente en la base de datos
            var usuarioExistente = await _context.Usuario.FindAsync(usuario.Id);

            if (usuarioExistente == null)
            {
                return NotFound(); // Manejar el caso en que el usuario no existe
            }

            // Excluir los campos "Rol" y "Password" del proceso de actualización
            usuarioExistente.Rol = usuarioExistente.Rol; // Marcar como no modificado
            usuarioExistente.Password = usuarioExistente.Password; // Marcar como no modificado

            // Actualizar el resto de los campos
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.DNI = usuario.DNI;
            usuarioExistente.Apellidos = usuario.Apellidos;
            usuarioExistente.FechaNacimiento = usuario.FechaNacimiento;
            usuarioExistente.Direccion = usuario.Direccion;
            usuarioExistente.Poblacion = usuario.Poblacion;
            usuarioExistente.Telefono = usuario.Telefono;
            usuarioExistente.Email = usuario.Email;

            // Marcar la entidad como modificada y guardar los cambios en la base de datos
            _context.Update(usuarioExistente);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index"); // Otra acción después de la modificación
        }

    }
}

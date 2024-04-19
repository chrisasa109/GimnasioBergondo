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

    }
}

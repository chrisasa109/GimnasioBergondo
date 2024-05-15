using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
        [Authorize]
        [HttpGet]
        public IActionResult Modificacion(int? id)
        {
            if (id == null)
            {
                int? userIdFromCookie = ObtenerDatosPorCookies().Id;
                id = userIdFromCookie;
            }
            else if (User.IsInRole("CLIENTE"))
            {
                return Forbid(); 
            }
            Usuario user = _context.Usuario.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar([Bind("Id,Nombre,DNI,Apellidos,FechaNacimiento,Direccion,Poblacion,Telefono,Email,FotoFronted")] Usuario usuario)
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
            return RedirectToAction("Detalles", "Usuario", new { id = usuarioExistente.Id });
        }

        [HttpGet]
        [Authorize]
        public IActionResult Detalles(int? id)
        {
            if (id == null || User.IsInRole("CLIENTE"))
            {
                int? userIdFromCookie = ObtenerDatosPorCookies().Id;
                id = userIdFromCookie;
            }
            Usuario user = _context.Usuario.First(u => u.Id == id);
            return View(user);
        }
        [Authorize]
        public async Task<IActionResult> Eliminar(int? id)
        {

            if (id == null || User.IsInRole("CLIENTE"))
            {
                int? userIdFromCookie = ObtenerDatosPorCookies().Id;
                id = userIdFromCookie;
            }
            Usuario? usuario = await _context.Usuario.FirstOrDefaultAsync(m => m.Id == id);

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
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
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public IActionResult ListaUsuarios()
        {
            List<Usuario> listaUsuarios = [];
            if (User.IsInRole("ADMINISTRADOR"))
            {
                listaUsuarios = _context.Usuario.ToList();
            }
            else
            {
                listaUsuarios = _context.Usuario.Where(u => u.Rol == Usuario.Role.CLIENTE).ToList();
            }
            IEnumerable<Usuario> EnumerableUsuario = listaUsuarios.AsEnumerable();
            return View(EnumerableUsuario);
        }

        public class AsignacionRolFront
        {
            public required int idUsuario {  get; set; }
            public required string rolFronted {  get; set; }
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public async Task<IActionResult> AsignarRol([FromBody] AsignacionRolFront recepcion)
        {
            Usuario userMod = _context.Usuario.Find(recepcion.idUsuario);
            if(Enum.TryParse(recepcion.rolFronted, true, out Usuario.Role rolFormateado))
            {
                userMod.Rol = rolFormateado;
                _context.SaveChangesAsync();
                TempData["cambioRol"] = "El rol de " + userMod.Nombre + userMod.Apellidos + " se ha cambiado correctamente.";
                return Ok();
            }
            return BadRequest();
        }
        private Usuario ObtenerDatosPorCookies()
        {
            int.TryParse(User.FindFirst("Id")?.Value, out int userId);
            var usuario = _context.Usuario.FirstOrDefault(x => x.Id == userId);
            return usuario;
        }

    }
}

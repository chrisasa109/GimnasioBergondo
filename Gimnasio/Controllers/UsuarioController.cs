using Gimnasio.Dates;
using Gimnasio.Dominio.IServices;
using Gimnasio.Models;
using Gimnasio.Service;
using Gimnasio.Transporte;
using Gimnasio.Transporte.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _IUsuarioService;
        private readonly ApplicationDbContext _context;
        private readonly SessionService _usuarioService;
        public UsuarioController(ApplicationDbContext context, SessionService usuarioService, IUsuarioService iUsuarioService)
        {
            _context = context;
            _usuarioService = usuarioService;
            _IUsuarioService = iUsuarioService;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(IFormCollection collection, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioDTO usuarioFront = Formulario(collection);
                    bool respuesta = await _IUsuarioService.RegistrarUsuario(usuarioFront);
                    if (respuesta)
                    {
                        TempData["iniciarSesion"] = "Debes de iniciar sesión para acceder a la plataforma.";
                        return RedirectToAction("Index", "Login");
                    }
                }
                catch { }
            }
            return View();
        }

        private UsuarioDTO Formulario(IFormCollection collection)
        {
            UsuarioDTO usuario = new UsuarioDTO
            {
                Nombre = collection["Nombre"],
                Apellidos = collection["Apellidos"],
                DNI = collection["DNI"],
                Direccion = collection["Direccion"],
                Poblacion = collection["Poblacion"],
                Telefono = collection["Telefono"],
                Email = collection["Email"],
                Password = collection["Password"]
            };

            if (DateOnly.TryParse(collection["FechaNacimiento"], out var fechaNacimiento))
            {
                usuario.FechaNacimiento = fechaNacimiento;
            }

            if (collection.Files["FotoFronted"] != null && collection.Files["FotoFronted"].Length > 0)
            {
                var foto = collection.Files["FotoFronted"];
                using (var memoryStream = new MemoryStream())
                {
                    foto.CopyTo(memoryStream);
                    usuario.Foto = memoryStream.ToArray();
                }
            }
            if (int.TryParse(collection["Id"], out var id))
            {
                if(id != null) {  usuario.Id = id; }
            }
            return usuario;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Modificacion(int? id)
        {
            UsuarioDTO usuario = await _IUsuarioService.ConsultarUsuario(AsignarID(id));
            return View(usuario);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(IFormCollection collection)
        {
            try
            {
                UsuarioDTO usuario = Formulario(collection);
                bool respuesta = await _IUsuarioService.ModificarUsuario(usuario);
                TempData["cambioCorrecto"] = "Los cambios se han guardado correctamente";
                return RedirectToAction("Detalles", "Usuario", new { id = usuario.Id });
            }
            catch { }
            return View("Modificacion", "Usuario");
           /* var usuarioExistente = await _context.Usuario.FindAsync(usuario.Id);

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
           */
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Detalles(int? id)
        {
            UsuarioDTO usuario = await _IUsuarioService.ConsultarUsuario(AsignarID(id));
            return View(usuario);
        }
        [Authorize]
        public async Task<IActionResult> Eliminar(int? id)
        {
            UsuarioDTO usuario = await _IUsuarioService.ConsultarUsuario(AsignarID(id));
            return View(usuario);
        }
        private int AsignarID(int? id)
        {
            if (id == null || User.IsInRole("CLIENTE"))
            {
                return _usuarioService.ObtenerUsuario().Id;
            }
            return (int)id;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EliminarUsuario(int? id)
        {
            if (id == null)
            {
                id = _usuarioService.ObtenerUsuario().Id;
            }
            Usuario? us = await _context.Usuario.FindAsync(id);
            if (us != null)
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

        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public async Task<IActionResult> AsignarRol([FromBody] AsignacionRolFront recepcion)
        {
            Usuario userMod = _context.Usuario.Find(recepcion.idUsuario);
            if (Enum.TryParse(recepcion.rolFronted, true, out Usuario.Role rolFormateado))
            {
                userMod.Rol = rolFormateado;
                _context.SaveChangesAsync();
                TempData["cambioRol"] = "El rol de " + userMod.Nombre + userMod.Apellidos + " se ha cambiado correctamente.";
                return Ok();
            }
            return BadRequest();
        }
    }
}

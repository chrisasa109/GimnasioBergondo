using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;
using Gimnasio.Transporte.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gimnasio.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _IUsuarioService;
        public UsuarioController(IUsuarioService iUsuarioService)
        {
            _IUsuarioService = iUsuarioService;
        }

        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(IFormCollection collection)
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
                if (id != null) { usuario.Id = id; }
            }
            return usuario;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Modificacion(int? id)
        {
            UsuarioDTO usuario = await _IUsuarioService.ConsultarUsuario(id);
            return View(usuario);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Modificar(IFormCollection collection)
        {
            try
            {
                UsuarioDTO usuario = Formulario(collection);
                bool respuesta = await _IUsuarioService.ModificarUsuario(usuario);
                TempData["cambioCorrecto"] = "Los cambios se han guardado correctamente";
                return RedirectToAction("Detalles", "Usuario", new { id = usuario.Id });
            }
            catch
            {
                return View("Modificacion", "Usuario");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Detalles(int? id)
        {
            UsuarioDTO usuario = await _IUsuarioService.ConsultarUsuario(id);
            return View(usuario);
        }
        [Authorize]
        public async Task<IActionResult> Eliminar(int? id)
        {
            UsuarioDTO usuario = await _IUsuarioService.ConsultarUsuario(id);
            return View(usuario);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EliminarUsuario(int? id)
        {
            bool resultado = await _IUsuarioService.EliminarUsuario(id);
            if (resultado)
            {
                return RedirectToAction("Salir", "Login");
            }
            return RedirectToAction("Eliminar", "Usuario");
        }

        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public async Task<ActionResult> ListaUsuarios()
        {
            List<UsuarioDTO> listaUsuarios = await _IUsuarioService.ObtenerListaUsuarios();
            IEnumerable<UsuarioDTO> EnumerableUsuario = listaUsuarios.AsEnumerable();
            return View(EnumerableUsuario);
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public async Task<IActionResult> AsignarRol([FromBody] AsignacionRolFront recepcion)
        {
            bool resultado = await _IUsuarioService.CambioRol(recepcion.idUsuario, recepcion.rolFronted);
            if (resultado)
            {
                TempData["cambioRol"] = "El rol se ha cambiado correctamente.";
                return Ok();
            }
            return BadRequest();
        }
    }
}

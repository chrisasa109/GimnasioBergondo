using Gimnasio.Dates;
using Gimnasio.Models;
using Gimnasio.Service;
using Gimnasio.Transporte.UsuarioActividad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class UsuarioActividadController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UsuarioService _usuarioService;
        public UsuarioActividadController(ApplicationDbContext context, UsuarioService usuarioService)
        {
            _context = context;
            _usuarioService = usuarioService;
        }

        public ActionResult Index(int? id)
        {
            int idUsuario = id ?? _usuarioService.ObtenerUsuario().Id;
            //Si salta error de conversión es porque el id de actividad por algún motivo lo lee como string
            var UserAct = _context.UsuarioActividad.Where(x => x.UsuarioId == idUsuario).Include(a => a.Actividad).ToList();

            return View(UserAct);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ModeloUserAct modelo)
        {
            try
            {
                int idUsuario = _usuarioService.ObtenerUsuario().Id;
                UsuarioActividad? existe = _context.UsuarioActividad.FirstOrDefault(a => a.UsuarioId == idUsuario && a.ActividadId == modelo.ActividadId);
                if (existe is null)
                {
                    if(_context.Contrato.Any(e => e.UsuarioId == _usuarioService.ObtenerUsuario().Id && DateOnly.FromDateTime(DateTime.Now) < e.FechaFin))
                    {
                        UsuarioActividad UsAc = new()
                        {
                            ActividadId = modelo.ActividadId,
                            Notas = modelo.Notas,
                            UsuarioId = idUsuario
                        };
                        await _context.UsuarioActividad.AddAsync(UsAc);
                        await _context.SaveChangesAsync();
                        TempData["apuntadoExitosamente"] = "Te has apuntado a la actividad de manera exitosa.";
                    }
                    else
                    {
                        TempData["NoContrato"] = "No tienes ninguna tarifa contratada para poder apuntarte a actividades";
                    }
                    
                }
                else
                {
                    TempData["actividadApuntado"] = "Ya estás apuntado/a a la actividad.";
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Detalles(int Id)
        {
            UsuarioActividad actividad = _context.UsuarioActividad.FirstOrDefault(a => a.Id == Id);
            actividad.Actividad = _context.Actividad.FirstOrDefault(a => a.Id == actividad.ActividadId);
            return View(actividad);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int eliminar)
        {
            var fila = await _context.UsuarioActividad.FirstAsync(a => a.Id == eliminar);
            _context.UsuarioActividad.Remove(fila);
            await _context.SaveChangesAsync();
            TempData["eliminadaCorrectamente"] = "Te has desapuntado de la actividad correctamente";
            return RedirectToAction("Index", "UsuarioActividad");
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarNota([Bind("Id,Notas")] UsuarioActividad UsAc)
        {
            if (ModelState.IsValid)
            {
                var fila = _context.UsuarioActividad.FirstOrDefault(a => a.Id == UsAc.Id);
                if (fila != null)
                {
                    fila.Notas = UsAc.Notas;
                    await _context.SaveChangesAsync();
                    TempData["exitoCambios"] = "Los cambios se han guardado correctamente.";
                }
                else
                {
                    TempData["errorCambios"] = "Los cambios no se han podido guardar.";
                }

            }
            return RedirectToAction("Index", "UsuarioActividad");
        }
        
        [HttpGet]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public ActionResult Listado()
        {
            var listaids = _context.UsuarioActividad.GroupBy(a => a.ActividadId).Select(b => b.Key).ToList();
            List<UsuarioActividad> lista = [];
            foreach (int item in listaids)
            {
                UsuarioActividad ua = _context.UsuarioActividad.First(a => a.ActividadId == item);
                ua.Actividad = _context.Actividad.FirstOrDefault(a => a.Id == ua.ActividadId);
                ua._usuario = _context.Usuario.FirstOrDefault(a => a.Id == ua.UsuarioId);
                lista.Add(ua);
            }
            return View(lista);
        }

        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public ActionResult ListaUsuarios(int idActividad)
        {
            List<UsuarioActividad> listaUA = _context.UsuarioActividad.Where(a => a.ActividadId == idActividad).ToList();
            listaUA = listaUA.Distinct().ToList();
            List<int> idsUsuarios = listaUA.Select(ua => ua.UsuarioId).ToList();
            List<Usuario> usuarios = _context.Usuario.Where(u => idsUsuarios.Contains(u.Id)).ToList();
            return PartialView("~/Views/Shared/_ListaUsuariosActividad.cshtml", usuarios);
        }
    }
}

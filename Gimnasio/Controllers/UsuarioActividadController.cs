using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Linq;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class UsuarioActividadController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsuarioActividadController(ApplicationDbContext context) { _context = context; }
        private Usuario ObtenerDatosPorCookies()
        {
            int.TryParse(User.FindFirst("Id")?.Value, out int userId);
            var usuario = _context.Usuario.FirstOrDefault(x => x.Id == userId);
            return usuario;
        }
        
        public ActionResult Index(int? id) 
        {
            int idUsuario =id ?? ObtenerDatosPorCookies().Id;
            //Si salta error de conversión es porque el id de actividad por algún motivo lo lee como string
            var UserAct = _context.UsuarioActividad.Where(x => x.UsuarioId == idUsuario).Include(a => a.Actividad).ToList();

            return View(UserAct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(int Id) 
        {
            int idUsuario = ObtenerDatosPorCookies().Id;
            UsuarioActividad UsAc = new UsuarioActividad
            {
                ActividadId = Id,
                UsuarioId = idUsuario
            };
            await _context.UsuarioActividad.AddAsync(UsAc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = idUsuario });
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
            return RedirectToAction("Index", "UsuarioActividad");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
    }
}

using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Controllers
{
    public class ActividadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActividadController(ApplicationDbContext context) { _context = context; }

        [HttpGet]
        public IActionResult Index()
        {
            var actividades = _context.Actividad.Where(x => x.CapacidadMaxima>0).ToList();
            return View(actividades);
        }

        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Descripcion,Duracion,CapacidadMaxima,FechaHora")] Actividad actividadFront)
        {
            if(ModelState.IsValid)
            {
                await _context.Actividad.AddAsync(actividadFront);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Actividad");
            }
            return View();
        }
        
    }
}

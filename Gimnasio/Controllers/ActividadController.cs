using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gimnasio.Controllers
{
    public class ActividadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActividadController(ApplicationDbContext context) { _context = context; }

        [HttpGet]
        public ActionResult IndexCalendar()
        {
            var actividades = _context.Actividad.Where(x => x.CapacidadMaxima > 0).ToList();
            List<ModeloCalendario> calendario = new List<ModeloCalendario>();
            foreach (Actividad item in actividades)
            {
                ModeloCalendario fila = new ModeloCalendario
                {
                    title = item.Descripcion.ToString(),
                    start = item.FechaHora.ToString("yyyy-MM-ddTHH:mm:ss"),
                    end = (item.FechaHora.AddMinutes(item.Duracion.Hour * 60 + item.Duracion.Minute)).ToString("yyyy-MM-ddTHH:mm:ss")
                };
                calendario.Add(fila);
            }
            ContentResult jsonResult = new ContentResult
            {
                Content = JsonConvert.SerializeObject(calendario),
                ContentType = "application/json"
            };
            return jsonResult;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var actividades = _context.Actividad.Where(x => x.CapacidadMaxima > 0).ToList();
            return View(actividades);
        }

        public class ModeloCalendario
        {
            public string title { get; set; }
            public string start { get; set; }
            public string end { get; set; }
        }

        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Descripcion,Duracion,CapacidadMaxima,FechaHora")] Actividad actividadFront)
        {
            if (ModelState.IsValid)
            {
                await _context.Actividad.AddAsync(actividadFront);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Actividad");
            }
            return View();
        }

    }
}

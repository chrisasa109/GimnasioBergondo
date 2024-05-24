using Gimnasio.Dates;
using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;
using Gimnasio.Transporte.Actividad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gimnasio.Controllers
{
    public class ActividadController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IActividadService _IActividadService;

        public ActividadController(ApplicationDbContext context, IActividadService actividadService) 
        { 
            _context = context; 
            _IActividadService = actividadService;
        }

        [HttpGet]
        public async Task<ActionResult> IndexCalendar()
        {
            List<ActividadDTO> actividades = await _IActividadService.ObtenerTodasActividadesDisponibles();
            List<ModeloCalendario> calendario = new List<ModeloCalendario>();
            foreach (ActividadDTO item in actividades)
            {
                ModeloCalendario fila = new ModeloCalendario
                {
                    id = item.Id,
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
        public async Task<ActionResult> Index()
        {
            List<ActividadDTO> actividades = await _IActividadService.ObtenerTodasActividadesDisponibles();
            return View(actividades);
        }

        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        [HttpPost]
        public async Task<ActionResult> Create([Bind("Descripcion,Duracion,CapacidadMaxima,FechaHora")] ActividadDTO actividadFront)
        {
            if (ModelState.IsValid)
            {
                bool creacion = await _IActividadService.AgregarActividad(actividadFront);
                return RedirectToAction("Index", "Actividad");
            }
            return View();
        }
    }
}

﻿using Gimnasio.Dates;
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


    }
}

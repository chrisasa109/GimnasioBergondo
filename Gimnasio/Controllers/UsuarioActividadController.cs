﻿using Gimnasio.Dates;
using Gimnasio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;

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
            int idUsuario = id ?? ObtenerDatosPorCookies().Id;
            //Si salta error de conversión es porque el id de actividad por algún motivo lo lee como string
            var UserAct = _context.UsuarioActividad.Where(x => x.UsuarioId == idUsuario).Include(a => a.Actividad).ToList();

            return View(UserAct);
        }

        public class ModeloUserAct
        {
            public int ActividadId { get; set; }
            [AllowNull]
            public string? Notas { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ModeloUserAct modelo)
        {
            try
            {
                int idUsuario = ObtenerDatosPorCookies().Id;
                UsuarioActividad UsAc = new()
                {
                    ActividadId = modelo.ActividadId,
                    Notas = modelo.Notas,
                    UsuarioId = idUsuario
                };

                await _context.UsuarioActividad.AddAsync(UsAc);
                await _context.SaveChangesAsync();
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
            List<UsuarioActividad> lista = _context.UsuarioActividad.ToList();
            foreach (UsuarioActividad item in lista)
            {
                item.Actividad = _context.Actividad.FirstOrDefault(a => a.Id == item.ActividadId);
                item._usuario = _context.Usuario.FirstOrDefault(u => u.Id == item.UsuarioId);
            }
            return View(lista);
        }

        [HttpPost]
        public ActionResult ListaUsuarios(int idActividad)
        {
            List<UsuarioActividad> listaUA = _context.UsuarioActividad.Where(a => a.ActividadId == idActividad).ToList();
            List<int> idsUsuarios = listaUA.Select(ua => ua.UsuarioId).ToList();
            List<Usuario> usuarios = _context.Usuario.Where(u => idsUsuarios.Contains(u.Id)).ToList();
            return PartialView("~/Views/Shared/_ListaUsuariosActividad.cshtml", usuarios);
        }



    }
}

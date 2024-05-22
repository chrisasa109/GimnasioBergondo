using Gimnasio.Dates;
using Gimnasio.Models;
using Gimnasio.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class ContratoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SessionService _usuarioService;
        public ContratoController(ApplicationDbContext context, SessionService usuarioService) 
        { 
            _context = context; 
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Index(int tarifaId)
        {
            if (_context.Contrato.Any(e => e.UsuarioId == _usuarioService.ObtenerUsuario().Id && DateOnly.FromDateTime(DateTime.Now) < e.FechaFin))
            {
                TempData["ContratoVigente"] = "Ya tienes un contrato vigente";
                return RedirectToAction("Detalles", "Contrato");
            }
            var contrato = new Contrato
            {
                UsuarioId = _usuarioService.ObtenerUsuario().Id,
                _usuario = _usuarioService.ObtenerUsuario(),
                FechaInicio = DateOnly.FromDateTime(DateTime.Now),
                FechaFin = DateOnly.FromDateTime(DateTime.Now).AddMonths(1),
                TarifaID = tarifaId,
                _tarifa = _context.Tarifa.FirstOrDefault(t => t.Id == tarifaId)
            };
            return View(contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contratar([Bind("UsuarioId,FechaInicio,FechaFin,Comentarios,FormaPago,TarifaID")] Contrato contratoFronted)
        {
            if (ModelState.IsValid)
            {
                if (_context.Contrato.Any(e => e.UsuarioId == contratoFronted.UsuarioId && DateOnly.FromDateTime(DateTime.Now) < contratoFronted.FechaFin))
                {
                    TempData["ContratoVigente"] = "Ya tienes un contrato vigente";
                }
                else
                {
                    _context.Contrato.Add(contratoFronted);
                    await _context.SaveChangesAsync();
                    TempData["nuevoContrato"] = "Tu contrato está activado";
                }
                return RedirectToAction("Detalles", "Contrato");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Detalles()
        {
            var contrato = _context.Contrato.FirstOrDefault(c => c.UsuarioId == _usuarioService.ObtenerUsuario().Id && DateOnly.FromDateTime(DateTime.Now) < c.FechaFin);
            if (contrato == null)
            {
                TempData["ContratoNoVigente"] = "No tienes contrato vigente. Contrata uno!";
                return RedirectToAction("Index", "Tarifa");
            }
            return View(contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardarCambios([Bind("Comentarios")] Contrato nuevo)
        {
            var contrato = _context.Contrato.FirstOrDefault(c => c.UsuarioId == _usuarioService.ObtenerUsuario().Id);
            if (contrato != null)
            {
                contrato.Comentarios = nuevo.Comentarios;
                await _context.SaveChangesAsync();
                TempData["CommentSuccess"] = "Los cambios se han guardado correctamente.";
            }
            else
            {
                TempData["CommentError"] = "No se han podido guardar los cambios.";
            }

            return RedirectToAction("Detalles", "Contrato");
        }
        [HttpPost]
        public ActionResult ComprobarContrato(int IdUsuario)
        {
            Contrato contract = _context.Contrato.FirstOrDefault(c => c.UsuarioId == IdUsuario && DateOnly.FromDateTime(DateTime.Now) < c.FechaFin);
            return PartialView("~/Views/Shared/_UsuarioContratoConsulta.cshtml", contract);
        }
    }
}
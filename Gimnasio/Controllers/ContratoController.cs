using Gimnasio.Dates;
using Gimnasio.Dominio.IServices;
using Gimnasio.Models;
using Gimnasio.Service;
using Gimnasio.Transporte;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class ContratoController : Controller
    {
        private readonly IContratoService _IContratoService;
        public ContratoController(IContratoService iContratoService) 
        { 
            _IContratoService = iContratoService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int tarifaId)
        {
            bool existeContrato = await _IContratoService.ContratoVigente();
            if(existeContrato)
            {
                TempData["ContratoVigente"] = "Ya tienes un contrato vigente";
                return RedirectToAction("Detalles", "Contrato");
            }
            else
            {
                ContratoDTO contratoPrevio = await _IContratoService.ContratoPrevio(tarifaId);
                return View(contratoPrevio);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contratar([Bind("UsuarioId,FechaInicio,FechaFin,Comentarios,FormaPago,TarifaID")] ContratoDTO contratoFronted)
        {
            if (ModelState.IsValid)
            {
                bool activado = await _IContratoService.ContratoActivado(contratoFronted.UsuarioId);
                if (activado)
                {
                    TempData["ContratoVigente"] = "Ya tienes un contrato vigente";
                }
                else
                {
                    bool add = await _IContratoService.RealizarContrato(contratoFronted);
                    TempData["nuevoContrato"] = "Tu contrato está activado";
                }
                return RedirectToAction("Detalles", "Contrato");
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Detalles()
        {
            ContratoDTO contrato = await _IContratoService.ObtenerContrato();
            if (contrato == null)
            {
                TempData["ContratoNoVigente"] = "No tienes contrato vigente. Contrata uno!";
                return RedirectToAction("Index", "Tarifa");
            }
            return View(contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GuardarCambios([Bind("Comentarios")] Contrato nuevo)
        {
            bool resultado = await _IContratoService.GuardarCambiosNotas(nuevo.Comentarios);
            if (resultado)
            {
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
            ContratoDTO contract =  _IContratoService.ComprobarContrato(IdUsuario);
            return PartialView("~/Views/Shared/_UsuarioContratoConsulta.cshtml", contract);
        }
    }
}
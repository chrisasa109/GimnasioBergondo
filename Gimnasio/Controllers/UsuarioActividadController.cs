using Gimnasio.Dominio.IServices;
using Gimnasio.Transporte;
using Gimnasio.Transporte.UsuarioActividad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gimnasio.Controllers
{
    [Authorize]
    public class UsuarioActividadController : Controller
    {
        private readonly IUsuarioActividadService _IUsuarioActividadService;
        private readonly IContratoService _IContratoService;
        public UsuarioActividadController(IUsuarioActividadService usuarioActividadService, IContratoService contratoService)
        {
            _IUsuarioActividadService = usuarioActividadService;
            _IContratoService = contratoService;
        }

        public async Task<ActionResult> Index(int? id)
        {
            List<UsuarioActividadDTO> lista = await _IUsuarioActividadService.ObtenerActividadesDelUsuarioPorId(id);
            return View(lista);
        }

        [HttpPost]
        public async Task<ActionResult> Agregar([FromBody] ModeloUserAct modelo)
        {
            try
            {
                bool existe = await _IUsuarioActividadService.ComprobarUsuarioApuntadoActividad(modelo.ActividadId);
                if (existe is false)
                {
                    bool contrato = await _IContratoService.ContratoActivado(null);
                    if (contrato)
                    {
                        bool apuntado = await _IUsuarioActividadService.ApuntarUsuarioActividad(modelo);
                        if (apuntado)
                        {
                            TempData["apuntadoExitosamente"] = "Te has apuntado a la actividad de manera exitosa.";
                        }
                        else
                        {
                            TempData["NoApuntado"] = "No te has podido apuntar a la actividad.";
                        }
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
        public async Task<ActionResult> Detalles(int Id)
        {
            UsuarioActividadDTO actividad = await _IUsuarioActividadService.ObtenerDetalleActividad(Id);
            return View(actividad);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int eliminar)
        {
            bool eliminado = await _IUsuarioActividadService.EliminarUsuarioActividad(eliminar);
            if (eliminado)
            {
                TempData["eliminadaCorrectamente"] = "Te has desapuntado de la actividad correctamente.";
            }
            else
            {
                TempData["NoEliminada"] = "No te has podido desapuntar de la actividad.";
            }

            return RedirectToAction("Index", "UsuarioActividad");
        }

        [HttpPost]
        public async Task<ActionResult> ActualizarNota([Bind("Id,Notas")] UsuarioActividadDTO UsAc)
        {
            if (ModelState.IsValid)
            {
                bool CambiosGuardados = await _IUsuarioActividadService.GuardarCambios(UsAc);
                if (CambiosGuardados)
                {
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
        public async Task<ActionResult> Listado()
        {
            List<UsuarioActividadDTO> lista = await _IUsuarioActividadService.ObtenerListaActividades();
            return View(lista);
        }

        [HttpPost]
        [Authorize(Roles = "ADMINISTRADOR,TRABAJADOR")]
        public async Task<ActionResult> ListaUsuarios(int idActividad)
        {
            List<UsuarioActividadDTO> usuarios = await _IUsuarioActividadService.ObtenerUsuariosDeActividad(idActividad);
            return PartialView("~/Views/Shared/_ListaUsuariosActividad.cshtml", usuarios);
        }
    }
}

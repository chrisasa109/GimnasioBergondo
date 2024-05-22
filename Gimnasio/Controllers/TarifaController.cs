using Gimnasio.Dates;
using Gimnasio.Dominio.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Controllers
{
    public class TarifaController : Controller
    {
        private readonly ITarifaService _ITarifaService;
        public TarifaController(ITarifaService iTarifaService)
        {
            _ITarifaService = iTarifaService;
        }


        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var tarifas = await _ITarifaService.ConsultaTarifas();
            return View(tarifas);   
        }
    }
}

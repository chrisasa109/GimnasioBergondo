using Gimnasio.Dates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gimnasio.Controllers
{
    public class TarifaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TarifaController(ApplicationDbContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tarifa.OrderBy(a => a.Precio).ToListAsync());
        }
    }
}

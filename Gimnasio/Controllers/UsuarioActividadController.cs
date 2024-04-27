using Gimnasio.Dates;
using Microsoft.AspNetCore.Mvc;

namespace Gimnasio.Controllers
{
    public class UsuarioActividadController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsuarioActividadController(ApplicationDbContext context) { _context = context; }

      /*  [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar() 
        { 
             
        }*/

    }
}

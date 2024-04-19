using Gimnasio.Dates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Gimnasio.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context) { _context = context; }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}

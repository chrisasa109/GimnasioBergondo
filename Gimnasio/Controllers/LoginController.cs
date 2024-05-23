using Gimnasio.Dominio.IServices;using Gimnasio.Transporte;using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Mvc;namespace Gimnasio.Controllers{    public class LoginController : Controller    {        private readonly ILoginService _ILoginService;        public LoginController(ILoginService ILoginService, IUsuarioService usuarioService)
        {
            _ILoginService = ILoginService;        }        [HttpGet]        public IActionResult Index()        {            return View();        }        [HttpPost]        [ValidateAntiForgeryToken]        public async Task<IActionResult> IniciarSesion([Bind("Email,Password")] LoginDTO _login)        {            if (ModelState.IsValid)
            {
                bool existeUsuario = await _ILoginService.ExisteUsuario(_login);

                if (!existeUsuario)
                {
                    TempData["errorUsuario"] = "No existe ninguna cuenta con el email introducido.";
                    return View(nameof(Index));
                }

                bool validacion = await _ILoginService.IniciarSesion(_login, HttpContext);

                if (validacion)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["errorUsuario"] = "Contraseña incorrecta.";
                    return View(nameof(Index));
                }
            }

            return View(nameof(Index));

        }        [Authorize]        public IActionResult Salir()        {
            _ILoginService.CerrarSesion(HttpContext);            return RedirectToAction("Index", "Home");        }    }}
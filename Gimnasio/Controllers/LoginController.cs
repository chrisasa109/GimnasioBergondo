﻿using Gimnasio.Dates;using Gimnasio.Dominio.IServices;using Gimnasio.Models;using Gimnasio.Transporte;using Microsoft.AspNetCore.Authentication;using Microsoft.AspNetCore.Authentication.Cookies;using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Identity;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using System.Security.Claims;namespace Gimnasio.Controllers{    public class LoginController : Controller    {        private readonly ILoginService _ILoginService;        public LoginController(ILoginService ILoginService)
        {
            _ILoginService = ILoginService;
        }        [HttpGet]        public IActionResult Index()        {            return View();        }        [HttpPost]        [ValidateAntiForgeryToken]        public async Task<IActionResult> IniciarSesion([Bind("Email,Password")] LoginDTO _login)        {            if (ModelState.IsValid)            {                bool validacion = await _ILoginService.IniciarSesion(_login);                if (validacion)
                {

                }
                else
                {

                }

                /*                var usuarioF = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == _login.Email);                if (usuarioF == null)                {                    TempData["errorUsuario"] = "No existe ninguna cuenta con el email introducido.";                }                else                {                    var passHasher = new PasswordHasher<Usuario>();                    if (passHasher.VerifyHashedPassword(usuarioF, usuarioF.Password, _login.Password) == PasswordVerificationResult.Success)                    {                        var claims = new List<Claim>                        {                            new(ClaimTypes.Name, usuarioF.Nombre),                            new(ClaimTypes.Role, usuarioF.Rol.ToString()),                            new("Id", usuarioF.Id.ToString())                        };                        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));                        return RedirectToAction("Index", "Home");                    }                    else                    {                        TempData["errorPassword"] = "La contraseña introducida no corresponde con el usuario.";                    }                }*/

            }            return View(nameof(Index));        }        [Authorize]        public async Task<IActionResult> Salir()        {            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);            return RedirectToAction("Index", "Home");        }    }}
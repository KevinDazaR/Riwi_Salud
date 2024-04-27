using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using RiwiSalud.Data;
using RiwiSalud.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace RiwiSalud.Controllers
{
    public class LoginController : Controller
    {
        public readonly BaseContext _context;
        public LoginController(BaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(string Correo, string Contraseña)
        {
            // Verificar si existe un asesor con el correo y contraseña dados.
            var asesor = await _context.Asesores.FirstOrDefaultAsync(a => a.Correo == Correo && a.Contraseña == Contraseña);

            if (asesor != null)
            {
                // Guardar datos en cookies.
                Response.Cookies.Append("Id", asesor.Id.ToString());
                Response.Cookies.Append("Nombre", asesor.Nombres);
                Response.Cookies.Append("Apellido", asesor.Apellidos);
                Response.Cookies.Append("Correo", asesor.Correo);

                // Crear lista de claims.
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, asesor.Nombres),
                new Claim("Apellido", asesor.Apellidos),
                new Claim("Correo", asesor.Correo),
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Redirigir a la página de inicio de asesores.
                return RedirectToAction("Inicio", "Asesores");
            }
            else
            {
                // Si no se encuentra el asesor, redirigir a la página de error o mostrar un mensaje.
                TempData["ErrorMessage"] = "Correo o contraseña incorrectos.";
                return RedirectToAction("Index", "Asesores");
            }
        }
    }
}
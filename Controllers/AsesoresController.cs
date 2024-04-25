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

    public class AsesoresController : Controller
    {
        /* Conexion con la db */
        public readonly BaseContext _context;

        /* Constructor Asesores */
        public AsesoresController(BaseContext context)
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


        public IActionResult Inicio()
        {
            var CookieNombre = HttpContext.Request.Cookies["Nombre"];
            ViewBag.CookieNombre = CookieNombre;
            var CookieApellido = HttpContext.Request.Cookies["Apellido"];
            ViewBag.CookieApellido = CookieApellido;

            return View();
        }
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Asesores");
        }

        public async Task<IActionResult> Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(Asesor asesor)
        {
            if (ModelState.IsValid)
            {
                // Here you should hash and salt the password before saving it
                // For example:
                // asesor.Contraseña = HashAndSaltPassword(asesor.Contraseña);

                _context.Asesores.Add(asesor);
                await _context.SaveChangesAsync();

                // Guardar datos en cookies.
                Response.Cookies.Append("Id", asesor.Id.ToString());
                Response.Cookies.Append("Nombre", asesor.Nombres);
                Response.Cookies.Append("Correo", asesor.Correo);

                // Crear lista de claims.
                var claims = new List<Claim>
         {
            new Claim(ClaimTypes.Name, asesor.Nombres),
            new Claim("Correo", asesor.Correo),
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Redirigir a la página de inicio de asesores.
                return RedirectToAction("Inicio", "Asesores");
            }

            return View(asesor);
        }


        public IActionResult InformacionUsuario(string nombre, string documento)
        {
            ViewBag.Nombre = nombre;
            ViewBag.Documento = documento;
            var CookieNombre = HttpContext.Request.Cookies["Nombre"];
            ViewBag.CookieNombre = CookieNombre;
            var CookieApellido = HttpContext.Request.Cookies["Apellido"];
            ViewBag.CookieApellido = CookieApellido;

            return View();
        }

        // AsesoresController.cs
        public async Task<IActionResult> BuscarUsuario(string tipoDocumento, string numeroDocumento)
        {
            if (string.IsNullOrWhiteSpace(tipoDocumento) || string.IsNullOrWhiteSpace(numeroDocumento))
            {
                TempData["ErrorMessage"] = "Por favor, seleccione el tipo de documento y proporcione un número de documento válido.";
                return RedirectToAction("Inicio", "Asesores");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NumeroDocumento == numeroDocumento && u.TipoDocumento == tipoDocumento);

            if (usuario != null)
            {
                return RedirectToAction("InformacionUsuario", new { nombre = usuario.Nombres, documento = usuario.NumeroDocumento });
            }
            else
            {
                TempData["ErrorMessage"] = "Usuario no encontrado.";
                return RedirectToAction("Inicio", "Asesores");
            }
        }

        public async Task<IActionResult> FinalizarTurno()
        {
            // Aquí puedes hacer cualquier limpieza adicional, como borrar cookies si es necesario.

            // Redirigir a la acción de búsqueda de usuario
            return RedirectToAction("Inicio", "Asesores");
        }


    }
}
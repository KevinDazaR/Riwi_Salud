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
using Microsoft.AspNetCore.Authorization;

namespace RiwiSalud.Controllers
{
    public class HomeController : Controller
    {
        public readonly BaseContext _context;
        public HomeController(BaseContext context)
        
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

         [HttpPost]
        public async Task<IActionResult> Index(string NumeroDocumento, string TipoDocumento)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NumeroDocumento == NumeroDocumento);

            if (usuario != null && TipoDocumento == usuario.TipoDocumento)
            {
                /* Apartado para obtener datos a cookies  */
                Response.Cookies.Append("Id", usuario.Id.ToString());
                Response.Cookies.Append("Nombre", usuario.Nombres);
                Response.Cookies.Append("Documento", usuario.NumeroDocumento);
                Response.Cookies.Append("TipoDocumento", usuario.TipoDocumento);
                
                /* Apartado para listar las cockies */
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, usuario.Nombres),
                    new Claim("Documento", usuario.NumeroDocumento)
                };

                /* Guardian */

                var claimsIndentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndentity));


                return RedirectToAction("Menu", "Usuarios");
            }
            else
            {
                var usuarioNoRegistrado = new UsuarioNoRegistrado
                {
                    TipoDocumento = TipoDocumento,
                    NumeroDocumento = NumeroDocumento
                };

                _context.UsuariosNoRegistrados.Add(usuarioNoRegistrado);
                await _context.SaveChangesAsync();

                Response.Cookies.Append("Id", usuarioNoRegistrado.Id.ToString());
                Response.Cookies.Append("Nombre", "Invitado");
                Response.Cookies.Append("Documento", usuarioNoRegistrado.NumeroDocumento);
                Response.Cookies.Append("TipoDocumento", usuarioNoRegistrado.TipoDocumento);

                /* Apartado para listar las cockies */
                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, "Invitado"),
                    new Claim("Documento", usuarioNoRegistrado.NumeroDocumento)
                };

                /* Guardian */

                var claimsIndentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndentity));

                return RedirectToAction("Menu", "Usuarios");
            }
        }
    }
}


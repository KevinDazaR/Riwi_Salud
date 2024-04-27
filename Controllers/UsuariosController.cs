using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using RiwiSalud.Data;
using RiwiSalud.Models;
using System.Linq;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace RiwiSalud.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        /* Conexion con la db */
        public readonly BaseContext _context;
        /* Constructor Usuarios */
        public UsuariosController(BaseContext context)
        {
            _context = context;
        }

        /* Actions para las vistas  */

        public IActionResult Index()
        {
            return View();
        }
        /* Opcion para cerrar sesion  */
 
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Usuarios");
        }

        /* [Authorize] */
        public async Task<IActionResult> Menu()
        {
            /* Definiendo las Cookies como variables */

            var CookieNombres = HttpContext.Request.Cookies["Nombre"];
            ViewBag.CookieNombres = CookieNombres;

            var CookieDocumento = HttpContext.Request.Cookies["Documento"];
            ViewBag.CookieDocumento = CookieDocumento;

            var CookieTipoDocumento = HttpContext.Request.Cookies["TipoDocumento"];
            ViewBag.CookieTipoDocumento = CookieTipoDocumento;

            return View();
        }

        public async Task<IActionResult> MenuCitasMedicas()
        {
            var CookieNombres = HttpContext.Request.Cookies["Nombre"];
            ViewBag.CookieNombres = CookieNombres;
            
            var CookieDocumento = HttpContext.Request.Cookies["Documento"];
            ViewBag.CookieDocumento = CookieDocumento;
            
            var CookieTipoDocumento = HttpContext.Request.Cookies["TipoDocumento"];
            ViewBag.CookieTipoDocumento = CookieTipoDocumento;
            return View();
        }
        public async Task<IActionResult> MenuMedicamentos()
        {
            var CookieNombres = HttpContext.Request.Cookies["Nombre"];
            ViewBag.CookieNombres = CookieNombres;

            var CookieApellidos = HttpContext.Request.Cookies["Apellidos"];
            ViewBag.CookieApellidos = CookieApellidos;
            
            var CookieDocumento = HttpContext.Request.Cookies["Documento"];
            ViewBag.CookieDocumento = CookieDocumento;
            
            var CookieTipoDocumento = HttpContext.Request.Cookies["TipoDocumento"];
            ViewBag.CookieTipoDocumento = CookieTipoDocumento;

            

            return View();
        }

        public async Task<IActionResult> MenuInformacion()
        {
            var CookieNombres = HttpContext.Request.Cookies["Nombre"];
            ViewBag.CookieNombres = CookieNombres;

            var CookieApellidos = HttpContext.Request.Cookies["Apellidos"];
            ViewBag.CookieApellidos = CookieApellidos;
            
            var CookieDocumento = HttpContext.Request.Cookies["Documento"];
            ViewBag.CookieDocumento = CookieDocumento;
            
            var CookieTipoDocumento = HttpContext.Request.Cookies["TipoDocumento"];
            ViewBag.CookieTipoDocumento = CookieTipoDocumento;

            return View();
        }

        public async Task<IActionResult> MenuPagos()
        {
            var CookieNombres = HttpContext.Request.Cookies["Nombre"];
            ViewBag.CookieNombres = CookieNombres;

            var CookieApellidos = HttpContext.Request.Cookies["Apellidos"];
            ViewBag.CookieApellidos = CookieApellidos;
            
            var CookieDocumento = HttpContext.Request.Cookies["Documento"];
            ViewBag.CookieDocumento = CookieDocumento;
            
            var CookieTipoDocumento = HttpContext.Request.Cookies["TipoDocumento"];
            ViewBag.CookieTipoDocumento = CookieTipoDocumento;

            return View();
        }

          public async Task<IActionResult> Turno(string ? LetrasTurno)
        {
            var contadorNumeroTurno = 0;

            var CookieId = HttpContext.Request.Cookies["Id"];
            ViewBag.CookieId = CookieId;

            var CookieNombres = HttpContext.Request.Cookies["Nombre"];
            ViewBag.CookieNombres = CookieNombres;

            var CookieApellidos = HttpContext.Request.Cookies["Apellidos"];
            ViewBag.CookieApellidos = CookieApellidos;
            
            var CookieDocumento = HttpContext.Request.Cookies["Documento"];
            ViewBag.CookieDocumento = CookieDocumento;
            
            var CookieTipoDocumento = HttpContext.Request.Cookies["TipoDocumento"];
            ViewBag.CookieTipoDocumento = CookieTipoDocumento;
            
            Response.Cookies.Append("LetrasTurno", LetrasTurno);
            var CookieLetrasTurno = HttpContext.Request.Cookies["LetrasTurno"];
            ViewBag.CookieLetrasTurno = CookieLetrasTurno;


                for (var i = 1; i < 5; i++)
                {
                    var numeroTurno = i.ToString();
                    var turnoCompleto =  CookieLetrasTurno + numeroTurno;

                    // K: Se crea una nueva instancia de la base de datos Turno y se establece y pasa el valor

                    var nuevoTurno = new Turno { N_Turno = turnoCompleto, IdUsuario = Int32.Parse(CookieId) };
                    
                    // Agregar el nuevo turno al contexto
                    _context.Turnos.Add(nuevoTurno);
                    Response.Cookies.Append("turnoCompleto", turnoCompleto);
                    var CookieTurnoCompleto = HttpContext.Request.Cookies["turnoCompleto"];
                    ViewBag.CookieTurnoCompleto = CookieTurnoCompleto;

                }

            // K: Se guardan los cambios en la base de datos de Turnos
            await _context.SaveChangesAsync();

            contadorNumeroTurno ++;


            if(CookieNombres == "Invitado")
            {
                var f = new Turno{
                FechaTurno = DateTime.Now,
                IdUsuarioNoRegistrado = Int32.Parse(CookieId),
                };
                
                TempData["FechaActual"] = DateTime.Now;

                _context.Turnos.Add(f);
                await _context.SaveChangesAsync();
            }
            else{
                var f = new Turno{
                FechaTurno = DateTime.Now,
                IdUsuario = Int32.Parse(CookieId),
                };
                
                TempData["FechaActual"] = DateTime.Now;
        
            _context.Turnos.Add(f);
            await _context.SaveChangesAsync();
            }

            // Cambie ToList por ToListAsync, se arregla errror - KDAZA
            var fecha = _context.Turnos.AsQueryable();
            fecha = fecha.Where(f => f.IdUsuario.ToString() == CookieId);
            ViewData["turnodata"] = fecha.ToListAsync();

            return View();
        }

        public IActionResult Crear(){
            return View();
        }
        [HttpPost]
        public IActionResult Crear(Usuario u){
            _context.Add(u);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
};
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
    
        /* login ingreso al sistema */
        [HttpPost]
        public async Task<IActionResult> Index(string NumeroDocumento)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NumeroDocumento == NumeroDocumento);

            if (usuario != null)
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

                var claimsIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndentity));

                return RedirectToAction("Menu", "Usuarios");
            }
            else
            {

                return RedirectToAction("Turno", "Usuarios");
            }
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

            var CookieApellidos = HttpContext.Request.Cookies["Apellidos"];
            ViewBag.CookieApellidos = CookieApellidos;
            
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

            var CookieId = HttpContext.Request.Cookies["Id"];
            ViewBag.CookieId = CookieId;

            var CookieNombres = HttpContext.Request.Cookies["Nombres"];
            ViewBag.CookieNombres = CookieNombres;

            var CookieApellidos = HttpContext.Request.Cookies["Apellidos"];
            ViewBag.CookieApellidos = CookieApellidos;
            
            var CookieDocumento = HttpContext.Request.Cookies["Documento"];
            ViewBag.CookieDocumento = CookieDocumento;
            
            var CookieTipoDocumento = HttpContext.Request.Cookies["TipoDocumento"];
            ViewBag.CookieTipoDocumento = CookieTipoDocumento;
            
            Response.Cookies.Append("LetrasTurno", LetrasTurno);
            
            //hace falta consultar por fecha

            var dataBase = _context.Turnos.AsQueryable();
            var Turnox = await dataBase.Where(x => x.service_abbreviation == LetrasTurno)
                .OrderBy(x => x.N_Turno).LastOrDefaultAsync();

            Console.WriteLine($"ESTE ES DE LA CONSULTA A LA BD: {Turnox.N_Turno}");
            if (Turnox != null)
            {
                var TurnoNumero = Int32.Parse(Turnox.N_Turno) +1;
                Console.WriteLine($" ESTE ES LA SUMA DE TURNONUMERO: {TurnoNumero}");
                

                var nuevoTurno = new Turno
                {
                    N_Turno = TurnoNumero.ToString(), 
                    IdUsuario = Int32.Parse(CookieId),
                    service_abbreviation = LetrasTurno,
                    FechaTurno = DateTime.Now
                };

                var CookieLetrasTurno = HttpContext.Request.Cookies["LetrasTurno"];
                ViewBag.CookieLetrasTurno = CookieLetrasTurno;

                Response.Cookies.Append("N_Turno", Turnox.N_Turno);
                var CookieN_Turno = HttpContext.Request.Cookies["N_Turno"];
                ViewBag.CookieN_Turno = CookieN_Turno;

                Console.WriteLine($"este es de la instancia pa ver q esta guardanndo: {nuevoTurno.N_Turno}");
                
                _context.Turnos.Add(nuevoTurno);

                // K: Se guardan los cambios en la base de datos de Turnos
                await _context.SaveChangesAsync();
                
                // return RedirectToAction("Turno");
            }

            else
            {
                return RedirectToAction("Menu");
            }

            return View();



         /*   for (var i = 1; i < 3; i++)
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
*/
     
            // var f = new Turno{
            //     FechaTurno = DateTime.Now,
            //     IdUsuario = Int32.Parse(CookieId),
            // };

            Response.Cookies.Append("FechaActual", DateTime.Now.ToString());
            var CookieFecha = HttpContext.Request.Cookies["FechaActual"];
            ViewBag.CookieFecha = CookieFecha;

            // _context.Turnos.Add(f);
            // await _context.SaveChangesAsync();

            // Cambie ToList por ToListAsync, se arregla errror - KDAZA
            var fecha = _context.Turnos.AsQueryable();
            fecha = fecha.Where(f => f.IdUsuario.ToString() == CookieId);
            ViewData["turnodata"] = fecha.ToListAsync();

            return View();
        }
        // public async Task<IActionResult> Index(string search){
        //     //Coleccion de registros u objetos  
        //     var users = from user in _context.Users select user;
        //     if(!string.IsNullOrEmpty(search)){
        //         users = users.Where(u => u.Names.Contains(search) || u.LastNames.Contains(search)); 
        //     }
        //     return View(await users.ToListAsync());
        // }

        // public async Task<IActionResult> Details(int? id){
        //     return View(await _context.Users.FirstOrDefaultAsync(user => user.Id == id));
        //     /* SELECT * from Users WHERE Id = id */
        // }

        // // action para crear vista create
        // public  IActionResult Create(){
        //     return View();
        // }

        // // action para registrar en la db
        // [HttpPost]
        // public IActionResult Create(User u){
        //     //Agregar el usuario al DbSet 
        //     _context.Users.Add(u);
        //     //Guardar los cambios en DbSet en la db
        //     _context.SaveChanges();
        //     //Redireciona a la lista de usuarios
        //     return  RedirectToAction("Index");

        // }

        // public async Task<IActionResult> Delete(int id){

        //     var user = await _context.Users.FindAsync(id); //Buscar el user por su id
        //     _context.Users.Remove(user); //Eliminar el usuario en el Dbset
        //     _context.SaveChanges(); //Guardar los cambios en el context 

        //     return RedirectToAction("Index"); //volver a la vista usuarios
        // }


        // public async Task<IActionResult> Edit(int id){
        //     return View(await _context.Users.FindAsync(id));
        // }

        // [HttpPost]
        // public async Task<IActionResult> Update(User user){

        //     _context.Users.Update(user);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction("Index");
        // }



    }
}
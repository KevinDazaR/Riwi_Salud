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

            if (usuario != null )
            {
                /* Apartado para obtener datos a cookies */
                Response.Cookies.Append("Id", usuario.Id.ToString());
                Response.Cookies.Append("Nombre", usuario.Nombres);
                Response.Cookies.Append("Documento", usuario.NumeroDocumento);

                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, usuario.Nombres),
                    new Claim("Documento", usuario.NumeroDocumento),
                };

                /* Guardian */

                var claimsIndentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIndentity));

                return RedirectToAction("Menu", "Usuarios");
            }
            else
            {

                return RedirectToAction("Turno", "Usuarios");

/*                 public IActionResult Create(UsuarioNoRegistrado u){
                _context.Add(u);
                _context.SaveChanges();
                return  RedirectToAction("Index"); */
            };


        }

        /* Opcion para cerrar sesion */

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Usuarios");
        }

        public async Task<IActionResult> Menu()
        {
            return View();
        }

        public async Task<IActionResult> MenuCitasMedicas()
        {
            return View();
        }
        public async Task<IActionResult> MenuMedicamentos()
        {
            return View();
        }
        public async Task<IActionResult> MenuPagos()
        {
            return View();
        }
        public async Task<IActionResult> Turno()
        {
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
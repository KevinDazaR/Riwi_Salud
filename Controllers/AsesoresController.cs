using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using RiwiSalud.Data;
using RiwiSalud.Models;
using System.Linq;

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
        /* Constructor Usuarios */
        public AsesoresController(BaseContext context){
            _context = context;
        }

        /* Actions para las vistas  */

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Inicio()
        {
                return View();
        }
        public async Task<IActionResult> Registro()
        {
            return View();
        }

        public IActionResult InformacionUsuario()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(Asesor asesor)
        {

            _context.Asesores.Add(asesor);
            await _context.SaveChangesAsync();
            return RedirectToAction("");
        }


    }
}
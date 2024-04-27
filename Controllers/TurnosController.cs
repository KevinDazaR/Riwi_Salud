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

    public class TurnosController : Controller
    {
        /* Conexion con la db */
        public readonly BaseContext _context;
        /* Constructor Turnos */
        public TurnosController(BaseContext context)
        {
            _context = context;
        }

        /* Actions para las vistas  */

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Pantalla()
        {
            var ultimosTurnos = _context.Turnos
                                .OrderByDescending(t => t.N_Turno)
                                .Take(5)
                                .ToList();

            return View(ultimosTurnos);
        }


    }
}
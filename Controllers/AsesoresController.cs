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
        {
            return View();
        }

        public IActionResult Inicio()
        {
            return View();
        }

        public IActionResult InformacionUsuario()
        {
            return View();
        }
    }
}
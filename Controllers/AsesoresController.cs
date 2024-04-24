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
        public readonly BaseContext _context;;

        public AsesoresController(BaseContext context){
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
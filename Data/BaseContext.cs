using Microsoft.EntityFrameworkCore;
using RiwiSalud.Models;

namespace RiwiSalud.Data
{
    public class BaseContext :DbContext 
    {

        public BaseContext(DbContextOptions<BaseContext> options) : base(options){}
        
        /* Registro de los modelos que se usan en la db  */
        
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioNoRegistrado> UsuarioNoRegistrados { get; set; }
        public DbSet<Asesor> Asesores { get; set; }

    }
}
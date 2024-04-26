using System.ComponentModel.DataAnnotations.Schema;

namespace RiwiSalud.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public DateTime ? FechaTurno {get; set;}

       [ForeignKey("IdUsuario")] 
        public string ? IdUsuario {get; set;}
        public string ? IdUsuarioNoRegistrado {get; set;}
        public string ? N_Turno {get; set;}
        public string ? Modulo {get; set;}

        
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace RiwiSalud.Models
{
    public class Turno
    {
        public int? Id { get; set; }
        public DateTime? FechaTurno { get; set; }
        
        public int? IdUsuario { get; set; }

        public int? IdUsuarioNoRegistrado { get; set; }

        public string? N_Turno { get; set; }
        public string? Modulo { get; set; }
    }
}

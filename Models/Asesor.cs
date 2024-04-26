using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RiwiSalud.Models
{
    public class Asesor
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string ? Nombres {get; set;}

        [Required(ErrorMessage = "El Apellido es obligatorio.")]
        public string ? Apellidos {get; set;}

        [Required(ErrorMessage = "El Correo es obligatorio.")]
        public string ? Correo {get; set;}

        [Required(ErrorMessage = "La contraseña es obligatorio.")]
        public string ? Contraseña {get; set;}

        [Required(ErrorMessage = "El modulo es obligatorio.")]
        public string ? Modulo {get; set;}
        
    }
}
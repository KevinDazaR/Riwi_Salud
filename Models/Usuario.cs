namespace RiwiSalud.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public DateOnly AñoNacimiento { get; set; }
        public string Estado { get; set; }
    }

}
using System.ComponentModel.DataAnnotations;

namespace Aquasoft_Reports.Models
{
    public class AQS_Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Correo { get; set; } 

        [DataType(DataType.Password)]
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public char Estado { get; set; }
    }
}

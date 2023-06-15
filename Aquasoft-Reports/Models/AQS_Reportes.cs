namespace Aquasoft_Reports.Models
{
    public class AQS_Reportes
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public byte[] Fotografia { get; set; }
        public string Autor { get; set; }
    }
}

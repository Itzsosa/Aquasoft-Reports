namespace Aquasoft_Reports.Models
{
    public class AQS_Eventos
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaEvento { get; set; }
        public byte[] FotoEvento { get; set; }
        public char Estado { get; set; }
    }

}

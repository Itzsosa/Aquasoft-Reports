using Microsoft.EntityFrameworkCore;
using Aquasoft_Reports.Models;

namespace Aquasoft_Reports.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {

        }
        public DbSet<AQS_Reportes>? AQS_Reportes { get; set; }
        // public DbSet<AQS_Usuario> AQS_Usuario { get; set; }
        // public DbSet<AQS_Usuario> AQS_Usuario { get; set; }
    }
}

using Entity;
using Microsoft.EntityFrameworkCore;

namespace Datos
{
    public class PulsacionesContext : DbContext
    {
        public PulsacionesContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Persona> Personas { get; set; }
    }
}

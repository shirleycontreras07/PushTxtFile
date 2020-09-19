using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Push.Models
{
    public class PushContext : DbContext
    {
        public PushContext() : base("PushContext")
        {

        }

        public DbSet<Encabezado> Encabezado { get; set; }
        public DbSet<Detalle> Detalle { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
using System.Data.Entity;

namespace ConsultorioSeguros.Models.MySql
{
    public class ConsultorioDbContext : DbContext
    {
        public ConsultorioDbContext(string connection_string_name) : base(connection_string_name)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.Entity<Seguro>().ToTable("seguro");
            modelBuilder.Entity<Cliente>().ToTable("cliente");
            modelBuilder.Entity<Seguro_cliente>().ToTable("seguro_cliente");
        }

        public virtual DbSet<Seguro> Seguro { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Seguro_cliente> Seguro_cliente { get; set; }
    }
}
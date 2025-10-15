using Microsoft.EntityFrameworkCore;
using WebService.Cap7.Model;

namespace WebService.Cap7.Data
{
    public class DatabaseContext : DbContext
    {
        public virtual DbSet<Residuo> Residuos { get; set; } // Define o DbSet para a classe Residuo
        public DbSet<Coleta> Coletas { get; set; } // DbSet para Coletas
        public DbSet<PontoDeDescarte> PontosDeDescarte { get; set; } // DbSet para ponto de descarte 
        public DbSet<Alerta> Alertas { get; set; } //DbSet para Alerta



        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        protected DatabaseContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}

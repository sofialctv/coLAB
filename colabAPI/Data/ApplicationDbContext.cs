using colabAPI.Business.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Defina aqui as tabelas como DbSet
        public DbSet<Financiador> Financiadores { get; set; }
        public DbSet<Pesquisador> Pesquisadores { get; set; }
        public DbSet<Bolsista> Bolsistas { get; set; }
        public DbSet<Bolsa> Bolsas { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Bolsista>().ToTable("Bolsistas");

            modelBuilder.Entity<Bolsa>()
                .Property(b => b.Categoria)
                .HasConversion<string>();
        }
        
    }
}
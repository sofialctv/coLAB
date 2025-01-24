using colabAPI.Business.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Data
{
    // Classe que representa o contexto do banco de dados, estendendo DbContext do Entity Framework
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Definição das tabelas (DbSet) do banco de dados que serão mapeadas pelo EF
        public DbSet<Financiador> Financiadores { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<HistoricoCargo> HistoricosCargo { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        
    }
}
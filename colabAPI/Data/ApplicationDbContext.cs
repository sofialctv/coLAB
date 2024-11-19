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

        // Configurações adicionais para o mapeamento do modelo relacional
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento entre 'Projeto' e 'Financiador'
            modelBuilder.Entity<Projeto>()
                .HasOne(p => p.Financiador)
                .WithMany(f => f.Projetos)
                .HasForeignKey(p => p.FinanciadorId)
                .OnDelete(DeleteBehavior.Restrict); // Não permite exclusão caso exista relacionamento

            modelBuilder.Entity<Projeto>()
                .Property(p => p.Categoria)
                .HasConversion<string>();

            modelBuilder.Entity<Projeto>()
                .Property(p => p.Status)
                .HasConversion<string>();
        }
    }
}
using colab.Business.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace colab.Data
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
        public DbSet<Orientador> Orientadores { get; set; }
        public DbSet<Pesquisador> Pesquisadores { get; set; }
        public DbSet<Bolsista> Bolsistas { get; set; }
        public DbSet<Bolsa> Bolsas { get; set; }

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
                .HasOne(p => p.Orientador)
                .WithMany()
                .HasForeignKey(p => p.OrientadorId);

            modelBuilder.Entity<Projeto>()
                .HasMany(p => p.Bolsistas)
                .WithMany(b => b.Projetos)
                .UsingEntity<Dictionary<string, object>>(
                    "ProjetoBolsista", // Essa é a tabela intermediária gerada para relação N<->N
                    j => j.HasOne<Bolsista>().WithMany().HasForeignKey("BolsistaId"),
                    j => j.HasOne<Projeto>().WithMany().HasForeignKey("ProjetoId")
                );

            modelBuilder.Entity<Bolsista>().ToTable("Bolsistas");

            // Converte o enum de int para uma string ao enviar para banco de dados
            modelBuilder.Entity<Bolsa>()
                .Property(b => b.Categoria)
                .HasConversion<string>();
        }

    }
}
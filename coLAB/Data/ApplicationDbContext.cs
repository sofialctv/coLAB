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

        // Definição das tabelas (DbSet) do banco de dados
        public DbSet<Financiador> Financiadores { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Bolsa> Bolsas { get; set; }
        public DbSet<HistoricoProjetoStatus> HistoricoStatusProjetos { get; set; }

        // Antigo, possivelmente serão 'removidos'
        public DbSet<Orientador> Orientadores { get; set; }
        public DbSet<Pesquisador> Pesquisadores { get; set; }
        public DbSet<Bolsista> Bolsistas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // relacionamento entre 'Projeto' e 'Financiador'
            modelBuilder.Entity<Projeto>()
                .HasOne(p => p.Financiador)
                .WithMany(f => f.Projetos)
                .HasForeignKey(p => p.FinanciadorId)
                .OnDelete(DeleteBehavior.Restrict);

            // relacionamento 1:N entre 'Projeto' e 'Bolsa'
            modelBuilder.Entity<Projeto>()
                .HasMany(p => p.Bolsas)
                .WithOne(b => b.Projeto)
                .HasForeignKey(b => b.ProjetoId);

            // relacionamento 1:N entre 'Projeto' e 'HistoricoStatusProjeto'
            modelBuilder.Entity<Projeto>()
                .HasMany(p => p.HistoricoStatus)
                .WithOne(h => h.Projeto)
                .HasForeignKey(h => h.ProjetoId);

            // config. para o enum 'ProjetoStatus' dentro de 'HistoricoStatusProjeto'
            modelBuilder.Entity<HistoricoProjetoStatus>()
                .Property(h => h.Status)
                .HasConversion<int>();

            // Antigo, possivelmente serão 'removidos'
            modelBuilder.Entity<Bolsista>().ToTable("Bolsistas");

            modelBuilder.Entity<Bolsa>()
                .Property(b => b.Categoria)
                .HasConversion<string>();
        }
    }
}
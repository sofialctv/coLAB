using colab.Business.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace colab.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Classe que representa o contexto do banco de dados, estendendo DbContext do Entity Framework
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Definição das tabelas (DbSet) do banco de dados que serão mapeadas pelo EF
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Financiador> Financiadores { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<HistoricoProjetoStatus> HistoricoStatusProjetos { get; set; }
        public DbSet<Bolsa> Bolsas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // relacionamento entre 'Pessoa' e 'Bolsa'
            modelBuilder.Entity<Pessoa>()
                .HasMany(p => p.Bolsas)
                .WithOne(b => b.Pessoa)
                .HasForeignKey(b => b.PessoaId);

            modelBuilder.Entity<Cargo>()
                .HasMany(c => c.Bolsas)
                .WithOne(b => b.Cargo)
                .HasForeignKey(b => b.CargoId);

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

            modelBuilder.Entity<Bolsa>()
                .Property(b => b.Escolaridade)
                .HasConversion<int>();


            base.OnModelCreating(modelBuilder);
        }
    } 
}
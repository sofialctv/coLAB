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
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<HistoricoCargo> HistoricosCargo { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Bolsa> Bolsas { get; set; }

        public DbSet<TipoBolsa> TipoBolsa { get; set; }

        // Definição das tabelas (DbSet) do banco de dados
        public DbSet<Financiador> Financiadores { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<HistoricoProjetoStatus> HistoricoStatusProjetos { get; set; }

        // Antigo, possivelmente serão 'removidos'
        public DbSet<Orientador> Orientadores { get; set; }
        public DbSet<Pesquisador> Pesquisadores { get; set; }
        public DbSet<Bolsista> Bolsistas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoricoCargo>()
                .HasOne(h => h.Pessoa)
                .WithMany(p => p.HistoricosCargo)
                .HasForeignKey(h => h.PessoaId)
                .IsRequired();
            base.OnModelCreating(modelBuilder);

            // relacionamento entre 'Projeto' e 'Financiador'
            modelBuilder.Entity<Projeto>()
                .HasOne(p => p.Financiador)
                .WithMany(f => f.Projetos)
                .HasForeignKey(p => p.FinanciadorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HistoricoCargo>()
                .HasOne(h => h.Cargo)
                .WithOne(c => c.HistoricoCargo)
                .HasForeignKey<HistoricoCargo>(c => c.CargoId)
                .IsRequired();
            // relacionamento 1:N entre 'Projeto' e 'Bolsa'
            modelBuilder.Entity<Projeto>()
                .HasMany(p => p.Bolsas)
                .WithOne(b => b.Projeto)
                .HasForeignKey(b => b.ProjetoId);

            // Relacionamento 1 para 1 entre Bolsa e Pessoa
            modelBuilder.Entity<Bolsa>()
                .HasOne(b => b.Pessoa) // Cada Bolsa tem uma Pessoa
                .WithOne(p => p.Bolsa) // Cada Pessoa tem uma Bolsa
                .HasForeignKey<Bolsa>(b => b.PessoaId) // Definir a chave estrangeira
                .IsRequired(); // Tornar o relacionamento obrigatório

            // Restante das configurações
            modelBuilder.Entity<TipoBolsa>()
                .HasOne(t => t.Bolsa)
                .WithOne(b => b.TipoBolsa)
                .HasForeignKey<Bolsa>(b => b.TipoBolsaId)
                .IsRequired();

            modelBuilder.Entity<Bolsa>()
                .HasIndex(b => b.TipoBolsaId)
                .IsUnique();

            modelBuilder.Entity<TipoBolsa>()
                .Property(b => b.escolaridade)
                .HasConversion<string>();
        }
    }
}
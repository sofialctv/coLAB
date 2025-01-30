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
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<HistoricoCargo> HistoricosCargo { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Bolsa> Bolsas { get; set; }

        public DbSet<TipoBolsa> TipoBolsa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoricoCargo>()
                .HasOne(h => h.Pessoa)
                .WithMany(p => p.HistoricosCargo)
                .HasForeignKey(h => h.PessoaId)
                .IsRequired();

            modelBuilder.Entity<HistoricoCargo>()
                .HasOne(h => h.Cargo)
                .WithOne(c => c.HistoricoCargo)
                .HasForeignKey<HistoricoCargo>(c => c.CargoId)
                .IsRequired();

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
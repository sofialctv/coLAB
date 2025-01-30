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
            
           modelBuilder.Entity<TipoBolsa>()
            .HasOne(t => t.Bolsa) // TipoBolsa tem uma Bolsa
            .WithOne(b => b.TipoBolsa) // Bolsa tem um TipoBolsa
            .HasForeignKey<Bolsa>(b => b.TipoBolsaId) // Chave estrangeira em Bolsa
            .IsRequired(); // Garantir que o relacionamento seja obrigatório

        modelBuilder.Entity<Bolsa>()
            .HasIndex(b => b.TipoBolsaId) // Definir um índice único para garantir 1 para 1
            .IsUnique(); // Garantir que cada TipoBolsaId seja único

            // Converte o enum de int para uma string ao enviar para banco de dados
            modelBuilder.Entity<TipoBolsa>()
                .Property(b => b.escolaridade)
                .HasConversion<string>();
        }
        
    }
}
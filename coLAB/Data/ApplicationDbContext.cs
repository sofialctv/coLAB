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
        public DbSet<HistoricoCargo> HistoricosCargo { get; set; }
        public DbSet<Financiador> Financiadores { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<HistoricoProjetoStatus> HistoricoStatusProjetos { get; set; }
        public DbSet<Bolsa> Bolsas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            // Relações de HistoricoCargo com Cargo e Pessoa
            modelBuilder.Entity<HistoricoCargo>()
                .HasOne(h => h.Pessoa)
                .WithMany(p => p.HistoricosCargo)
                .HasForeignKey(h => h.PessoaId)
                .IsRequired();

            modelBuilder.Entity<HistoricoCargo>()
                .HasOne(h => h.Cargo)
                .WithOne(c => c.HistoricoCargo)
                .HasForeignKey<HistoricoCargo>(h => h.CargoId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);

            // Relacionamento entre 'Projeto' e 'Financiador'
            modelBuilder.Entity<Projeto>()
                .HasOne(p => p.Financiador)
                .WithMany(f => f.Projetos)
                .HasForeignKey(p => p.FinanciadorId)
                .OnDelete(DeleteBehavior.Restrict); // Não permite exclusão caso exista relacionamento
        }
    } 
}
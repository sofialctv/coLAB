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
        public DbSet<Bolsa> Bolsas { get; set; }
    }
}
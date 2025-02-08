using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using colab.Data;
using Microsoft.EntityFrameworkCore;

namespace colab.Business.Repository.Implementations
{
    public class BolsaRepository : IBolsaRepository
    {
        private readonly ApplicationDbContext _DbContext;

        public BolsaRepository(ApplicationDbContext context)
        {
            _DbContext = context;
        }

        public async Task<IEnumerable<Bolsa>> GetAllAsync()
        {
            return await _DbContext.Bolsas
                .Include(b => b.Pessoa)
                .Include(b => b.TipoBolsa)
                .Include(b => b.Projeto)
                .ToListAsync();
        }

        public async Task<Bolsa> GetByIdAsync(int id)
        {
            var bolsa = await _DbContext.Bolsas
                .Include(b => b.Pessoa)
                .Include(b => b.TipoBolsa)
                .Include(b => b.Projeto)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bolsa == null)
            {
                throw new KeyNotFoundException($"Bolsa com ID {id} não foi encontrada.");
            }

            return bolsa;
        }

        public async Task AddAsync(Bolsa bolsa)
        {
            await _DbContext.Bolsas.AddAsync(bolsa);
        }

        public async Task UpdateAsync(Bolsa bolsa)
        {
            _DbContext.Bolsas.Update(bolsa);
        }

        public async Task DeleteAsync(int id)
        {
            var bolsa = await _DbContext.Bolsas.FindAsync(id);
            if (bolsa != null)
            {
                _DbContext.Bolsas.Remove(bolsa);
            }
        }

        public async Task Save()
        {
            await _DbContext.SaveChangesAsync();
        }
    }
}

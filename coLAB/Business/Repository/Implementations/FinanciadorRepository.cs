using colab.Business.Repository.Interfaces;
using colab.Business.Models.Entities;
using colab.Data;
using Microsoft.EntityFrameworkCore;

namespace colab.Business.Repository.Implementations
{
    public class FinanciadorRepository : IFinanciadorRepository
    {
        private readonly ApplicationDbContext _context;

        public FinanciadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Financiador>> GetAllAsync()
        {
            return await _context.Financiadores
                .ToListAsync();

        }

        public async Task<Financiador> GetByIdAsync(int id)
        {
            return await _context.Financiadores
                .FirstOrDefaultAsync(f => f.Id == id);

        }

        public async Task<Financiador> AddAsync(Financiador financiador)
        {
            _context.Financiadores.Add(financiador);
            await _context.SaveChangesAsync();
            return financiador;
        }

        public async Task<Financiador> UpdateAsync(Financiador financiador)
        {
            _context.Financiadores.Update(financiador);
            await _context.SaveChangesAsync();
            return financiador;
        }

        public async Task DeleteAsync(int id)
        {
            var financiador = await _context.Financiadores.FindAsync(id);
            if (financiador != null)
            {
                _context.Financiadores.Remove(financiador);
                await _context.SaveChangesAsync();
            }
        }
    }
}


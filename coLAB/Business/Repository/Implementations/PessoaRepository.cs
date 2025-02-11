using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using colab.Data;
using Microsoft.EntityFrameworkCore;

namespace colab.Business.Repository.Implementations
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ApplicationDbContext _context;

        public PessoaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            return await _context.Pessoas
                .Include(p => p.HistoricosCargo)
                    .ThenInclude(h => h.Cargo)
                .Include(p => p.Bolsa)
                .ToListAsync();
        }

        public async Task<Pessoa?> GetByIdAsync(int id)
        {
            return await _context.Pessoas
                .Include(p => p.HistoricosCargo)
                    .ThenInclude(h => h.Cargo)
                .Include(p => p.Bolsa)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pessoa> AddAsync(Pessoa pessoa)
        {
            await _context.Pessoas.AddAsync(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task<Pessoa> UpdateAsync(Pessoa pessoa)
        {
            _context.Pessoas.Update(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task DeleteAsync(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoas.Remove(pessoa);
                await _context.SaveChangesAsync();
            }
        }
    }
}

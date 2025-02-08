using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using colab.Data;
using Microsoft.EntityFrameworkCore;

namespace colab.Business.Repository.Implementations
{
    public class TipoBolsaRepository : ITipoBolsaRepository
    {
        private readonly ApplicationDbContext _dbContext;

        // Injeta o contexto do banco no construtor
        public TipoBolsaRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<TipoBolsa>> GetAllAsync()
        {
            return await _dbContext.TipoBolsa.ToListAsync();
        }

        public async Task<TipoBolsa> GetByIdAsync(int id)
        {
            return await _dbContext.TipoBolsa
                .FirstOrDefaultAsync(tb => tb.Id == id);
        }

        public async Task AddAsync(TipoBolsa tipoBolsa)
        {
            _dbContext.TipoBolsa.Add(tipoBolsa);
            await _dbContext.SaveChangesAsync();  // Garantir que a operação seja persistida no banco
        }

        public async Task UpdateAsync(TipoBolsa tipoBolsa)
        {
            var existeBolsa = await _dbContext.TipoBolsa.FindAsync(tipoBolsa.Id);
            if (existeBolsa != null)
            {
                _dbContext.Entry(existeBolsa).CurrentValues.SetValues(tipoBolsa);
                await _dbContext.SaveChangesAsync();  // Garantir que a atualização seja persistida no banco
            }
        }

        public async Task DeleteAsync(int tipoBolsaID)
        {
            var tipoBolsa = await _dbContext.TipoBolsa.FindAsync(tipoBolsaID);
            if (tipoBolsa != null)
            {
                _dbContext.TipoBolsa.Remove(tipoBolsa);
                await _dbContext.SaveChangesAsync();  // Garantir que a exclusão seja persistida no banco
            }
        }
    }
}

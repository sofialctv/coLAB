using System.Collections.Generic;
using System.Threading.Tasks;
using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Repository.Implementations
{
    public class HistoricoCargoRepository : IHistoricoCargoRepository
    {
        private readonly ApplicationDbContext _context;

        public HistoricoCargoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistoricoCargo>> GetAllAsync()
        {
            return await _context.HistoricosCargo
                .ToListAsync();
        }

        public async Task<HistoricoCargo> GetByIdAsync(int id)
        {
            return await _context.HistoricosCargo
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<HistoricoCargo> AddAsync(HistoricoCargo historicoCargo)
        {
            _context.HistoricosCargo.Add(historicoCargo);
            await _context.SaveChangesAsync();
            return historicoCargo;
        }

        public async Task<HistoricoCargo> UpdateAsync(HistoricoCargo historicoCargo)
        {
            _context.HistoricosCargo.Update(historicoCargo);
            await _context.SaveChangesAsync();
            return historicoCargo;
        }

        public async Task DeleteAsync(int id)
        {
            var historicoCargo = await _context.HistoricosCargo.FindAsync(id);
            if (historicoCargo != null)
            {
                _context.HistoricosCargo.Remove(historicoCargo);
                await _context.SaveChangesAsync();
            }
        }
    }
}

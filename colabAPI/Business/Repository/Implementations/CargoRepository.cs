using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Repository.Implementations
{
    public class CargoRepository : ICargoRepository
    {
        private readonly ApplicationDbContext _context;

        public CargoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cargo>> GetAllAsync()
        {
            return await _context.Cargos
                .ToListAsync();

        }

        public async Task<Cargo> GetByIdAsync(int id)
        {
            return await _context.Cargos
                .FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task<Cargo> AddAsync(Cargo cargo)
        {
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync();
            return cargo;
        }

        public async Task<Cargo> UpdateAsync(Cargo cargo)
        {
            _context.Cargos.Update(cargo);
            await _context.SaveChangesAsync();
            return cargo;
        }

        public async Task DeleteAsync(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);
            if (cargo != null)
            {
                _context.Cargos.Remove(cargo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
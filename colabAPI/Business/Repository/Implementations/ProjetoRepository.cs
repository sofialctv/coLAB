using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Repository.Implementations
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjetoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Projeto>> GetAllAsync()
        {
            return await _context.Projetos
                .Include(p => p.Financiador)
                .ToListAsync();
        }

        public async Task<Projeto> GetByIdAsync(int id)
        {
            return await _context.Projetos
                .Include(p => p.Financiador)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Projeto> AddAsync(Projeto projeto)
        {
            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
            return projeto;
        }

        public async Task<Projeto> UpdateAsync(Projeto projeto)
        {
            _context.Projetos.Update(projeto);
            await _context.SaveChangesAsync();
            return projeto;
        }

        public async Task DeleteAsync(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto != null)
            {
                _context.Projetos.Remove(projeto);
                await _context.SaveChangesAsync();
            }
        }
    }
}

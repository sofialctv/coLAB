using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Repository.Implementations
{
    public class BolsistaRepository : IBolsistaRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        
        public BolsistaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        
        // GET all bolsistas
        public async Task<IEnumerable<Bolsista>> GetAllAsync()
        {
            return await _context.Bolsistas
                .Include(b => b.Bolsa)
                .Include(b => b.Orientador)
                .ToListAsync();

        }

        // GET bolsista by id
        public async Task<Bolsista> GetByIdAsync(int id)
        {
            return await _context.Bolsistas
                .Include(b => b.Bolsa)
                .Include(b => b.Orientador)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        // POST 
        public async Task<Bolsista> AddAsync(Bolsista bolsista)
        {
            _context.Bolsistas.Add(bolsista);
            await _context.SaveChangesAsync();
            return bolsista;
        }
        
        // PUT
        public async Task<Bolsista> UpdateAsync(Bolsista bolsista)
        {
            _context.Bolsistas.Update(bolsista);
            await _context.SaveChangesAsync();
            return bolsista;
        }
        
        // DELETE
        public async Task DeleteAsync(int id)
        {
            var bolsista = await _context.Bolsistas.FindAsync(id);
            
            if (bolsista != null)
            {
                _context.Bolsistas.Remove(bolsista);
                await _context.SaveChangesAsync();
            }
        }
        
        

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

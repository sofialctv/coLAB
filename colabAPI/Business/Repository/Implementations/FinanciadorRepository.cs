using Microsoft.EntityFrameworkCore;
using colabAPI.Data;
using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;

namespace colabAPI.Business.Repository.Implementations
{
    public class FinanciadorRepository : IFinanciadorRepository, IDisposable
    {
        private readonly ApplicationDbContext _DbContext;

        public FinanciadorRepository(ApplicationDbContext context)
        {
            _DbContext = context;
        }

        // Retorna todos os financiadores e carrega os projetos relacionados a eles usando Eager Loading
        public async Task<IEnumerable<Financiador>> GetAllAsync()
        {
            return await _DbContext.Financiadores
                                   .Include(f => f.Projetos) // Carrega os projetos relacionados
                                   .ToListAsync();
        }

        // Retorna um financiador por ID, incluindo os projetos relacionados
        public async Task<Financiador?> GetByIdAsync(int id)
        {
            return await _DbContext.Financiadores
                                   .Include(f => f.Projetos) // Eager Loading dos projetos
                                   .FirstOrDefaultAsync(f => f.Id == id); // Usa FirstOrDefaultAsync para evitar exceções
        }

        // Adiciona um novo financiador ao banco de dados
        public async Task AddAsync(Financiador financiador)
        {
            await _DbContext.Financiadores.AddAsync(financiador);
            await SaveAsync();
        }

        // Atualiza um financiador existente
        public async Task UpdateAsync(Financiador financiador)
        {
            _DbContext.Entry(financiador).State = EntityState.Modified;
            await SaveAsync();
        }

        // Remove um financiador por meio de seu ID
        public async Task DeleteAsync(int id)
        {
            var financiador = await _DbContext.Financiadores.FindAsync(id);
            if (financiador != null)
            {
                _DbContext.Financiadores.Remove(financiador);
                await SaveAsync();
            }
        }

        // Confirma as alterações no banco de dados
        public async Task SaveAsync()
        {
            await _DbContext.SaveChangesAsync();
        }

        // Verifica se o financiador existe no banco de dados
        public async Task<bool> ExistsAsync(int id)
        {
            return await _DbContext.Financiadores.AnyAsync(f => f.Id == id);
        }


        // Implementação de IDisposable, muito útil para liberação de recursos 
        private bool _disposed = false;

        // Libera os recursos utilizados pelo contexto
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _DbContext.Dispose();
                }
            }
            _disposed = true;
        }

        // Chama o método Dispose para liberar recursos e suprimir finalização
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
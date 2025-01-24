using System.Collections.Generic;
using System.Threading.Tasks;
using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Repository.Implementations
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
            return await _context.Pessoas.ToListAsync();
        }

        public async Task<Pessoa> GetByIdAsync(int id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        public async Task<Pessoa> AddAsync(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task<Pessoa> UpdateAsync(Pessoa pessoa)
        {
            _context.Pessoa.Update(pessoa);
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

using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using colab.Data;
using Microsoft.EntityFrameworkCore;

namespace colab.Business.Repository.Implementations
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
                .Include(p => p.Bolsas)
                .Include(p => p.HistoricoStatus)
                .ToListAsync();
        }

        public async Task<Projeto> GetByIdAsync(int id)
        {
            return await _context.Projetos
                .Include(p => p.Financiador)
                .Include(p => p.Bolsas)
                .Include(p => p.HistoricoStatus)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Projeto> AddAsync(Projeto projeto)
        {
            // Adicionar o projeto e suas relações
            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
            return projeto;
        }

        public async Task<Projeto> UpdateAsync(Projeto projeto)
        {
            // Atualizar o projeto
            _context.Entry(projeto).State = EntityState.Modified;

            // Atualizar relacionamentos manuais, se necessário
            if (projeto.Financiador != null)
            {
                _context.Entry(projeto.Financiador).State = EntityState.Modified;
            }

            if (projeto.HistoricoStatus != null)
            {
                foreach (var historico in projeto.HistoricoStatus)
                {
                    _context.Entry(historico).State = EntityState.Modified;
                }
            }

            await _context.SaveChangesAsync();
            return projeto;
        }

        public async Task DeleteAsync(int id)
        {
            var projeto = await _context.Projetos
                .Include(p => p.Bolsas)
                .Include(p => p.HistoricoStatus)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (projeto != null)
            {
                // Remover relacionamentos dependentes
                _context.Bolsas.RemoveRange(projeto.Bolsas);
                _context.HistoricoStatusProjetos.RemoveRange(projeto.HistoricoStatus);

                // Remover o projeto
                _context.Projetos.Remove(projeto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddHistoricoStatusAsync(HistoricoProjetoStatus historicoStatus)
        {
            _context.HistoricoStatusProjetos.Add(historicoStatus);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateHistoricoStatusAsync(HistoricoProjetoStatus historico)
        {
            _context.HistoricoStatusProjetos.Update(historico);
            await _context.SaveChangesAsync();
        }
    }
}
using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using colab.Data;
using Microsoft.EntityFrameworkCore;

namespace colab.Business.Repository.Implementations
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
            // retorna uma lista de todos os bolsistas incluindo a Bolsa e o Pesquisador relacionados
            return await _context.Bolsistas
                .Include(b => b.Bolsa)
                .Include(b => b.Orientador)
                .ToListAsync();

        }

        // GET bolsista by id
        public async Task<Bolsista> GetByIdAsync(int id)
        {
            // retorna a primeira correspondência de acordo com o ID de parâmetro
            return await _context.Bolsistas
                .Include(b => b.Bolsa)
                .Include(b => b.Orientador)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        // GET bolsistas by id
        public async Task<List<Bolsista>> GetByIdsAsync(List<int> bolsistasIds)
        {
            return await _context.Bolsistas
                .Where(b => bolsistasIds.Contains(b.Id))
                .ToListAsync();
        }

        // POST
        public async Task<Bolsista> AddAsync(Bolsista bolsista)
        {
            _context.Bolsistas.Add(bolsista); // adiciona o objeto ao contexto do banco de dados
            await _context.SaveChangesAsync(); // salva as alterações feitas no banco de dados
            return bolsista;
        }

        // PUT
        public async Task<Bolsista> UpdateAsync(Bolsista bolsista)
        {
            _context.Bolsistas.Update(bolsista); // diz ao contexto que o objeto precisa ser atualizado
            await _context.SaveChangesAsync();
            return bolsista;
        }

        // DELETE
        public async Task DeleteAsync(int id)
        {
            var bolsista = await _context.Bolsistas.FindAsync(id);

            if (bolsista != null)
            {
                _context.Bolsistas.Remove(bolsista); // marca o bolsista para remoção do banco de dados
                await _context.SaveChangesAsync();
            }
        }



        // ************ IDisposable ************ \\

        private bool _disposed = false; // variável usada para controlar se o objeto foi descartado (disposed) ou não

        protected virtual void Dispose(bool disposing) // chamado internamente para liberar recursos
        {
            if (!_disposed) // verifica se o objeto já foi descartado
            {
                if (disposing)
                {
                    _context.Dispose(); // libera recursos associados à conexão com o banco de dados
                }
            }
            _disposed = true; // indica que o objeto foi descartado
        }

        public void Dispose()
        {
            Dispose(true); // libera os recursos gerenciados
            GC.SuppressFinalize(this); // indica ao coletor de lixo que o objeto foi descartado de forma explícita
        }
    }
}

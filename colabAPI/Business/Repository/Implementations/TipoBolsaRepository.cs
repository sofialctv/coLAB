using colabAPI.Business.Models.Entities;
using colabAPI.Business.DTOs;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Repository.Implementations
{
    public class TipoBolsaRepository : ITipoBolsaRepository, IDisposable
    {
        private readonly ApplicationDbContext _DbContext;

        // Injeta o contexto do banco no construtor
        public TipoBolsaRepository(ApplicationDbContext context)
        {
            _DbContext = context;
        }
        
        public async Task<IEnumerable<TipoBolsa>> GetAllAsync()
        {
            return await _DbContext.TipoBolsa
                .ToListAsync(); // Retorna as entidades diretamente como uma lista
        }
        
        public async Task<TipoBolsa> GetByIdAsync(int id)
        {
            var tipoBolsa = await _DbContext.TipoBolsa
                .FirstOrDefaultAsync(b => b.Id == id);

            if (tipoBolsa == null)
            {
                throw new KeyNotFoundException($"Tipo de Bolsa com ID {id} não foi encontrada.");
            }

            return tipoBolsa;
        }
        
        // Adiciona uma nova tipo de bolsa ao banco de dados
        public async Task AddAsync(TipoBolsa tipoBolsa)
        {
            _DbContext.TipoBolsa.Add(tipoBolsa);
        }
        
        // Atualiza os dados de uma tipo de bolsa existente
        public async Task UpdateAsync(TipoBolsa tipoBolsa)
        {
            if (tipoBolsa == null || tipoBolsa.Id <= 0)
            {
                throw new ArgumentException("Dados de bolsa invalido");
            }

            
            var existeBolsa = _DbContext.TipoBolsa.Find(tipoBolsa.Id); // Verifica se o Tipo de bolsa existe
            if (existeBolsa != null) 
            {
                // Atualiza os valores da bolsa existente
                _DbContext.Entry(existeBolsa).CurrentValues.SetValues(tipoBolsa);
            }
            else
            {
                throw new KeyNotFoundException("Bolsa não encontrada"); // Erro se não encontrada
            }
            
            _DbContext.SaveChanges();
        }
        
        public async Task DeleteAsync(int tipoBolsaID)
        {
            var tipoBolsa = _DbContext.TipoBolsa.Find(tipoBolsaID); // Busca o Tipo de bolsa pelo ID
            if (tipoBolsa != null)
            {
                _DbContext.TipoBolsa.Remove(tipoBolsa); // Remove a Tipo de bolsa  
            }
        }
        
        // Salva as mudanças no banco de dados
        public async Task Save()
        {
            _DbContext.SaveChanges();
        }
        
        private bool _disposed = false; // Flag para rastrear se os recursos já foram liberados.

        // Método protegido para liberar recursos.
        // Ele é chamado tanto pelo método público Dispose() quanto, teoricamente, pelo coletor de lixo (GC).
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed) // Verifica se o repositório já foi descartado.
            {
                if (disposing)
                {
                    _DbContext.Dispose(); // Libera o ApplicationDbContext, que gerencia conexões com o banco.
                }
            }
            _disposed = true; // Marca que os recursos já foram liberados.
        }

        // Método público para descartar o repositório.
        // Implementa a interface IDisposable, permitindo o uso de 'using' ou liberação explícita.
        public void Dispose()
        {
            Dispose(true); // Chama o método Dispose(bool), liberando recursos gerenciados.
            GC.SuppressFinalize(this); // Evita que o coletor de lixo tente liberar o repositório novamente.
        }
    }
}

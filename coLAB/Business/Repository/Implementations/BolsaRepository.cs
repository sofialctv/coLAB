using Microsoft.EntityFrameworkCore;
using colab.Business.Repository.Interfaces;
using colab.Data;
using colab.Business.DTOs;
using colab.Business.Models.Entities;

namespace colab.Business.Repository.Implementations
{
    public class BolsaRepository : IBolsaRepository, IDisposable
    {
        private readonly ApplicationDbContext _DbContext;

        // Injeta o contexto do banco no construtor
        public BolsaRepository(ApplicationDbContext context)
        {
            _DbContext = context;
        }

        public async Task<IEnumerable<BolsaDTO>> GetAllAsync()
        {
            return _DbContext.Bolsas
                .Include(b => b.Pesquisador) // Inclui o relacionamento Pesquisador
                .Select(b => new BolsaDTO // Converte as entidades para DTOs
                {
                    Id = b.Id,
                    Valor = b.Valor,
                    DataInicio = b.DataInicio,
                    DataFim = b.DataFim,
                    DataPrevistaFim = b.DataPrevistaFim,
                    Ativo = b.Ativo,
                    Categoria = b.Categoria,
                    PesquisadorId = b.PesquisadorId,
                }).ToList();
        }

        // Retorna uma bolsa específica como BolsaDTO pelo ID
        public async Task<BolsaDTO> GetByIdAsync(int id)
        {
            var bolsa = _DbContext.Bolsas
                .Include(b => b.Pesquisador) // Inclui o relacionamento Pesquisador
                .FirstOrDefault(b => b.Id == id); // Busca pelo ID

            if (bolsa == null) return null;

            // Converte a entidade para um DTO e retorna
            return new BolsaDTO
            {
                Id = bolsa.Id,
                Valor = bolsa.Valor,
                DataInicio = bolsa.DataInicio,
                DataFim = bolsa.DataFim,
                DataPrevistaFim = bolsa.DataPrevistaFim,
                Ativo = bolsa.Ativo,
                Categoria = bolsa.Categoria,
                PesquisadorId = bolsa.PesquisadorId,
            };
        }

        // Adiciona uma nova bolsa ao banco de dados
        public async Task AddAsync(Bolsa bolsa)
        {
            _DbContext.Bolsas.Add(bolsa);
        }

        // Atualiza os dados de uma bolsa existente
        public async Task UpdateAsync(Bolsa bolsa)
        {
            if (bolsa == null || bolsa.Id <= 0)
            {
                throw new ArgumentException("Dados de bolsa invalido");
            }


            var existeBolsa = _DbContext.Bolsas.Find(bolsa.Id); // Verifica se a bolsa existe
            if (existeBolsa != null)
            {
                // Atualiza os valores da bolsa existente
                _DbContext.Entry(existeBolsa).CurrentValues.SetValues(bolsa);
            }
            else
            {
                throw new KeyNotFoundException("Bolsa não encontrada"); // Erro se não encontrada
            }

            _DbContext.SaveChanges();
        }

        public async Task DeleteAsync(int bolsaID)
        {
            var bolsa = _DbContext.Bolsas.Find(bolsaID); // Busca a bolsa pelo ID
            if (bolsa != null)
            {
                _DbContext.Bolsas.Remove(bolsa); // Remove a bolsa  
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

using colabAPI.Business.Models.Entities;
using colabAPI.Business.DTOs;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Repository.Implementations
{
    public class BolsaRepository : IBolsaRepository, IDisposable
    {
        private readonly ApplicationDbContext _DbContext;

        public BolsaRepository(ApplicationDbContext context)
        {
            _DbContext = context;
        }
        
        public IEnumerable<BolsaDTO> GetBolsas()
        {
            return _DbContext.Bolsas
                .Include(b => b.Pesquisador)
                .Select(b => new BolsaDTO
                {
                    Id = b.Id,
                    Valor = b.Valor,
                    DataInicio = b.DataInicio,
                    DataFim = b.DataFim,
                    DataPrevistaFim = b.DataPrevistaFim,
                    Ativo = b.Ativo,
                    Categoria = b.Categoria,
                    PesquisadorId = b.PesquisadorId,
                    PesquisadorNome = b.Pesquisador != null ? b.Pesquisador.Nome : null
                }).ToList();
        }
        
        public BolsaDTO GetBolsaByID(int bolsaId)
        {
            var bolsa = _DbContext.Bolsas
                .Include(b => b.Pesquisador)
                .FirstOrDefault(b => b.Id == bolsaId);

            if (bolsa == null) return null;

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
                PesquisadorNome = bolsa.Pesquisador != null ? bolsa.Pesquisador.Nome : null
            };
        }
        
        public void InsertBolsa(Bolsa bolsa)
        {
            _DbContext.Bolsas.Add(bolsa);
        }
        
        public void DeleteBolsa(int bolsaID)
        {
            var bolsa = _DbContext.Bolsas.Find(bolsaID);
            if (bolsa != null)
            {
                _DbContext.Bolsas.Remove(bolsa);
            }
        }
        
        public void UpdateBolsa(Bolsa bolsa)
        {
            if (bolsa == null || bolsa.Id <= 0)
            {
                throw new ArgumentException("Invalid bolsa data");
            }


            var existeBolsa = _DbContext.Bolsas.Find(bolsa.Id);

            if (existeBolsa != null)
            {
                _DbContext.Entry(existeBolsa).CurrentValues.SetValues(bolsa);
            }
            else
            {
                throw new KeyNotFoundException("Bolsa not found");
            }

            // Save changes to the context
            _DbContext.SaveChanges();
        }
        
        public void Save()
        {
            _DbContext.SaveChanges();
        }
        
        private bool _disposed = false;

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

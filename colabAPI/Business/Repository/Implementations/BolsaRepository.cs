using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Repository.Implementations
{
    public class BolsaRepository : IBolsaRepository, IDisposable
    {
        private ApplicationDbContext _DbContext;
        
        public BolsaRepository(ApplicationDbContext context)
        {
            this._DbContext = context;
        }
        
        public IEnumerable<Bolsa> GetBolsas()
        {
            return _DbContext.Bolsas.ToList();
        }
        
        public Bolsa GetBolsaById(int id)
        {
            return _DbContext.Bolsas.Find(id);
        }
        
        public void InsertBolsa(Bolsa bolsa)
        {
            _DbContext.Bolsas.Add(bolsa);
        }
        
        public void DeleteBolsa(int bolsaID)
        {
            Bolsa bolsa = _DbContext.Bolsas.Find(bolsaID);
            _DbContext.Bolsas.Remove(bolsa);
        }
        
        public void UpdateBolsa(Bolsa bolsa)
        {
            _DbContext.Entry(bolsa).State = EntityState.Modified;
        }
        
        public void Save()
        {
            _DbContext.SaveChanges();
        }
        
        private bool disposed = false;
        
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

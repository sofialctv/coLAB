using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Repository.Implementations
{
    public class BolsistaRepository : IBolsistaRepository
    {
        private ApplicationDbContext _context;

        public BolsistaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all bolsistas
        public IEnumerable<Bolsista> GetBolsistas()
        {
            return _context.Bolsistas.ToList();
        }

        // Get bolsista by id
        public Bolsista GetBolsistaById(int id)
        {
            return _context.Bolsistas.Find(id);
        }

        // Post 
        public void InsertBolsista(Bolsista bolsista)
        {
            _context.Bolsistas.Add(bolsista);
        }
        
        // Put 
        public void UpdateBolsista(Bolsista bolsista)
        {
            _context.Entry(bolsista).State = EntityState.Modified;
        }
        
        // Delete 
        public void DeleteBolsista(int bolsistaId)
        {
            var bolsista = _context.Bolsistas.Find(bolsistaId);
            _context.Bolsistas.Remove(bolsista);
        }
        
        // Save
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

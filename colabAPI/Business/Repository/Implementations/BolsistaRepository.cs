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

        public BolsistaDto ConvertToDto(Bolsista bolsista)
        { // Usando Reflection nos atributos da classe
            
            var bolsistaDto = new BolsistaDto();
            var sourceProperties = bolsista.GetType().GetProperties();
            var dtoProperties = bolsistaDto.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var dtoProperty = dtoProperties.FirstOrDefault(b => 
                    b.Name == sourceProperty.Name);

                if (dtoProperty != null && dtoProperty.CanWrite)
                {
                    dtoProperty.SetValue(bolsistaDto, sourceProperty.GetValue(bolsista));
                }
            }
            
            return bolsistaDto;
        }

        

        // GET all bolsistas
        public async Task<IEnumerable<Bolsista>> GetAllAsync()
        {
            return await _context.Bolsistas
                .Include(b => b.Bolsa)
                .ToListAsync();

        }

        // GET bolsista by id
        public async Task<BolsistaDto> GetByIdAsync(int bolsistaId)
        {
            var bolsistaRequisitado = await _context.Bolsistas.FindAsync(bolsistaId);

            if (bolsistaRequisitado == null)
            {
                return null;
            }
            else
            {
                return ConvertToDto(bolsistaRequisitado);
            }
        }

        // POST 
        public async Task<Bolsista> AddAsync(Bolsista bolsista)
        {
            _context.Bolsistas.Add(bolsista);
            await _context.SaveChangesAsync();
            return bolsista;
        }
        
        // PUT
        public async Task<bool> UpdateAsync(Bolsista bolsista)
        {
            var existingBolsista = await _context.Bolsistas
                .FindAsync(bolsista.Id);

            if (existingBolsista == null)
            {
                throw new ArgumentNullException(
                    nameof(bolsista),
                    "Bolsista não encontrado.");
            }
            
            // Usando Reflection nos atributos da classe
            var properties = typeof(Bolsista).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(bolsista);
                var existingValue = property.GetValue(existingBolsista);

                if (newValue == null || existingValue == null)
                {
                    throw new ArgumentNullException(
                        property.Name,
                        "Atributo não encontrado.");
                }
                
                property.SetValue(existingBolsista, newValue);
                await _context.SaveChangesAsync();
            }

            return true;
        }
        
        // DELETE
        public async Task<bool> DeleteAsync(int bolsistaId)
        {
            var bolsista = await _context.Bolsistas.FindAsync(bolsistaId);
            if (bolsista == null)
            {
                return false;
            }
            
            _context.Bolsistas.Remove(bolsista);
            await _context.SaveChangesAsync();
            return true;
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

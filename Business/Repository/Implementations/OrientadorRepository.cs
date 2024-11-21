using colabAPI.Business.Repository.Interfaces;
using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Repository.Implementations
{
    public class OrientadorRepository : IOrientadorRepository
    {
        private readonly ApplicationDbContext _context;

        public OrientadorDTO ConvertToDto(Orientador orientador)
        { // Usando Reflection nos atributos da classe

            var orientadorDto = new OrientadorDTO();
            var sourceProperties = orientador.GetType().GetProperties();
            var dtoProperties = orientadorDto.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var dtoProperty = dtoProperties.FirstOrDefault(b =>
                    b.Name == sourceProperty.Name);

                if (dtoProperty != null && dtoProperty.CanWrite)
                {
                    dtoProperty.SetValue(orientadorDto, sourceProperty.GetValue(orientador));
                }
            }

            return orientadorDto;
        }

        public OrientadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET all bolsistas
        public async Task<IEnumerable<OrientadorDTO>> GetAllAsync()
        {
            var orientadores = await _context.Orientadores.ToListAsync();
            return orientadores.Select(o => ConvertToDto(o));
        }

        // GET bolsista by id
        public async Task<OrientadorDTO> GetByIdAsync(int orientadorID)
        {
            var orientadorRequisitado = _context.Orientadores.Find(orientadorID);
            if (orientadorRequisitado == null)
            {
                return null;
            }
            
            return ConvertToDto(orientadorRequisitado);            
        }

        // POST 
        public async Task AddAsync(Orientador orientador)
        {
            _context.Orientadores.Add(orientador);
            await _context.SaveChangesAsync();
        }

        // PUT
        public async Task<bool> UpdateAsync(Orientador orientador)
        {
            var existingOrientador = await _context.Orientadores
                .FindAsync(orientador.Id);

            if (existingOrientador == null)
            {
                throw new ArgumentNullException(
                    nameof(orientador),
                    "Orientador não encontrado.");
            }

            // Usando Reflection nos atributos da classe
            var properties = typeof(Orientador).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(orientador);
                var existingValue = property.GetValue(existingOrientador);

                if (newValue == null && existingValue == null)
                {

                }
                else if (newValue == null || existingValue == null)
                {
                    throw new ArgumentNullException(
                        property.Name,
                        "Atributo não encontrado.");
                }

                property.SetValue(existingOrientador, newValue);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int orientadorId)
        {
            var orientador = await _context.Orientadores.FindAsync(orientadorId);
            if (orientador == null)
            {
                return false;
            }

            _context.Orientadores.Remove(orientador);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

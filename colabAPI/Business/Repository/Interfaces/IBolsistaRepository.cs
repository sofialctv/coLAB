using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces
{
    public interface IBolsistaRepository
    {
        // Métodos
        Task<IEnumerable<Bolsista>> GetAllAsync();
        Task<Bolsista> GetByIdAsync(int id);
        Task<List<Bolsista>> GetByIdsAsync(List<int> ids);
        Task<Bolsista> AddAsync(Bolsista bolsista);
        Task<Bolsista> UpdateAsync(Bolsista bolsista);
        Task DeleteAsync(int id);
    }
}
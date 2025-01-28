using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces
{
    public interface IBolsaRepository
    {
        Task<IEnumerable<Bolsa>> GetAllAsync();
        Task<Bolsa> GetByIdAsync(int id);
        Task AddAsync(Bolsa bolsa);
        Task UpdateAsync(Bolsa bolsa);
        Task DeleteAsync(int id);
        Task Save();
    }
}

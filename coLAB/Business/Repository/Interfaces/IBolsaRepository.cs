using colab.Business.DTOs;
using colab.Business.Models.Entities;

namespace colab.Business.Repository.Interfaces
{
    public interface IBolsaRepository
    {
        Task<IEnumerable<BolsaDTO>> GetAllAsync();
        Task<BolsaDTO> GetByIdAsync(int id);
        Task AddAsync(Bolsa bolsa);
        Task UpdateAsync(Bolsa bolsa);
        Task DeleteAsync(int id);
        Task Save();
    }
}
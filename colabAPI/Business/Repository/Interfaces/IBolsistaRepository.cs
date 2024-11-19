using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces
{
    public interface IBolsistaRepository
    {
        // Métodos
        Task<IEnumerable<BolsistaDto>> GetAllAsync();
        Task<BolsistaDto> GetByIdAsync(int id);
        Task AddAsync(Bolsista bolsista);
        Task<bool> UpdateAsync(Bolsista bolsista);
        Task<bool> DeleteAsync(int id);
        BolsistaDto ConvertToDto(Bolsista bolsista);
    }
}
using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces
{
    public interface IOrientadorRepository
    {
        Task<IEnumerable<OrientadorDTO>> GetAllAsync();
        Task<OrientadorDTO> GetByIdAsync(int id);
        Task AddAsync(Orientador orientador);
        Task<bool> UpdateAsync(Orientador orientador);
        Task<bool> DeleteAsync(int id);
        OrientadorDTO ConvertToDto(Orientador orientador);
    }
}

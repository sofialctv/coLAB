using colab.Business.Models.Entities;

namespace colab.Business.Repository.Interfaces
{
    public interface ICargoRepository
    {
        Task<IEnumerable<Cargo>> GetAllAsync();
        Task<Cargo> GetByIdAsync(int id);
        Task<Cargo> AddAsync(Cargo cargo);
        Task<Cargo> UpdateAsync(Cargo cargo);
        Task DeleteAsync(int id);
    }
}
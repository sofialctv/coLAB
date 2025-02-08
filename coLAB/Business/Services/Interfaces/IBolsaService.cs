using colab.Business.Models.Entities;

namespace colab.Business.Services.Interfaces
{
    public interface IBolsaService
    {
        Task<IEnumerable<Bolsa>> GetAllAsync();
        Task<Bolsa> GetByIdAsync(int id);
        Task AddAsync(Bolsa bolsa);
        Task UpdateAsync(Bolsa bolsa);
        Task DeleteAsync(int id);
    }
}

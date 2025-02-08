using colab.Business.Models.Entities;

namespace colab.Business.Services.Interfaces
{
    public interface ITipoBolsaService
    {
        Task<IEnumerable<TipoBolsa>> GetAllAsync();
        Task<TipoBolsa> GetByIdAsync(int id);
        Task AddAsync(TipoBolsa tipoBolsa);
        Task UpdateAsync(TipoBolsa tipoBolsa);
        Task DeleteAsync(int id);
    }
}

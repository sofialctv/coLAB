using colab.Business.Models.Entities;

namespace colab.Business.Repository.Interfaces;

public interface ITipoBolsaRepository
{ 
    Task<IEnumerable<TipoBolsa>> GetAllAsync();
    Task<TipoBolsa> GetByIdAsync(int id);
    Task AddAsync(TipoBolsa tipoBolsa);
    Task UpdateAsync(TipoBolsa tipoBolsa);
    Task DeleteAsync(int id);
}
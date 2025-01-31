using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces;

public interface ITipoBolsaRepository
{ 
    Task<IEnumerable<TipoBolsa>> GetAllAsync();
    Task<TipoBolsa> GetByIdAsync(int id);
    Task AddAsync(TipoBolsa tipoBolsa);
    Task UpdateAsync(TipoBolsa tipoBolsa);
    Task DeleteAsync(int id);
    Task Save();
}
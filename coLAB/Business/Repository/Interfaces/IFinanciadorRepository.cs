using colab.Business.Models.Entities;

namespace colab.Business.Repository.Interfaces
{
    public interface IFinanciadorRepository
    {
        Task<IEnumerable<Financiador>> GetAllAsync();
        Task<Financiador> GetByIdAsync(int id);
        Task<Financiador> AddAsync(Financiador financiador);
        Task<Financiador> UpdateAsync(Financiador financiador);
        Task DeleteAsync(int id);
    }
}

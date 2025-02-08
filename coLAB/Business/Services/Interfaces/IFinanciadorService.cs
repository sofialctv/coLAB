using colab.Business.Models.Entities;

namespace colab.Business.Services.Interfaces
{
    public interface IFinanciadorService
    {
        Task<IEnumerable<Financiador>> GetAllAsync();
        Task<Financiador> GetByIdAsync(int id);
        Task<Financiador> AddAsync(Financiador financiador);
        Task<Financiador> UpdateAsync(Financiador financiador);
        Task DeleteAsync(int id);
    }
}

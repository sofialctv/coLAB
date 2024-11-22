using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces
{
    // Interface que define os métodos assíncronos para interação com os dados dos financiadores
    public interface IFinanciadorRepository
    {
        Task<IEnumerable<Financiador>> GetAllAsync();
        Task<Financiador?> GetByIdAsync(int id);
        Task AddAsync(Financiador financiador);
        Task UpdateAsync(Financiador financiador);
        Task DeleteAsync(int id);
    }
}

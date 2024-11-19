using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces
{
    public interface IProjetoRepository
    {
        Task<IEnumerable<Projeto>> GetAllAsync();
        Task<Projeto> GetByIdAsync(int id);
        Task<Projeto> AddAsync(Projeto projeto);
        Task<Projeto> UpdateAsync(Projeto projeto);
        Task DeleteAsync(int id);
    }
}

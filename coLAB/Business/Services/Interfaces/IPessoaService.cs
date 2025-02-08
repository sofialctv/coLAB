using colab.Business.Models.Entities;

namespace colab.Business.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Task<Pessoa> GetByIdAsync(int id);
        Task<Pessoa> AddAsync(Pessoa pessoa);
        Task<Pessoa> UpdateAsync(Pessoa pessoa);
        Task DeleteAsync(int id);
    }
}

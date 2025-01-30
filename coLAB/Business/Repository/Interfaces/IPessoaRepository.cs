using System.Collections.Generic;
using System.Threading.Tasks;
using colab.Business.Models.Entities;

namespace colab.Business.Repository.Interfaces
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> GetAllAsync();
        Task<Pessoa> GetByIdAsync(int id);
        Task<Pessoa> AddAsync(Pessoa pessoa);
        Task<Pessoa> UpdateAsync(Pessoa pessoa);
        Task DeleteAsync(int id);
    }
}
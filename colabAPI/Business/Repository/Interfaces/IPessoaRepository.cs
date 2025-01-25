using System.Collections.Generic;
using System.Threading.Tasks;
using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces
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
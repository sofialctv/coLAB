using System.Collections.Generic;
using System.Threading.Tasks;

namespace colabAPI.Business.Repository.Interfaces
{
    public interface ICargoRepository
    {
        Task<IEnumerable< >> GetAllAsync();
        Task< > GetByIdAsync(int id);
        Task AddAsync();
        Task UpdateAsync();
        Task DeleteAsync(int id);
        Task Save();
    }
}
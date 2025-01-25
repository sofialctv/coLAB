using System.Collections.Generic;
using System.Threading.Tasks;
using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces
{
    public interface IHistoricoCargoRepository
    {
        Task<IEnumerable<HistoricoCargo>> GetAllAsync();
        Task<HistoricoCargo> GetByIdAsync(int id);
        Task<HistoricoCargo> AddAsync(HistoricoCargo historicoCargo);
        Task<HistoricoCargo> UpdateAsync(HistoricoCargo historicoCargo);
        Task DeleteAsync(int id);
    }
}
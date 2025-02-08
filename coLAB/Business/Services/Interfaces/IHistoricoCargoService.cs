using colab.Business.Models.Entities;

namespace colab.Business.Services.Interfaces
{
    public interface IHistoricoCargoService
    {
        Task<IEnumerable<HistoricoCargo>> GetAllAsync();
        Task<HistoricoCargo> GetByIdAsync(int id);
        Task<HistoricoCargo> AddAsync(HistoricoCargo historicoCargo);
        Task<HistoricoCargo> UpdateAsync(HistoricoCargo historicoCargo);
        Task DeleteAsync(int id);
    }
}

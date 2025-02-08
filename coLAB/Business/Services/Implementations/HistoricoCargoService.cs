using colab.Business.Models.Entities;
using colab.Business.Repository.Implementations;
using colab.Business.Repository.Interfaces;
using colab.Business.Services.Interfaces;

namespace colab.Business.Services.Implementations
{
    public class HistoricoCargoService : IHistoricoCargoService
    {
        private readonly IHistoricoCargoRepository _historicoCargoRepository;

        public HistoricoCargoService(IHistoricoCargoRepository historicoCargoRepository)
        {
            _historicoCargoRepository = historicoCargoRepository;
        }

        public async Task<IEnumerable<HistoricoCargo>> GetAllAsync()
        {
            return await _historicoCargoRepository.GetAllAsync();
        }

        public async Task<HistoricoCargo> GetByIdAsync(int id)
        {
            return await _historicoCargoRepository.GetByIdAsync(id);
        }

        public async Task<HistoricoCargo> AddAsync(HistoricoCargo historicoCargo)
        {
            return await _historicoCargoRepository.AddAsync(historicoCargo);
        }

        public async Task<HistoricoCargo> UpdateAsync(HistoricoCargo historicoCargo)
        {
            return await _historicoCargoRepository.UpdateAsync(historicoCargo);
        }

        public async Task DeleteAsync(int id)
        {
            await _historicoCargoRepository.DeleteAsync(id);
        }
    }
}

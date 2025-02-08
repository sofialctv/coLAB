using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using colab.Business.Services.Interfaces;

namespace colab.Business.Services.Implementations
{
    public class ProjetoService : IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoService(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public async Task<IEnumerable<Projeto>> GetAllAsync()
        {
            return await _projetoRepository.GetAllAsync();
        }

        public async Task<Projeto> GetByIdAsync(int id)
        {
            return await _projetoRepository.GetByIdAsync(id);
        }

        public async Task<Projeto> AddAsync(Projeto projeto)
        {
            return await _projetoRepository.AddAsync(projeto);
        }

        public async Task<Projeto> UpdateAsync(Projeto projeto)
        {
            return await _projetoRepository.UpdateAsync(projeto);
        }

        public async Task DeleteAsync(int id)
        {
            await _projetoRepository.DeleteAsync(id);
        }

        public async Task AddHistoricoStatusAsync(HistoricoProjetoStatus historicoStatus)
        {
            await _projetoRepository.AddHistoricoStatusAsync(historicoStatus);
        }

        public async Task UpdateHistoricoStatusAsync(HistoricoProjetoStatus historico)
        {
            await _projetoRepository.UpdateHistoricoStatusAsync(historico);
        }
    }
}

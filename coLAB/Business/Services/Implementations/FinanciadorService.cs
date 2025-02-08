using colab.Business.Models.Entities;
using colab.Business.Repository.Implementations;
using colab.Business.Repository.Interfaces;
using colab.Business.Services.Interfaces;

namespace colab.Business.Services.Implementations
{
    public class FinanciadorService : IFinanciadorService
    {
        private readonly IFinanciadorRepository _financiadorRepository;

        public FinanciadorService(IFinanciadorRepository financiadorRepository)
        {
            _financiadorRepository = financiadorRepository;
        }

        public async Task<IEnumerable<Financiador>> GetAllAsync()
        {
            return await _financiadorRepository.GetAllAsync();
        }

        public async Task<Financiador> GetByIdAsync(int id)
        {
            return await _financiadorRepository.GetByIdAsync(id);
        }

        public async Task<Financiador> AddAsync(Financiador financiador)
        {
            return await _financiadorRepository.AddAsync(financiador);
        }

        public async Task<Financiador> UpdateAsync(Financiador financiador)
        {
            return await _financiadorRepository.UpdateAsync(financiador);
        }

        public async Task DeleteAsync(int id)
        {
            await _financiadorRepository.DeleteAsync(id);
        }
    }
}

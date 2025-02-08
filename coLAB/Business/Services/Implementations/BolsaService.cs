using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using colab.Business.Services.Interfaces;

namespace colab.Business.Services.Implementations
{
    public class BolsaService : IBolsaService
    {
        private readonly IBolsaRepository _bolsaRepository;

        public BolsaService(IBolsaRepository bolsaRepository)
        {
            _bolsaRepository = bolsaRepository;
        }

        public async Task<IEnumerable<Bolsa>> GetAllAsync()
        {
            return await _bolsaRepository.GetAllAsync();
        }

        public async Task<Bolsa> GetByIdAsync(int id)
        {
            var bolsa = await _bolsaRepository.GetByIdAsync(id);
            if (bolsa == null)
            {
                throw new KeyNotFoundException($"Bolsa com ID {id} não foi encontrada.");
            }
            return bolsa;
        }

        public async Task AddAsync(Bolsa bolsa)
        {
            if (bolsa == null)
            {
                throw new ArgumentException("Dados inválidos para a bolsa.");
            }

            await _bolsaRepository.AddAsync(bolsa);
            await _bolsaRepository.Save();
        }

        public async Task UpdateAsync(Bolsa bolsa)
        {
            if (bolsa == null || bolsa.Id <= 0)
            {
                throw new ArgumentException("Dados de bolsa inválidos.");
            }

            var existeBolsa = await _bolsaRepository.GetByIdAsync(bolsa.Id);
            if (existeBolsa == null)
            {
                throw new KeyNotFoundException("Bolsa não encontrada.");
            }

            await _bolsaRepository.UpdateAsync(bolsa);
            await _bolsaRepository.Save();
        }

        public async Task DeleteAsync(int id)
        {
            var bolsa = await _bolsaRepository.GetByIdAsync(id);
            if (bolsa == null)
            {
                throw new KeyNotFoundException("Bolsa não encontrada.");
            }

            await _bolsaRepository.DeleteAsync(id);
            await _bolsaRepository.Save();
        }
    }
}

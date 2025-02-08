using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using colab.Business.Services.Interfaces;

namespace colab.Business.Services.Implementations
{
    public class TipoBolsaService : ITipoBolsaService
    {
        private readonly ITipoBolsaRepository _tipoBolsaRepository;

        public TipoBolsaService(ITipoBolsaRepository tipoBolsaRepository)
        {
            _tipoBolsaRepository = tipoBolsaRepository;
        }

        public async Task<IEnumerable<TipoBolsa>> GetAllAsync()
        {
            return await _tipoBolsaRepository.GetAllAsync();
        }

        public async Task<TipoBolsa> GetByIdAsync(int id)
        {
            var tipoBolsa = await _tipoBolsaRepository.GetByIdAsync(id);
            if (tipoBolsa == null)
            {
                throw new KeyNotFoundException($"Tipo de Bolsa com ID {id} não encontrada.");
            }
            return tipoBolsa;
        }

        public async Task AddAsync(TipoBolsa tipoBolsa)
        {
            if (tipoBolsa == null)
            {
                throw new ArgumentNullException(nameof(tipoBolsa), "O tipo de bolsa não pode ser nulo.");
            }

            // Lógica de validação antes de adicionar
            if (string.IsNullOrEmpty(tipoBolsa.nome))
            {
                throw new ArgumentException("Nome do tipo de bolsa é obrigatório.");
            }

            await _tipoBolsaRepository.AddAsync(tipoBolsa);
        }

        public async Task UpdateAsync(TipoBolsa tipoBolsa)
        {
            if (tipoBolsa == null)
            {
                throw new ArgumentNullException(nameof(tipoBolsa), "O tipo de bolsa não pode ser nulo.");
            }

            // Lógica de validação antes de atualizar
            if (tipoBolsa.Id <= 0)
            {
                throw new ArgumentException("ID do tipo de bolsa inválido.");
            }

            var tipoBolsaExistente = await _tipoBolsaRepository.GetByIdAsync(tipoBolsa.Id);
            if (tipoBolsaExistente == null)
            {
                throw new KeyNotFoundException($"Tipo de Bolsa com ID {tipoBolsa.Id} não encontrada.");
            }

            await _tipoBolsaRepository.UpdateAsync(tipoBolsa);
        }

        public async Task DeleteAsync(int id)
        {
            var tipoBolsa = await _tipoBolsaRepository.GetByIdAsync(id);
            if (tipoBolsa == null)
            {
                throw new KeyNotFoundException($"Tipo de Bolsa com ID {id} não encontrada.");
            }

            await _tipoBolsaRepository.DeleteAsync(id);
        }
    }
}

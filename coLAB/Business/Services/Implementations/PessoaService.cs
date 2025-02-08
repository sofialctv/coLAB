using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using colab.Business.Services.Interfaces;

namespace colab.Business.Services.Implementations
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            return await _pessoaRepository.GetAllAsync();
        }

        public async Task<Pessoa> GetByIdAsync(int id)
        {
            return await _pessoaRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Pessoa com ID {id} não encontrada.");
        }

        public async Task<Pessoa> AddAsync(Pessoa pessoa)
        {
            return await _pessoaRepository.AddAsync(pessoa);
        }

        public async Task<Pessoa> UpdateAsync(Pessoa pessoa)
        {
            return await _pessoaRepository.UpdateAsync(pessoa);
        }

        public async Task DeleteAsync(int id)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                throw new KeyNotFoundException($"Pessoa com ID {id} não encontrada.");
            }
            await _pessoaRepository.DeleteAsync(id);
        }
    }
}

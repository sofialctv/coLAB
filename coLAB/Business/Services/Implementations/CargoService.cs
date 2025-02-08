using colab.Business.Models.Entities;
using colab.Business.Services.Interfaces;
using colab.Business.Repository.Interfaces;
using colab.Business.Repository.Implementations;

namespace colab.Business.Services.Implementations
{
    public class CargoService : ICargoService
    {
        private readonly ICargoRepository _cargoRepository;

        public CargoService(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<IEnumerable<Cargo>> GetAllAsync()
        {
            return await _cargoRepository.GetAllAsync();
        }

        public async Task<Cargo> GetByIdAsync(int id)
        {
            return await _cargoRepository.GetByIdAsync(id);
        }

        public async Task<Cargo> AddAsync(Cargo cargo)
        {
            return await _cargoRepository.AddAsync(cargo);
        }

        public async Task<Cargo> UpdateAsync(Cargo cargo)
        {
            return await _cargoRepository.UpdateAsync(cargo);
        }

        public async Task DeleteAsync(int id)
        {
            await _cargoRepository.DeleteAsync(id);
        }
    }
}

using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces
{
    public interface IBolsaRepository
    {
        IEnumerable<BolsaDTO> GetBolsas();
        BolsaDTO GetBolsaByID(int bolsaId);
        void InsertBolsa(Bolsa bolsa);
        void DeleteBolsa(int bolsaID);
        void UpdateBolsa(Bolsa bolsa);
        void Save();
    }
}
using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces
{
    public interface IBolsaRepository
    {
        IEnumerable<Bolsa> GetBolsas();
        Bolsa getBolsaByID(int bolsaId);
        void InsertBolsa(Bolsa bolsa);
        void DeleteBolsa(int bolsaID);
        void UpdateBolsa(Bolsa bolsa);
        void Save();
    }
}

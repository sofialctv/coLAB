using colabAPI.Business.Models.Entities;

namespace colabAPI.Business.Repository.Interfaces
{
    public interface IBolsistaRepository
    {
        IEnumerable<Bolsista> GetBolsistas();
        Bolsista GetBolsistaById(int bolsistaId);
        void InsertBolsista(Bolsista bolsista);
        void UpdateBolsista(Bolsista bolsista);
        void DeleteBolsista(int bolsistaId);
        void Save();
    }
}

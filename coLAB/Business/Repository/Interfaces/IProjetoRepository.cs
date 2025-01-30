using colab.Business.Models.Entities;

namespace colab.Business.Repository.Interfaces
{
    public interface IProjetoRepository
    {
        Task<IEnumerable<Projeto>> GetAllAsync();
        Task<Projeto> GetByIdAsync(int id);
        Task<Projeto> AddAsync(Projeto projeto);
        Task<Projeto> UpdateAsync(Projeto projeto);
        Task DeleteAsync(int id);

        Task AddHistoricoStatusAsync(HistoricoProjetoStatus historicoStatus);

        Task UpdateHistoricoStatusAsync(HistoricoProjetoStatus historico);
    }
}
using colab.Business.DTOs;
using colab.Business.Models.Entities;

namespace colab.Business.Repository.Interfaces
{
    // Interface que define o contrato para o repositório de Orientador
    public interface IOrientadorRepository
    {
        // Método para obter todos os orientadores no formato DTO
        Task<IEnumerable<OrientadorDTO>> GetAllAsync();

        // Método para obter um orientador específico pelo ID no formato DTO
        Task<OrientadorDTO> GetByIdAsync(int id);

        // Método para adicionar um novo orientador à base de dados
        Task AddAsync(Orientador orientador);

        // Método para atualizar os dados de um orientador existente
        Task<bool> UpdateAsync(Orientador orientador);

        // Método para excluir um orientador da base de dados usando o ID
        Task<bool> DeleteAsync(int id);

        // Método para converter uma entidade Orientador em seu DTO correspondente
        OrientadorDTO ConvertToDto(Orientador orientador);
    }
}
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;
using colabAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Business.Repository.Implementations
{
    // Implementação do repositório para a entidade Orientador
    public class OrientadorRepository : IOrientadorRepository
    {
        // Contexto de banco de dados do Entity Framework
        private readonly ApplicationDbContext _context;

        // Método para converter uma entidade Orientador para seu DTO correspondente
        public OrientadorDTO ConvertToDto(Orientador orientador)
        {
            // Utilizando Reflection para mapear as propriedades da classe automaticamente
            var orientadorDto = new OrientadorDTO();
            var sourceProperties = orientador.GetType().GetProperties();
            var dtoProperties = orientadorDto.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                // Encontrando a propriedade correspondente no DTO com base no nome
                var dtoProperty = dtoProperties.FirstOrDefault(b =>
                    b.Name == sourceProperty.Name);

                // Verifica se a propriedade existe no DTO e se pode ser escrita
                if (dtoProperty != null && dtoProperty.CanWrite)
                {
                    // Define o valor da propriedade do DTO com base na propriedade da entidade
                    dtoProperty.SetValue(orientadorDto, sourceProperty.GetValue(orientador));
                }
            }

            return orientadorDto;
        }

        // Construtor que recebe o contexto de banco de dados via injeção de dependência
        public OrientadorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para recuperar todos os orientadores e convertê-los para DTOs
        public async Task<IEnumerable<OrientadorDTO>> GetAllAsync()
        {
            var orientadores = await _context.Orientadores.ToListAsync();
            return orientadores.Select(o => ConvertToDto(o));
        }

        // Método para recuperar um orientador específico pelo ID
        public async Task<OrientadorDTO> GetByIdAsync(int orientadorID)
        {
            // Busca o orientador no banco de dados
            var orientadorRequisitado = _context.Orientadores.Find(orientadorID);
            if (orientadorRequisitado == null)
            {
                // Retorna null se o orientador não for encontrado
                return null;
            }

            return ConvertToDto(orientadorRequisitado);
        }

        // Método para adicionar um novo orientador ao banco de dados
        public async Task AddAsync(Orientador orientador)
        {
            _context.Orientadores.Add(orientador);
            await _context.SaveChangesAsync();
        }

        // Método para atualizar um orientador existente
        public async Task<bool> UpdateAsync(Orientador orientador)
        {
            // Busca o orientador existente pelo ID
            var existingOrientador = await _context.Orientadores
                .FindAsync(orientador.Id);

            if (existingOrientador == null)
            {
                // Lança exceção se o orientador não for encontrado
                throw new ArgumentNullException(
                    nameof(orientador),
                    "Orientador não encontrado.");
            }

            // Utiliza Reflection para atualizar todas as propriedades do orientador
            var properties = typeof(Orientador).GetProperties();
            foreach (var property in properties)
            {
                var newValue = property.GetValue(orientador);
                var existingValue = property.GetValue(existingOrientador);

                // Lógica adicional para tratar valores nulos
                if (newValue == null && existingValue == null)
                {
                    // Ignora a propriedade se ambos os valores forem nulos
                    continue;
                }
                else if (newValue == null || existingValue == null)
                {
                    // Lança exceção se houver inconsistência nos valores
                    throw new ArgumentNullException(
                        property.Name,
                        "Atributo não encontrado.");
                }

                // Atualiza o valor da propriedade no objeto existente
                property.SetValue(existingOrientador, newValue);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        // Método para excluir um orientador pelo ID
        public async Task<bool> DeleteAsync(int orientadorId)
        {
            // Busca o orientador no banco de dados
            var orientador = await _context.Orientadores.FindAsync(orientadorId);
            if (orientador == null)
            {
                // Retorna false se o orientador não for encontrado
                return false;
            }

            // Remove o orientador do banco de dados
            _context.Orientadores.Remove(orientador);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

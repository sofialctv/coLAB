using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace colabAPI.Business.Models.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Define a rota base como "api/orientador"
    public class OrientadorController : ControllerBase
    {
        // Dependência do repositório de Orientador
        private readonly IOrientadorRepository _orientadorRepository;

        // Construtor que injeta a dependência do repositório
        public OrientadorController(IOrientadorRepository orientadorRepository)
        {
            _orientadorRepository = orientadorRepository;
        }

        // GET: api/orientadores
        // Retorna todos os orientadores como uma lista de DTOs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrientadorDTO>>> GetAllOrientadores()
        {
            var orientadores = await _orientadorRepository.GetAllAsync();
            return Ok(orientadores); // Retorna um status 200 com os dados
        }

        // GET: api/orientador/{id}
        // Retorna um orientador específico com base no ID
        [HttpGet("{id}")]
        public async Task<ActionResult<OrientadorDTO>> GetOrientadorById(int id)
        {
            var orientador = await _orientadorRepository.GetByIdAsync(id);

            if (orientador == null)
            {
                return NotFound(); // Retorna um status 404 se não encontrado
            }

            return Ok(orientador); // Retorna um status 200 com os dados
        }

        // POST: api/orientador
        // Cria um novo orientador com base nos dados enviados
        [HttpPost]
        public async Task<ActionResult<OrientadorDTO>> CreateOrientador(OrientadorDTO orientadorDto)
        {
            var orientador = new Orientador();

            // Mapeia as propriedades do DTO para a entidade usando Reflection
            var dtoProperties = typeof(OrientadorDTO).GetProperties();
            var orientadorProperties = typeof(Orientador).GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                var orientadorProperty = orientadorProperties
                    .FirstOrDefault(b => b.Name == dtoProperty.Name);
                if (orientadorProperty != null && orientadorProperty.CanWrite)
                {
                    orientadorProperty.SetValue(orientador, dtoProperty.GetValue(orientadorDto));
                }
            }

            // Adiciona o novo orientador no repositório
            await _orientadorRepository.AddAsync(orientador);

            // Retorna um status 201 Created com a localização do novo recurso
            return CreatedAtAction(
                nameof(GetOrientadorById),
                new { id = orientador.Id },
                _orientadorRepository.ConvertToDto(orientador));
        }

        // PUT: api/orientador/{id}
        // Atualiza os dados de um orientador existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrientador(int id, OrientadorDTO orientadorDto)
        {

            var orientador = new Orientador();

            // Mapeia as propriedades do DTO para a entidade usando Reflection
            var dtoProperties = orientadorDto.GetType().GetProperties();
            var orientadorProperties = orientador.GetType().GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                var orientadorProperty = orientadorProperties
                    .FirstOrDefault(b => b.Name == dtoProperty.Name);
                if (orientadorProperty != null && orientadorProperty.CanWrite)
                {
                    orientadorProperty.SetValue(orientador, dtoProperty.GetValue(orientadorDto));
                }
            }

            // Atualiza o orientador no repositório
            var updated = await _orientadorRepository.UpdateAsync(orientador);

            if (!updated)
            {
                return NotFound(); // Retorna um status 404 se o orientador não for encontrado
            }

            return NoContent(); // Retorna um status 204 se a atualização foi bem-sucedida
        }

        // DELETE: api/orientador/{id}
        // Exclui um orientador pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrientador(int id)
        {
            var deleted = await _orientadorRepository.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(); // Retorna um status 404 se o orientador não for encontrado
            }

            return NoContent(); // Retorna um status 204 se a exclusão foi bem-sucedida
        }
    }
}

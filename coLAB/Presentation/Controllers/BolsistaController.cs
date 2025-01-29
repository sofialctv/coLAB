using colab.Business.DTOs;
using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace colab.Presentation.Controllers
{

    [ApiController] // controlador de API no ASP.NET Core
    [Route("api/[controller]")]
    public class BolsistaController : ControllerBase
    {
        private readonly IBolsistaRepository _bolsistaRepository;

        public BolsistaController(IBolsistaRepository bolsistaRepository)
        {
            _bolsistaRepository = bolsistaRepository;
        }

        // GET: api/bolsista
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BolsistaDTO>>> GetAll()
        {
            var bolsistas = await _bolsistaRepository.GetAllAsync(); // chamada assíncrona 
            var bolsistasDtos = bolsistas.Select(
                b => ConvertToDto(b)) // converte cada bolsista para DTO
                .ToList(); // converte o resultado para uma lista

            return Ok(bolsistasDtos); // retorna uma resposta HTTP 200 e a lista de DTO
        }

        // GET: api/bolsista/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BolsistaDTO>> GetById(int id)
        {
            var bolsista = await _bolsistaRepository.GetByIdAsync(id);

            if (bolsista == null)
            {
                return NotFound(); // 404 Not Found
            }

            var bolsistaDto = ConvertToDto(bolsista);

            return Ok(bolsistaDto); // retorna uma resposta HTTP 200 e o BolsistaDTO
        }

        // POST api/bolsista
        [HttpPost]
        public async Task<ActionResult> Create(BolsistaDTO bolsistaDto)
        {
            var bolsista = new Bolsista();

            MapProperties(bolsistaDto, bolsista);

            var createdBolsista = await _bolsistaRepository.AddAsync(bolsista);

            return CreatedAtAction( // retorna uma resposta HTTP 201 Created
                nameof(GetById), // especifica a ação usada para recuperar o recurso recém-criado 
                new { id = createdBolsista.Id }, // cria um objeto anônimo com o ID do Bolsista recém-criado para compor a URL
                createdBolsista); // inclui o objeto Bolsista recém-criado no corpo da resposta
        }

        // PUT: api/bolsista/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BolsistaDTO bolsistaDto)
        {
            if (id != bolsistaDto.Id) // verifica se o id fornecido na requisição é igual ao id do objeto
            {
                return BadRequest(); // 400 Bad Request
            }

            var bolsista = await _bolsistaRepository.GetByIdAsync(id);

            if (bolsista == null)
            {
                return NotFound(); // 404 Not Found
            }

            MapProperties(bolsistaDto, bolsista);

            await _bolsistaRepository.UpdateAsync(bolsista);

            return NoContent(); // 204 No Content (atualização bem-sucedida sem conteúdo adicional)
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bolsista = await _bolsistaRepository.GetByIdAsync(id);

            if (bolsista == null)
            {
                return NotFound(); // 404 Not Found
            }

            await _bolsistaRepository.DeleteAsync(id);

            return NoContent(); // 204 No Content
        }

        // *-*-*-*-*-*-* Métodos auxiliares *-*-*-*-*-*-* \\

        /// <summary>
        /// Converte um objeto do tipo <see cref="Bolsista"/> para um objeto do tipo <see cref="BolsistaDTO"/>.
        /// </summary>
        /// <param name="bolsista">O objeto de origem, do tipo <see cref="Bolsista"/>, cujas propriedades serão copiadas.</param>
        /// <returns>Retorna um objeto do tipo <see cref="BolsistaDTO"/> com as propriedades copiadas do objeto de origem.</returns>
        /// <remarks>
        /// Este método utiliza reflexão para mapear as propriedades do objeto <see cref="Bolsista"/> para o objeto <see cref="BolsistaDTO"/>. 
        /// A correspondência é feita pelo nome das propriedades e os valores são copiados de um objeto para o outro.
        /// </remarks>
        private BolsistaDTO ConvertToDto(Bolsista bolsista)
        {
            // Usando Reflection nos atributos da classe
            var bolsistaDto = new BolsistaDTO();
            var sourceProperties = bolsista.GetType().GetProperties();
            var dtoProperties = bolsistaDto.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var dtoProperty = dtoProperties.FirstOrDefault(b =>
                    b.Name == sourceProperty.Name);

                if (dtoProperty != null && dtoProperty.CanWrite)
                {
                    dtoProperty.SetValue(bolsistaDto, sourceProperty.GetValue(bolsista));
                }
            }

            return bolsistaDto;
        }

        /// <summary>
        /// Mapeia as propriedades de um objeto do tipo <see cref="BolsistaDTO"/> para um objeto do tipo <see cref="Bolsista"/>.
        /// </summary>
        /// <param name="bolsistaDto">O objeto de origem, do tipo <see cref="BolsistaDTO"/>, cujas propriedades serão copiadas.</param>
        /// <param name="bolsista">O objeto de destino, do tipo <see cref="Bolsista"/>, que receberá os valores das propriedades do objeto de origem.</param>
        /// <remarks>
        /// Este método utiliza reflexão para mapear as propriedades de um objeto <see cref="BolsistaDTO"/> para um objeto <see cref="Bolsista"/>. 
        /// A correspondência é feita pelo nome das propriedades, e os valores são copiados de um objeto para o outro apenas se a propriedade de destino for gravável (writable).
        /// </remarks>
        private static void MapProperties(BolsistaDTO bolsistaDto, Bolsista bolsista)
        {
            var dtoProperties = bolsistaDto.GetType().GetProperties();
            var bolsistaProperties = bolsista.GetType().GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                var bolsistaProperty = bolsistaProperties.
                    // Método LINQ que percorre a coleção e retorna o primeiro item que atende à condição na expressão lambda
                    FirstOrDefault(b => b.Name == dtoProperty.Name);

                if (bolsistaProperty != null && bolsistaProperty.CanWrite)
                {
                    bolsistaProperty.SetValue(bolsista, dtoProperty.GetValue(bolsistaDto));
                }
            }
        }


    }
}

using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace colabAPI.Presentation.Controllers
{
  
    [ApiController]
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
        public async Task<ActionResult<IEnumerable<BolsistaDto>>> GetAll()
        {
            var bolsistas = await _bolsistaRepository.GetAllAsync();
            var bolsistasDtos = bolsistas.Select(b => ConvertToDto(b)).ToList();
            
            return Ok(bolsistasDtos);
        }
        
        // GET: api/bolsista/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BolsistaDto>> GetById(int id)
        {
            var bolsista = await _bolsistaRepository.GetByIdAsync(id);

            if (bolsista == null)
            {
                return NotFound();
            }

            var bolsistaDto = ConvertToDto(bolsista);
            
            return Ok(bolsistaDto);
        }
        
        // POST api/bolsista
        [HttpPost]
        public async Task<ActionResult> Create(BolsistaDto bolsistaDto)
        {
            var bolsista = new Bolsista();
            
            MapProperties(bolsistaDto, bolsista);

            var createdBolsista = await _bolsistaRepository.AddAsync(bolsista);
            
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdBolsista.Id },
                createdBolsista);
        }
        
        // PUT: api/bolsista/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BolsistaDto bolsistaDto)
        {
            if (id != bolsistaDto.Id)
            {
                return BadRequest();
            }

            var bolsista = await _bolsistaRepository.GetByIdAsync(id);

            if (bolsista == null)
            {
                return NotFound();
            }
            
            MapProperties(bolsistaDto, bolsista);

            await _bolsistaRepository.UpdateAsync(bolsista);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bolsista = await _bolsistaRepository.GetByIdAsync(id);

            if (bolsista == null)
            {
                return NotFound();
            }

            await _bolsistaRepository.DeleteAsync(id);
            
            return NoContent();
        }
        
        // *-*-*-*-*-*-* Métodos auxiliares *-*-*-*-*-*-* \\
         
        /// <summary>
        /// Converte um objeto do tipo <see cref="Bolsista"/> para um objeto do tipo <see cref="BolsistaDto"/>.
        /// </summary>
        /// <param name="bolsista">O objeto de origem, do tipo <see cref="Bolsista"/>, cujas propriedades serão copiadas.</param>
        /// <returns>Retorna um objeto do tipo <see cref="BolsistaDto"/> com as propriedades copiadas do objeto de origem.</returns>
        /// <remarks>
        /// Este método utiliza reflexão para mapear as propriedades do objeto <see cref="Bolsista"/> para o objeto <see cref="BolsistaDto"/>. 
        /// A correspondência é feita pelo nome das propriedades e os valores são copiados de um objeto para o outro.
        /// </remarks>
        private BolsistaDto ConvertToDto(Bolsista bolsista)
        { 
            // Usando Reflection nos atributos da classe
            var bolsistaDto = new BolsistaDto();
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
        /// Mapeia as propriedades de um objeto do tipo <see cref="BolsistaDto"/> para um objeto do tipo <see cref="Bolsista"/>.
        /// </summary>
        /// <param name="bolsistaDto">O objeto de origem, do tipo <see cref="BolsistaDto"/>, cujas propriedades serão copiadas.</param>
        /// <param name="bolsista">O objeto de destino, do tipo <see cref="Bolsista"/>, que receberá os valores das propriedades do objeto de origem.</param>
        /// <remarks>
        /// Este método utiliza reflexão para mapear as propriedades de um objeto <see cref="BolsistaDto"/> para um objeto <see cref="Bolsista"/>. 
        /// A correspondência é feita pelo nome das propriedades, e os valores são copiados de um objeto para o outro apenas se a propriedade de destino for gravável (writable).
        /// </remarks>
        private static void MapProperties(BolsistaDto bolsistaDto, Bolsista bolsista)
        {
            var dtoProperties = bolsistaDto.GetType().GetProperties();
            var bolsistaProperties = bolsista.GetType().GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                var bolsistaProperty = bolsistaProperties.
                    FirstOrDefault(b => b.Name == dtoProperty.Name);
                
                if (bolsistaProperty != null && bolsistaProperty.CanWrite)
                {
                    bolsistaProperty.SetValue(bolsista, dtoProperty.GetValue(bolsistaDto));
                }
            }
        }
        
        
    }
}

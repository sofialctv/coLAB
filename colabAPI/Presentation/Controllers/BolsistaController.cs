using colabAPI.Business.DTOs;
using Microsoft.AspNetCore.Mvc;
using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;

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
        public async Task<ActionResult<IEnumerable<BolsistaDto>>> GetAllBolsistas()
        {
            var bolsistas = _bolsistaRepository.GetAllAsync();
            return Ok(bolsistas);
        }
        
        // GET: api/bolsista/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BolsistaDto>> GetBolsistaById(int id)
        {
            var bolsista = _bolsistaRepository.GetByIdAsync(id);

            if (bolsista == null)
            {
                return NotFound();
            }
            
            return Ok(bolsista);
        }
        
        // POST api/bolsista
        [HttpPost]
        public async Task<ActionResult<BolsistaDto>> CreateBolsista(BolsistaDto bolsistaDto)
        {
            var bolsista = new Bolsista();

            // Usando Reflection nos atributos da classe
            var dtoProperties = typeof(BolsistaDto).GetProperties();
            var bolsistaProperties = typeof(Bolsista).GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                var bolsistaProperty = bolsistaProperties.
                    FirstOrDefault(b => b.Name == dtoProperty.Name);
                if (bolsistaProperty != null && bolsistaProperty.CanWrite)
                {
                    bolsistaProperty.SetValue(bolsista, dtoProperty.GetValue(bolsistaDto));
                }
            }

            var createdBolsista = await _bolsistaRepository.AddAsync(bolsista);
            
            return CreatedAtAction(
                nameof(GetBolsistaById),
                new { id = createdBolsista.Id },
                createdBolsista);
        }
        
        // PUT: api/bolsista/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBolsista(int id, BolsistaDto bolsistaDto)
        {
            if (id != bolsistaDto.Id)
            {
                return BadRequest();
            }
            
            var bolsista = new Bolsista();
            
            // Usando Reflection nos atributos da classe
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
            
            var updated = await _bolsistaRepository.UpdateAsync(bolsista);

            if (!updated)
            {
                return NotFound();
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBolsista(int id)
        {
            var deleted = await _bolsistaRepository.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }
            
            return NoContent();
        }
        
        
        
    }
}

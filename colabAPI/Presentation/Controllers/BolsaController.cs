using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using colabAPI.Business.Repository.Interfaces;

namespace colabAPI.Business.Models.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BolsaController : ControllerBase
    {
        private readonly IBolsaRepository _bolsaRepository;
        
        public BolsaController(IBolsaRepository bolsaRepository)
        {
            _bolsaRepository = bolsaRepository;
        }
        // api/Bolsa
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bolsas = await _bolsaRepository.GetAllAsync();
            return Ok(bolsas);
        }
        
        // api/Bolsa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bolsa = await _bolsaRepository.GetByIdAsync(id);
            if (bolsa == null)
            {
                return NotFound();
            }
            return Ok(bolsa);
        }
        
        // api/Bolsa
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BolsaDTO bolsaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Bolsa bolsa = new Bolsa(bolsaDTO);

            await _bolsaRepository.AddAsync(bolsa);
            await _bolsaRepository.Save();

            return CreatedAtAction(nameof(GetById), new { id = bolsa.Id }, bolsa);
        }
        
        // api/Bolsa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BolsaDTO bolsaDTO)
        {
            if (id != bolsaDTO.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingBolsa = await _bolsaRepository.GetByIdAsync(id);
            Bolsa bolsa = new Bolsa(bolsaDTO);
            if (existingBolsa == null)
            {
                return NotFound();
            }

            await _bolsaRepository.UpdateAsync(bolsa);
            await _bolsaRepository.Save();

            return NoContent();
        }
        
        // api/Bolsa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bolsa = await _bolsaRepository.GetByIdAsync(id);
            if (bolsa == null)
            {
                return NotFound();
            }

            await _bolsaRepository.DeleteAsync(id);
            await _bolsaRepository.Save();

            return NoContent();
        }
        
        
    }
}

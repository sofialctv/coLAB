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
        private readonly IBolsaRepository _bolsaRepository; // Interface para acessar o repositorio de Bolsa
        
        // Injeta o repositório no construtor
        public BolsaController(IBolsaRepository bolsaRepository)
        {   
            _bolsaRepository = bolsaRepository;
        }
        // GET: api/Bolsa
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bolsas = await _bolsaRepository.GetAllAsync();
            return Ok(bolsas);
        }
        
        // GET: api/Bolsa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bolsa = await _bolsaRepository.GetByIdAsync(id);
            if (bolsa == null)
            {
                return NotFound(); // Famoso erro 404
            }
            return Ok(bolsa);
        }
        
        // POST: api/Bolsa
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BolsaDTO bolsaDTO)
        {
            if (!ModelState.IsValid) // Valida o modelo
            {
                return BadRequest(ModelState); // Solicitação invalida erro 400
            }

            Bolsa bolsa = new Bolsa(bolsaDTO);

            await _bolsaRepository.AddAsync(bolsa);
            await _bolsaRepository.Save();

            return CreatedAtAction(nameof(GetById), new { id = bolsa.Id }, bolsa);
        }
        
        // PUT: api/Bolsa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BolsaDTO bolsaDTO)
        {
            if (id != bolsaDTO.Id || !ModelState.IsValid) // Verifica se o ID corresponde e o modelo é valido
            {
                return BadRequest(); // Solicitação invalida erro 400
            }

            var existingBolsa = await _bolsaRepository.GetByIdAsync(id);
            Bolsa bolsa = new Bolsa(bolsaDTO);
            if (existingBolsa == null)
            {
                return NotFound(); // Famoso erro 404
            }

            await _bolsaRepository.UpdateAsync(bolsa);
            await _bolsaRepository.Save();

            return NoContent();
        }
        
        // DELETE: api/Bolsa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bolsa = await _bolsaRepository.GetByIdAsync(id);
            if (bolsa == null)
            {
                return NotFound(); // Famoso erro 404
            }

            await _bolsaRepository.DeleteAsync(id);
            await _bolsaRepository.Save();

            return NoContent();
        }
        
        
    }
}

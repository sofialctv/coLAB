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
            var bolsas = _bolsaRepository.GetBolsas();
            return Ok(bolsas);
        }
        
        // api/Bolsa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bolsa = _bolsaRepository.GetBolsaByID(id);
            if (bolsa == null)
            {
                return NotFound();
            }
            return Ok(bolsa);
        }
        // api/Bolsa
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Bolsa bolsa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bolsaRepository.InsertBolsa(bolsa);
            _bolsaRepository.Save();

            return CreatedAtAction(nameof(GetById), new { id = bolsa.Id }, bolsa);
        }
        
        // api/Bolsa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Bolsa bolsa)
        {
            if (id != bolsa.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingBolsa = _bolsaRepository.GetBolsaByID(id);
            if (existingBolsa == null)
            {
                return NotFound();
            }

            _bolsaRepository.UpdateBolsa(bolsa);
            _bolsaRepository.Save();

            return NoContent();
        }
        
        // api/Bolsa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bolsa = _bolsaRepository.GetBolsaByID(id);
            if (bolsa == null)
            {
                return NotFound();
            }

            _bolsaRepository.DeleteBolsa(id);
            _bolsaRepository.Save();

            return NoContent();
        }
        
        
    }
}

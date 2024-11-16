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
        
        // GET: api/Bolsa
        [HttpGet]
        public IActionResult GetAll()
        {
            var bolsas = _bolsaRepository.GetBolsas();
            return Ok(bolsas);
        }
        
        // GET: api/Bolsa/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var bolsa = _bolsaRepository.getBolsaByID(id);
            if (bolsa == null)
            {
                return NotFound();
            }
            return Ok(bolsa);
        }
        
        // POST: api/Bolsa
        [HttpPost]
        public IActionResult Create([FromBody] Bolsa bolsa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bolsaRepository.InsertBolsa(bolsa);
            _bolsaRepository.Save();

            return CreatedAtAction(nameof(GetById), new { id = bolsa.Id }, bolsa);
        }
        
        // PUT: api/Bolsa/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Bolsa bolsa)
        {
            if (id != bolsa.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingBolsa = _bolsaRepository.getBolsaByID(id);
            if (existingBolsa == null)
            {
                return NotFound();
            }

            _bolsaRepository.UpdateBolsa(bolsa);
            _bolsaRepository.Save();

            return NoContent();
        }
        
        // DELETE: api/Bolsa/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bolsa = _bolsaRepository.getBolsaByID(id);
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

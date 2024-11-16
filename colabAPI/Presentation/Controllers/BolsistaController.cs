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
        public IActionResult GetAllBolsistas()
        {
            var bolsistas = _bolsistaRepository.GetBolsistas();
            return Ok(bolsistas);
        }
        
        // GET: api/bolsista/{id}
        [HttpGet("{id}")]
        public IActionResult GetBolsistaById(int id)
        {
            var bolsista = _bolsistaRepository.GetBolsistaById(id);

            if (bolsista == null)
            {
                return NotFound();
            }
            
            return Ok(bolsista);
        }
        
        // POST api/bolsista
        [HttpPost]
        public IActionResult CreateBolsista([FromBody] Bolsista bolsista)
        {
            if (bolsista == null)
            {
                return BadRequest();
            }
            
            _bolsistaRepository.InsertBolsista(bolsista);
            _bolsistaRepository.Save();

            return CreatedAtAction(nameof(GetBolsistaById), 
                new { id = bolsista.BolsistaId }, bolsista);
        }
        
        // PUT: api/bolsista/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBolsista(int id, [FromBody] Bolsista bolsista)
        {
            if (bolsista == null || bolsista.BolsistaId != id)
            {
                return BadRequest();
            }
            
            var existingBolsista = _bolsistaRepository.GetBolsistaById(id);
            if (existingBolsista == null)
            {
                return NotFound();
            }
            
            _bolsistaRepository.UpdateBolsista(existingBolsista);
            _bolsistaRepository.Save();
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBolsista(int id)
        {
            var bolsista = _bolsistaRepository.GetBolsistaById(id);
            if (bolsista == null)
            {
                return NotFound();
            }
            
            _bolsistaRepository.DeleteBolsista(id);
            _bolsistaRepository.Save();
            
            return NoContent();
        }
        
        
        
    }
}

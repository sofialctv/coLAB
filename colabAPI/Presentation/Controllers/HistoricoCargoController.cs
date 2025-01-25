using AutoMapper;
using colabAPI.Business.DTOs.Request;
using colabAPI.Business.DTOs.Response;
using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace colabAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoCargoController : ControllerBase
    {
        private readonly IHistoricoCargoRepository _historicoCargoRepository; 
        private readonly IMapper _mapper; 

        public HistoricoCargoController(IHistoricoCargoRepository historicoCargoRepository, IMapper mapper)
        {
            _historicoCargoRepository = historicoCargoRepository;
            _mapper = mapper;
        }

        // GET: api/HistoricoCargo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoCargoResponseDTO>>> GetAll()
        {
            var historicosCargo = await _historicoCargoRepository.GetAllAsync();
            var historicosCargoDTO = _mapper.Map<IEnumerable<HistoricoCargoResponseDTO>>(historicosCargo);
            return Ok(historicosCargoDTO);
        }

        // GET: api/HistoricoCargo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoCargoResponseDTO>> GetById(int id)
        {
            var historicoCargo = await _historicoCargoRepository.GetByIdAsync(id);
            if (historicoCargo == null)
            {
                return NotFound();
            }
            var historicoCargoDTO = _mapper.Map<HistoricoCargoResponseDTO>(historicoCargo);
            return Ok(historicoCargoDTO);
        }

        // POST: api/HistoricoCargo
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HistoricoCargoRequestDTO historicoCargoRequestDTO)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var historicoCargo = _mapper.Map<HistoricoCargo>(historicoCargoRequestDTO);

            await _historicoCargoRepository.AddAsync(historicoCargo);
            
            var historicoCargoResponseDTO = _mapper.Map<HistoricoCargoResponseDTO>(historicoCargo);

            return CreatedAtAction(nameof(GetById), new { id = historicoCargo.Id }, historicoCargoResponseDTO);
        }

        // PUT: api/HistoricoCargo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] HistoricoCargoRequestDTO historicoCargoRequestDTO)
        {
            if (id != historicoCargoRequestDTO.Id || !ModelState.IsValid) 
            {
                return BadRequest(); 
            }

            var existingHistoricoCargo = await _historicoCargoRepository.GetByIdAsync(id);
            if (existingHistoricoCargo == null)
            {
                return NotFound(); 
            }

            var historicoCargo = _mapper.Map(historicoCargoRequestDTO, existingHistoricoCargo);

            await _historicoCargoRepository.UpdateAsync(historicoCargo);

            return NoContent();
        }

        // DELETE: api/HistoricoCargo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var historicoCargo = await _historicoCargoRepository.GetByIdAsync(id);
            if (historicoCargo == null)
            {
                return NotFound(); 
            }

            await _historicoCargoRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}

using AutoMapper;
using colab.Business.Repository.Interfaces;
using colabAPI.Business.DTOs.Request;
using colabAPI.Business.DTOs.Response;
using colabAPI.Business.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace colabAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanciadorController : ControllerBase
    {
        private readonly IFinanciadorRepository _financiadorRepository;
        private readonly IMapper _mapper;

        public FinanciadorController(IFinanciadorRepository financiadorRepository, IMapper mapper)
        {
            _financiadorRepository = financiadorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinanciadorResponseDTO>>> GetAll()
        {
            var financiadores = await _financiadorRepository.GetAllAsync();
            var financiadoresDTO = _mapper.Map<IEnumerable<FinanciadorResponseDTO>>(financiadores);
            return Ok(financiadoresDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinanciadorResponseDTO>> GetById(int id)
        {
            var financiador = await _financiadorRepository.GetByIdAsync(id);
            if (financiador == null)
            {
                return NotFound();
            }
            var financiadorDTO = _mapper.Map<FinanciadorResponseDTO>(financiador);
            return Ok(financiadorDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FinanciadorRequestDTO financiadorRequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var financiador = _mapper.Map<Financiador>(financiadorRequestDTO);

            await _financiadorRepository.AddAsync(financiador);

            var financiadorResponseDTO = _mapper.Map<FinanciadorResponseDTO>(financiador);

            return CreatedAtAction(nameof(GetById), new { id = financiador.Id }, financiadorResponseDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FinanciadorRequestDTO financiadorRequestDTO)
        {
            if (id != financiadorRequestDTO.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingfinanciador = await _financiadorRepository.GetByIdAsync(id);
            if (existingfinanciador == null)
            {
                return NotFound();
            }

            var financiador = _mapper.Map(financiadorRequestDTO, existingfinanciador);

            await _financiadorRepository.UpdateAsync(financiador);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var financiador = await _financiadorRepository.GetByIdAsync(id);
            if (financiador == null)
            {
                return NotFound();
            }

            await _financiadorRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
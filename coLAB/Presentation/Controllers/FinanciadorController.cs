using AutoMapper;
using colab.Business.Services.Interfaces;
using colab.Business.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using colab.Business.DTOs;
using colab.Business.DTOs.Request;
using colab.Business.DTOs.Response;

namespace colabAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanciadorController : ControllerBase
    {
        private readonly IFinanciadorService _financiadorService;
        private readonly IMapper _mapper;

        public FinanciadorController(IFinanciadorService financiadorService, IMapper mapper)
        {
            _financiadorService = financiadorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinanciadorResponseDTO>>> GetAll()

        {
            var financiadores = await _financiadorService.GetAllAsync();
            var financiadoresDTO = _mapper.Map<IEnumerable<FinanciadorResponseDTO>>(financiadores);
            return Ok(financiadoresDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinanciadorResponseDTO>> GetById(int id)
        {
            var financiador = await _financiadorService.GetByIdAsync(id);
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

            await _financiadorService.AddAsync(financiador);

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

            var existingfinanciador = await _financiadorService.GetByIdAsync(id);
            if (existingfinanciador == null)
            {
                return NotFound();
            }

            var financiador = _mapper.Map(financiadorRequestDTO, existingfinanciador);

            await _financiadorService.UpdateAsync(financiador);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var financiador = await _financiadorService.GetByIdAsync(id);
            if (financiador == null)
            {
                return NotFound();
            }

            await _financiadorService.DeleteAsync(id);

            return NoContent();
        }
    }
}
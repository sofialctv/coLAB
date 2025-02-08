using AutoMapper;
using colab.Business.Models.Entities;
using colab.Business.DTOs.Request;
using colab.Business.DTOs.Response;
using colab.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace colab.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BolsaController : ControllerBase
    {
        private readonly IBolsaService _bolsaService;
        private readonly IMapper _mapper;

        public BolsaController(IBolsaService bolsaService, IMapper mapper)
        {
            _bolsaService = bolsaService;
            _mapper = mapper;
        }

        // GET: api/Bolsa
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bolsas = await _bolsaService.GetAllAsync();
            var bolsaDtos = _mapper.Map<IEnumerable<BolsaResponseDTO>>(bolsas);

            return Ok(bolsaDtos);
        }

        // GET: api/Bolsa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bolsa = await _bolsaService.GetByIdAsync(id);
            if (bolsa == null)
            {
                return NotFound();
            }

            var bolsaDto = _mapper.Map<BolsaResponseDTO>(bolsa);
            return Ok(bolsaDto);
        }

        // POST: api/Bolsa
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BolsaRequestDTO bolsaRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bolsa = _mapper.Map<Bolsa>(bolsaRequestDto);
            await _bolsaService.AddAsync(bolsa);

            var bolsaResponseDto = _mapper.Map<BolsaResponseDTO>(bolsa);
            return CreatedAtAction(nameof(GetById), new { id = bolsa.Id }, bolsaResponseDto);
        }

        // PUT: api/Bolsa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BolsaRequestDTO bolsaRequestDto)
        {
            if (!ModelState.IsValid || id <= 0)
            {
                return BadRequest();
            }

            var existingBolsa = await _bolsaService.GetByIdAsync(id);
            if (existingBolsa == null)
            {
                return NotFound();
            }

            _mapper.Map(bolsaRequestDto, existingBolsa);
            await _bolsaService.UpdateAsync(existingBolsa);

            return NoContent();
        }

        // DELETE: api/Bolsa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bolsa = await _bolsaService.GetByIdAsync(id);
            if (bolsa == null)
            {
                return NotFound();
            }

            await _bolsaService.DeleteAsync(id);
            return NoContent();
        }
    }
}

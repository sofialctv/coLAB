using AutoMapper;
using colab.Business.DTOs.Response;
using colab.Business.DTOs.Request;
using colab.Business.Models.Entities;
using colab.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace colab.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoBolsaController : ControllerBase
    {
        private readonly ITipoBolsaService _tipoBolsaService; // Agora estamos utilizando o serviço
        private readonly IMapper _mapper;

        // Injeta o serviço no construtor
        public TipoBolsaController(ITipoBolsaService tipoBolsaService, IMapper mapper)
        {
            _tipoBolsaService = tipoBolsaService;
            _mapper = mapper;
        }

        // GET: api/TipoBolsa
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tipoBolsa = await _tipoBolsaService.GetAllAsync();

            // Mapeia as entidades TipoBolsa para os DTOs TipoBolsaResponseDTO
            var tipoBolsaDtos = _mapper.Map<IEnumerable<TipoBolsaResponseDTO>>(tipoBolsa);

            return Ok(tipoBolsaDtos);
        }

        // GET: api/TipoBolsa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var tipoBolsa = await _tipoBolsaService.GetByIdAsync(id);

                // Mapeia a entidade TipoBolsa para o DTO TipoBolsaResponseDTO
                var tipoBolsaDto = _mapper.Map<TipoBolsaResponseDTO>(tipoBolsa);
                return Ok(tipoBolsaDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Retorna 404 se não encontrar o tipo de bolsa
            }
        }

        // POST: api/TipoBolsa
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TipoBolsaRequestDTO tipoBolsaRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna erro 400 caso o modelo seja inválido
            }

            // Mapeia o DTO TipoBolsaRequestDTO para a entidade TipoBolsa
            var tipoBolsa = _mapper.Map<TipoBolsa>(tipoBolsaRequestDto);

            await _tipoBolsaService.AddAsync(tipoBolsa);

            // Mapeia a entidade TipoBolsa salva para o DTO TipoBolsaResponseDTO para retornar ao cliente
            var tipoBolsaResponseDto = _mapper.Map<TipoBolsaResponseDTO>(tipoBolsa);

            return CreatedAtAction(nameof(GetById), new { id = tipoBolsa.Id }, tipoBolsaResponseDto);
        }

        // PUT: api/TipoBolsa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TipoBolsaRequestDTO tipoBolsaRequestDto)
        {
            if (!ModelState.IsValid || id <= 0)
            {
                return BadRequest(); // Retorna erro 400 para solicitação inválida
            }

            try
            {
                // Mapeia o DTO para a entidade existente
                var existingTipoBolsa = await _tipoBolsaService.GetByIdAsync(id);
                _mapper.Map(tipoBolsaRequestDto, existingTipoBolsa);

                await _tipoBolsaService.UpdateAsync(existingTipoBolsa);

                return NoContent(); // Retorna 204 quando a atualização é bem-sucedida
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Retorna erro 404 se não encontrar o tipo de bolsa
            }
        }

        // DELETE: api/TipoBolsa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _tipoBolsaService.DeleteAsync(id);
                return NoContent(); // Retorna 204, indicando que a operação foi bem-sucedida
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Retorna erro 404 caso não encontre o tipo de bolsa
            }
        }
    }
}

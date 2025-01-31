using AutoMapper;
using colab.Business.DTOs.Response;
using colab.Business.DTOs.Request;
using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace colab.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoBolsaController : ControllerBase
    {
        private readonly ITipoBolsaRepository _tipobolsaRepository; // Interface para acessar o repositorio de TipoBolsa
        private readonly IMapper _mapper; 
        
        // Injeta o repositório no construtor
        public TipoBolsaController(ITipoBolsaRepository tipobolsaRepository, IMapper mapper)
        {   
            _tipobolsaRepository = tipobolsaRepository;
            _mapper = mapper;
        }
        
        // GET: api/TipoBolsa
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tipoBolsa = await _tipobolsaRepository.GetAllAsync();
    
            // Mapeia as entidades Bolsa para os DTOs BolsaResponseDTO
            var tipoBolsaDtos = _mapper.Map<IEnumerable<TipoBolsaResponseDTO>>(tipoBolsa);
    
            return Ok(tipoBolsaDtos);
        }
        
        // GET: api/TipoBolsa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tipoBolsa = await _tipobolsaRepository.GetByIdAsync(id);
            if (tipoBolsa == null)
            {
                return NotFound(); // Retorna 404 se a bolsa não for encontrada
            }

            // Mapeia a entidade TipoBolsa para o DTO TipoBolsaResponseDTO
            var tipoBolsaDto = _mapper.Map<TipoBolsaResponseDTO>(tipoBolsa);
    
            return Ok(tipoBolsaDto);
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

            await _tipobolsaRepository.AddAsync(tipoBolsa);
            await _tipobolsaRepository.Save();

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

            var existingTipoBolsa = await _tipobolsaRepository.GetByIdAsync(id);
            if (existingTipoBolsa == null)
            {
                return NotFound(); // Retorna erro 404 se a tipobolsa não for encontrada
            }

            // Atualiza a entidade existente com os valores do DTO
            _mapper.Map(tipoBolsaRequestDto, existingTipoBolsa);

            await _tipobolsaRepository.UpdateAsync(existingTipoBolsa);
            await _tipobolsaRepository.Save();

            return NoContent(); // Retorna 204 quando a atualização é bem-sucedida
        }

        
        // DELETE: api/TipoBolsa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Busca a TipoBolsa pelo ID
            var tipoBolsa = await _tipobolsaRepository.GetByIdAsync(id);
            if (tipoBolsa == null)
            {
                return NotFound(); // Retorna erro 404 caso não encontre a bolsa
            }

            // Remove a TipoBolsa
            await _tipobolsaRepository.DeleteAsync(id);
            await _tipobolsaRepository.Save();

            return NoContent(); // Retorna 204, indicando que a operação foi bem-sucedida
        }

        
        
    }
}

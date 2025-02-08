using AutoMapper;
using colab.Business.DTOs.Request;
using colab.Business.DTOs.Response;
using colab.Business.Models.Entities;
using colab.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace colab.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoCargoController : ControllerBase
    {
        private readonly IHistoricoCargoService _historicoCargoService; 
        private readonly ICargoService _cargoService;
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper; 

        public HistoricoCargoController(IHistoricoCargoService historicoCargoService, ICargoService cargoService, IPessoaService pessoaService, IMapper mapper)
        {
            _historicoCargoService = historicoCargoService;
            _pessoaService = pessoaService;
            _cargoService = cargoService;
            _mapper = mapper;
        }

        // GET: api/HistoricoCargo
        [HttpGet]
        public async Task<IEnumerable<HistoricoCargoResponseDTO>> GetAll()
        {
            var historicosCargo = await _historicoCargoService.GetAllAsync();
            var historicosCargoDTO = _mapper.Map<IEnumerable<HistoricoCargoResponseDTO>>(historicosCargo);
            return (historicosCargoDTO);
        }

        // GET: api/HistoricoCargo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoCargoResponseDTO>> GetById(int id)
        {
            var historicoCargo = await _historicoCargoService.GetByIdAsync(id);
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
            
            var cargo = await _cargoService.GetByIdAsync(historicoCargoRequestDTO.CargoId);
            if (cargo == null)
            {
                return BadRequest("Cargo não encontrado.");
            }
            
            var pessoa = await _pessoaService.GetByIdAsync(historicoCargoRequestDTO.PessoaId);
            if (pessoa == null)
            {
                return BadRequest("Pessoa não encontrada.");
            }

            var historicoCargo = _mapper.Map<HistoricoCargo>(historicoCargoRequestDTO);
            historicoCargo.Cargo = cargo;
            historicoCargo.Pessoa = pessoa;

            await _historicoCargoService.AddAsync(historicoCargo);
            
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

            var existingHistoricoCargo = await _historicoCargoService.GetByIdAsync(id);
            if (existingHistoricoCargo == null)
            {
                return NotFound(); 
            }
            
            var cargo = await _cargoService.GetByIdAsync(historicoCargoRequestDTO.CargoId);
            if (cargo == null)
            {
                return BadRequest("Cargo não encontrado.");
            }
            
            var pessoa = await _pessoaService.GetByIdAsync(historicoCargoRequestDTO.PessoaId);
            if (pessoa == null)
            {
                return BadRequest("Pessoa não encontrada.");
            }

            
            _mapper.Map(historicoCargoRequestDTO, existingHistoricoCargo);
            existingHistoricoCargo.Cargo = cargo;
            existingHistoricoCargo.Pessoa = pessoa;

            await _historicoCargoService.UpdateAsync(existingHistoricoCargo);

            return NoContent();
        }

        // DELETE: api/HistoricoCargo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var historicoCargo = await _historicoCargoService.GetByIdAsync(id);
            if (historicoCargo == null)
            {
                return NotFound(); 
            }

            await _historicoCargoService.DeleteAsync(id);

            return NoContent();
        }
    }
}

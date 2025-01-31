using AutoMapper;
using colab.Business.DTOs.Request;
using colab.Business.DTOs.Response;
using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace colab.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoCargoController : ControllerBase
    {
        private readonly IHistoricoCargoRepository _historicoCargoRepository; 
        private readonly ICargoRepository _cargoRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper; 

        public HistoricoCargoController(IHistoricoCargoRepository historicoCargoRepository, ICargoRepository cargoRepository, IPessoaRepository pessoaRepository, IMapper mapper)
        {
            _historicoCargoRepository = historicoCargoRepository;
            _pessoaRepository = pessoaRepository;
            _cargoRepository = cargoRepository;
            _mapper = mapper;
        }

        // GET: api/HistoricoCargo
        [HttpGet]
        public async Task<IEnumerable<HistoricoCargoResponseDTO>> GetAll()
        {
            var historicosCargo = await _historicoCargoRepository.GetAllAsync();
            var historicosCargoDTO = _mapper.Map<IEnumerable<HistoricoCargoResponseDTO>>(historicosCargo);
            return (historicosCargoDTO);
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
            
            var cargo = await _cargoRepository.GetByIdAsync(historicoCargoRequestDTO.CargoId);
            if (cargo == null)
            {
                return BadRequest("Cargo não encontrado.");
            }
            
            var pessoa = await _pessoaRepository.GetByIdAsync(historicoCargoRequestDTO.PessoaId);
            if (pessoa == null)
            {
                return BadRequest("Pessoa não encontrada.");
            }

            var historicoCargo = _mapper.Map<HistoricoCargo>(historicoCargoRequestDTO);
            historicoCargo.Cargo = cargo;
            historicoCargo.Pessoa = pessoa;

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
            
            var cargo = await _cargoRepository.GetByIdAsync(historicoCargoRequestDTO.CargoId);
            if (cargo == null)
            {
                return BadRequest("Cargo não encontrado.");
            }
            
            var pessoa = await _pessoaRepository.GetByIdAsync(historicoCargoRequestDTO.PessoaId);
            if (pessoa == null)
            {
                return BadRequest("Pessoa não encontrada.");
            }

            
            _mapper.Map(historicoCargoRequestDTO, existingHistoricoCargo);
            existingHistoricoCargo.Cargo = cargo;
            existingHistoricoCargo.Pessoa = pessoa;

            await _historicoCargoRepository.UpdateAsync(existingHistoricoCargo);

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

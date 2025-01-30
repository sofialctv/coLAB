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
    public class CargoController : ControllerBase
    {
        private readonly ICargoRepository _cargoRepository; 
        private readonly IMapper _mapper; 

        public CargoController(ICargoRepository cargoRepository, IMapper mapper)
        {
            _cargoRepository = cargoRepository;
            _mapper = mapper;
        }

        // GET: api/Cargo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargoResponseDTO>>> GetAll()
        {
            var cargos = await _cargoRepository.GetAllAsync();
            var cargosDTO = _mapper.Map<IEnumerable<CargoResponseDTO>>(cargos);
            return Ok(cargosDTO);
        }

        // GET: api/Cargo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CargoResponseDTO>> GetById(int id)
        {
            var cargo = await _cargoRepository.GetByIdAsync(id);
            if (cargo == null)
            {
                return NotFound();
            }
            var cargoDTO = _mapper.Map<CargoResponseDTO>(cargo);
            return Ok(cargoDTO);
        }

        // POST: api/Cargo
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CargoRequestDTO cargoRequestDTO)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var cargo = _mapper.Map<Cargo>(cargoRequestDTO);

            await _cargoRepository.AddAsync(cargo);
            
            var cargoResponseDTO = _mapper.Map<CargoResponseDTO>(cargo);

            return CreatedAtAction(nameof(GetById), new { id = cargo.Id }, cargoResponseDTO);
        }

        // PUT: api/Cargo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CargoRequestDTO cargoRequestDTO)
        {
            if (id != cargoRequestDTO.Id || !ModelState.IsValid) 
            {
                return BadRequest(); 
            }

            var existingCargo = await _cargoRepository.GetByIdAsync(id);
            if (existingCargo == null)
            {
                return NotFound(); 
            }

            var cargo = _mapper.Map(cargoRequestDTO, existingCargo);

            await _cargoRepository.UpdateAsync(cargo);

            return NoContent();
        }

        // DELETE: api/Cargo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cargo = await _cargoRepository.GetByIdAsync(id);
            if (cargo == null)
            {
                return NotFound(); 
            }

            await _cargoRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}

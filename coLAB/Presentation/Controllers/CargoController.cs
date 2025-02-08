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
    public class CargoController : ControllerBase
    {
        private readonly ICargoService _cargoService; 
        private readonly IMapper _mapper; 

        public CargoController(ICargoService cargoService, IMapper mapper)
        {
            _cargoService = cargoService;
            _mapper = mapper;
        }

        // GET: api/Cargo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargoResponseDTO>>> GetAll()
        {
            var cargos = await _cargoService.GetAllAsync();
            var cargosDTO = _mapper.Map<IEnumerable<CargoResponseDTO>>(cargos);
            return Ok(cargosDTO);
        }

        // GET: api/Cargo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CargoResponseDTO>> GetById(int id)
        {
            var cargo = await _cargoService.GetByIdAsync(id);
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

            await _cargoService.AddAsync(cargo);
            
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

            var existingCargo = await _cargoService.GetByIdAsync(id);
            if (existingCargo == null)
            {
                return NotFound(); 
            }

            var cargo = _mapper.Map(cargoRequestDTO, existingCargo);

            await _cargoService.UpdateAsync(cargo);

            return NoContent();
        }

        // DELETE: api/Cargo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cargo = await _cargoService.GetByIdAsync(id);
            if (cargo == null)
            {
                return NotFound(); 
            }

            await _cargoService.DeleteAsync(id);

            return NoContent();
        }
    }
}

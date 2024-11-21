using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace colabAPI.Business.Models.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrientadorController : ControllerBase
    {
        private readonly IOrientadorRepository _orientadorRepository;

        public OrientadorController(IOrientadorRepository orientadorRepository)
        {
            _orientadorRepository = orientadorRepository;
        }

        // GET: api/orientadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrientadorDTO>>> GetAllOrientadores()
        {
            var orientadores = await _orientadorRepository.GetAllAsync();
            return Ok(orientadores);
        }

        // GET: api/orientador/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrientadorDTO>> GetOrientadorById(int id)
        {
            var orientador = _orientadorRepository.GetByIdAsync(id);

            if (orientador == null)
            {
                return NotFound();
            }

            return Ok(orientador);
        }

        // POST api/orientador
        [HttpPost]
        public async Task<ActionResult<OrientadorDTO>> CreateOrientador(OrientadorDTO orientadorDto)
        {
            var orientador = new Orientador();

            // Usando Reflection nos atributos da classe
            var dtoProperties = typeof(OrientadorDTO).GetProperties();
            var orientadorProperties = typeof(Orientador).GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                var orientadorProperty = orientadorProperties.
                    FirstOrDefault(b => b.Name == dtoProperty.Name);
                if (orientadorProperty != null && orientadorProperty.CanWrite)
                {
                    orientadorProperty.SetValue(orientador, dtoProperty.GetValue(orientadorDto));
                }
            }

            await _orientadorRepository.AddAsync(orientador);
            return CreatedAtAction(
                nameof(GetOrientadorById),
                new { id = orientador.Id },
                _orientadorRepository.ConvertToDto(orientador));
        }

        // PUT: api/orientador/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrientador(int id, OrientadorDTO orientadorDto)
        {
            if (id != orientadorDto.Id)
            {
                return BadRequest();
            }

            var orientador = new Orientador();

            // Usando Reflection nos atributos da classe
            var dtoProperties = orientadorDto.GetType().GetProperties();
            var orientadorProperties = orientador.GetType().GetProperties();

            foreach (var dtoProperty in dtoProperties)
            {
                var orientadorProperty = orientadorProperties.
                    FirstOrDefault(b => b.Name == dtoProperty.Name);
                if (orientadorProperty != null && orientadorProperty.CanWrite)
                {
                    orientadorProperty.SetValue(orientador, dtoProperty.GetValue(orientadorDto));
                }
            }

            var updated = await _orientadorRepository.UpdateAsync(orientador);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrientador(int id)
        {
            var deleted = await _orientadorRepository.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

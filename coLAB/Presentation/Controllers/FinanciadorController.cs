using Microsoft.AspNetCore.Mvc;
using colab.Business.DTOs;
using colab.Business.Repository.Interfaces;
using colab.Business.Models.Entities;

namespace colab.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanciadorController : ControllerBase
    {
        private readonly IFinanciadorRepository _financiadorRepository;

        public FinanciadorController(IFinanciadorRepository financiadorRepository)
        {
            _financiadorRepository = financiadorRepository;
        }

        // Método GET para obter todos os financiadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Financiador>>> GetAll()
        {
            var financiadores = await _financiadorRepository.GetAllAsync();
            return Ok(financiadores);
        }

        // Método GET para obter um financiador por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Financiador>> GetById(int id)
        {
            var financiador = await _financiadorRepository.GetByIdAsync(id);

            if (financiador == null)
            {
                return NotFound(new { message = "Financiador não encontrado." });
            }

            return Ok(financiador);
        }

        // Método POST para criar um novo financiador
        [HttpPost]
        public async Task<ActionResult<Financiador>> Create(FinanciadorDTO financiadorDto)
        {

            var financiador = new Financiador
            {
                Nome = financiadorDto.Nome,
                Email = financiadorDto.Email
            };

            await _financiadorRepository.AddAsync(financiador);
            return CreatedAtAction(nameof(GetAll), new { id = financiador.Id }, financiador);
        }

        // Método PUT para atualizar um financiador existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FinanciadorDTO financiadorDto)
        {
            if (id != financiadorDto.Id)
            {
                return BadRequest(new { message = "ID do Financiador não encontrado." });
            }

            var financiador = await _financiadorRepository.GetByIdAsync(id);

            if (financiador == null)
            {
                return NotFound(new { message = "Financiador não encontrado." });
            }

            financiador.Nome = financiadorDto.Nome;
            financiador.Email = financiadorDto.Email;

            await _financiadorRepository.UpdateAsync(financiador);
            return NoContent();
        }

        // Método DELETE para excluir um financiador pelo ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var financiador = await _financiadorRepository.GetByIdAsync(id);

            if (financiador == null)
            {
                return NotFound(new { message = "Financiador não encontrado." });
            }

            await _financiadorRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
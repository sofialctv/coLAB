using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;
using colabAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjetoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetoDTO>>> GetAll()
        {
            var projetos = await _context.Projetos
                .Include(p => p.Financiador)
                .Select(p => new ProjetoDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    DataInicio = p.DataInicio,
                    DataFim = p.DataFim,
                    DataPrevistaFim = p.DataPrevistaFim,
                    Descricao = p.Descricao,
                    Orcamento = p.Orcamento,
                    FinanciadorId = p.FinanciadorId,
                    FinanciadorNome = p.Financiador != null ? p.Financiador.Nome : null,
                    Categoria = p.Categoria,
                    Status = p.Status,
                })
                .ToListAsync();

            return Ok(projetos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjetoDTO>> GetById(int id)
        {
            var projeto = await _context.Projetos
                .Include(p => p.Financiador)
                .Where(p => p.Id == id)
                .Select(p => new ProjetoDTO
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    DataInicio = p.DataInicio,
                    DataFim = p.DataFim,
                    DataPrevistaFim = p.DataPrevistaFim,
                    Descricao = p.Descricao,
                    Orcamento = p.Orcamento,
                    FinanciadorId = p.FinanciadorId,
                    FinanciadorNome = p.Financiador != null ? p.Financiador.Nome : null,
                    Categoria = p.Categoria,
                    Status = p.Status,
                })
                .FirstOrDefaultAsync();

            if (projeto == null)
            {
                return NotFound(new { message = "Projeto não encontrado" });
            }

            return Ok(projeto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjetoDTO projetoDto)
        {
            var projeto = new Projeto
            {
                Nome = projetoDto.Nome,
                DataInicio = projetoDto.DataInicio,
                DataFim = projetoDto.DataFim,
                DataPrevistaFim = projetoDto.DataPrevistaFim,
                Descricao = projetoDto.Descricao,
                Orcamento = projetoDto.Orcamento,
                FinanciadorId = projetoDto.FinanciadorId,
                Categoria = projetoDto.Categoria,
                Status = projetoDto.Status
            };

            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = projeto.Id }, projeto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProjetoDTO projetoDto)
        {
            if (id != projetoDto.Id)
            {
                return BadRequest(new { message = "ID do projeto não corresponde" });
            }

            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto == null)
            {
                return NotFound(new { message = "Projeto não encontrado" });
            }

            projeto.Nome = projetoDto.Nome;
            projeto.DataInicio = projetoDto.DataInicio;
            projeto.DataFim = projetoDto.DataFim;
            projeto.DataPrevistaFim = projetoDto.DataPrevistaFim;
            projeto.Descricao = projetoDto.Descricao;
            projeto.Orcamento = projetoDto.Orcamento;
            projeto.FinanciadorId = projetoDto.FinanciadorId;
            projeto.Categoria = projetoDto.Categoria;
            projeto.Status = projetoDto.Status;

            _context.Entry(projeto).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var projeto = await _context.Projetos.FindAsync(id);
            if (projeto == null)
            {
                return NotFound(new { message = "Projeto não encontrado" });
            }

            _context.Projetos.Remove(projeto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

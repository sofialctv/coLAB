using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using colabAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colabAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoController(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetoDTO>>> GetAll()
        {
            var projetos = await _projetoRepository.GetAllAsync();

            var projetoDtos = projetos.Select(p => new ProjetoDTO
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
                OrientadorNome = p.Orientador != null ? p.Orientador.Nome : null,
                Categoria = p.Categoria,
                Status = p.Status,
            }).ToList();

            return Ok(projetoDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjetoDTO>> GetById(int id)
        {
            var projeto = await _projetoRepository.GetByIdAsync(id);
            if (projeto == null)
            {
                return NotFound(new { message = "Projeto não encontrado" });
            }

            var projetoDto = new ProjetoDTO
            {
                Id = projeto.Id,
                Nome = projeto.Nome,
                DataInicio = projeto.DataInicio,
                DataFim = projeto.DataFim,
                DataPrevistaFim = projeto.DataPrevistaFim,
                Descricao = projeto.Descricao,
                Orcamento = projeto.Orcamento,
                FinanciadorId = projeto.FinanciadorId,
                FinanciadorNome = projeto.Financiador?.Nome,
                OrientadorNome = projeto.Orientador?.Nome,
                Categoria = projeto.Categoria,
                Status = projeto.Status,
            };

            return Ok(projetoDto);
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
                OrientadorId = projetoDto.OrientadorId,
                Categoria = projetoDto.Categoria,
                Status = projetoDto.Status
            };

            var createdProjeto = await _projetoRepository.AddAsync(projeto);

            return CreatedAtAction(nameof(GetById), new { id = createdProjeto.Id }, createdProjeto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProjetoDTO projetoDto)
        {
            if (id != projetoDto.Id)
            {
                return BadRequest(new { message = "ID do projeto não corresponde" });
            }

            var projeto = await _projetoRepository.GetByIdAsync(id);
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
            projeto.OrientadorId = projetoDto.OrientadorId;
            projeto.Categoria = projetoDto.Categoria;
            projeto.Status = projetoDto.Status;

            await _projetoRepository.UpdateAsync(projeto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var projeto = await _projetoRepository.GetByIdAsync(id);
            if (projeto == null)
            {
                return NotFound(new { message = "Projeto não encontrado" });
            }

            await _projetoRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
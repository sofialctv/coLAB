using colab.Business.DTOs;
using colab.Business.Repository.Interfaces;
using colab.Business.Models.Entities;
using colab.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace colab.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IBolsistaRepository _bolsistaRepository;

        public ProjetoController(IProjetoRepository projetoRepository, IBolsistaRepository bolsistaRepository)
        {
            _projetoRepository = projetoRepository;
            _bolsistaRepository = bolsistaRepository;
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
                FinanciadorNome = p.Financiador?.Nome,
                OrientadorId = p.OrientadorId,
                OrientadorNome = p.Orientador?.Nome,
                BolsistasIds = p.Bolsistas?.Select(b => b.Id).ToList(),
                Categoria = p.Categoria,
                Status = p.Status
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
                OrientadorId = projeto.OrientadorId,
                OrientadorNome = projeto.Orientador?.Nome,
                BolsistasIds = projeto.Bolsistas?.Select(b => b.Id).ToList(),
                Categoria = projeto.Categoria,
                Status = projeto.Status
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

            // Associando Bolsistas pelo Id
            if (projetoDto.BolsistasIds != null && projetoDto.BolsistasIds.Any())
            {
                Console.WriteLine("BolsistasIds: " + string.Join(", ", projetoDto.BolsistasIds));

                var bolsistas = await _bolsistaRepository.GetByIdsAsync(projetoDto.BolsistasIds);
                if (bolsistas.Count != projetoDto.BolsistasIds.Count)
                {
                    return BadRequest(new { message = "Um ou mais Bolsistas fornecidos não foram encontrados" });
                }

                projeto.Bolsistas = bolsistas;
            }

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

            // Atualiza os bolsistas associados
            if (projetoDto.BolsistasIds != null)
            {
                var bolsistasExistentes = await _bolsistaRepository.GetByIdsAsync(projetoDto.BolsistasIds);
                if (bolsistasExistentes.Count != projetoDto.BolsistasIds.Count)
                {
                    return BadRequest(new { message = "Um ou mais Bolsistas não foram encontrados" });
                }

                projeto.Bolsistas = bolsistasExistentes;
            }

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
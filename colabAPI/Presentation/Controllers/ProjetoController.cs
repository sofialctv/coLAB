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
        private readonly IBolsaRepository _bolsaRepository;

        public ProjetoController(IProjetoRepository projetoRepository, IBolsaRepository bolsaRepository)
        {
            _projetoRepository = projetoRepository;
            _bolsaRepository = bolsaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetoResponseDTO>>> GetAll()
        {
            var projetos = await _projetoRepository.GetAllAsync();

            var projetoDtos = projetos.Select(p => new ProjetoResponseDTO
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
                // Mapeando o Histórico de Status do Projeto para o DTO
                HistoricoStatus = p.HistoricoStatus?.Select(h => new HistoricoProjetoStatusResponseDTO
                {
                    Id = h.Id,
                    DataInicio = h.DataInicio,
                    DataFim = h.DataFim,
                    Status = (int)h.Status
                }).ToList(),
                // Mapeando Bolsas para o DTO
                Bolsas = p.Bolsas?.Select(b => new BolsaDTO
                {
                    Id = b.Id,
                    Valor = b.Valor,
                    Categoria = b.Categoria,
                    DataInicio = b.DataInicio,
                    DataFim = b.DataFim,
                    Ativo = b.Ativo
                }).ToList()
            }).ToList();

            return Ok(projetoDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjetoResponseDTO>> GetById(int id)
        {
            var projeto = await _projetoRepository.GetByIdAsync(id);
            if (projeto == null)
            {
                return NotFound(new { message = "Projeto não encontrado" });
            }

            var projetoDto = new ProjetoResponseDTO
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
                // histórico de status para DTO
                HistoricoStatus = projeto.HistoricoStatus?.Select(h => new HistoricoProjetoStatusResponseDTO
                {
                    Id = h.Id,
                    DataInicio = h.DataInicio,
                    DataFim = h.DataFim,
                    Status = (int)h.Status
                }).ToList(),
                // bolsas para DTO
                Bolsas = projeto.Bolsas?.Select(b => new BolsaDTO
                {
                    Id = b.Id,
                    Valor = b.Valor,
                    Categoria = b.Categoria,
                    DataInicio = b.DataInicio,
                    DataFim = b.DataFim,
                    Ativo = b.Ativo
                }).ToList()
            };

            return Ok(projetoDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjetoRequestDTO projetoDto)
        {
            var projeto = new Projeto
            {
                Nome = projetoDto.Nome,
                DataInicio = projetoDto.DataInicio,
                DataFim = projetoDto.DataFim,
                DataPrevistaFim = projetoDto.DataPrevistaFim,
                Descricao = projetoDto.Descricao,
                Orcamento = projetoDto.Orcamento,
                FinanciadorId = projetoDto.FinanciadorId
            };

            var createdProjeto = await _projetoRepository.AddAsync(projeto);

            // Criar registro de histórico de status
            var historicoStatus = new HistoricoProjetoStatus
            {
                ProjetoId = createdProjeto.Id,
                Status = projetoDto.Status,
                DataInicio = DateTime.UtcNow // Definindo o início como o momento da criação
            };

            await _projetoRepository.AddHistoricoStatusAsync(historicoStatus);

            return CreatedAtAction(nameof(GetById), new { id = createdProjeto.Id }, createdProjeto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProjetoRequestDTO projetoDto)
        {
            if (id != projetoDto.Id)
            {
                return BadRequest(new { message = "ID do projeto não corresponde" });
            }

            var projeto = await _projetoRepository.GetByIdAsync(id); // Certifique-se de incluir o carregamento do histórico
            if (projeto == null)
            {
                return NotFound(new { message = "Projeto não encontrado" });
            }

            // Buscar o último status no histórico
            var ultimoStatus = projeto.HistoricoStatus?
                .OrderByDescending(h => h.DataInicio)
                .FirstOrDefault();

            // Verificar se o status mudou
            var statusAlterado = ultimoStatus?.Status != projetoDto.Status;

            projeto.Nome = projetoDto.Nome;
            projeto.DataInicio = projetoDto.DataInicio;
            projeto.DataFim = projetoDto.DataFim;
            projeto.DataPrevistaFim = projetoDto.DataPrevistaFim;
            projeto.Descricao = projetoDto.Descricao;
            projeto.Orcamento = projetoDto.Orcamento;
            projeto.FinanciadorId = projetoDto.FinanciadorId;

            await _projetoRepository.UpdateAsync(projeto);

            // Criar novo registro de histórico se o status mudou
            if (statusAlterado)
            {
                var historicoStatus = new HistoricoProjetoStatus
                {
                    ProjetoId = projeto.Id,
                    Status = projetoDto.Status,
                    DataInicio = DateTime.UtcNow
                };

                await _projetoRepository.AddHistoricoStatusAsync(historicoStatus);

                if (ultimoStatus != null)
                {
                    ultimoStatus.DataFim = DateTime.UtcNow;
                    await _projetoRepository.UpdateHistoricoStatusAsync(ultimoStatus);
                }
            }

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
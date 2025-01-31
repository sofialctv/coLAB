using colab.Business.DTOs;
using colab.Business.Repository.Interfaces;
using colab.Business.Models.Entities;
using colab.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using colab.Business.DTOs.Request;

namespace colab.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IBolsaRepository _bolsaRepository;
        private readonly IMapper _mapper;

        public ProjetoController(IProjetoRepository projetoRepository, IBolsaRepository bolsaRepository, IMapper mapper)
        {
            _projetoRepository = projetoRepository;
            _bolsaRepository = bolsaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetoResponseDTO>>> GetAll()
        {
            var projetos = await _projetoRepository.GetAllAsync();
            var projetoDtos = _mapper.Map<List<ProjetoResponseDTO>>(projetos);
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

            var projetoDto = _mapper.Map<ProjetoResponseDTO>(projeto);
            return Ok(projetoDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjetoRequestDTO projetoDto)
        {
            var projeto = _mapper.Map<Projeto>(projetoDto);
            var createdProjeto = await _projetoRepository.AddAsync(projeto);

            var historicoStatus = new HistoricoProjetoStatus
            {
                ProjetoId = createdProjeto.Id,
                Status = projetoDto.Status,
                DataInicio = DateTime.UtcNow
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

            var projeto = await _projetoRepository.GetByIdAsync(id);
            if (projeto == null)
            {
                return NotFound(new { message = "Projeto não encontrado" });
            }

            // Buscar o último status no histórico
            var ultimoStatus = projeto.HistoricoStatus?
                .OrderByDescending(h => h.DataInicio)
                .FirstOrDefault();

            // Verificar se o status mudou
            bool statusAlterado = ultimoStatus?.Status != projetoDto.Status;

            // Atualizar os campos do projeto
            _mapper.Map(projetoDto, projeto);
            await _projetoRepository.UpdateAsync(projeto);

            // Criar novo registro de histórico se o status mudou
            if (statusAlterado)
            {
                var novoHistorico = new HistoricoProjetoStatus
                {
                    ProjetoId = projeto.Id,
                    Status = projetoDto.Status,
                    DataInicio = DateTime.UtcNow
                };

                await _projetoRepository.AddHistoricoStatusAsync(novoHistorico);

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
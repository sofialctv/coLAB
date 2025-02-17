﻿using colab.Business.DTOs;
using colab.Business.DTOs.Request;
using colab.Business.Models.Entities;
using colab.Business.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace colab.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjetoController : ControllerBase
    {
        private readonly IProjetoService _projetoService;
        private readonly IMapper _mapper;

        public ProjetoController(IProjetoService projetoService, IMapper mapper)
        {
            _projetoService = projetoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjetoResponseDTO>>> GetAll()
        {
            var projetos = await _projetoService.GetAllAsync();
            var projetoDtos = _mapper.Map<List<ProjetoResponseDTO>>(projetos);
            return Ok(projetoDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjetoResponseDTO>> GetById(int id)
        {
            var projeto = await _projetoService.GetByIdAsync(id);
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
            var createdProjeto = await _projetoService.AddAsync(projeto);

            var historicoStatus = new HistoricoProjetoStatus
            {
                ProjetoId = createdProjeto.Id,
                Status = projetoDto.Status,
                DataInicio = DateTime.UtcNow
            };

            await _projetoService.AddHistoricoStatusAsync(historicoStatus);

            return CreatedAtAction(nameof(GetById), new { id = createdProjeto.Id }, createdProjeto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProjetoRequestDTO projetoDto)
        {
            if (id != projetoDto.Id)
            {
                return BadRequest(new { message = "ID do projeto não corresponde" });
            }

            var projeto = await _projetoService.GetByIdAsync(id);
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
            await _projetoService.UpdateAsync(projeto);

            // Criar novo registro de histórico se o status mudou
            if (statusAlterado)
            {
                var novoHistorico = new HistoricoProjetoStatus
                {
                    ProjetoId = projeto.Id,
                    Status = projetoDto.Status,
                    DataInicio = DateTime.UtcNow
                };

                await _projetoService.AddHistoricoStatusAsync(novoHistorico);

                if (ultimoStatus != null)
                {
                    ultimoStatus.DataFim = DateTime.UtcNow;
                    await _projetoService.UpdateHistoricoStatusAsync(ultimoStatus);
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var projeto = await _projetoService.GetByIdAsync(id);
            if (projeto == null)
            {
                return NotFound(new { message = "Projeto não encontrado" });
            }

            await _projetoService.DeleteAsync(id);
            return NoContent();
        }
    }
}

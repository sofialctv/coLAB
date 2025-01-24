using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using colabAPI.Business.DTOs;
using colabAPI.Business.Models.Entities;
using colabAPI.Business.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace colabAPI.Business.Models.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository; 
        private readonly IMapper _mapper; 

        public PessoaController(IPessoaRepository pessoaRepository, IMapper mapper)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

        // GET: api/Pessoa
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pessoas = await _pessoaRepository.GetAllAsync();
            var pessoasDTO = _mapper.Map<IEnumerable<PessoaResponseDTO>>(pessoas);
            return Ok(pessoasDTO);
        }

        // GET: api/Pessoa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            var pessoaDTO = _mapper.Map<PessoaResponseDTO>(pessoa);
            return Ok(pessoaDTO);
        }

        // POST: api/Pessoa
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PessoaRequestDTO pessoaRequestDTO)
        {
            if (!ModelState.IsValid) // Valida o modelo
            {
                return BadRequest(ModelState); // Solicitação inválida erro 400
            }

            var pessoa = _mapper.Map<Pessoa>(pessoaRequestDTO);

            await _pessoaRepository.AddAsync(pessoa);
            await _pessoaRepository.Save();

            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
        }

        // PUT: api/Pessoa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PessoaRequestDTO pessoaRequestDTO)
        {
            if (id != pessoaRequestDTO.Id || !ModelState.IsValid) // Verifica se o ID corresponde e o modelo é válido
            {
                return BadRequest(); // Solicitação inválida erro 400
            }

            var existingPessoa = await _pessoaRepository.GetByIdAsync(id);
            if (existingPessoa == null)
            {
                return NotFound(); // Famoso erro 404
            }

            var pessoa = _mapper.Map(pessoaRequestDTO, existingPessoa);

            await _pessoaRepository.UpdateAsync(pessoa);
            await _pessoaRepository.Save();

            return NoContent();
        }

        // DELETE: api/Pessoa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound(); // Famoso erro 404
            }

            await _pessoaRepository.DeleteAsync(id);
            await _pessoaRepository.Save();

            return NoContent();
        }
    }
}

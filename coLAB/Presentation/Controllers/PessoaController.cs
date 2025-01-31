using AutoMapper;
using colab.Business.DTOs.Request;
using colab.Business.DTOs.Response;
using colab.Business.Models.Entities;
using colab.Business.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace colab.Presentation.Controllers
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
        public async Task<ActionResult<IEnumerable<PessoaResponseDTO>>> GetAll()
        {
            var pessoas = await _pessoaRepository.GetAllAsync();
            var pessoasDTO = _mapper.Map<IEnumerable<PessoaResponseDTO>>(pessoas);
            return Ok(pessoasDTO);
        }

        // GET: api/Pessoa/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaResponseDTO>> GetById(int id)
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
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var pessoa = _mapper.Map<Pessoa>(pessoaRequestDTO);

            await _pessoaRepository.AddAsync(pessoa);
            
            var pessoaResponseDTO = _mapper.Map<PessoaResponseDTO>(pessoa);

            return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoaResponseDTO);
        }

        // PUT: api/Pessoa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PessoaRequestDTO pessoaRequestDTO)
        {
            if (id != pessoaRequestDTO.Id || !ModelState.IsValid) 
            {
                return BadRequest(); 
            }

            var existingPessoa = await _pessoaRepository.GetByIdAsync(id);
            if (existingPessoa == null)
            {
                return NotFound(); 
            }

            var pessoa = _mapper.Map(pessoaRequestDTO, existingPessoa);

            await _pessoaRepository.UpdateAsync(pessoa);

            return NoContent();
        }

        // DELETE: api/Pessoa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(id);
            if (pessoa == null)
            {
                return NotFound(); 
            }

            await _pessoaRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}

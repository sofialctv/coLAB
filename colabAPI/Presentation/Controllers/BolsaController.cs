using AutoMapper;
using colabAPI.Business.DTOs;
using colabAPI.Business.DTOs.Request;
using colabAPI.Business.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using colabAPI.Business.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;

namespace colabAPI.Business.Models.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BolsaController : ControllerBase
    {
        private readonly IBolsaRepository _bolsaRepository; // Interface para acessar o repositorio de Bolsa
        private readonly IMapper _mapper; 
        
        // Injeta o repositório no construtor
        public BolsaController(IBolsaRepository bolsaRepository, IMapper mapper)
        {   
            _bolsaRepository = bolsaRepository;
            _mapper = mapper;
        }
        
        // GET: api/Bolsa
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bolsas = await _bolsaRepository.GetAllAsync();
    
            // Mapeia as entidades Bolsa para os DTOs BolsaResponseDTO
            var bolsaDtos = _mapper.Map<IEnumerable<BolsaResponseDTO>>(bolsas);
    
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Serializa a resposta com as configurações de loop de referência ignoradas
            var jsonResponse = JsonConvert.SerializeObject(bolsaDtos, settings);

            return Content(jsonResponse, "application/json");
        }
        
        // GET: api/Bolsa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bolsa = await _bolsaRepository.GetByIdAsync(id);
            if (bolsa == null)
            {
                return NotFound(); // Retorna 404 se a bolsa não for encontrada
            }

            // Mapeia a entidade Bolsa para o DTO BolsaResponseDTO
            var bolsaDto = _mapper.Map<BolsaResponseDTO>(bolsa);
    
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            // Serializa a resposta com as configurações de loop de referência ignoradas
            var jsonResponse = JsonConvert.SerializeObject(bolsaDto, settings);

            return Content(jsonResponse, "application/json");
        }
        
        // POST: api/Bolsa
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BolsaRequestDTO bolsaRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna erro 400 caso o modelo seja inválido
            }

            // Mapeia o DTO BolsaRequestDTO para a entidade Bolsa
            var bolsa = _mapper.Map<Bolsa>(bolsaRequestDto);

            try
            {
                // Adiciona a bolsa e tenta salvar no banco de dados
                await _bolsaRepository.AddAsync(bolsa);
                await _bolsaRepository.Save();
            }
            catch (DbUpdateException ex)
            {
                // Verifica se o erro é uma violação de chave duplicada
                if (ex.InnerException is PostgresException postgresEx && postgresEx.SqlState == "23505")
                {
                    // Lidar com a violação de chave duplicada
                    return Conflict(new { message = "Já existe uma bolsa com este TipoBolsaId." }); 
                }
                else
                {
                    // Caso o erro não seja uma violação de chave duplicada, relança o erro
                    throw;
                }
            }


            // Mapeia a entidade Bolsa salva para o DTO BolsaResponseDTO para retornar ao cliente
            var bolsaResponseDto = _mapper.Map<BolsaResponseDTO>(bolsa);
    
            return CreatedAtAction(nameof(GetById), new { id = bolsa.Id }, bolsaResponseDto);
        }


        
        // PUT: api/Bolsa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BolsaRequestDTO bolsaRequestDto)
        {
            if (!ModelState.IsValid || id <= 0)
            {
                return BadRequest(); // Retorna erro 400 para solicitação inválida
            }

            var existingBolsa = await _bolsaRepository.GetByIdAsync(id);
            if (existingBolsa == null)
            {
                return NotFound(); // Retorna erro 404 se a bolsa não for encontrada
            }

            // Atualiza a entidade existente com os valores do DTO
            _mapper.Map(bolsaRequestDto, existingBolsa);

            await _bolsaRepository.UpdateAsync(existingBolsa);
            await _bolsaRepository.Save();

            return NoContent(); // Retorna 204 quando a atualização é bem-sucedida
        }

        
        // DELETE: api/Bolsa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Busca a bolsa pelo ID
            var bolsa = await _bolsaRepository.GetByIdAsync(id);
            if (bolsa == null)
            {
                return NotFound(); // Retorna erro 404 caso não encontre a bolsa
            }

            // Remove a bolsa
            await _bolsaRepository.DeleteAsync(id);
            await _bolsaRepository.Save();

            return NoContent(); // Retorna 204, indicando que a operação foi bem-sucedida
        }

        
        
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using colabAPI.Business.Models.Entities;
using colabAPI.Data;

[Route("api/[controller]")]
[ApiController]

    public class FinanciadoresController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FinanciadoresController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Financiador>>> GetFinanciadores()
    {
        return await _context.Financiadores.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Financiador>> GetFinanciador(int id)
    {
        var financiador = await _context.Financiadores.FindAsync(id);

        if (financiador == null)
        {
            return NotFound();
        }

        return financiador;
    }

    [HttpPost]
    public async Task<ActionResult<Financiador>> PostFinanciador(Financiador financiador)
    {
        _context.Financiadores.Add(financiador);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFinanciador", new { id = financiador.Id }, financiador);
    }
}



using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/clientes")]
[ApiController]
public class ClienteController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClienteController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("cliente")]
public async Task<IActionResult> CriarCliente(ClienteModel cliente)
{
    _context.Clientes.Add(cliente);
    await _context.SaveChangesAsync();
    return CreatedAtAction(nameof(ObterCliente), new { id = cliente.Id }, cliente);
}

 [HttpGet("{id}")]
    public async Task<ActionResult<ClienteModel>> ObterCliente(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return cliente;
    }

    
}
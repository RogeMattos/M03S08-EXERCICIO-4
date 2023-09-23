

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
    public async Task<IActionResult> CriarCliente(ClienteCreateDTO clienteCreateDTO)
    {
        // Mapear o ClienteCreateDTO para um modelo de domínio (ClienteModel)
        var cliente = new ClienteModel
        {
            Nome = clienteCreateDTO.Nome,
            Saldo = clienteCreateDTO.Saldo,
            // Mapear outros campos, se necessário
        };

        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync();

        // Mapear o cliente de volta para um DTO para retornar na resposta
        var clienteReadDTO = new ClienteReadDTO
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Saldo = cliente.Saldo,
            // Mapear outros campos, se necessário
        };

        return CreatedAtAction(nameof(ObterCliente), new { id = clienteReadDTO.Id }, clienteReadDTO);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClienteReadDTO>> ObterCliente(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }

        // Mapear o ClienteModel para um ClienteReadDTO para retornar na resposta
        var clienteReadDTO = new ClienteReadDTO
        {
            Id = cliente.Id,
            Nome = cliente.Nome,
            Saldo = cliente.Saldo,
            // Mapear outros campos, se necessário
        };

        return clienteReadDTO;
    }

    // Outros métodos para atualizar e excluir clientes usando o DTO ClienteUpdateDTO
}

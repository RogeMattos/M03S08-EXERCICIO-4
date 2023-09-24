
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/venda")]
[ApiController]
public class VendaController : ControllerBase
{
    private readonly AppDbContext _context;

    public VendaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("venda")]
    public async Task<IActionResult> RealizarVenda(VendaCreateDTO vendaCreateDTO)
    {
        var produto = await _context.Produtos.FindAsync(vendaCreateDTO.ProdutoId);
        var cliente = await _context.Clientes.FindAsync(vendaCreateDTO.ClienteId);

        if (produto == null || cliente == null)
        {
            return NotFound("Produto ou cliente não encontrado.");
        }

        if (produto.Preco <= 0)
        {
            return BadRequest("O preço do produto deve ser maior que zero.");
        }

        if (cliente.Saldo < produto.Preco - vendaCreateDTO.Desconto)
        {
            return BadRequest("Saldo insuficiente para realizar a venda.");
        }

        var venda = new VendaModel
        {
            ProdutoId = vendaCreateDTO.ProdutoId,
            ClienteId = vendaCreateDTO.ClienteId,
            Desconto = vendaCreateDTO.Desconto
        };

        _context.Vendas.Add(venda);
        await _context.SaveChangesAsync();

        // Atualiza o saldo do cliente após a venda
        cliente.Saldo -= produto.Preco - vendaCreateDTO.Desconto;
        await _context.SaveChangesAsync();

        // Mapear a venda de volta para um DTO para retornar na resposta
        var vendaReadDTO = new VendaReadDTO
        {
            Id = venda.Id,
            ProdutoId = venda.ProdutoId,
            ClienteId = venda.ClienteId,
            Desconto = venda.Desconto
            // Mapear outros campos, se necessário
        };

        return CreatedAtAction(nameof(ObterVenda), new { id = vendaReadDTO.Id }, vendaReadDTO);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VendaReadDTO>> ObterVenda(int id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda == null)
        {
            return NotFound();
        }

        // Mapear a VendaModel para um VendaReadDTO para retornar na resposta
        var vendaReadDTO = new VendaReadDTO
        {
            Id = venda.Id,
            ProdutoId = venda.ProdutoId,
            ClienteId = venda.ClienteId,
            Desconto = venda.Desconto
            // Mapear outros campos, se necessário
        };

        return vendaReadDTO;
    }

    // Outros métodos para atualizar e excluir vendas usando o DTO VendaUpdateDTO
}

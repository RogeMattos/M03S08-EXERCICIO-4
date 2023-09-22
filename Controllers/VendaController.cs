
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
public async Task<IActionResult> RealizarVenda(int produtoId, int clienteId, decimal desconto)
{
    var produto = await _context.Produtos.FindAsync(produtoId);
    var cliente = await _context.Clientes.FindAsync(clienteId);

    if (produto == null || cliente == null)
    {
        return NotFound("Produto ou cliente não encontrado.");
    }

    if (produto.Preco <= 0)
    {
        return BadRequest("O preço do produto deve ser maior que zero.");
    }

    if (cliente.Saldo < produto.Preco - desconto)
    {
        return BadRequest("Saldo insuficiente para realizar a venda.");
    }

    var venda = new VendaModel
    {
        ProdutoId = produtoId,
        ClienteId = clienteId,
        Desconto = desconto
    };

    _context.Vendas.Add(venda);
    await _context.SaveChangesAsync();

    // Atualiza o saldo do cliente após a venda
    cliente.Saldo -= produto.Preco - desconto;
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(ObterVenda), new { id = venda.Id }, venda);
}

[HttpGet("{id}")]
    public async Task<ActionResult<VendaModel>> ObterVenda(int id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda == null)
        {
            return NotFound();
        }
        return venda;
    }


}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/produtos")]
[ApiController]
public class ProdutoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdutoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CriarProduto(ProdutoCreateDTO produtoCreateDTO)
    {
        // Mapear o ProdutoCreateDTO para um modelo de domínio (ProdutoModel)
        var produto = new ProdutoModel
        {
            Nome = produtoCreateDTO.Nome,
            Preco = produtoCreateDTO.Preco,
            // Mapear outros campos, se necessário
        };

        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();

        // Mapear o produto de volta para um DTO para retornar na resposta
        var produtoReadDTO = new ProdutoReadDTO
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            // Mapear outros campos, se necessário
        };

        return CreatedAtAction(nameof(ObterProduto), new { id = produtoReadDTO.Id }, produtoReadDTO);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoReadDTO>> ObterProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }

        // Mapear o ProdutoModel para um ProdutoReadDTO para retornar na resposta
        var produtoReadDTO = new ProdutoReadDTO
        {
            Id = produto.Id,
            Nome = produto.Nome,
            Preco = produto.Preco,
            // Mapear outros campos, se necessário
        };

        return produtoReadDTO;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarProduto(int id, ProdutoUpDateDTO produtoUpdateDTO)
    {
        if (id != produtoUpdateDTO.Id)
        {
            return BadRequest();
        }

        // Verificar se o produto existe
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }

        // Atualizar as propriedades do ProdutoModel com base no ProdutoUpdateDTO
        produto.Validade = produtoUpdateDTO.Validade;
        produto.Preco = produtoUpdateDTO.Preco;
        // Atualizar outros campos, se necessário

        _context.Entry(produto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarProduto(int id)
    {
        var produto = await _context.Produtos.FindAsync(id);
        if (produto == null)
        {
            return NotFound();
        }

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}

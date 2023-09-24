using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class ProdutoReadDTO
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR"), Required, StringLength(250)]
    public string? Nome { get; set; }

    [Column(TypeName = "decimal(18, 2)")] // Decimal com duas casas decimais
    public decimal Preco { get; set; }

    public DateTime Validade { get; set; }
    public virtual ICollection<VendaModel>? Vendas { get; set; }

}



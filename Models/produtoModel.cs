
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Produto")]
public class ProdutoModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR"), Required, StringLength(250)]
    public string Nome { get; set; }

    [Column(TypeName = "decimal(18, 2)")] // Decimal com duas casas decimais
    public decimal Preco { get; set; }

    public DateTime Validade { get; set; }

    // Relação entre Produto e Venda:
    // Um Produto pode estar relacionado a várias Vendas (um para muitos).
    // Isso significa que o mesmo produto pode ser vendido várias vezes para 
    // diferentes clientes. Portanto, a classe Produto terá uma coleção de Vendas.
    public virtual ICollection<VendaModel> Vendas { get; set; }
}
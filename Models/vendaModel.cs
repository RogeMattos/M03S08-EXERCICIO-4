
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Venda")]
public class VendaModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Produto")]
    public int ProdutoId { get; set; }

    [ForeignKey("Cliente")]
    public int ClienteId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
     public decimal Desconto { get; set; }

    //Relação entre Venda, Produto e Cliente:
    //Uma Venda está relacionada a um Produto específico (um para um, pois cada venda
    //refere-se a um produto) e a um Cliente específico (um para um, pois cada venda é feita
    //por um cliente). Portanto, a classe Venda terá propriedades que representam o Produto
    //associado e o Cliente associado.
    
    public ProdutoModel Produto { get; set; }
    public ClienteModel Cliente { get; set; }
}
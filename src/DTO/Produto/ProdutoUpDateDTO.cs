

using System.ComponentModel.DataAnnotations.Schema;

public class ProdutoUpDateDTO
 { 
    public int Id { get; set; }

     [Column(TypeName = "decimal(18, 2)")] // Decimal com duas casas decimais
    public decimal Preco { get; set; }

    public DateTime Validade { get; set; }
    
 }
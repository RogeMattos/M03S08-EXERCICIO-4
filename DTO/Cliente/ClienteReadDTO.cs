using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



    public class ClienteReadDTO
    {
     
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR"), Required, StringLength(250)]
    public string? Nome { get; set; }

    [Column(TypeName = "decimal(18, 2)")] // Decimal com duas casas decimais
    public decimal Saldo { get; set; }

    // Relação entre Cliente e Venda:
    // Um Cliente pode estar relacionado a várias Vendas (um para muitos).
    // Isso significa que um cliente pode fazer várias compras ao longo do
    // tempo. Portanto, a classe Cliente terá uma coleção de Vendas.
    public virtual ICollection<VendaModel>? Vendas { get; set; }
        
    }

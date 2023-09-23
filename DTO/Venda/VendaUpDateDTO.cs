using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



public class VendaUpDateDTO
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public int ClienteId { get; set; }
    public decimal Desconto { get; set; }
}

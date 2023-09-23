using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



    public class ClienteCreateDTO
    {
    [Column(TypeName = "VARCHAR"), Required, StringLength(250)]
    public string? Nome { get; set; }

    [Column(TypeName = "decimal(18, 2)")] // Decimal com duas casas decimais
    public decimal Saldo { get; set; }

        
    }

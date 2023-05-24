using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Banco;

[Table("boletos")]
public partial class Boleto
{
    [Key]
    [Column("ticket")]
    public long Ticket { get; set; }

    [Column("cliente")]
    public long? Cliente { get; set; }

    [Column("fechaExcibicion")]
    public string? FechaExcibicion { get; set; }

    [ForeignKey("Cliente")]
    [InverseProperty("Boletos")]
    public virtual Cliente? ClienteNavigation { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Banco;

[Table("pagos")]
public partial class Pago
{
    [Key]
    [Column("folio")]
    public long Folio { get; set; }

    [Column("prestamo")]
    public long? Prestamo { get; set; }

    [Column("fecha")]
    public string? Fecha { get; set; }

    [Column("cantidad")]
    public long? Cantidad { get; set; }

    [Column("numDePago")]
    public long? NumDePago { get; set; }

    [ForeignKey("Prestamo")]
    [InverseProperty("Pagos")]
    public virtual Prestamo? PrestamoNavigation { get; set; }
}

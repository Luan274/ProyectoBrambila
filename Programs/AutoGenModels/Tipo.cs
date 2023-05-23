using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.Shared.AutoGen;

[Table("tipo")]
public partial class Tipo
{
    [Key]
    [Column("tipoID")]
    public long TipoId { get; set; }

    [Column("tipo")]
    public string? Tipo1 { get; set; }

    [Column("interes")]
    public double? Interes { get; set; }

    [InverseProperty("TipoNavigation")]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Banco;

[Table("estado")]
public partial class Estado
{
    [Key]
    [Column("estadoID")]
    public long EstadoId { get; set; }

    [Column("tipo")]
    public string? Tipo { get; set; }

    [InverseProperty("EstadoNavigation")]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}

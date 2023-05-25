using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Banco;

[Table("vacaciones")]
public partial class Vacacione
{
    [Key]
    [Column("folio")]
    public long Folio { get; set; }

    [Column("empleadoID")]
    public long? EmpleadoId { get; set; }

    [Column("gerenteID")]
    public long? GerenteId { get; set; }

    [Column("fechaInicio")]
    public string? FechaInicio { get; set; }

    [Column("fechaRegreso")]
    public string? FechaRegreso { get; set; }

    [Column("estado")]
    public string? Estado { get; set; }

    [ForeignKey("EmpleadoId")]
    [InverseProperty("Vacaciones")]
    public virtual Empleado? Empleado { get; set; }

    [ForeignKey("GerenteId")]
    [InverseProperty("Vacaciones")]
    public virtual Gerente? Gerente { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.Shared.AutoGen;

[Table("prestamo")]
public partial class Prestamo
{
    [Key]
    [Column("folio")]
    public long Folio { get; set; }

    [Column("empleado")]
    public string? Empleado { get; set; }

    [Column("tipo")]
    public long? Tipo { get; set; }

    [Column("fecSolicitud")]
    public string? FecSolicitud { get; set; }

    [Column("fecAprovado")]
    public string? FecAprovado { get; set; }

    [Column("pagoMensual")]
    public long? PagoMensual { get; set; }

    [Column("fecSiguientePago")]
    public string? FecSiguientePago { get; set; }

    [Column("estado")]
    public long? Estado { get; set; }

    [Column("usuario")]
    public long? Usuario { get; set; }

    [Column("pagosTotales")]
    public long? PagosTotales { get; set; }

    [Column("fecLiquidacion")]
    public string? FecLiquidacion { get; set; }

    [ForeignKey("Empleado")]
    [InverseProperty("Prestamos")]
    public virtual Empleado? EmpleadoNavigation { get; set; }

    [ForeignKey("Estado")]
    [InverseProperty("Prestamos")]
    public virtual Estado? EstadoNavigation { get; set; }

    [InverseProperty("PrestamoNavigation")]
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    [ForeignKey("Tipo")]
    [InverseProperty("Prestamos")]
    public virtual Tipo? TipoNavigation { get; set; }

    [ForeignKey("Usuario")]
    [InverseProperty("Prestamos")]
    public virtual Cliente? UsuarioNavigation { get; set; }
}

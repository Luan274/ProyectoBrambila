using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Banco;

[Table("gerente")]
public partial class Gerente
{
    [Key]
    [Column("gerenteID")]
    public long GerenteId { get; set; }

    [Column("empleadoID")]
    public long? EmpleadoId { get; set; }

    [Column("clienteID")]
    public long? ClienteId { get; set; }

    [ForeignKey("ClienteId")]
    [InverseProperty("Gerentes")]
    public virtual Cliente? Cliente { get; set; }

    [ForeignKey("EmpleadoId")]
    [InverseProperty("Gerentes")]
    public virtual Empleado? Empleado { get; set; }

    [InverseProperty("Gerente")]
    public virtual ICollection<Vacacione> Vacaciones { get; set; } = new List<Vacacione>();
}

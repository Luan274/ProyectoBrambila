using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WorkingWithEFCore.Shared.AutoGen;

[Table("usuario")]
public partial class Usuario
{
    [Key]
    [Column("userID")]
    public long UserId { get; set; }

    [Column("nombre")]
    public string? Nombre { get; set; }

    [Column("apellido")]
    public string? Apellido { get; set; }

    [Column("usuario")]
    public string? Usuario1 { get; set; }

    [Column("contrasena")]
    public string? Contrasena { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    [InverseProperty("Usuario")]
    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}

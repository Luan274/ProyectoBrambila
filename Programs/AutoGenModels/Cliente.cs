using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Banco;

[Table("cliente")]
public partial class Cliente
{
    [Key]
    [Column("clienteID")]
    public long ClienteId { get; set; }

    [Column("userID")]
    public long? UserId { get; set; }

    [Column("fechaDeNac")]
    public string? FechaDeNac { get; set; }

    [Column("CURP")]
    public string? Curp { get; set; }

    [Column("comportamiento")]
    public string? Comportamiento { get; set; }

    [Column("aprovado")]
    public long? Aprovado { get; set; }

    [Column("saldo")]
    public long? Saldo { get; set; }

    [Column("horaLogin")]
    public string? HoraLogin { get; set; }

    [Column("intentosFallidos")]
    public long? IntentosFallidos { get; set; }

    [InverseProperty("ClienteNavigation")]
    public virtual ICollection<Boleto> Boletos { get; set; } = new List<Boleto>();

    [InverseProperty("Cliente")]
    public virtual ICollection<Gerente> Gerentes { get; set; } = new List<Gerente>();

    [InverseProperty("UsuarioNavigation")]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    [ForeignKey("UserId")]
    [InverseProperty("Clientes")]
    public virtual Usuario? User { get; set; }
}

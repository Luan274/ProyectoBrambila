using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Console;

namespace Banco;

[Table("empleado")]
public partial class Empleado
{
    [Key]
    [Column("nomina")]
    public string Nomina { get; set; } = null!;

    [Column("diasDeVac")]
    public long? DiasDeVac { get; set; }

    [Column("usuarioID")]
    public long? UsuarioId { get; set; }

    [Column("fecEntrada")]
    public string? FecEntrada { get; set; }

    [InverseProperty("Empleado")]
    public virtual ICollection<Gerente> Gerentes { get; set; } = new List<Gerente>();

    [InverseProperty("EmpleadoNavigation")]
    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();

    [ForeignKey("UsuarioId")]
    [InverseProperty("Empleados")]
    public virtual Usuario? Usuario { get; set; }

    [InverseProperty("Empleado")]
    public virtual ICollection<Vacacione> Vacaciones { get; set; } = new List<Vacacione>();
    static (int affected, string productId) AddEmpleado(string nomina, string fecEntrada, long diasDeVac)
    {
        using (Bank db = new())
        {
            if (db.Empleados is null) return (0, "");
            Empleado e = new()
            {
                Nomina = nomina,
                FecEntrada = fecEntrada,
                DiasDeVac = diasDeVac,
            };

            EntityEntry<Empleado> entity = db.Empleados.Add(e);
            int affected = db.SaveChanges();
            WriteLine($"State: {entity.State}, Nomina: {e.Nomina}");
            return (affected, e.Nomina);
        }
    }
}

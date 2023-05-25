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
    public long Nomina { get; set; }

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
    public static (int affected, long nomina) addEmpleado(string fecEntrada, long Usuario)
    {
        using (Bank db = new())
        {
            if (db.Empleados is null) return (0, 0);
            DateOnly fecha;
            if(!DateOnly.TryParse(fecEntrada, out fecha)) return (0,0);
            Empleado e = new()
            {
                DiasDeVac = 0,
                FecEntrada = fecEntrada,
                UsuarioId = Usuario
            };
            
            EntityEntry<Empleado> entity = db.Empleados.Add(e);
            int affected;
            try{
                affected = db.SaveChanges();
                }catch(Microsoft.EntityFrameworkCore.DbUpdateException ex){
                    WriteLine($"{ex}");
                    affected = 0;
                }
            return (affected, e.Nomina);
        }
    }

    public static void ListEmpleado(int? []? IdToHighlight = null)
    {
        using(Bank db = new())
        {
            if((db.Empleados is null) || (!db.Empleados.Any()))
            {
                WriteLine("There are no employees");
                return;
            }
            WriteLine("| {0,-6} | {1,-12} | {2,12} | {3,5} |",
            "Nomina", "Ingreso", "Vacaciones", "Usuario");
            foreach (Empleado c in db.Empleados)
            {
                ConsoleColor previousColor = ForegroundColor;
                if((IdToHighlight is not null) && (IdToHighlight.Contains((int)c.Nomina)))
                {
                    ForegroundColor = ConsoleColor.Green;
                }
                WriteLine("| {0,-6} | {1,-12} | {2,12} | {3,5} |",
                c.Nomina, c.FecEntrada, c.DiasDeVac, c.UsuarioId);
                ForegroundColor = previousColor;
            }
        }
    }

}

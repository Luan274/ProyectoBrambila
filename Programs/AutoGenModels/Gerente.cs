using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Console;

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

    public static (int affected, long gerenteID) add(long empleado, long cliente)
    {
        using (Bank db = new())
        {
            if (db.Gerentes is null) return (0, 0);
            Gerente g = new()
            {
                EmpleadoId = empleado,
                ClienteId = cliente
            };
            
            EntityEntry<Gerente> entity = db.Gerentes.Add(g);
            int affected;
            try{
                affected = db.SaveChanges();
                }catch(Microsoft.EntityFrameworkCore.DbUpdateException ex){
                    WriteLine($"{ex}");
                    affected = 0;
                }
            return (affected, g.GerenteId);
        }
    }

    public static void List(int? []? IdToHighlight = null)
    {
        using(Bank db = new())
        {
            if((db.Gerentes is null) || (!db.Gerentes.Any()))
            {
                WriteLine("There are no Managers");
                return;
            }
            WriteLine("| {0,-3} | {1,-7} | {2,12} |",
            "ID", "Emp ID", "Cliente ID");
            foreach (Gerente g in db.Gerentes)
            {
                ConsoleColor previousColor = ForegroundColor;
                if((IdToHighlight is not null) && (IdToHighlight.Contains((int)g.GerenteId)))
                {
                    ForegroundColor = ConsoleColor.Green;
                }
                WriteLine("| {0,-3} | {1,-7} | {2,12} |",
                g.GerenteId, g.EmpleadoId, g.ClienteId);
                ForegroundColor = previousColor;
            }
        }
    }
}

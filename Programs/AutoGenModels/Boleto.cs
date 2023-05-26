using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Console;

namespace Banco;

[Table("boletos")]
public partial class Boleto
{
    [Key]
    [Column("ticket")]
    public long Ticket { get; set; }

    [Column("cliente")]
    public long? Cliente { get; set; }

    [Column("fechaExcibicion")]
    public string? FechaExcibicion { get; set; }

    [ForeignKey("Cliente")]
    [InverseProperty("Boletos")]
    public virtual Cliente? ClienteNavigation { get; set; }

    public static (int affected, long ticket) add(long cliente, string fecha)
    {
        using (Bank db = new())
        {
            if (db.Boletos is null) return (0, 0);
            DateOnly fechaE;
            if(!DateOnly.TryParse(fecha, out fechaE)) return (0,0);
            Boleto b = new()
            {
                Cliente = cliente,
                FechaExcibicion = fechaE.ToString()
            };
            
            EntityEntry<Boleto> entity = db.Boletos.Add(b);
            int affected;
            try{
                affected = db.SaveChanges();
                }catch(Microsoft.EntityFrameworkCore.DbUpdateException ex){
                    WriteLine($"{ex}");
                    affected = 0;
                }
            return (affected, b.Ticket);
        }
    }

    public static void List(int? []? IdToHighlight = null)
    {
        using(Bank db = new())
        {
            if((db.Boletos is null) || (!db.Boletos.Any()))
            {
                WriteLine("There are no Tickets");
                return;
            }
            WriteLine("| {0,-3} | {1,-10} | {2,12} |",
            "#", "Cliente", "Fecha Exp");
            foreach (Boleto b in db.Boletos)
            {
                ConsoleColor previousColor = ForegroundColor;
                if((IdToHighlight is not null) && (IdToHighlight.Contains((int)b.Ticket)))
                {
                    ForegroundColor = ConsoleColor.Green;
                }
                WriteLine("| {0,-3} | {1,-10} | {2,12} |",
                b.Ticket, b.Cliente, b.FechaExcibicion);
                ForegroundColor = previousColor;
            }
        }
    }
}

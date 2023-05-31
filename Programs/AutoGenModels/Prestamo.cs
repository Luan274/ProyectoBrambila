using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Console;

namespace Banco;

[Table("prestamo")]
public partial class Prestamo
{
    [Key]
    [Column("folio")]
    public long Folio { get; set; }

    [Column("empleado")]
    public long? Empleado { get; set; }

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

    public static (int affected, long folio) 
    addPrestamo(long empleado, long tipo, string fecSolicitud, decimal pagoMensual, 
    string fecSiguientePago, long usuario, long estado, long pagosTotales, string fecLiquidacion)
    {
        using (Bank db = new())
        {
            if (db.Prestamos is null) return (0, 0);
            DateOnly fecha;
            if(!DateOnly.TryParse(fecSolicitud, out fecha)) return (0,0);
            if(!DateOnly.TryParse(fecSiguientePago, out fecha)) return (0,0);
            if(!DateOnly.TryParse(fecLiquidacion, out fecha)) return (0,0);

            Prestamo p = new()
            {
                Empleado = empleado,
                Tipo = tipo,
                FecSolicitud = fecSolicitud,
                FecAprovado = null,
                //PagoMensual = pagoMensual, 
                FecSiguientePago = fecSiguientePago, 
                Usuario = usuario,
                Estado = estado, 
                PagosTotales = pagosTotales,
                FecLiquidacion = fecLiquidacion
            };
            
            EntityEntry<Prestamo> entity = db.Prestamos.Add(p);
            int affected;
            try{
                affected = db.SaveChanges();
                }catch(Microsoft.EntityFrameworkCore.DbUpdateException e){
                    WriteLine($"{e}");
                    affected = 0;
                }
            return (affected, p.Folio);
        }
    }

    public static void ListPrestamos(int? []? IdToHighlight = null)
    {
        using(Bank db = new())
        {
            if((db.Prestamos is null) || (!db.Prestamos.Any()))
            {
                WriteLine("There are no loans");
                return;
            }
            WriteLine("| {0,-6} | {1,-12} | {2,12} | {3,5} | {4, 5} | {5, 5} | {6, 12} | {7, 10} | {8, 5} | {9, 5} | {10, 5} |",
            "Folio", "Empleado", "Tipo", "FecSolicitud", "fecAprovada", "pagoMensual", "fecSiguientePago", "usuario", "Estado", "pagosTotales", "fecLiquidacion");
            foreach (Prestamo p in db.Prestamos)
            {
                ConsoleColor previousColor = ForegroundColor;
                if((IdToHighlight is not null) && (IdToHighlight.Contains((int)p.Folio)))
                {
                    ForegroundColor = ConsoleColor.Green;
                }
                WriteLine("| {0,-6} | {1,-12} | {2,12} | {3,5} | {4, 5} | {5, 5} | {6, 12} | {7, 10} | {8, 5} | {9, 5} | {10, 5} |",
                p.Folio, p.Empleado, p.Tipo, p.FecSolicitud, p.FecAprovado, p.PagoMensual, p.FecSiguientePago, p.Usuario, p.Estado, p.PagosTotales, p.FecLiquidacion);
                ForegroundColor = previousColor;
            }
        }
    }
}

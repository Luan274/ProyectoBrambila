using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Console;

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
    public string? Aprovado { get; set; }

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

    //Funcion para añadir el cliente
    public static (int affected, long clienteId) addCliente(long userID, string fechaNacimiento, string curp, DateTime horaLogin){
        using (Bank db = new()){
            if(db.Clientes is null) return (0, 0);
            DateOnly fecha;
            if(!DateOnly.TryParse(fechaNacimiento, out fecha) || fecha.Year < 1962) return (0,0);
            Cliente c = new(){
                UserId = userID,
                FechaDeNac = fecha.ToString("dd/MM/yyyy"),
                Curp = curp, 
                Comportamiento = "Bueno", 
                Aprovado = "Pendiente", 
                Saldo = 10000, 
                HoraLogin = horaLogin.ToString(),
                IntentosFallidos = 0
            };

            EntityEntry<Cliente> entity = db.Clientes.Add(c);
            int affected = db.SaveChanges();
            return(affected, c.ClienteId);
        }
    }

    public static void ListCliente(int? []? clienteIdToHighlight = null)
    {
        using(Bank db = new())
        {
            if((db.Usuarios is null) || (!db.Usuarios.Any()))
            {
                WriteLine("There are no clients");
                return;
            }
            WriteLine("| {0,-3} | {1,-16} | {2,18} | {3,15} | {4,8} | {5,8} | {6, 25} | {7,3} | {8,3}",
            "Id", "DoB", "CURP", "Comportamiento", "Aprovado", "Saldo", "HoraLogIn", "Int", "UserID");
            foreach (Cliente c in db.Clientes)
            {
                ConsoleColor previousColor = ForegroundColor;
                if((clienteIdToHighlight is not null) && (clienteIdToHighlight.Contains((int)c.ClienteId)))
                {
                    ForegroundColor = ConsoleColor.Green;
                }
                WriteLine("| {0, -3} | {1,-16} | {2,18} | {3,15} | {4,8} | {5,8} | {6, 25} | {7,3} | {8,3}",
                c.ClienteId, c.FechaDeNac, c.Curp, c.Comportamiento, c.Aprovado, c.Saldo, c.HoraLogin, c.IntentosFallidos, c.UserId);
                ForegroundColor = previousColor;
            }
        }
    }

}

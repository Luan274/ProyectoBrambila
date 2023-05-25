using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Console;

namespace Banco;

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

    private static string CrearUsuario(string nombre, string apellido, DateOnly DoB){
        string usuario = nombre.ToLower() + "." + apellido.ToLower() + DoB.Year;
        return usuario;
    } 

    private static string CrearContra(string nombre, string apellido, DateOnly DoB){
        string contra = apellido.Substring(0, Math.Min(apellido.Length, 3)) + DoB.Year + DoB.Month + DoB.Day;   
        return contra;
    } 

    public static (long affected, long UserId) AddUsuario(string nombre, string apellido, string DoB)
    {
        using (Bank db = new())
        {
            if(db.Usuarios is null) return (0,0);
            if(!(Validaciones.ContieneNumeros(nombre) || Validaciones.ContieneNumeros(apellido))) return (0,0);
            DateOnly fecha;
            if(!DateOnly.TryParse(DoB, out fecha)) return (0,0);
            
            
            Usuario u = new()
            {
                Nombre = nombre,
                Apellido = apellido,
                Usuario1 = CrearUsuario(nombre, apellido, fecha),
                Contrasena = CrearContra(nombre, apellido, fecha)
            };
            

            EntityEntry<Usuario> entity = db.Usuarios.Add(u);
            int affected;
        try{
            affected = db.SaveChanges();
            }catch(Microsoft.EntityFrameworkCore.DbUpdateException e){
                WriteLine($"{e}");
                affected = 0;
            }
            return (affected, u.UserId);
        }
    }

    public static void ListUsuario(int? []? userIdToHighlight = null)
    {
        using(Bank db = new())
        {
            if((db.Usuarios is null) || (!db.Usuarios.Any()))
            {
                WriteLine("There are no users");
                return;
            }
            WriteLine("| {0,-3} | {1,-15} | {2,15} | {3,25} | {4}",
            "Id", "First Name", "Last Name", "Username", "Password");
            foreach (Usuario u in db.Usuarios)
            {
                ConsoleColor previousColor = ForegroundColor;
                if((userIdToHighlight is not null) && (userIdToHighlight.Contains((int)u.UserId)))
                {
                    ForegroundColor = ConsoleColor.Green;
                }
                WriteLine("| {0:000} | {1,-15} | {2,15:$#,##0.00} | {3,25} | {4}",
                u.UserId, u.Nombre, u.Apellido, u.Usuario1, u.Contrasena);
                ForegroundColor = previousColor;
            }
        }
    }

}

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

    public static (long affected, long UserId) AddUsuario(string nombre, string apellido)
    {
        using (Bank db = new())
        {
            if(db.Usuarios is null) return (0,0);
            if(!(Validaciones.ContieneNumeros(nombre) || Validaciones.ContieneNumeros(apellido))) return (0,0);           
            
            Usuario u = new()
            {
                Nombre = nombre,
                Apellido = apellido,
                Usuario1 = "",
                Contrasena = ""
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

    public static (long affected, long UserId) AddUsuario(string nombre, string apellido, string dob)
    {
        using (Bank db = new())
        {
            if(db.Usuarios is null) return (0,0);
            if(!(Validaciones.ContieneNumeros(nombre) || Validaciones.ContieneNumeros(apellido))) return (0,0);           
            DateOnly d;
            if(!DateOnly.TryParse(dob, out d)) return (0,0);
            Usuario u = new()
            {
                Nombre = nombre,
                Apellido = apellido,
                Usuario1 = CrearUsuario(nombre, apellido, d),
                Contrasena = CrearContra(nombre, apellido, d)
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

    public static (long affected, long Id) AprovarUsuario(long id, bool aprovado){
        using (Bank db = new())
        {
            if (db.Clientes is null || db.Usuarios is null) return (0, 0);

            var cliente = db.Clientes.FirstOrDefault(c => c.ClienteId == id);

            if (cliente != null)
            {
                cliente.Aprovado = aprovado ? "Aceptada" : "Rechazada";
                if(aprovado){
                    var usuario = db.Usuarios.FirstOrDefault(u => u.UserId == cliente.UserId);

                    if (usuario != null )
                    {
                        DateOnly d;
                        if(!DateOnly.TryParse(cliente.FechaDeNac, out d)) return (0,0);
                        usuario.Usuario1 = CrearUsuario(usuario.Nombre ?? string.Empty, usuario.Apellido ?? string.Empty, d); // Cambiar el nombre de usuario
                        usuario.Contrasena = CrearContra(usuario.Nombre ?? string.Empty, usuario.Apellido ?? string.Empty, d); // Cambiar la contraseña

                        db.Clientes.Update(cliente);
                        db.Usuarios.Update(usuario);
                        db.SaveChanges();

                        return (1, id);
                    }
                }
                else
                {
                    db.Clientes.Update(cliente);
                    db.SaveChanges();
                    return (1, id);
                }
            }

            return (0, 0);
        }
    }
}

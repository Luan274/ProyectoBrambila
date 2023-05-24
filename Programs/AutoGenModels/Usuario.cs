using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

    private static string CrearUsuario(string nombre, string apellido, DateTime DoB){
        string usuario = nombre.ToLower() + "." + apellido.ToLower() + DoB.Year;
        return usuario;
    } 

    private static string CrearContra(string nombre, string apellido, DateTime DoB){
        string contra = apellido.Substring(0, Math.Min(apellido.Length, 3)) + DoB.Year + DoB.Month + DoB.Day;   
        return contra;
    } 

    public static (long affected, long productId) AddProduct(string nombre, string apellido, string DoB)
    {
        using (Bank db = new())
        {
            if(db.Usuarios is null) return (0,0);
            if(Validaciones.ContieneNumeros(nombre) || Validaciones.ContieneNumeros(apellido)) return (0,0);
            DateTime fecha;
            if(!DateTime.TryParse(DoB, out fecha)) return (0,0);
            
            
            Usuario u = new()
            {
                Nombre = nombre,
                Apellido = apellido,
                Usuario1 = CrearUsuario(nombre, apellido, fecha),
                Contrasena = CrearContra(nombre, apellido, fecha)
            };
            

            EntityEntry<Usuario> entity = db.Usuarios.Add(u);
            int affected =db.SaveChanges();
            Console.WriteLine($"State: {entity.State}, ProductId: {u.UserId}");
            return (affected, u.UserId);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Banco;

public partial class Bank : DbContext
{
    public Bank()
    {
    }

    public Bank(DbContextOptions<Bank> options)
        : base(options)
    {
    }

    public virtual DbSet<Boleto> Boletos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Gerente> Gerentes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Prestamo> Prestamos { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vacacione> Vacaciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Filename=bank.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Boleto>(entity =>
        {
            entity.Property(e => e.Ticket).ValueGeneratedNever();

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.Boletos).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.Property(e => e.ClienteId).ValueGeneratedNever();

            entity.HasOne(d => d.User).WithMany(p => p.Clientes).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasOne(d => d.Usuario).WithMany(p => p.Empleados).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.Property(e => e.EstadoId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Gerente>(entity =>
        {
            entity.Property(e => e.GerenteId).ValueGeneratedNever();

            entity.HasOne(d => d.Cliente).WithMany(p => p.Gerentes).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Empleado).WithMany(p => p.Gerentes).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.Property(e => e.Folio).ValueGeneratedNever();

            entity.HasOne(d => d.PrestamoNavigation).WithMany(p => p.Pagos).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Prestamo>(entity =>
        {
            entity.Property(e => e.Folio).ValueGeneratedNever();

            entity.HasOne(d => d.EmpleadoNavigation).WithMany(p => p.Prestamos).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Prestamos).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.TipoNavigation).WithMany(p => p.Prestamos).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Prestamos).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.Property(e => e.TipoId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.UserId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Vacacione>(entity =>
        {
            entity.Property(e => e.Folio).ValueGeneratedNever();

            entity.HasOne(d => d.Empleado).WithMany(p => p.Vacaciones).OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Gerente).WithMany(p => p.Vacaciones).OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

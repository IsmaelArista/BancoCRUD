using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BancoCRUD.Models;

public partial class BancobdContext : DbContext
{
    public BancobdContext()
    {
    }

    public BancobdContext(DbContextOptions<BancobdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cuentum> Cuenta { get; set; }

    public virtual DbSet<Tarjetum> Tarjeta { get; set; }

    public virtual DbSet<Transaccion> Transaccions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=bancobd;uid=root;password=IPN.Data28", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cuentum>(entity =>
        {
            entity.HasKey(e => e.IdCuenta).HasName("PRIMARY");

            entity.ToTable("cuenta");

            entity.HasIndex(e => e.IdUsuario, "ID_usuario _idx");

            entity.Property(e => e.IdCuenta).HasColumnName("ID_cuenta");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_usuario");
            entity.Property(e => e.NumeroCuenta)
                .HasMaxLength(45)
                .HasColumnName("Numero_cuenta");
            entity.Property(e => e.SaldoCuenta).HasColumnName("Saldo_cuenta");
            entity.Property(e => e.TipoCuenta)
                .HasMaxLength(15)
                .HasColumnName("Tipo_cuenta");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ID_usuario ");
        });

        modelBuilder.Entity<Tarjetum>(entity =>
        {
            entity.HasKey(e => e.IdTarjeta).HasName("PRIMARY");

            entity.ToTable("tarjeta");

            entity.HasIndex(e => e.IdCuenta, "ID_cuenta_idx");

            entity.Property(e => e.IdTarjeta).HasColumnName("ID_tarjeta");
            entity.Property(e => e.FechaExpiracion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_expiracion");
            entity.Property(e => e.IdCuenta).HasColumnName("ID_cuenta");
            entity.Property(e => e.NumeroTarjeta).HasColumnName("Numero_tarjeta");
            entity.Property(e => e.TipoTarjeta)
                .HasMaxLength(15)
                .HasColumnName("Tipo_tarjeta");

            entity.HasOne(d => d.IdCuentaNavigation).WithMany(p => p.Tarjeta)
                .HasForeignKey(d => d.IdCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ID_cuenta_tarjeta");
        });

        modelBuilder.Entity<Transaccion>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PRIMARY");

            entity.ToTable("transaccion");

            entity.HasIndex(e => e.IdCuenta, "ID_cuenta_idx");

            entity.Property(e => e.IdTransaccion).HasColumnName("ID_transaccion");
            entity.Property(e => e.DescripcionTransaccion)
                .HasMaxLength(100)
                .HasColumnName("Descripcion_transaccion");
            entity.Property(e => e.FechaTransaccion)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_transaccion");
            entity.Property(e => e.IdCuenta).HasColumnName("ID_cuenta");
            entity.Property(e => e.MontoTransaccion).HasColumnName("Monto_transaccion");
            entity.Property(e => e.TipoTransaccion)
                .HasMaxLength(45)
                .HasColumnName("Tipo_transaccion");

            entity.HasOne(d => d.IdCuentaNavigation).WithMany(p => p.Transaccions)
                .HasForeignKey(d => d.IdCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ID_cuenta");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("users");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_usuario");
            entity.Property(e => e.ContrasenaUsuario)
                .HasMaxLength(45)
                .HasColumnName("Contrasena_usuario");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(45)
                .HasColumnName("Nombre_usuario");
            entity.Property(e => e.SaldoUsuario).HasColumnName("Saldo_usuario");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(15)
                .HasColumnName("Tipo_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

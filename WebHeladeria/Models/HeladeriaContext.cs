using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebHeladeria.Models;

public partial class HeladeriaContext : DbContext
{
    public HeladeriaContext()
    {
    }

    public HeladeriaContext(DbContextOptions<HeladeriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Gusto> Gustos { get; set; }

    public virtual DbSet<Localidade> Localidades { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<VentasDetalle> VentasDetalles { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LegionNachi\\MSSQLSERVER01; Database=Heladeria; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gusto>(entity =>
        {
            entity.HasKey(e => e.IdGusto).HasName("PK__Gustos__63F35898EF1A9029");

            entity.Property(e => e.IdGusto).HasColumnName("id_gusto");
            entity.Property(e => e.DescripGusto)
                .HasMaxLength(120)
                .HasColumnName("descrip_gusto");
            entity.Property(e => e.NombreGusto)
                .HasMaxLength(60)
                .HasColumnName("nombre_gusto");
        });

        modelBuilder.Entity<Localidade>(entity =>
        {
            entity.HasKey(e => e.IdLocalidad).HasName("PK__Localida__9A5E82AAC33CAB24");

            entity.Property(e => e.IdLocalidad).HasColumnName("id_localidad");
            entity.Property(e => e.NombreLocalidad)
                .HasMaxLength(60)
                .HasColumnName("nombre_localidad");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__4C758013589F068C");

            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");
            entity.Property(e => e.CalleSucursal)
                .HasMaxLength(60)
                .HasColumnName("calle_sucursal");
            entity.Property(e => e.IdLocalidad).HasColumnName("localidad");
            entity.Property(e => e.NroSucursal).HasColumnName("nro_sucursal");

            entity.HasOne(d => d.LocalidadNavigation).WithMany(p => p.Sucursales)
                .HasForeignKey(d => d.IdLocalidad)
                .HasConstraintName("FK__Sucursale__local__6383C8BA");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__4E3E04ADC0ED55DF");

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.ContraUsuario)
                .HasMaxLength(150)
                .HasColumnName("contra_usuario");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(120)
                .HasColumnName("nombre_usuario");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__459533BF41C6759F");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK__Ventas__id_sucur__68487DD7");
        });

        modelBuilder.Entity<VentasDetalle>(entity =>
        {
            entity.HasKey(e => new { e.IdVenta, e.IdGusto }).HasName("PK__Ventas_D__D3AA06367E70A2B5");

            entity.ToTable("Ventas_Detalle");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.IdGusto).HasColumnName("id_gusto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");

            entity.HasOne(d => d.IdGustoNavigation).WithMany(p => p.VentasDetalles)
                .HasForeignKey(d => d.IdGusto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas_De__id_gu__6C190EBB");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.VentasDetalles)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas_De__id_ve__6B24EA82");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

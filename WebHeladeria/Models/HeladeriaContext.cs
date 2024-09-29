using System;
using System.Collections.Generic;
using Dapper;
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

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<VentasDetalle> VentasDetalles { get; set; }

    public async Task<List<Reporte>> ObtenerReporteAsync() //Genera la clase Reporte
    {
        var sql = @"
            SELECT 
                g.id_gusto AS IdGusto,
                g.nombre_gusto AS NombreGusto,
                SUM(vd.cantidad) AS CantidadTotalPedida
            FROM 
                Ventas_Detalle vd
            JOIN 
                Gustos g ON vd.id_gusto = g.id_gusto
            GROUP BY 
                g.id_gusto, g.nombre_gusto
            ORDER BY 
                CantidadTotalPedida DESC";

        //return await this.Set<Reporte>().FromSqlRaw(sql).ToListAsync();
            var reportes = await this.Database
            .GetDbConnection()
            .QueryAsync<Reporte>(sql);  // Usa Dapper

        return reportes.ToList();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LegionNachi\\MSSQLSERVER01; Database=Heladeria; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Gusto>(entity =>
        {
            entity.HasKey(e => e.IdGusto).HasName("PK__Gustos__63F35898C0EB452C");

            entity.Property(e => e.IdGusto).HasColumnName("id_gusto");
            entity.Property(e => e.DescripGusto)
                .HasMaxLength(120)
                .HasColumnName("descrip_gusto");
            entity.Property(e => e.NombreGusto)
                .HasMaxLength(60)
                .HasColumnName("nombre_gusto");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.IdSucursal).HasName("PK__Sucursal__4C75801316C3B782");

            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");
            entity.Property(e => e.CalleSucursal)
                .HasMaxLength(60)
                .HasColumnName("calle_sucursal");
            entity.Property(e => e.Localidad)
                .HasMaxLength(60)
                .HasColumnName("localidad");
            entity.Property(e => e.NroSucursal).HasColumnName("nro_sucursal");
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
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas__459533BF533118BA");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdSucursal).HasColumnName("id_sucursal");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK__Ventas__id_sucur__3D5E1FD2");
        });

        modelBuilder.Entity<VentasDetalle>(entity =>
        {
            entity.HasKey(e => new { e.IdVenta, e.IdGusto }).HasName("PK__Ventas_D__D3AA0636D6C8CB1C");

            entity.ToTable("Ventas_Detalle");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.IdGusto).HasColumnName("id_gusto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");

            entity.HasOne(d => d.IdGustoNavigation).WithMany(p => p.VentasDetalles)
                .HasForeignKey(d => d.IdGusto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas_De__id_gu__412EB0B6");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.VentasDetalles)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ventas_De__id_ve__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

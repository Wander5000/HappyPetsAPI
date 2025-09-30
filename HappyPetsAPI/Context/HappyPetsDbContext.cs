using System;
using System.Collections.Generic;
using HappyPetsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyPetsAPI.Context;

public partial class HappyPetsDbContext : DbContext
{
    public HappyPetsDbContext()
    {
    }

    public HappyPetsDbContext(DbContextOptions<HappyPetsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<DetallesVenta> DetallesVenta { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Imagen> Imagenes { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
        //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HappyPetsDB;Username=postgres;Password=Wander15032007;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("Categorias_pkey");

            entity.Property(e => e.Descripcion).HasColumnType("character varying");
            entity.Property(e => e.NombreCategoria).HasColumnType("character varying");
        });

        modelBuilder.Entity<DetallesVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("DetallesVenta_pkey");

            entity.HasOne(d => d.ProductoNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DetallesVenta_Producto_fkey");

            entity.HasOne(d => d.VentaNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("DetallesVenta_Venta_fkey");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("Estados_pkey");

            entity.Property(e => e.NombreEstado).HasColumnType("character varying");
        });

        modelBuilder.Entity<Imagen>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("Imagenes_pkey");

            entity.Property(e => e.Url).HasColumnType("character varying");

            entity.HasOne(d => d.ProductoNavigation).WithMany(p => p.Imagenes)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Imagenes_Producto_fkey");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("Productos_pkey");

            entity.Property(e => e.NombreProducto).HasColumnType("character varying");

            entity.HasOne(d => d.CategoriaProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Productos_CategoriaProducto_fkey");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("Roles_pkey");

            entity.Property(e => e.NombreRol).HasColumnType("character varying");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("Usuarios_pkey");

            entity.Property(e => e.Correo).HasColumnType("character varying");
            entity.Property(e => e.Direccion).HasColumnType("character varying");
            entity.Property(e => e.NombreUsuario).HasColumnType("character varying");
            entity.Property(e => e.NumeroDocumento).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");
            entity.Property(e => e.TipoDocumento).HasColumnType("character varying");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Usuarios_Rol_fkey");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("Ventas_pkey");

            entity.Property(e => e.MetodoPago).HasColumnType("character varying");
            entity.Property(e => e.Observaciones).HasColumnType("character varying");

            entity.HasOne(d => d.EstadoNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Estado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Ventas_Estado_fkey");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Ventas_Usuario_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

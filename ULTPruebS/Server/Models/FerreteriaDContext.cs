using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ULTPruebS.Server.Models;

public partial class FerreteriaDContext : DbContext
{
    public FerreteriaDContext()
    {
    }

    public FerreteriaDContext(DbContextOptions<FerreteriaDContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }
    public virtual DbSet<Inventario> Inventarios { get; set; }
        
    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Factura1> Factura1s { get; set; }

    public virtual DbSet<Producto1> Producto1s { get; set; }

    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }  // Nueva tabla UserRole

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240C320A2DCD");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreCategoria");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compra__48B99DB790F96C9C");

            entity.ToTable("Compra");

            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.Cliente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaCompra)
                .HasColumnType("date")
                .HasColumnName("fechaCompra");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.TotalCompra)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("totalCompra");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compra__idProvee__48CFD27E");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra).HasName("PK__DetalleC__62C252C1706291BF");

            entity.ToTable("DetalleCompra");

            entity.Property(e => e.IdDetalleCompra).HasColumnName("idDetalleCompra");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.SubtotalCompra)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("subtotalCompra");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleCompra_Compra");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_DetalleCompra_Producto1");
        });


        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__Inventar__1927B20C15596615");

            entity.ToTable("Inventario");

            entity.Property(e => e.IdInventario).HasColumnName("idInventario");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("categoria");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Detalle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detalle");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Inventario_Categoria");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_Inventario_Producto");
        });


        modelBuilder.Entity<DetalleVenta>(static entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__AAA5CEC2F5BAFBFD");

            entity.Property(e => e.Descuento).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("IVA");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TasaIva)
                .HasDefaultValueSql("((15))")
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("TasaIVA");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdProducto1Navigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto1)
                .HasConstraintName("FK_DetalleVenta_Producto1");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DetalleVe__IdVen__09A971A2");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__3CD5687EA64451B0");

            entity.ToTable("Factura");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.DetallesFactura)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detallesFactura");
            entity.Property(e => e.EstadoFactura)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estadoFactura");
            entity.Property(e => e.FechaFactura)
                .HasColumnType("datetime")
                .HasColumnName("fechaFactura");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.MontoPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("montoPago");
            entity.Property(e => e.MontoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montoTotal");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Venta");
        });

        modelBuilder.Entity<Factura1>(entity =>
        {
            entity.HasKey(e => e.IdFactura1).HasName("PK__Factura1__69F30A4E0B661EE4");

            entity.ToTable("Factura1");

            entity.Property(e => e.IdFactura1).HasColumnName("idFactura1");
            entity.Property(e => e.DescuentoTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaEmision)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("IVA");
            entity.Property(e => e.NumeroFactura)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalFactura).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Factura1s)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura1__idVent__2180FB33");
        });

        modelBuilder.Entity<Producto1>(entity =>
        {
            entity.HasKey(e => e.IdProducto1).HasName("PK__Producto__09889210B9C022A2");

            entity.ToTable("Producto1");

            entity.Property(e => e.Caracteristicas)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("caracteristicas");
            entity.Property(e => e.Detalle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detalle");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Ganancia)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ganancia");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.IdUnidad).HasColumnName("idUnidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioCompra)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precioCompra");
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("precioVenta");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.StockMinimo).HasColumnName("stockMinimo");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Producto1s)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto1_Categoria");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Producto1s)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto1_Proveedor");

            entity.HasOne(d => d.IdUnidadNavigation).WithMany(p => p.Producto1s)
                .HasForeignKey(d => d.IdUnidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto1_Unidad");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__A3FA8E6BA1265FF4");

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.DireccionEmpresa)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccionEmpresa");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreProveedor");
            entity.Property(e => e.NumeroRuc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numeroRUC");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Reporte>(static entity =>
        {
            entity.HasKey(e => e.IdReporte).HasName("PK__Reporte__40D65D3CD4B8CA30");

            entity.ToTable("Reporte");

            entity.Property(e => e.IdReporte).HasColumnName("idReporte");
            entity.Property(e => e.Cambio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Cliente)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Descuento).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DetallesReporte)
                .HasColumnType("text")
                .HasColumnName("detallesReporte");
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("IVA");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MontoPagado).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Periodo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("periodo");
            entity.Property(e => e.Subtotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TipoReporte)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoReporte");
            entity.Property(e => e.TipoTransaccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("FK_Reporte_Compra");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK_Reporte_Venta");
        });

        modelBuilder.Entity<UnidadMedida>(entity =>
        {
            entity.HasKey(e => e.IdUnidad).HasName("PK__UnidadMe__34C1E8D7910C944C");

            entity.Property(e => e.IdUnidad).HasColumnName("idUnidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreUnidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreUnidad");
            entity.Property(e => e.Simbolo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("simbolo");
            entity.Property(e => e.TipoUnidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoUnidad");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC07D1102700");

            entity.Property(e => e.ContraseñaHash).HasMaxLength(255);
            entity.Property(e => e.NombreUsuario).HasMaxLength(100);
        });


        base.OnModelCreating(modelBuilder); // Asegúrate de llamar al método base

        // Configuración de la entidad UserRole
        modelBuilder.Entity<UserRole>()
            .HasKey(ur => ur.idUserRole); // Establecer la clave primaria

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Usuario) // Relación de UserRole con Usuario
            .WithMany(u => u.UserRoles) // Relación inversa
            .HasForeignKey(ur => ur.idUsuario); // Clave foránea


        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta1__BC1240BD58321FB8");

            entity.Property(e => e.Cambio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Cliente)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DescuentoTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fechaVenta");
            entity.Property(e => e.MontoPagado).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using Microsoft.EntityFrameworkCore;
using diplo.Models;

namespace diplo.Data;

public class AppDbContext : DbContext
{
  /*
* CON SCHEMA:
* private const string? Schema = "pos";
*
* SIN SCHEMA:
* private const string? Schema = null;
*/

  private const string? Schema = "prueva";
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  public DbSet<Categoria> Categorias => Set<Categoria>();
  public DbSet<Producto> Productos => Set<Producto>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // =========================
    // Tabla categorias
    // =========================
    modelBuilder.Entity<Categoria>(entity =>
    {
      entity.ToTable("categorias", Schema);

      entity.HasKey(e => e.CategoriaId);

      entity.Property(e => e.CategoriaId)
              .HasColumnName("CategoriaId");

      entity.Property(e => e.Nombre)
              .HasColumnName("Nombre")
              .HasMaxLength(50)
              .IsRequired();

      entity.Property(e => e.Descripcion)
              .HasColumnName("Descripcion")
              .HasColumnType("varchar(max)");
    });

    // =========================
    // Tabla productos
    // =========================
    modelBuilder.Entity<Producto>(entity =>
    {
      entity.ToTable("productos", Schema);

      entity.HasKey(e => e.ProductoId);

      entity.Property(e => e.ProductoId)
              .HasColumnName("ProductoId");

      entity.Property(e => e.Nombre)
              .HasColumnName("Nombre")
              .HasMaxLength(30)
              .IsRequired();

      entity.Property(e => e.Descripcion)
              .HasColumnName("Descripcion")
              .HasColumnType("varchar(max)");

      entity.Property(e => e.ImagenUrl)
              .HasColumnName("ImagenUrl")
              .HasMaxLength(255);

      entity.Property(e => e.Precio)
              .HasColumnName("Precio")
              .HasColumnType("decimal(10,2)");

      entity.Property(e => e.Stock)
              .HasColumnName("Stock");

      entity.Property(e => e.CategoriaId)
              .HasColumnName("CategoriaId");

      entity.HasOne(e => e.Categoria)
              .WithMany(e => e.Productos)
              .HasForeignKey(e => e.CategoriaId)
              .HasConstraintName("FK_productos_categorias")
              .OnDelete(DeleteBehavior.NoAction);
    });
  }
}
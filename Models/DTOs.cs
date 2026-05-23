namespace diplo.Models;

// DTO para Categoría (sin incluir productos)
public class CategoriaDto
{
    public int CategoriaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
}

// DTO para Categoría con productos
public class CategoriaConProductosDto
{
    public int CategoriaId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public ICollection<ProductoDto> Productos { get; set; } = new List<ProductoDto>();
}

// DTO para Producto (sin incluir categoría completa)
public class ProductoDto
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string? ImagenUrl { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public int CategoriaId { get; set; }
}

// DTO para Producto con categoría
public class ProductoConCategoriaDto
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public string? ImagenUrl { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public CategoriaDto? Categoria { get; set; }
}

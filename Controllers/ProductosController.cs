using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using diplo.Data;
using diplo.Models;

namespace diplo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/productos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductoConCategoriaDto>>> GetProductos()
    {
        return await _context.Productos
            .Include(p => p.Categoria)
            .AsNoTracking()
            .Select(p => new ProductoConCategoriaDto
            {
                ProductoId = p.ProductoId,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                ImagenUrl = p.ImagenUrl,
                Precio = p.Precio,
                Stock = p.Stock,
                Categoria = new CategoriaDto
                {
                    CategoriaId = p.Categoria!.CategoriaId,
                    Nombre = p.Categoria.Nombre,
                    Descripcion = p.Categoria.Descripcion
                }
            })
            .ToListAsync();
    }

    // GET: api/productos/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoConCategoriaDto>> GetProducto(int id)
    {
        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProductoId == id);

        if (producto == null)
            return NotFound(new { mensaje = "Producto no encontrado" });

        var dto = new ProductoConCategoriaDto
        {
            ProductoId = producto.ProductoId,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            ImagenUrl = producto.ImagenUrl,
            Precio = producto.Precio,
            Stock = producto.Stock,
            Categoria = new CategoriaDto
            {
                CategoriaId = producto.Categoria!.CategoriaId,
                Nombre = producto.Categoria.Nombre,
                Descripcion = producto.Categoria.Descripcion
            }
        };

        return dto;
    }

    // POST: api/productos
    [HttpPost]
    public async Task<ActionResult<ProductoConCategoriaDto>> CrearProducto(ProductoDto productoDto)
    {
        var categoriaExiste = await _context.Categorias
            .AnyAsync(c => c.CategoriaId == productoDto.CategoriaId);

        if (!categoriaExiste)
        {
            return BadRequest(new
            {
                mensaje = "La categoría enviada no existe"
            });
        }

        var producto = new Producto
        {
            Nombre = productoDto.Nombre,
            Descripcion = productoDto.Descripcion,
            ImagenUrl = productoDto.ImagenUrl,
            Precio = productoDto.Precio,
            Stock = productoDto.Stock,
            CategoriaId = productoDto.CategoriaId
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        var categoria = await _context.Categorias.FindAsync(productoDto.CategoriaId);
        var resultDto = new ProductoConCategoriaDto
        {
            ProductoId = producto.ProductoId,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            ImagenUrl = producto.ImagenUrl,
            Precio = producto.Precio,
            Stock = producto.Stock,
            Categoria = new CategoriaDto
            {
                CategoriaId = categoria!.CategoriaId,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            }
        };

        return CreatedAtAction(
            nameof(GetProducto),
            new { id = producto.ProductoId },
            resultDto
        );
    }

    // PUT: api/productos/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarProducto(int id, ProductoDto productoDto)
    {
        var productoExiste = await _context.Productos
            .AnyAsync(p => p.ProductoId == id);

        if (!productoExiste)
            return NotFound(new { mensaje = "Producto no encontrado" });

        var categoriaExiste = await _context.Categorias
            .AnyAsync(c => c.CategoriaId == productoDto.CategoriaId);

        if (!categoriaExiste)
        {
            return BadRequest(new
            {
                mensaje = "La categoría enviada no existe"
            });
        }

        var producto = await _context.Productos.FindAsync(id);
        if (producto != null)
        {
            producto.Nombre = productoDto.Nombre;
            producto.Descripcion = productoDto.Descripcion;
            producto.ImagenUrl = productoDto.ImagenUrl;
            producto.Precio = productoDto.Precio;
            producto.Stock = productoDto.Stock;
            producto.CategoriaId = productoDto.CategoriaId;

            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/productos/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarProducto(int id)
    {
        var producto = await _context.Productos.FindAsync(id);

        if (producto == null)
            return NotFound(new { mensaje = "Producto no encontrado" });

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using diplo.Data;
using diplo.Models;

namespace De.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;

    public CategoriasController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/categorias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> GetCategorias()
    {
        return await _context.Categorias
            .AsNoTracking()
            .Select(c => new CategoriaDto
            {
                CategoriaId = c.CategoriaId,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            })
            .ToListAsync();
    }

    // GET: api/categorias/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaConProductosDto>> GetCategoria(int id)
    {
        var categoria = await _context.Categorias
            .AsNoTracking()
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.CategoriaId == id);

        if (categoria == null)
            return NotFound(new { mensaje = "Categoría no encontrada" });

        var dto = new CategoriaConProductosDto
        {
            CategoriaId = categoria.CategoriaId,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            Productos = categoria.Productos
                .Select(p => new ProductoDto
                {
                    ProductoId = p.ProductoId,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    ImagenUrl = p.ImagenUrl,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    CategoriaId = p.CategoriaId
                })
                .ToList()
        };

        return dto;
    }

    // POST: api/categorias
    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> CrearCategoria(CategoriaDto categoriaDto)
    {
        var categoria = new Categoria
        {
            Nombre = categoriaDto.Nombre,
            Descripcion = categoriaDto.Descripcion
        };

        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();

        var resultDto = new CategoriaDto
        {
            CategoriaId = categoria.CategoriaId,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion
        };

        return CreatedAtAction(
            nameof(GetCategoria),
            new { id = categoria.CategoriaId },
            resultDto
        );
    }

    // PUT: api/categorias/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarCategoria(int id, CategoriaDto categoriaDto)
    {
        var existe = await _context.Categorias.AnyAsync(c => c.CategoriaId == id);

        if (!existe)
            return NotFound(new { mensaje = "Categoría no encontrada" });

        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria != null)
        {
            categoria.Nombre = categoriaDto.Nombre;
            categoria.Descripcion = categoriaDto.Descripcion;
            
            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();
        }

        return NoContent();
    }

    // DELETE: api/categorias/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarCategoria(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);

        if (categoria == null)
            return NotFound(new { mensaje = "Categoría no encontrada" });

        var tieneProductos = await _context.Productos
            .AnyAsync(p => p.CategoriaId == id);

        if (tieneProductos)
        {
            return BadRequest(new
            {
                mensaje = "No se puede eliminar la categoría porque tiene productos relacionados"
            });
        }

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
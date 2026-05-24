using Microsoft.VisualStudio.TestTools.UnitTesting;
using diplo.Models;

namespace MiApi.Tests;

[TestClass]
public class ModelosTests
{
    [TestMethod]
    public void Producto_Creacion_AsignaPropiedadesCorrectamente()
    {
        // Arrange
        var producto = new Producto
        {
            ProductoId = 1,
            Nombre = "Teclado Mecánico",
            Descripcion = "Teclado RGB con switches red",
            Precio = 450.00m,
            Stock = 15,
            CategoriaId = 3
        };

        // Assert
        Assert.AreEqual(1, producto.ProductoId);
        Assert.AreEqual("Teclado Mecánico", producto.Nombre);
        Assert.AreEqual("Teclado RGB con switches red", producto.Descripcion);
        Assert.AreEqual(450.00m, producto.Precio);
        Assert.AreEqual(15, producto.Stock);
        Assert.AreEqual(3, producto.CategoriaId);
    }

    [TestMethod]
    public void Categoria_Creacion_InicializaListaDeProductos()
    {
        // Arrange
        var categoria = new Categoria
        {
            CategoriaId = 2,
            Nombre = "Periféricos",
            Descripcion = "Teclados, mouse y audífonos"
        };

        // Assert
        Assert.AreEqual(2, categoria.CategoriaId);
        Assert.AreEqual("Periféricos", categoria.Nombre);
        Assert.IsNotNull(categoria.Productos);
        Assert.AreEqual(0, categoria.Productos.Count);
    }
}

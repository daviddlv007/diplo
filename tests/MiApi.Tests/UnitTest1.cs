using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiApi.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1_Suma_Correcta()
    {
        int resultado = 2 + 3;
        Assert.AreEqual(5, resultado);
    }

    [TestMethod]
    public void TestMethod2_Verdadero()
    {
        Assert.IsTrue(true);
    }
}

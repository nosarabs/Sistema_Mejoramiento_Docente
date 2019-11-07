using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador.Controllers;
using System.Web.Mvc;


namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    
    public class ResultadosFormularioControllerTest
    {
        //COD-4: Visualizar el promedio para las respuestas de las preguntas de tipo escalar
        [TestMethod]
        public void TestPromedioFormulario() //Comprueba que el promedio retornado no es nulo
        {
            ResultadosFormularioController controller = new ResultadosFormularioController();
            String resultado = controller.getPromedio("00000001", "CI0128", 1, 2, 2019, DateTime.Parse("2019-10-31"), DateTime.Parse("2019-11-02"), "00000001", "00000001") as String; //Metodo en el controlador y sus respectivos parametros
            Assert.IsNotNull(resultado); //Comprobacion de que no es null 
        }

        // COD-22: Visualizar la desviación estándar para las respuestas de las preguntas de escala numérica
        [TestMethod]
        public void TestDesviacionFormulario()
        {
            // Arrange
            ResultadosFormularioController controlador = new ResultadosFormularioController();
            // Act
            String resultado = controlador.obtenerDesviacionEstandar("00000001", "CI0128", 1, 2, 2019, DateTime.Parse("2019-10-31"), DateTime.Parse("2019-11-02"), "00000001", "00000001") as String; //Metodo en el controlador y sus respectivos parametros
            // Assert
            Assert.IsNotNull(resultado); //Comprobacion de que no es null 
        }

        [TestMethod]
        public void TestJustificacionNotNULL()
        {
            // Arrange
            ResultadosFormularioController controller = new ResultadosFormularioController();
            // Act
            String result = controller.getJustificacionPregunta("00000001", "CI0128", 1, 2, 2019, DateTime.Parse("2019-10-31"), DateTime.Parse("2019-11-02"), "00000001", "00000001");
            // Assert
            Assert.IsNotNull(result);
        }
    }
}

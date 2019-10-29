using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador.Controllers;
using System.Web.Mvc;


namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    //COD-4: Visualizar el promedio para las respuestas de las preguntas de tipo escalar
    public class ResultadosFormularioControllerTest
    {
        [TestMethod]
        public void TestFormularioNotNull() //Comprueba que la vista de formularios no es nula
        {
            ResultadosFormularioController controller = new ResultadosFormularioController();
            String resultado = controller.getPromedio("00000001", "CI0128", 1, 2, 2019, "00000001") as String; //Metodo en el controlador y sus respectivos parametros
            Assert.IsNotNull(resultado); //Comprobacion de que no es null 
        }
    }
}

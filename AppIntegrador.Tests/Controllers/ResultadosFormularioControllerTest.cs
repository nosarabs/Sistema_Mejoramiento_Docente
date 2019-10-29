using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador;
using AppIntegrador.Controllers;
using AppIntegrador.Models;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class ResultadosFormularioControllerTest
    {
        /*
        HU: COD-1
        Tarea técnica: obtener el tipo de la pregunta a partir de un código de formulario existente
        */
        [TestMethod]
        public void TestTipoNotNull()
        {
            //Arrange
            ResultadosFormularioController controller = new ResultadosFormularioController();
            String codFormulario = "00000001";

            //Act
            String result = controller.GetTipoPregunta(codFormulario);

            //Assert
            Assert.IsNotNull(result);
        }

        /*
        HU: COD-1
        Tarea técnica: obtener la lista de preguntas asociadas a un código de formulario existente
        */
        [TestMethod]
        public void TestObtenerPreguntaNotNull()
        {
            //Arrange
            ResultadosFormularioController controller = new ResultadosFormularioController();
            String codigoFormulario = "00000001";

            //Act
            List<Preguntas> result = controller.ObtenerPreguntas(codigoFormulario);

            //Assert
            Assert.IsNotNull(result);
        }

    }
}

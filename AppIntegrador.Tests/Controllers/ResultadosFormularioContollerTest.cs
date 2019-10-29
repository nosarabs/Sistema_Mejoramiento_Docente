﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador;
using AppIntegrador.Controllers;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class ResultadosFormularioContollerTest
    {
        /*
         * Se utiliza este test para corroborar que el método devuelve un valor si la justificación existe.
         */

        [TestMethod]
        public void TestJustificacionNotNULL()
        {

            // Arrange
            ResultadosFormularioController controller = new ResultadosFormularioController();
            // Act
            String result = controller.getJustificacionPregunta("00000001","CI0128",1,2,2019,"00000001");
            // Assert
            Assert.IsNotNull(result);

        }

        /*Este test tiene como propósito probar lo que retorna el método para obtener las justificaciones de una pregunta cuando
        se envían parámetros inválidos.*/
        [TestMethod]
        public void TestJustificacionParametrosInvalidos()
        {

            // Arrange
            ResultadosFormularioController controller = new ResultadosFormularioController();
            // Act
            String result = controller.getJustificacionPregunta("0A500W01", "CI0128", 1, 2, 2019, "00000001");
            // Assert
            Assert.AreEqual(result, "[]");

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AppIntegrador;
using AppIntegrador.Controllers;

namespace AppIntegrador.Tests.Controllers
{
    // Clase de prueba asociada a la historia
    // BKS2: Yo como administrativo quiero crer una sección de preguntas para estrucutrar un formulario
    [TestClass]
    public class SeccionControllerTest
    {
        [TestMethod]
        // Prueba de que la vista no sea nula
        // Todo: asociarlo al modelo del entity framework.
        public void TestIndexNotNull()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Act
            ViewResult result = seccionController.Index("","","") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Prueba de que la vista no sea nula
        public void TestCreateNotNull()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Act
            ViewResult result = seccionController.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // Prueba de que la vista no sea nula
        public void TestEditNotNull()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Act
            ViewResult result = seccionController.Edit("00000001") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}

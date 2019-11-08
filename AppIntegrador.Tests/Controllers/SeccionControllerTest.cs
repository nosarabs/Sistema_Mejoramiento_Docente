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
        //RIP CF5
        // Prueba de que la vista no sea nula
        public void TestCreateIndexFiltroCodigoNotNull()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Act
            ViewResult result = seccionController.Index("0000001","","") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //RIP CF5
        // Prueba de que la vista no sea nula
        public void TestCreateIndexFiltroNotNull()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Act
            ViewResult result = seccionController.Index(null,null,null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //RIP CF5
        // Prueba de que la vista no sea nula
        public void TestCreateIndexTipoU()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Act
            ViewResult result = seccionController.Index("", "", "U") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        //RIP CF5
        // Prueba de que la vista no sea nula
        public void TestCreateIndexTipoM()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Act
            ViewResult result = seccionController.Index("", "", "M") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        //RIP CF5
        // Prueba de que la vista no sea nula
        public void TestCreateIndexTipoL()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Act
            ViewResult result = seccionController.Index("", "", "L") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //RIP CF5
        // Prueba de que la vista no sea nula
        public void TestCreateIndexTipoS()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Act
            ViewResult result = seccionController.Index("", "", "S") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        //RIP CF5
        // Prueba de que la vista no sea nula
        public void TestCreateIndexTipoE()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Act
            ViewResult result = seccionController.Index("", "", "E") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        //RIP CF5
        // Prueba de que la vista no sea nula
        public void TestCreateIndexFiltroEnunciadoNotNull()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Act
            ViewResult result = seccionController.Index("", "Info", "") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void test

    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AppIntegrador;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using Moq;
using System.Web.Helpers;

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
        public void TestIndexTipos()
        {
            // Arrange
            SeccionController seccionController = new SeccionController();

            // Se prueban las de si/no/nr
            ViewResult result = seccionController.Index("", "", "S") as ViewResult;
            Assert.IsNotNull(result);

            // Se prueban las escalares
            result = seccionController.Index("", "", "E") as ViewResult;
            Assert.IsNotNull(result);

            // Se prueban las de respuesta libre
            result = seccionController.Index("", "", "L") as ViewResult;
            Assert.IsNotNull(result);

            // Se prueban las de seleccion multiple
            result = seccionController.Index("", "", "M") as ViewResult;
            Assert.IsNotNull(result);

            // Se prueba las de seleccion unica
            result = seccionController.Index("", "", "U") as ViewResult;
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
        public void TestCreateFilterNullNotNull()
        {
            SeccionController seccionController = new SeccionController();

            // Null
            ViewResult result = seccionController.Create(null, null, null, null) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        // RIP CF5
        public void TestCreateFilterCodigoNotNull()
        {
            SeccionController seccionController = new SeccionController();

            ViewResult result = seccionController.Create("00000001", "", "", "") as ViewResult;
            Assert.IsNotNull(result);

        }

        [TestMethod]
        // RIP CF5
        public void TestCreateFilterEnunciadoNotNull()
        {
            SeccionController seccionController = new SeccionController();

            ViewResult result = seccionController.Create(null, "Info", null, null) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateFilterTiposNotNull()
        {
            SeccionController seccionController = new SeccionController();

            ViewResult result = seccionController.Create("", "", "U", "") as ViewResult;
            Assert.IsNotNull(result);

            result = seccionController.Create("", "", "M", "") as ViewResult;
            Assert.IsNotNull(result);

            result = seccionController.Create("", "", "L", "") as ViewResult;
            Assert.IsNotNull(result);

            result = seccionController.Create("", "", "S", "") as ViewResult;
            Assert.IsNotNull(result);

            result = seccionController.Create("", "", "E", "") as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAgregarPreguntas()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            SeccionController seccionController = new SeccionController(mockDb.Object);

            string codSeccion = "00000001";
            List<string> codPreguntas = new List<string> { "00000001", "00000002" };

            JsonResult result = seccionController.AgregarPreguntas(codSeccion, codPreguntas) as JsonResult;

            Assert.AreEqual("{ insertadoExitoso = True }", result.Data.ToString());
        }
    }
}

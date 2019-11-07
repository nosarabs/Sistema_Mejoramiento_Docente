using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador.Controllers;
using System.Web.Mvc;
using AppIntegrador.Models;

namespace AppIntegrador.Tests.Controllers
{
    /// <summary>
    /// Summary description for PreguntasTest
    /// </summary>
    [TestClass]
    public class PreguntasTest
    {
        public PreguntasTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        // Historia RIP-CBX
        [TestMethod]
        public void PreguntasGeneralNoNulo()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void PreguntaSiNoNulo()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.PreguntaSiNo() as ViewResult;

            Assert.IsNotNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void PreguntaEscalarNoNulo()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.PreguntaEscalar() as ViewResult;

            Assert.IsNotNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void RespuestaLibreNulo()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.RespuestaLibre() as ViewResult;

            Assert.IsNotNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void SeleccionUnicaNoNula()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.PreguntaConOpciones() as ViewResult;

            Assert.IsNotNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void OpcionesDeSeleccionNoNula()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.OpcionesDeSeleccion(1, 'U') as ViewResult;

            Assert.IsNotNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void OpcionesDeSeleccionNula()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.OpcionesDeSeleccion(-1, 'U') as ViewResult;

            Assert.IsNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void EstilosNoNula()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.Estilos() as ViewResult;

            Assert.IsNotNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void GuardarRespuestaLibreNula()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.GuardarRespuestaLibre(null) as ViewResult;

            Assert.IsNotNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void GuardarPreguntaSiNoNula()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.GuardarPreguntaSiNo(null) as ViewResult;

            Assert.IsNotNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void CreatePostNoNula()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.Create(null, null) as ViewResult;

            Assert.IsNotNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void CreatePostNoNulaConPregunta()
        {
            PreguntasController preguntas = new PreguntasController();
            preguntas.ModelState.Clear();

            Pregunta pregunta = new Pregunta
            {
                Codigo = "",
                Enunciado = ""
            };

            ViewResult result = preguntas.Create(pregunta, null) as ViewResult;

            Assert.IsTrue(preguntas.ViewData.ModelState.Count == 1, "Datos incompletos");
        }

        // Historia RIP-CBX
        [TestMethod]
        public void CreatePostNoNulaConPreguntaTipo()
        {
            PreguntasController preguntas = new PreguntasController();
            preguntas.ModelState.Clear();

            Pregunta pregunta = new Pregunta
            {
                Codigo = "chetos21",
                Enunciado = "adsadsaa",
            };

            ViewResult result = preguntas.Create(pregunta, null) as ViewResult;

            Assert.IsTrue(preguntas.ViewData.ModelState.Count == 1, "Una pregunta de selección única necesita al menos una opción");
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarCrear()
        {
            var controller = new PreguntasController();
            var result = controller.Create() as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarOpciones()
        {
            var controller = new PreguntasController();
            var result = controller.OpcionesDeSeleccion(7, 'U') as ViewResult;

            Assert.AreEqual("OpcionesDeSeleccion", result.ViewName);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarPreguntaConOpciones()
        {
            var controller = new PreguntasController();
            var result = controller.PreguntaConOpciones() as ViewResult;

            Assert.AreEqual("PreguntaConOpciones", result.ViewName);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarPreguntaSiNo()
        {
            var controller = new PreguntasController();
            var result = controller.PreguntaSiNo() as ViewResult;

            Assert.AreEqual("PreguntaSiNo", result.ViewName);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarPreguntaEscalar()
        {
            var controller = new PreguntasController();
            var result = controller.PreguntaEscalar() as ViewResult;

            Assert.AreEqual("PreguntaEscalar", result.ViewName);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarRespuestaLibre()
        {
            var controller = new PreguntasController();
            var result = controller.RespuestaLibre() as ViewResult;

            Assert.AreEqual("RespuestaLibre", result.ViewName);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarViewbagGeneral()
        {
            // Arrange
            var controller = new PreguntasController();
            // Act
            ViewResult result = controller.Create() as ViewResult;
            // Assert
            Assert.AreEqual("Crear pregunta", result.ViewBag.Message);
        }


        // Historia RIP-CBX
        [TestMethod]
        public void ProbarEstilos()
        {
            var controller = new PreguntasController();
            var result = controller.Estilos() as ViewResult;

            Assert.AreEqual("Estilos", result.ViewName);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarGuardarRespuestaLibre()
        {
            var controller = new PreguntasController();
            var result = controller.GuardarRespuestaLibre(null) as ViewResult;

            Assert.AreEqual("GuardarRespuestaLibre", result.ViewName);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarGuardarPreguntaSiNo()
        {
            var controller = new PreguntasController();
            var result = controller.GuardarPreguntaSiNo(null) as ViewResult;

            Assert.AreEqual("GuardarPreguntaSiNo", result.ViewName);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarPostNoNulaConPregunta()
        {
            PreguntasController preguntas = new PreguntasController();
            preguntas.ModelState.Clear();

            Pregunta pregunta = new Pregunta
            {
                Codigo = "",
                Enunciado = ""
            };

            ViewResult result = preguntas.Create(pregunta, null) as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

    }
}

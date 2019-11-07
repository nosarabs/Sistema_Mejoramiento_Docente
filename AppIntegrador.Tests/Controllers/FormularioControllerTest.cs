using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using Moq;

namespace AppIntegrador.Tests.Controllers
{
    /// <summary>
    /// Summary description for FormularioControllerTest
    /// </summary>
    [TestClass]
    public class FormularioControllerTest
    {
        public FormularioControllerTest()
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

        [TestMethod]
        public void TestCreateNotNull()
        {
            FormulariosController formulario = new FormulariosController();
            ViewResult result = formulario.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateView()
        {
            FormulariosController formulario = new FormulariosController();
            ViewResult result = formulario.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void TestIndexNotNull()
        {
            FormulariosController formulario = new FormulariosController();
            ViewResult result = formulario.Index(null, null, null) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexView()
        {
            FormulariosController formulario = new FormulariosController();
            ViewResult result = formulario.Index(null, null, null) as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        /*[TestMethod]
        public void TextIndexNotNullAndView()
        {
            FormulariosController controller = new FormulariosController();
            ViewResult result = controller.LlenarFormulario() as ViewResult;
            Assert.IsNotNull(result, "Null");
            Assert.AreEqual("Index", result.ViewName, "ViewName");
        }*/


        // RIP-EDF7
        // Verificación de que el programa no se caiga si el formulario no tiene ninguna sección asociada.
        [TestMethod]
        public void TestLlenarFormulariosSinSeccionesDataMock()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            string codFormulario = "CI0128G2";
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formularios de prueba para CI0128"
            };
            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            // Act
            var result = controller.LlenarFormulario(codFormulario);

            // Assert
            Assert.IsNotNull(result);
        }

        // RIP-ELF
        [TestMethod]
        public void TestLlenarFormulariosConPreguntasDeOpcionConOpcionesNulas()
        {
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPNUL";
            string codSeccion = "SECCPNUL";
            string codPregunta = "PREGNUL";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas con opciones nulas"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            // Se crea la sección de prueba
            Seccion seccion = new Seccion
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba con preguntas con opciones nulas"
            };

            mockDb.Setup(m => m.Seccion.Find(codSeccion)).Returns(seccion);

            // Se crea una pregunta con opciones de prueba. Pero no se le agregarán opciones
            Pregunta pregunta = new Pregunta
            {
                Codigo = codPregunta,
                Enunciado = "¿Es esta su pregunta sin opciones favorita?",
                Tipo = "U"
            };

            mockDb.Setup(m => m.Pregunta.Find(codPregunta)).Returns(pregunta);

            // Se agrega la pregunta sin opciones a la sección de prueba
            Seccion_tiene_pregunta seccion_tiene_pregunta = new Seccion_tiene_pregunta
            {
                SCodigo = codSeccion,
                PCodigo = codPregunta
            };

            // Se agrega la sección al formulario
            Formulario_tiene_seccion formulario_Tiene_Seccion = new Formulario_tiene_seccion
            {
                FCodigo = codFormulario,
                SCodigo = codSeccion
            };

            mockDb.Setup(m => m.Formulario_tiene_seccion.Find(codFormulario, codSeccion)).Returns(formulario_Tiene_Seccion);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            var result = controller.LlenarFormulario(codFormulario);

            Assert.IsNotNull(result);
        }

        // RIP-ELFSN
        // Verificación de que el programa no se caiga si se le pasan parámetros nulos.
        [TestMethod]
        public void TestGuardarRespuestasNullParameters()
        {
            FormulariosController controller = new FormulariosController();
            ActionResult result = controller.GuardarRespuestas(null, null);
            Assert.IsNotNull(result);
        }

        // RIP-ELFSN
        // Verificación de que el programa no se caiga si el formulario tiene secciones, pero no hay ninguna pregunta.
        [TestMethod]
        public void TestLlenarFormulariosSinPreguntasDataMock()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            string codFormulario = "CI0128G2";
            string codSeccion = "12345678";
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formularios de prueba para CI0128"
            };
            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            Seccion seccion = new Seccion
            {
                Codigo = codSeccion,
                Nombre = "Nombre de sección"
            };
            mockDb.Setup(m => m.Seccion.Find(codSeccion)).Returns(seccion);

            Formulario_tiene_seccion formulario_Tiene_Seccion = new Formulario_tiene_seccion
            {
                FCodigo = codFormulario,
                SCodigo = codSeccion
            };
            mockDb.Setup(m => m.Formulario_tiene_seccion.Find(codFormulario, codSeccion)).Returns(formulario_Tiene_Seccion);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            var result = controller.LlenarFormulario(codFormulario);

            Assert.IsNotNull(result);
        }
    }
}

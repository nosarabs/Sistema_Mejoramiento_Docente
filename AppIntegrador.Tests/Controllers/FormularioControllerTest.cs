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
using System.Security.Principal;
using System.Web;
using System.Web.Routing;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

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

        [TestMethod]
        public void TestDetailsNull()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            // Se prueba que el método no se caiga con parámetros nulos
            var result = controller.Details(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDetails()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            string codFormulario = "CI0128G2";
            string nombreFormulario = "Formulario de prueba";
            string codSeccion = "Secci01";

            // Se crea un formulario para el mock de la base de datos
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = nombreFormulario
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            Seccion seccion = new Seccion()
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba"
            };

            mockDb.Setup(m => m.Seccion.Find(codSeccion)).Returns(seccion);

            // Agregar la sección al formulario
            Formulario_tiene_seccion fts = new Formulario_tiene_seccion()
            {
                FCodigo = codFormulario,
                SCodigo = codSeccion
            };

            mockDb.Setup(m => m.Formulario_tiene_seccion.Find(codSeccion)).Returns(fts);

            var mock = new Mock<DbSet<Seccion>>();

            // Se prueba que el método no se caiga con un código de formulario válido
            var result = controller.Details(codSeccion);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDeleteNull()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            // Se prueba que el método no se caiga con parámetros nulos
            var result = controller.Delete(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDelete()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            string codFormulario = "CI0128G2";
            string nombreFormulario = "Formulario de prueba";
            string codSeccion = "Secci01";

            // Se crea un formulario para el mock de la base de datos
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = nombreFormulario
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            Seccion seccion = new Seccion()
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba"
            };

            mockDb.Setup(m => m.Seccion.Find(codSeccion)).Returns(seccion);

            // Agregar la sección al formulario
            Formulario_tiene_seccion fts = new Formulario_tiene_seccion()
            {
                FCodigo = codFormulario,
                SCodigo = codSeccion
            };

            mockDb.Setup(m => m.Formulario_tiene_seccion.Find(codSeccion)).Returns(fts);

            var mock = new Mock<DbSet<Seccion>>();

            // Se prueba que el método no se caiga con un código de formulario válido
            var result = controller.Delete(codSeccion);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestDeleteConfirmed()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            string codFormulario = "CI0128G2";
            string nombreFormulario = "Formulario de prueba";
            string codSeccion = "Secci01";

            // Se crea un formulario para el mock de la base de datos
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = nombreFormulario
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            Seccion seccion = new Seccion()
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba"
            };

            mockDb.Setup(m => m.Seccion.Find(codSeccion)).Returns(seccion);

            // Agregar la sección al formulario
            Formulario_tiene_seccion fts = new Formulario_tiene_seccion()
            {
                FCodigo = codFormulario,
                SCodigo = codSeccion
            };

            mockDb.Setup(m => m.Formulario_tiene_seccion.Find(codSeccion)).Returns(fts);

            var mock = new Mock<DbSet<Seccion>>();

            // Se prueba que el método no se caiga con un código de formulario válido
            var result = controller.DeleteConfirmed(codSeccion);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestEditNull()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            // Se prueba que el método no se caiga con parámetros nulos
            ActionResult result = controller.Edit("");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestEdit()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            string codFormulario = "CI0128G2";
            string nombreFormulario = "Formulario de prueba";
            string codSeccion = "Secci01";

            // Se crea un formulario para el mock de la base de datos
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = nombreFormulario
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            Seccion seccion = new Seccion()
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba"
            };

            mockDb.Setup(m => m.Seccion.Find(codSeccion)).Returns(seccion);

            // Agregar la sección al formulario
            Formulario_tiene_seccion fts = new Formulario_tiene_seccion()
            {
                FCodigo = codFormulario,
                SCodigo = codSeccion
            };

            mockDb.Setup(m => m.Formulario_tiene_seccion.Find(codSeccion)).Returns(fts);

            var mock = new Mock<DbSet<Seccion>>();

            // Se prueba que el método no se caiga con un código de formulario válido
            var result = controller.Edit(codSeccion);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestEditWithBind()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            string codFormulario = "CI0128G2";
            string nombreFormulario = "Formulario de prueba";

            // Se crea un formulario para el mock de la base de datos
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = nombreFormulario
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            var mock = new Mock<DbSet<Seccion>>();

            // Se prueba que el método no se caiga con un código de formulario válido
            var result = controller.Edit(formulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAgregarFormulario()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            string codFormulario = "CI0128G2";
            string nombreFormulario = "Formulario de prueba";

            // Se crea un formulario para el mock de la base de datos
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = nombreFormulario
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            // Se prueba que el método no se caiga con un código de formulario válido
            var result = controller.AgregarFormulario(formulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAgregarSeccion()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            string codSeccion = "Secci01";

            Seccion seccion = new Seccion()
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba"
            };

            mockDb.Setup(m => m.Seccion.Find(codSeccion)).Returns(seccion);

            var mock = new Mock<DbSet<Seccion>>();

            // Se prueba que el método no se caiga con un código de formulario válido
            var result = controller.AgregarSeccion(seccion);

            Assert.IsNotNull(result);
        }

        
        [TestMethod]
        public void TestIndexFilters()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            Formulario form = new Formulario()
            {
                Codigo = "CI0128IE",
                Nombre = "Sección de prueba"
            };

            Formulario form2 = new Formulario()
            {
                Codigo = "CI0122IE",
                Nombre = "Sección de p3ueba"
            };

            IQueryable<Formulario> formularios = new List<Formulario> { form, form2 }.AsQueryable();

            var mock = new Mock<DbSet<Formulario>>();

            mock.As<IQueryable<Formulario>>().Setup(m => m.Provider).Returns(formularios.Provider);
            mock.As<IQueryable<Formulario>>().Setup(m => m.Expression).Returns(formularios.Expression);
            mock.As<IQueryable<Formulario>>().Setup(m => m.ElementType).Returns(formularios.ElementType);
            mock.As<IQueryable<Formulario>>().Setup(m => m.GetEnumerator()).Returns(formularios.GetEnumerator());

            mockDb.Setup(x => x.Formulario).Returns(mock.Object);

            // Se prueba que el método no se caiga con parámetros nulos
            var result = controller.Index(null, null, null);
            Assert.IsNotNull(result);

            // Se prueba que el método no se caiga con un paramétro de código formulario real
            var result1 = controller.Index("CI0128", "", "");
            Assert.IsNotNull(result);

            // Se prueba que el método no se caiga con un parámetro de nombre real
            var result2 = controller.Index("", "Prueba", "");
            Assert.IsNotNull(result);

            // Se prueba que el método no se caiga con un parámetro de tipo de pregunta real
            var result3 = controller.Index("", "", "libre");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAsociarSeccionesAFormulario()
        {
            // la variable para realizar el mock
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "CI0128G2";
            string nombreFormulario = "Formulario de prueba";
            string codSeccion = "Secci01";

            // Se crea un formulario para el mock de la base de datos
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = nombreFormulario
            };
            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            // Se crea una sección de prueba
            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección sobre algo",
                Orden = 0
            };

            // Se crea un objeto de prueba de tipo SeccionesFormulario
            FormulariosController.SeccionesFormulario formularioPrueba = new FormulariosController.SeccionesFormulario();
            formularioPrueba.codigo = codFormulario;
            formularioPrueba.nombre = nombreFormulario;
            formularioPrueba.seccionesAsociadas = new List<String>();
            formularioPrueba.seccionesAsociadas.Add(codSeccion);

            // Instancia del controller para accesar a los métodos que se probarán de FormulariosController
            FormulariosController controller = new FormulariosController(mockDb.Object);

            // Se llama el método del controller para ver si devuelve un resultado válido
            var result = controller.AsociarSeccionesAFormulario(formularioPrueba);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLlenarFormulariosSinHttpContextDataMock()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            string codFormulario = "CI0128G2";
            FormulariosController controller = new FormulariosController(mockDb.Object);

            // Act
            var result = controller.LlenarFormulario(codFormulario);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestVistaPreviaSinHttpContextDataMock()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            string codFormulario = "CI0128G2";
            FormulariosController controller = new FormulariosController(mockDb.Object);

            // Act
            var result = controller.VistaPrevia(codFormulario);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLlenarFormulariosSinCodigoFormulario()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            // Act
            var result = controller.LlenarFormulario(null);
            testSetup.SetupHttpContext(controller);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestVistaPreviaSinCodigoFormulario()
        {
            TestSetup testSetup = new TestSetup();

            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            // Act
            var result = controller.VistaPrevia(null);
            testSetup.SetupHttpContext(controller);

            // Assert
            Assert.IsNotNull(result);
        }

        // RIP-EDF7
        // Verificación de que el programa no se caiga si el formulario no tiene ninguna sección asociada.
        [TestMethod]
        public void TestLlenarFormulariosSinSeccionesDataMock()
        {
            TestSetup testSetup = new TestSetup();

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
            testSetup.SetupHttpContext(controller);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestVistaPreviaSinSeccionesDataMock()
        {
            TestSetup testSetup = new TestSetup();

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
            var result = controller.VistaPrevia(codFormulario);
            testSetup.SetupHttpContext(controller);

            // Assert
            Assert.IsNotNull(result);
        }

        // RIP-ELFSN
        // Verificación de que el programa no se caiga si se le pasan parámetros nulos.
        [TestMethod]
        public void TestGuardarRespuestasNullParameters()
        {
            TestSetup testSetup = new TestSetup();

            FormulariosController controller = new FormulariosController();
            testSetup.SetupHttpContext(controller);
            ActionResult result = controller.GuardarRespuestas(null, null);
            Assert.IsNotNull(result);
        }

        // RIP-ELFSN
        // Verificación de que el programa no se caiga si el formulario tiene secciones, pero no hay ninguna pregunta.
        [TestMethod]
        public void TestLlenarFormulariosSinPreguntasDataMock()
        {
            TestSetup testSetup = new TestSetup();

            var mockDb = new Mock<DataIntegradorEntities>();
            string codFormulario = "CI0128G2";
            string codSeccion = "12345678";
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formularios de prueba para CI0128"
            };
            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección sobre algo",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            var result = controller.LlenarFormulario(codFormulario);

            testSetup.SetupHttpContext(controller);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestVistaPreviaSinPreguntasDataMock()
        {
            TestSetup testSetup = new TestSetup();

            var mockDb = new Mock<DataIntegradorEntities>();
            string codFormulario = "CI0128G2";
            string codSeccion = "12345678";
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formularios de prueba para CI0128"
            };
            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección sobre algo",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            var result = controller.VistaPrevia(codFormulario);

            testSetup.SetupHttpContext(controller);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLlenarFormulariosConPreguntasEscalarSinRespuestaGuardadaDataMock()
        {
            TestSetup testSetup = new TestSetup();

            var mockDb = new Mock<DataIntegradorEntities>();
            string codFormulario = "CI0128G2";
            string codSeccion = "12345678";
            string codPregunta = "escalar";
            Formulario formulario = new Formulario() { Codigo = codFormulario, Nombre = "Formularios de prueba para CI0128" };
            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección sobre algo",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "¿Cómo calificaría este curso?",
                Tipo = "E",
                Orden = 0
            };
            var mockedObtenerPreguntas = testSetup.SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
                (new List<ObtenerPreguntasDeSeccion_Result> { pregunta });
            mockDb.Setup(x => x.ObtenerPreguntasDeSeccion(codSeccion)).Returns(mockedObtenerPreguntas.Object);

            Pregunta_con_opciones pregunta_Con_Opciones = new Pregunta_con_opciones
            {
                Codigo = codPregunta,
                TituloCampoObservacion = "¿Por qué?"
            };
            mockDb.Setup(x => x.Pregunta_con_opciones.Find(codPregunta)).Returns(pregunta_Con_Opciones);

            Escalar escalar = new Escalar
            {
                Codigo = codPregunta,
                Incremento = 1,
                Minimo = 1,
                Maximo = 10
            };
            mockDb.Setup(x => x.Escalar.Find(codPregunta)).Returns(escalar);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.LlenarFormulario(codFormulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestVistaPreviaConPreguntasEscalarSinRespuestaGuardadaDataMock()
        {
            TestSetup testSetup = new TestSetup();

            var mockDb = new Mock<DataIntegradorEntities>();
            string codFormulario = "CI0128G2";
            string codSeccion = "12345678";
            string codPregunta = "escalar";
            Formulario formulario = new Formulario() { Codigo = codFormulario, Nombre = "Formularios de prueba para CI0128" };
            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección sobre algo",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "¿Cómo calificaría este curso?",
                Tipo = "E",
                Orden = 0
            };
            var mockedObtenerPreguntas = testSetup.SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
                (new List<ObtenerPreguntasDeSeccion_Result> { pregunta });
            mockDb.Setup(x => x.ObtenerPreguntasDeSeccion(codSeccion)).Returns(mockedObtenerPreguntas.Object);

            Pregunta_con_opciones pregunta_Con_Opciones = new Pregunta_con_opciones
            {
                Codigo = codPregunta,
                TituloCampoObservacion = "¿Por qué?"
            };
            mockDb.Setup(x => x.Pregunta_con_opciones.Find(codPregunta)).Returns(pregunta_Con_Opciones);

            Escalar escalar = new Escalar
            {
                Codigo = codPregunta,
                Incremento = 1,
                Minimo = 1,
                Maximo = 10
            };
            mockDb.Setup(x => x.Escalar.Find(codPregunta)).Returns(escalar);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.VistaPrevia(codFormulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateFailed()
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
            var result = controller.Create(formulario, 0);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCreateSuccesful()
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
            var result = controller.Create(formulario, 1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestAplicarFiltros()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            Seccion seccion = new Seccion()
            {
                Codigo = "CI0128IE",
                Nombre = "Sección de prueba"
            };

            Seccion seccion2 = new Seccion()
            {
                Codigo = "CI0122IE",
                Nombre = "Sección de p3ueba"
            };

            IQueryable<Seccion> secciones = new List<Seccion> { seccion, seccion2 }.AsQueryable();

            var mock = new Mock<DbSet<Seccion>>();

            mock.As<IQueryable<Seccion>>().Setup(m => m.Provider).Returns(secciones.Provider);
            mock.As<IQueryable<Seccion>>().Setup(m => m.Expression).Returns(secciones.Expression);
            mock.As<IQueryable<Seccion>>().Setup(m => m.ElementType).Returns(secciones.ElementType);
            mock.As<IQueryable<Seccion>>().Setup(m => m.GetEnumerator()).Returns(secciones.GetEnumerator());

            mockDb.Setup(x => x.Seccion).Returns(mock.Object);

            // Se prueba que el método no se caiga con parámetros nulos
            var result = controller.AplicarFiltro(null,null,null);
            Assert.IsNotNull(result);

            // Se prueba que el método no se caiga con un paramétro de código formulario real
            var result1 = controller.AplicarFiltro("CI0128", "", "");
            Assert.IsNotNull(result);

            // Se prueba que el método no se caiga con un parámetro de nombre real
            var result2 = controller.AplicarFiltro("", "Prueba", "");
            Assert.IsNotNull(result);

            // Se prueba que el método no se caiga con un parámetro de tipo de pregunta real
            var result3 = controller.AplicarFiltro("", "", "libre");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLlenarFormulariosConPreguntasEscalarConRespuestaGuardadaDataMock()
        {
            TestSetup testSetup = new TestSetup();

            var mockDb = new Mock<DataIntegradorEntities>();
            string codFormulario = "CI0128G2";
            string codSeccion = "12345678";
            string codPregunta = "escalar";
            Formulario formulario = new Formulario() { Codigo = codFormulario, Nombre = "Formularios de prueba para CI0128" };
            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección sobre algo",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerRespuestasAFormulario_Result respuestas = new ObtenerRespuestasAFormulario_Result
            {
                Correo = "admin@mail.com",
                CSigla = "CI0128",
                GNumero = 1,
                GAnno = 2019,
                GSemestre = 2,
                FCodigo = codFormulario,
            };

            // Se prepara el retorno del procedimiento almacenado en el mock
            var mockedObtenerRespuestas = testSetup.SetupMockProcedure<ObtenerRespuestasAFormulario_Result>
                (new List<ObtenerRespuestasAFormulario_Result> { respuestas });
            mockDb.Setup(x => x.ObtenerRespuestasAFormulario(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero,
                respuestas.GAnno, respuestas.GSemestre)).Returns(mockedObtenerRespuestas.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "¿Cómo calificaría este curso?",
                Tipo = "E",
                Orden = 0
            };
            var mockedObtenerPreguntas = testSetup.SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
                (new List<ObtenerPreguntasDeSeccion_Result> { pregunta });
            mockDb.Setup(x => x.ObtenerPreguntasDeSeccion(codSeccion)).Returns(mockedObtenerPreguntas.Object);

            Pregunta_con_opciones pregunta_Con_Opciones = new Pregunta_con_opciones
            {
                Codigo = codPregunta,
                TituloCampoObservacion = "¿Por qué?"
            };
            mockDb.Setup(x => x.Pregunta_con_opciones.Find(codPregunta)).Returns(pregunta_Con_Opciones);

            Escalar escalar = new Escalar
            {
                Codigo = codPregunta,
                Incremento = 1,
                Minimo = 1,
                Maximo = 10
            };
            mockDb.Setup(x => x.Escalar.Find(codPregunta)).Returns(escalar);

            ObtenerRespuestasAPreguntaConOpciones_Result obtenerRespuestasAPreguntaConOpciones = new ObtenerRespuestasAPreguntaConOpciones_Result
            {
                Correo = respuestas.Correo,
                FCodigo = respuestas.FCodigo,
                CSigla = respuestas.CSigla,
                GNumero = respuestas.GNumero,
                GAnno = respuestas.GAnno,
                GSemestre = respuestas.GSemestre,
                SCodigo = codSeccion,
                PCodigo = codPregunta,
                Justificacion = "Porque sí."
            };
            var mockedRespuestaPreguntaConOpciones = testSetup.SetupMockProcedure<ObtenerRespuestasAPreguntaConOpciones_Result>(new List<ObtenerRespuestasAPreguntaConOpciones_Result> { obtenerRespuestasAPreguntaConOpciones });
            mockDb.Setup(x => x.ObtenerRespuestasAPreguntaConOpciones(obtenerRespuestasAPreguntaConOpciones.FCodigo, obtenerRespuestasAPreguntaConOpciones.Correo,
                obtenerRespuestasAPreguntaConOpciones.CSigla, obtenerRespuestasAPreguntaConOpciones.GNumero, obtenerRespuestasAPreguntaConOpciones.GSemestre, obtenerRespuestasAPreguntaConOpciones.GAnno,
                obtenerRespuestasAPreguntaConOpciones.SCodigo, obtenerRespuestasAPreguntaConOpciones.PCodigo)).Returns(mockedRespuestaPreguntaConOpciones.Object);

            ObtenerOpcionesSeleccionadas_Result obtenerOpciones = new ObtenerOpcionesSeleccionadas_Result
            {
                Correo = respuestas.Correo,
                FCodigo = respuestas.FCodigo,
                CSigla = respuestas.CSigla,
                GNumero = respuestas.GNumero,
                GAnno = respuestas.GAnno,
                GSemestre = respuestas.GSemestre,
                SCodigo = codSeccion,
                PCodigo = codPregunta,
                OpcionSeleccionada = 0
            };
            var mockedObtenerOpciones = testSetup.SetupMockProcedure<ObtenerOpcionesSeleccionadas_Result>(new List<ObtenerOpcionesSeleccionadas_Result> { obtenerOpciones });
            mockDb.Setup(x => x.ObtenerOpcionesSeleccionadas(obtenerOpciones.FCodigo, obtenerOpciones.Correo, 
                obtenerOpciones.CSigla, obtenerOpciones.GNumero, obtenerOpciones.GSemestre, obtenerOpciones.GAnno, 
                obtenerOpciones.SCodigo, obtenerOpciones.PCodigo)).Returns(mockedObtenerOpciones.Object);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.LlenarFormulario(codFormulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestVistaPreviaConPreguntasEscalarConRespuestaGuardadaDataMock()
        {
            TestSetup testSetup = new TestSetup();

            var mockDb = new Mock<DataIntegradorEntities>();
            string codFormulario = "CI0128G2";
            string codSeccion = "12345678";
            string codPregunta = "escalar";
            Formulario formulario = new Formulario() { Codigo = codFormulario, Nombre = "Formularios de prueba para CI0128" };
            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección sobre algo",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerRespuestasAFormulario_Result respuestas = new ObtenerRespuestasAFormulario_Result
            {
                Correo = "admin@mail.com",
                CSigla = "CI0128",
                GNumero = 1,
                GAnno = 2019,
                GSemestre = 2,
                FCodigo = codFormulario,
            };

            // Se prepara el retorno del procedimiento almacenado en el mock
            var mockedObtenerRespuestas = testSetup.SetupMockProcedure<ObtenerRespuestasAFormulario_Result>
                (new List<ObtenerRespuestasAFormulario_Result> { respuestas });
            mockDb.Setup(x => x.ObtenerRespuestasAFormulario(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero,
                respuestas.GAnno, respuestas.GSemestre)).Returns(mockedObtenerRespuestas.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "¿Cómo calificaría este curso?",
                Tipo = "E",
                Orden = 0
            };
            var mockedObtenerPreguntas = testSetup.SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
                (new List<ObtenerPreguntasDeSeccion_Result> { pregunta });
            mockDb.Setup(x => x.ObtenerPreguntasDeSeccion(codSeccion)).Returns(mockedObtenerPreguntas.Object);

            Pregunta_con_opciones pregunta_Con_Opciones = new Pregunta_con_opciones
            {
                Codigo = codPregunta,
                TituloCampoObservacion = "¿Por qué?"
            };
            mockDb.Setup(x => x.Pregunta_con_opciones.Find(codPregunta)).Returns(pregunta_Con_Opciones);

            Escalar escalar = new Escalar
            {
                Codigo = codPregunta,
                Incremento = 1,
                Minimo = 1,
                Maximo = 10
            };
            mockDb.Setup(x => x.Escalar.Find(codPregunta)).Returns(escalar);

            ObtenerRespuestasAPreguntaConOpciones_Result obtenerRespuestasAPreguntaConOpciones = new ObtenerRespuestasAPreguntaConOpciones_Result
            {
                Correo = respuestas.Correo,
                FCodigo = respuestas.FCodigo,
                CSigla = respuestas.CSigla,
                GNumero = respuestas.GNumero,
                GAnno = respuestas.GAnno,
                GSemestre = respuestas.GSemestre,
                SCodigo = codSeccion,
                PCodigo = codPregunta,
                Justificacion = "Porque sí."
            };
            var mockedRespuestaPreguntaConOpciones = testSetup.SetupMockProcedure<ObtenerRespuestasAPreguntaConOpciones_Result>(new List<ObtenerRespuestasAPreguntaConOpciones_Result> { obtenerRespuestasAPreguntaConOpciones });
            mockDb.Setup(x => x.ObtenerRespuestasAPreguntaConOpciones(obtenerRespuestasAPreguntaConOpciones.FCodigo, obtenerRespuestasAPreguntaConOpciones.Correo,
                obtenerRespuestasAPreguntaConOpciones.CSigla, obtenerRespuestasAPreguntaConOpciones.GNumero, obtenerRespuestasAPreguntaConOpciones.GSemestre, obtenerRespuestasAPreguntaConOpciones.GAnno,
                obtenerRespuestasAPreguntaConOpciones.SCodigo, obtenerRespuestasAPreguntaConOpciones.PCodigo)).Returns(mockedRespuestaPreguntaConOpciones.Object);

            ObtenerOpcionesSeleccionadas_Result obtenerOpciones = new ObtenerOpcionesSeleccionadas_Result
            {
                Correo = respuestas.Correo,
                FCodigo = respuestas.FCodigo,
                CSigla = respuestas.CSigla,
                GNumero = respuestas.GNumero,
                GAnno = respuestas.GAnno,
                GSemestre = respuestas.GSemestre,
                SCodigo = codSeccion,
                PCodigo = codPregunta,
                OpcionSeleccionada = 0
            };
            var mockedObtenerOpciones = testSetup.SetupMockProcedure<ObtenerOpcionesSeleccionadas_Result>(new List<ObtenerOpcionesSeleccionadas_Result> { obtenerOpciones });
            mockDb.Setup(x => x.ObtenerOpcionesSeleccionadas(obtenerOpciones.FCodigo, obtenerOpciones.Correo,
                obtenerOpciones.CSigla, obtenerOpciones.GNumero, obtenerOpciones.GSemestre, obtenerOpciones.GAnno,
                obtenerOpciones.SCodigo, obtenerOpciones.PCodigo)).Returns(mockedObtenerOpciones.Object);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.VistaPrevia(codFormulario);

            Assert.IsNotNull(result);
        }

        // RIP-ELF
        [TestMethod]
        public void TestLlenarFormulariosConPreguntasDeOpcionConOpcionesNulas()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPNUL";
            string codSeccion = "SECCPNUL";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas con opciones nulas"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba con preguntas con opciones nulas",
                Orden = 0
            };

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.LlenarFormulario(codFormulario);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void TestVistaPreviaConPreguntasDeOpcionConOpcionesNulas()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPNUL";
            string codSeccion = "SECCPNUL";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas con opciones nulas"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba con preguntas con opciones nulas",
                Orden = 0
            };

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.VistaPrevia(codFormulario);

            Assert.IsNotNull(result);
        }
        // RIP-ELF
        [TestMethod]
        public void TestObtenerSeccionesConPreguntas()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPNUL";
            string codSeccion = "SECCPNUL";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas con opciones nulas"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba con preguntas con opciones nulas",
                Orden = 0
            };

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.ObtenerSeccionConPreguntas(codFormulario);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void TestGuardarRespuestasAPreguntaSeleccionUnica()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPSU";
            string codSeccion = "SECCPSU";
            string codPregunta = "PREGSU";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas de seleccion única"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            Pregunta pregunta = new Pregunta()
            {
                Codigo = codPregunta,
                Enunciado = "¿Si no sé, es la _?",
                Tipo = "U"
            };

            Pregunta_con_opciones pregunta_Con_Opciones = new Pregunta_con_opciones
            {
                Codigo = codPregunta,
                Pregunta_con_opciones_de_seleccion = new Pregunta_con_opciones_de_seleccion()
            };
            mockDb.Setup(x => x.Pregunta_con_opciones.Find(codPregunta)).Returns(pregunta_Con_Opciones);

            var mockedOpciones = testSetup.SetupMockProcedure<ObtenerOpcionesDePregunta_Result>(new List<ObtenerOpcionesDePregunta_Result>
            {
                new ObtenerOpcionesDePregunta_Result { Orden = 0, Texto ="A" },
                new ObtenerOpcionesDePregunta_Result { Orden = 1, Texto ="B" },
                new ObtenerOpcionesDePregunta_Result { Orden = 2, Texto ="C" },
                new ObtenerOpcionesDePregunta_Result { Orden = 3, Texto ="D" }
            });
            mockDb.Setup(x => x.ObtenerOpcionesDePregunta(codPregunta)).Returns(mockedOpciones.Object);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            List<int> opcionesDePregunta = new List<int>( ) { 0 };

            testSetup.SetupHttpContext(controller);

            Respuestas_a_formulario respuestas = new Respuestas_a_formulario()
            {
                FCodigo = codFormulario,
                Correo = "admin@mail.com",
                CSigla = "CI0128",
                GNumero = 2,
                GAnno = 2019,
                GSemestre = 2,
                Fecha = DateTime.Today,
                Finalizado = false
            }; 

            PreguntaConNumeroSeccion preguntaConSeccion = new PreguntaConNumeroSeccion()
            {
                OrdenSeccion = 0,
                OrdenPregunta = 0,
                Pregunta = pregunta,
                Opciones = opcionesDePregunta,
                RespuestaLibreOJustificacion = "Para que cubra más del coverage"
            };

            // Si no se cae en esta linea, significa que el guardar funciona correctamente
            controller.GuardarRespuestaAPregunta(preguntaConSeccion, codSeccion, respuestas);

        }

        [TestMethod]
        public void TestGuardarRespuestasAPreguntaLibre()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPSU";
            string codSeccion = "SECCPSU";
            string codPregunta = "PREGSU";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas de seleccion única"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            Pregunta pregunta = new Pregunta()
            {
                Codigo = codPregunta,
                Enunciado = "¿Qué piensa de Brexit?",
                Tipo = "L"
            };

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            Respuestas_a_formulario respuestas = new Respuestas_a_formulario()
            {
                FCodigo = codFormulario,
                Correo = "admin@mail.com",
                CSigla = "CI0128",
                GNumero = 2,
                GAnno = 2019,
                GSemestre = 2,
                Fecha = DateTime.Today,
                Finalizado = false
            };

            PreguntaConNumeroSeccion preguntaConSeccion = new PreguntaConNumeroSeccion()
            {
                OrdenSeccion = 0,
                OrdenPregunta = 0,
                Pregunta = pregunta,
                RespuestaLibreOJustificacion = "Para que cubra más del coverage"
            };

            // Si no se cae en esta linea, significa que el guardar funciona correctamente
            controller.GuardarRespuestaAPregunta(preguntaConSeccion, codSeccion, respuestas);

        }

        [TestMethod]
        public void TestGuardarRespuestas()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPSU";
            string codSeccion1 = "SECCPSU";
            string codSeccion2 = "DSIFSDAF";
            string codPregunta = "PREGSU";
            string codPregunta2 = "Pregun2";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas de seleccion única"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            Seccion seccion = new Seccion()
            {
                Codigo = codSeccion1,
                Nombre = "Sección de prueba"
            };

            Seccion seccion2 = new Seccion()
            {
                Codigo = codSeccion2,
                Nombre = "Sección de prueba 2"
            };

            Pregunta pregunta = new Pregunta()
            {
                Codigo = codPregunta,
                Enunciado = "¿Qué piensa de Brexit?",
                Tipo = "L"
            };

            Pregunta pregunta2 = new Pregunta()
            {
                Codigo = codPregunta,
                Enunciado = "¿Qué piensa de Brexit?",
                Tipo = "U"
            };

            Seccion_tiene_pregunta seccion_con_pregunta_1 = new Seccion_tiene_pregunta()
            {
                PCodigo = codPregunta,
                SCodigo = codSeccion1
            };

            Seccion_tiene_pregunta seccion_con_pregunta_2 = new Seccion_tiene_pregunta()
            {
                PCodigo = codPregunta2,
                SCodigo = codSeccion2
            };

            Pregunta_con_opciones pregunta_Con_Opciones = new Pregunta_con_opciones
            {
                Codigo = codPregunta,
                Pregunta_con_opciones_de_seleccion = new Pregunta_con_opciones_de_seleccion()
            };
            mockDb.Setup(x => x.Pregunta_con_opciones.Find(codPregunta)).Returns(pregunta_Con_Opciones);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            List<int> opcionesDePregunta = new List<int>();
            opcionesDePregunta.Append(0);

            testSetup.SetupHttpContext(controller);

            Respuestas_a_formulario respuestas = new Respuestas_a_formulario()
            {
                FCodigo = codFormulario,
                Correo = "admin@mail.com",
                CSigla = "CI0128",
                GNumero = 2,
                GAnno = 2019,
                GSemestre = 2,
                Fecha = DateTime.Today,
                Finalizado = false
            };

            PreguntaConNumeroSeccion preguntaConSeccion1 = new PreguntaConNumeroSeccion()
            {
                OrdenSeccion = 0,
                OrdenPregunta = 0,
                Pregunta = pregunta,
                RespuestaLibreOJustificacion = "Libre"
            };

            PreguntaConNumeroSeccion preguntaConSeccion2 = new PreguntaConNumeroSeccion()
            {
                OrdenSeccion = 1,
                OrdenPregunta = 0,
                Pregunta = pregunta2,
                RespuestaLibreOJustificacion = "Unica"
            };

            List<PreguntaConNumeroSeccion> preguntas = new List<PreguntaConNumeroSeccion>();
            preguntas.Append(preguntaConSeccion1);
            preguntas.Append(preguntaConSeccion2);

            SeccionConPreguntas seccionP = new SeccionConPreguntas()
            {
                CodigoSeccion = codSeccion1,
                Nombre = "nsdlkfj;a",
                Preguntas = preguntas,
                Orden = 0,
                Edicion = true
            };

            SeccionConPreguntas seccionP2 = new SeccionConPreguntas()
            {
                CodigoSeccion = codSeccion2,
                Nombre = "seccion2nsdlkfj;a",
                Preguntas = preguntas,
                Orden = 0,
                Edicion = true
            };

            List<SeccionConPreguntas> secciones = new List<SeccionConPreguntas>() { seccionP, seccionP2 };

            // Si no se cae en esta linea, significa que el guardar funciona correctamente
            controller.GuardarRespuestas(respuestas, secciones);

        }

        [TestMethod]
        public void TestBorrarSeccion()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPSU";
            string codSeccion = "SECCPSU";
            string codPregunta = "PREGSU";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas de seleccion única"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "¿Si no sé, es la _?",
                Tipo = "U",
                Orden = 0,
            };

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.BorrarSeccion(codFormulario, codSeccion);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestEliminarSeccionNoNull()
        {
            TestSetup testSetup = new TestSetup();
            string codFormulario = "TESTPSU";
            string codSeccion = "SECCPSU";


            FormulariosController controller = new FormulariosController();

            testSetup.SetupHttpContext(controller);

            var result = controller.EliminarSeccion(codFormulario, codSeccion);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestEliminarPreguntaNoNull()
        {
            TestSetup testSetup = new TestSetup();
            string codSeccion = "SECCPSU";
            string codPregunta = "PREGSU";

            FormulariosController controller = new FormulariosController();

            testSetup.SetupHttpContext(controller);

            var result = controller.EliminarPregunta(codSeccion, codPregunta);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestEliminarSeccionNull()
        {
            TestSetup testSetup = new TestSetup();
            FormulariosController controller = new FormulariosController();

            testSetup.SetupHttpContext(controller);

            var result = controller.EliminarSeccion(null, null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestEliminarPreguntaNull()
        {
            TestSetup testSetup = new TestSetup();
            FormulariosController controller = new FormulariosController();

            testSetup.SetupHttpContext(controller);

            var result = controller.EliminarPregunta(null, null);

            Assert.IsNull(result);
        }

       
        [TestMethod]
        public void TestBorrarPregunta()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPSU";
            string codSeccion = "SECCPSU";
            string codPregunta = "PREGSU";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas de seleccion única"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "¿Si no sé, es la _?",
                Tipo = "U",
                Orden = 0,
            };

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.BorrarPregunta(codSeccion, codPregunta);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLlenarFormulariosConPreguntasDeOpcionUnica()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPSU";
            string codSeccion = "SECCPSU";
            string codPregunta = "PREGSU";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas de seleccion única"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "¿Si no sé, es la _?",
                Tipo = "U",
                Orden = 0,
            };
            var mockedObtenerPreguntas = testSetup.SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
                (new List<ObtenerPreguntasDeSeccion_Result> { pregunta });
            mockDb.Setup(x => x.ObtenerPreguntasDeSeccion(codSeccion)).Returns(mockedObtenerPreguntas.Object);

            Pregunta_con_opciones pregunta_Con_Opciones = new Pregunta_con_opciones
            {
                Codigo = codPregunta,
                Pregunta_con_opciones_de_seleccion = new Pregunta_con_opciones_de_seleccion()
            };
            mockDb.Setup(x => x.Pregunta_con_opciones.Find(codPregunta)).Returns(pregunta_Con_Opciones);

            var mockedOpciones = testSetup.SetupMockProcedure<ObtenerOpcionesDePregunta_Result>(new List<ObtenerOpcionesDePregunta_Result>
            {
                new ObtenerOpcionesDePregunta_Result { Orden = 0, Texto ="A" },
                new ObtenerOpcionesDePregunta_Result { Orden = 1, Texto ="B" },
                new ObtenerOpcionesDePregunta_Result { Orden = 2, Texto ="C" },
                new ObtenerOpcionesDePregunta_Result { Orden = 3, Texto ="D" }
            });
            mockDb.Setup(x => x.ObtenerOpcionesDePregunta(codPregunta)).Returns(mockedOpciones.Object);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.LlenarFormulario(codFormulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestVistaPreviaConPreguntasDeOpcionUnica()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPSU";
            string codSeccion = "SECCPSU";
            string codPregunta = "PREGSU";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas de seleccion única"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "¿Si no sé, es la _?",
                Tipo = "U",
                Orden = 0,
            };
            var mockedObtenerPreguntas = testSetup.SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
                (new List<ObtenerPreguntasDeSeccion_Result> { pregunta });
            mockDb.Setup(x => x.ObtenerPreguntasDeSeccion(codSeccion)).Returns(mockedObtenerPreguntas.Object);

            Pregunta_con_opciones pregunta_Con_Opciones = new Pregunta_con_opciones
            {
                Codigo = codPregunta,
                Pregunta_con_opciones_de_seleccion = new Pregunta_con_opciones_de_seleccion()
            };
            mockDb.Setup(x => x.Pregunta_con_opciones.Find(codPregunta)).Returns(pregunta_Con_Opciones);

            var mockedOpciones = testSetup.SetupMockProcedure<ObtenerOpcionesDePregunta_Result>(new List<ObtenerOpcionesDePregunta_Result>
            {
                new ObtenerOpcionesDePregunta_Result { Orden = 0, Texto ="A" },
                new ObtenerOpcionesDePregunta_Result { Orden = 1, Texto ="B" },
                new ObtenerOpcionesDePregunta_Result { Orden = 2, Texto ="C" },
                new ObtenerOpcionesDePregunta_Result { Orden = 3, Texto ="D" }
            });
            mockDb.Setup(x => x.ObtenerOpcionesDePregunta(codPregunta)).Returns(mockedOpciones.Object);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.VistaPrevia(codFormulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLlenarFormulariosConPreguntaRespuestaLibre()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPRL";
            string codSeccion = "SECCPRL";
            string codPregunta = "PREGRL";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas de respuesta libre"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "Explique qué es la ingeniería de los requerimientos.",
                Tipo = "L",
                Orden = 0,
            };
            var mockedObtenerPreguntas = testSetup.SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
                (new List<ObtenerPreguntasDeSeccion_Result> { pregunta });
            mockDb.Setup(x => x.ObtenerPreguntasDeSeccion(codSeccion)).Returns(mockedObtenerPreguntas.Object);

            ObtenerRespuestasAFormulario_Result respuestas = new ObtenerRespuestasAFormulario_Result
            {
                Correo = "admin@mail.com",
                CSigla = "CI0128",
                GNumero = 1,
                GAnno = 2019,
                GSemestre = 2,
                FCodigo = codFormulario,
            };

            // Se prepara el retorno del procedimiento almacenado en el mock
            var mockedObtenerRespuestas = testSetup.SetupMockProcedure<ObtenerRespuestasAFormulario_Result>
                (new List<ObtenerRespuestasAFormulario_Result> { respuestas });
            mockDb.Setup(x => x.ObtenerRespuestasAFormulario(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero,
                respuestas.GAnno, respuestas.GSemestre)).Returns(mockedObtenerRespuestas.Object);

            var respuestaLibreList = new List<ObtenerRespuestaLibreGuardada_Result>
            {
                new ObtenerRespuestaLibreGuardada_Result
                {
                    FCodigo = respuestas.FCodigo,
                    Correo = respuestas.Correo,
                    CSigla = respuestas.CSigla,
                    GNumero = respuestas.GNumero,
                    GAnno = respuestas.GAnno,
                    GSemestre = respuestas.GSemestre,
                    SCodigo = codSeccion,
                    PCodigo = codPregunta
                }
            };
            mockDb.Setup(x => x.ObtenerRespuestaLibreGuardada(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero,
                respuestas.GAnno, respuestas.GSemestre, codPregunta, codSeccion)).Returns(respuestaLibreList.AsQueryable());

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.LlenarFormulario(codFormulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestVistaPreviaConPreguntaRespuestaLibre()
        {
            TestSetup testSetup = new TestSetup();

            var mockDb = new Mock<DataIntegradorEntities>();

            string codFormulario = "TESTPRL";
            string codSeccion = "SECCPRL";
            string codPregunta = "PREGRL";

            // Se crea el formulario de prueba
            Formulario formulario = new Formulario()
            {
                Codigo = codFormulario,
                Nombre = "Formulario de prueba con preguntas de respuesta libre"
            };

            mockDb.Setup(m => m.Formulario.Find(codFormulario)).Returns(formulario);

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba",
                Orden = 0
            };

            var mockedObtenerSecciones = testSetup.SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "Explique qué es la ingeniería de los requerimientos.",
                Tipo = "L",
                Orden = 0,
            };
            var mockedObtenerPreguntas = testSetup.SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
                (new List<ObtenerPreguntasDeSeccion_Result> { pregunta });
            mockDb.Setup(x => x.ObtenerPreguntasDeSeccion(codSeccion)).Returns(mockedObtenerPreguntas.Object);

            ObtenerRespuestasAFormulario_Result respuestas = new ObtenerRespuestasAFormulario_Result
            {
                Correo = "admin@mail.com",
                CSigla = "CI0128",
                GNumero = 1,
                GAnno = 2019,
                GSemestre = 2,
                FCodigo = codFormulario,
            };

            // Se prepara el retorno del procedimiento almacenado en el mock
            var mockedObtenerRespuestas = testSetup.SetupMockProcedure<ObtenerRespuestasAFormulario_Result>
                (new List<ObtenerRespuestasAFormulario_Result> { respuestas });
            mockDb.Setup(x => x.ObtenerRespuestasAFormulario(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero,
                respuestas.GAnno, respuestas.GSemestre)).Returns(mockedObtenerRespuestas.Object);

            var respuestaLibreList = new List<ObtenerRespuestaLibreGuardada_Result>
            {
                new ObtenerRespuestaLibreGuardada_Result
                {
                    FCodigo = respuestas.FCodigo,
                    Correo = respuestas.Correo,
                    CSigla = respuestas.CSigla,
                    GNumero = respuestas.GNumero,
                    GAnno = respuestas.GAnno,
                    GSemestre = respuestas.GSemestre,
                    SCodigo = codSeccion,
                    PCodigo = codPregunta
                }
            };
            mockDb.Setup(x => x.ObtenerRespuestaLibreGuardada(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero,
                respuestas.GAnno, respuestas.GSemestre, codPregunta, codSeccion)).Returns(respuestaLibreList.AsQueryable());

            FormulariosController controller = new FormulariosController(mockDb.Object);

            testSetup.SetupHttpContext(controller);

            var result = controller.VistaPrevia(codFormulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProbarVistaPreviaNula()
        {
            FormulariosController controller = new FormulariosController();
            var result = controller.SeccionConPreguntas(null) as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ProbarVistaPreviaFormularioNula()
        {
            FormulariosController controller = new FormulariosController();
            var result = controller.VistaPrevia(null) as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ProbarVistaPreviaSeccExiste()
        {
            FormulariosController controller = new FormulariosController();
            var result = controller.SeccionConPreguntas("00000001") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProbarVistaPreviaSeccNoExiste()
        {
            FormulariosController controller = new FormulariosController();
            var result = controller.SeccionConPreguntas("NOEXISTE") as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ProbarVistaPreviaPregFormNoExiste()
        {
            FormulariosController controller = new FormulariosController();
            var result = controller.TodasLasPreguntas("NOEXISTE") as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ProbarVistaPreviaPregFormExiste()
        {
            FormulariosController controller = new FormulariosController();
            var result = controller.TodasLasPreguntas("00000001") as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}

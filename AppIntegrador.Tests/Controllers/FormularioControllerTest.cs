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
        public void TestLlenarFormulariosSinCodigoFormulario()
        {
            var mockDb = new Mock<DataIntegradorEntities>();
            FormulariosController controller = new FormulariosController(mockDb.Object);

            // Act
            var result = controller.LlenarFormulario(null);
            SetupHttpContext(controller);

            // Assert
            Assert.IsNotNull(result);
        }

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
            SetupHttpContext(controller);

            // Assert
            Assert.IsNotNull(result);
        }

        // RIP-ELFSN
        // Verificación de que el programa no se caiga si se le pasan parámetros nulos.
        [TestMethod]
        public void TestGuardarRespuestasNullParameters()
        {
            FormulariosController controller = new FormulariosController();
            SetupHttpContext(controller);
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

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección sobre algo",
                Orden = 0
            };

            var mockedObtenerSecciones = SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            var result = controller.LlenarFormulario(codFormulario);

            SetupHttpContext(controller);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLlenarFormulariosConPreguntasEscalarSinRespuestaGuardadaDataMock()
        {
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

            var mockedObtenerSecciones = SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "¿Cómo calificaría este curso?",
                Tipo = "E",
                Orden = 0
            };
            var mockedObtenerPreguntas = SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
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

            SetupHttpContext(controller);

            var result = controller.LlenarFormulario(codFormulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLlenarFormulariosConPreguntasEscalarConRespuestaGuardadaDataMock()
        {
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

            var mockedObtenerSecciones = SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
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
            var mockedObtenerRespuestas = SetupMockProcedure<ObtenerRespuestasAFormulario_Result>
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
            var mockedObtenerPreguntas = SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
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
            var mockedRespuestaPreguntaConOpciones = SetupMockProcedure<ObtenerRespuestasAPreguntaConOpciones_Result>(new List<ObtenerRespuestasAPreguntaConOpciones_Result> { obtenerRespuestasAPreguntaConOpciones });
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
            var mockedObtenerOpciones = SetupMockProcedure<ObtenerOpcionesSeleccionadas_Result>(new List<ObtenerOpcionesSeleccionadas_Result> { obtenerOpciones });
            mockDb.Setup(x => x.ObtenerOpcionesSeleccionadas(obtenerOpciones.FCodigo, obtenerOpciones.Correo, 
                obtenerOpciones.CSigla, obtenerOpciones.GNumero, obtenerOpciones.GSemestre, obtenerOpciones.GAnno, 
                obtenerOpciones.SCodigo, obtenerOpciones.PCodigo)).Returns(mockedObtenerOpciones.Object);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            SetupHttpContext(controller);

            var result = controller.LlenarFormulario(codFormulario);

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

            ObtenerSeccionesDeFormulario_Result seccion = new ObtenerSeccionesDeFormulario_Result
            {
                Codigo = codSeccion,
                Nombre = "Sección de prueba con preguntas con opciones nulas",
                Orden = 0
            };

            var mockedObtenerSecciones = SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "¿Es esta su pregunta sin opciones favorita?",
                Tipo = "U",
                Orden = 0
            };
            var mockedObtenerPreguntas = SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
                (new List<ObtenerPreguntasDeSeccion_Result> { pregunta });
            mockDb.Setup(x => x.ObtenerPreguntasDeSeccion(codSeccion)).Returns(mockedObtenerPreguntas.Object);

            Pregunta_con_opciones pregunta_Con_Opciones = new Pregunta_con_opciones
            {
                Codigo = codPregunta
            };
            mockDb.Setup(x => x.Pregunta_con_opciones.Find(codPregunta)).Returns(pregunta_Con_Opciones);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            SetupHttpContext(controller);

            var result = controller.LlenarFormulario(codFormulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLlenarFormulariosConPreguntasDeOpcionUnica()
        {
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

            var mockedObtenerSecciones = SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "¿Si no sé, es la _?",
                Tipo = "U",
                Orden = 0,
        };
            var mockedObtenerPreguntas = SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
                (new List<ObtenerPreguntasDeSeccion_Result> { pregunta });
            mockDb.Setup(x => x.ObtenerPreguntasDeSeccion(codSeccion)).Returns(mockedObtenerPreguntas.Object);

            Pregunta_con_opciones pregunta_Con_Opciones = new Pregunta_con_opciones
            {
                Codigo = codPregunta,
                Pregunta_con_opciones_de_seleccion = new Pregunta_con_opciones_de_seleccion()
            };
            mockDb.Setup(x => x.Pregunta_con_opciones.Find(codPregunta)).Returns(pregunta_Con_Opciones);

            var mockedOpciones = SetupMockProcedure<ObtenerOpcionesDePregunta_Result>(new List<ObtenerOpcionesDePregunta_Result>
            {
                new ObtenerOpcionesDePregunta_Result { Orden = 0, Texto ="A" },
                new ObtenerOpcionesDePregunta_Result { Orden = 1, Texto ="B" },
                new ObtenerOpcionesDePregunta_Result { Orden = 2, Texto ="C" },
                new ObtenerOpcionesDePregunta_Result { Orden = 3, Texto ="D" }
            });
            mockDb.Setup(x => x.ObtenerOpcionesDePregunta(codPregunta)).Returns(mockedOpciones.Object);

            FormulariosController controller = new FormulariosController(mockDb.Object);

            SetupHttpContext(controller);

            var result = controller.LlenarFormulario(codFormulario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestLlenarFormulariosConPreguntaRespuestaLibre()
        {
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

            var mockedObtenerSecciones = SetupMockProcedure<ObtenerSeccionesDeFormulario_Result>
                (new List<ObtenerSeccionesDeFormulario_Result> { seccion });
            mockDb.Setup(x => x.ObtenerSeccionesDeFormulario(codFormulario)).Returns(mockedObtenerSecciones.Object);

            ObtenerPreguntasDeSeccion_Result pregunta = new ObtenerPreguntasDeSeccion_Result
            {
                Codigo = codPregunta,
                Enunciado = "Explique qué es la ingeniería de los requerimientos.",
                Tipo = "L",
                Orden = 0,
            };
            var mockedObtenerPreguntas = SetupMockProcedure<ObtenerPreguntasDeSeccion_Result>
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
            var mockedObtenerRespuestas = SetupMockProcedure<ObtenerRespuestasAFormulario_Result>
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

            SetupHttpContext(controller);

            var result = controller.LlenarFormulario(codFormulario);

            Assert.IsNotNull(result);
        }

        /**
         * Método genérico para preparar el Mock del retorno de un procedimiento almacenado.
         * Para más información de cómo funciona, ver
         * https://gisdevblog.wordpress.com/2018/04/04/mocking-stored-procedure-call-in-entity-framework/
         */
        private Mock<ObjectResult<T>> SetupMockProcedure<T>(List<T> data)
        {
            var mockedObjectResult = new Mock<ObjectResult<T>>();
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());
            return mockedObjectResult;
        }

        private void SetupHttpContext(FormulariosController controller)
        {
            if(controller != null)
            {
                controller.ControllerContext = new ControllerContext
                {
                    Controller = controller,
                    HttpContext = new MockHttpContext(new CustomPrincipal("admin@mail.com"))
                };
            }
        }

        private class CustomPrincipal : IPrincipal
        {
            public IIdentity Identity { get; private set; }
            public bool IsInRole(string role) { return false; }
            public CustomPrincipal(string user)
            {
                Identity = new GenericIdentity(user);
            }
        }

        private class MockHttpContext : HttpContextBase
        {
            private readonly IPrincipal user;

            public MockHttpContext(IPrincipal principal)
            {
                this.user = principal;
            }

            public override IPrincipal User
            {
                get
                {
                    return user;
                }
                set
                {
                    base.User = value;
                }
            }
        }
    }
    class TestableObjectResult<T> : ObjectResult<T>
    {
        
    }
}

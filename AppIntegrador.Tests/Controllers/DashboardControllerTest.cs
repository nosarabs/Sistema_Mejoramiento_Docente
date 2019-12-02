using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using System.Web.Mvc;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Security.Principal;
using System.Web;
using System.Reflection;
using System.Web.SessionState;
using System.IO;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class DashboardControllerTest
    {

        [TestMethod]
        public void IndexNotNullTest()
        {
            //Arrange
            DashboardController controller = new DashboardController();

            //Act
            ViewResult view = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(view);
        }

        //Este test tiene como propósito corroborar que los resultados del procedimientos almacenado que implementa los filtros se serializan correctamente.
        [TestMethod]
        public void ObtenerFormulariosParametrosNulosTest()
        {
            //Arrange

            //Se crea el mock de la base de datos
            var mockDb = new Mock<DataIntegradorEntities>();
            var mockFiltrosDb = new Mock<FiltrosEntities>();

            //Se instancia el controlador y se le pasa como parámetro el mock
            DashboardController controller = new DashboardController(mockDb.Object, mockFiltrosDb.Object);

            //Se crear un formulario como dummy data
            var formulariosDummy = new List<FormulariosFiltros>
            {
                new FormulariosFiltros
                {
                    FCodigo = "00000001",
                    FNombre = "Formulario de prueba",
                    CSigla = "CI0128",
                    GNumero = 1,
                    GSemestre = 2,
                    GAnno = 2,
                    FechaInicio = DateTime.Parse("09/06/2019"),
                    FechaFin = DateTime.Parse("11/06/2019")
                },
                new FormulariosFiltros
                {
                    FCodigo = "00000002",
                    FNombre = "Formulario de fin de curso",
                    CSigla = "CI0128",
                    GNumero = 1,
                    GSemestre = 2,
                    GAnno = 2,
                    FechaInicio = DateTime.Parse("09/11/2019"),
                    FechaFin = DateTime.Parse("11/11/2019")
                }
        };

            //Se hace el mock del procedimiento almacenado que utiliza el método del controlador
            var mockedObjectResult = new Mock<IQueryable<FormulariosFiltros>>();
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(formulariosDummy.GetEnumerator());
            mockFiltrosDb.Setup(x => x.ObtenerFormulariosFiltros(null, null, null, null)).Returns(mockedObjectResult.Object);

            //Act

            //Se hace el llamado al controlador y se obtiene el JSON
            string formulariosJson = controller.ObtenerFormularios(null, null, null, null);

            //Se deserializa el JSON
            var formularios = JsonConvert.DeserializeObject<List<FormulariosFiltros>>(formulariosJson, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });

            //Assert

            //Se comparan los miembros del formulario dummy con los del formulario retornado por el controlador
            Assert.IsTrue(CompararFormularios(formulariosDummy, formularios));
        }

        //Este test tiene como propósito corroborar que los resultados del procedimientos almacenado que implementa los filtros se serializan correctamente.
        [TestMethod]
        public void ObtenerFormulariosParametrosNoExistentesTest()
        {
            //Arrange

            //Se crea el mock de la base de datos
            var mockDb = new Mock<DataIntegradorEntities>();
            var mockFiltrosDb = new Mock<FiltrosEntities>();

            //Se instancia el controlador y se le pasa como parámetro el mock
            DashboardController controller = new DashboardController(mockDb.Object, mockFiltrosDb.Object);

            //Se crean los parámetros del controlador
            var unidadesAcademicas = new List<UAsFiltros> { new UAsFiltros { CodigoUA = "01" } };
            var carrerasEnfasis = new List<CarrerasEnfasisFiltros> { new CarrerasEnfasisFiltros { CodCarrera = "01", CodEnfasis = "01" } };
            var grupos = new List<GruposFiltros> { new GruposFiltros { SiglaCurso = "CI0128", NumGrupo = 1, Semestre = 2, Anno = 2019 } };
            var profesores = new List<ProfesoresFiltros> { new ProfesoresFiltros { Correo = "ismael@mail.com" } };

            //Se crear un formulario como dummy data
            var formulariosDummy = new List<FormulariosFiltros>
            {
                new FormulariosFiltros
                {
                    FCodigo = "00000001",
                    FNombre = "Formulario de prueba",
                    CSigla = "CI0128",
                    GNumero = 1,
                    GSemestre = 2,
                    GAnno = 2,
                    FechaInicio = DateTime.Parse("09/06/2019"),
                    FechaFin = DateTime.Parse("11/06/2019")
                },
                new FormulariosFiltros
                {
                    FCodigo = "00000002",
                    FNombre = "Formulario de fin de curso",
                    CSigla = "CI0128",
                    GNumero = 1,
                    GSemestre = 2,
                    GAnno = 2,
                    FechaInicio = DateTime.Parse("09/11/2019"),
                    FechaFin = DateTime.Parse("11/11/2019")
                }
        };

            //Se hace el mock del procedimiento almacenado que utiliza el método del controlador
            var mockedObjectResult = new Mock<IQueryable<FormulariosFiltros>>();
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(formulariosDummy.GetEnumerator());
            mockFiltrosDb.Setup(x => x.ObtenerFormulariosFiltros(null, null, null, null)).Returns(mockedObjectResult.Object);

            //Act

            //Se hace el llamado al controlador y se obtiene el JSON
            string formulariosJson = controller.ObtenerFormularios(unidadesAcademicas, carrerasEnfasis, grupos, profesores);

            //Se deserializa el JSON
            var formularios = JsonConvert.DeserializeObject<List<FormulariosFiltros>>(formulariosJson, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });

            //Assert

            //Se comparan los miembros del formulario dummy con los del formulario retornado por el controlador
            Assert.IsFalse(CompararFormularios(formulariosDummy, formularios));
        }

        private bool CompararFormularios (List<FormulariosFiltros> dummyFormularios, List<FormulariosFiltros> controllerformularios)
        {
            bool resultado = dummyFormularios.Count() == controllerformularios.Count();
            int indice = 0;
            FormulariosFiltros dummyFormulario, controllerFormulario;

            while (resultado && indice < dummyFormularios.Count())
            {
                dummyFormulario = dummyFormularios[indice];
                controllerFormulario = controllerformularios[indice];

                resultado =     dummyFormulario.FCodigo.Equals(controllerFormulario.FCodigo)
                                && dummyFormulario.FNombre.Equals(controllerFormulario.FNombre)
                                && dummyFormulario.CSigla.Equals(controllerFormulario.CSigla)
                                && dummyFormulario.GNumero.Equals(controllerFormulario.GNumero)
                                && dummyFormulario.GSemestre.Equals(controllerFormulario.GSemestre)
                                && dummyFormulario.GAnno.Equals(controllerFormulario.GAnno)
                                && dummyFormulario.FechaInicio.Equals(controllerFormulario.FechaInicio)
                                && dummyFormulario.FechaFin.Equals(controllerFormulario.FechaFin);

                ++indice;
            }

            return resultado;
        }

        [TestInitialize]
        public void Init()
        {
            //No aseguramos que admin no haya quedado logeado por otros tests.
            CurrentUser.deleteCurrentUser("admin@mail.com");

            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new HttpRequest("", "http://localhost/", "");

            // Step 2: Setup the HTTP Response
            var httpResponse = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            var httpContext = new HttpContext(httpRequest, httpResponse);
            var sessionContainer =
                new HttpSessionStateContainer("admin@mail.com",
                                               new SessionStateItemCollection(),
                                               new HttpStaticObjectsCollection(),
                                               10,
                                               true,
                                               HttpCookieMode.AutoDetect,
                                               SessionStateMode.InProc,
                                               false);
            httpContext.Items["AspSession"] =
                typeof(HttpSessionState)
                .GetConstructor(
                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            var fakeIdentity = new GenericIdentity("admin@mail.com");
            var principal = new GenericPrincipal(fakeIdentity, null);

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
            HttpContext.Current.User = principal;
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "00000001", "00000001");
        }

        [TestCleanup]
        public void Cleanup()
        {
            //Nos aseguramos que admin quede deslogeado despues de cada test.
            CurrentUser.deleteCurrentUser("admin@mail.com");
        }
    }
}

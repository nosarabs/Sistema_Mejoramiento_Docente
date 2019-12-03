using AppIntegrador;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace AppIntegrador.Tests.Controllers
{
    /// <summary>
    /// Summary description for PlanDeMejoraTest
    /// </summary>
    [TestClass]
    public class PlanDeMejoraTest
    {
        public PlanDeMejoraTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        [TestMethod]
        public void TestIndexNotNull()
        {
            PlanDeMejoraController controller = new PlanDeMejoraController();
            ViewResult result = controller.Index("admin") as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestIndexName()
        {
            PlanDeMejoraController plan = new PlanDeMejoraController();
            var indexResult = plan.Index("admin") as ViewResult;
            Assert.AreEqual("Index", indexResult.ViewName);
            plan.Dispose();
        }

        [TestMethod]
        public void CreateNuevo() 
        {
            PlanDeMejoraController controlador = new PlanDeMejoraController();
            ViewResult resultado = controlador.Crear() as ViewResult;
            Assert.IsNotNull(resultado);
        }

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
        public void CrearPlanDeMejoraDataMockTest()
        {
            DataIntegradorEntities test = new DataIntegradorEntities();
            var db = new Mock<DataIntegradorEntities>();
            String planNombre = "Plan de prueba";
            DateTime inicio = new DateTime(2019,12,01);
            DateTime Fin = new DateTime(2020, 12, 01);

            PlanDeMejora plan = new PlanDeMejora() { nombre = planNombre, fechaInicio = inicio, fechaFin = Fin};

            db.Setup(m => m.PlanDeMejora.Add(plan));
            db.Setup(m => m.SaveChanges());
            var controller = new PlanDeMejoraController(db.Object);
            var result = controller.Crear(plan);
            Assert.IsNotNull(result);
            controller.Dispose();
        }

        [TestMethod]
        public void CrearPlanDeMejoraIntegrationTest()
        {
            String planNombre = "Plan de prueba de integración";
            DateTime inicio = new DateTime(2019, 12, 01);
            DateTime Fin = new DateTime(2020, 12, 01);

            PlanDeMejora plan = new PlanDeMejora() { nombre = planNombre, fechaInicio = inicio, fechaFin = Fin };

            var controller = new PlanDeMejoraController();
            var result = controller.Crear(plan);
            Assert.IsNotNull(result);
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

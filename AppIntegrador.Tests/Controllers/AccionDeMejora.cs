using AppIntegrador;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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
    /// Summary description for AccionDeMejora
    /// </summary>
    [TestClass]
    public class AccionDeMejora
    {
        public AccionDeMejora()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

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
        public void TestMethod1()
        {
            HttpContext context = System.Web.HttpContext.Current;

            // Arrange
            var controller = new AccionDeMejoraController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void IndexNotNull()
        {
            AccionDeMejoraController accion = new AccionDeMejoraController();
            var indexResult = accion.Index();
            Assert.IsNotNull(indexResult);
        }

        [TestMethod]
        public void IndexName()
        {
            var accion = new AccionDeMejoraController();
            var indexResult = accion.Index() as ViewResult;
            Assert.AreEqual("Index", indexResult.ViewName);
            accion.Dispose();
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

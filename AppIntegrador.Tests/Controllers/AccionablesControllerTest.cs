using AppIntegrador;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class AccionablesControllerTest
    {
        [TestMethod]
        public void IndexNotNullTest()
        {
            var controller = new AccionablesController();
            var indexResult = controller.Index() as ViewResult;
            Assert.IsNotNull(indexResult);
            controller.Dispose();
        }

        [TestMethod]
        public void IndexNameTest()
        {
            var controller = new AccionablesController();
            var indexResult = controller.Index() as ViewResult;
            Assert.AreEqual("Índice", indexResult.ViewName);
            controller.Dispose();
        }

        [TestMethod]
        public void CreateNotNull()
        {
            Mock<HttpSessionStateBase> mockSession = new Mock<HttpSessionStateBase>();
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.SetupGet(context => context.Session).Returns(mockSession.Object);


            var controller = new AccionablesController();
            var resultado = controller.Create(0, "nombObj", "descrAcMej", "FechaInicio", "FechaFin", 1, true);
            Assert.IsNotNull(resultado);
        }

        [TestMethod]
        public void CreateDataMockTest()
        {
            var mockdb = new Mock<DataIntegradorEntities>();
            Accionable acc = new Accionable() { codPlan = 666, nombreObj = "ObjPrueba", descripAcMej = "AcMejPrueba", descripcion = "descripcionPrueba", fechaInicio = Convert.ToDateTime("1995-09-29"), fechaFin = Convert.ToDateTime("2004-09-29") };

            mockdb.Setup(m => m.Accionable.Add(acc));
            mockdb.Setup(m => m.SaveChanges());

            var controller = new AccionablesController(mockdb.Object);
            var result = controller.Create(acc);
            Assert.IsNotNull(result);
            controller.Dispose();
        }
        [TestMethod]
        public void TablaAccionablesTest()
        {
            int codPlan = 666;
            String nombreObj = "ObjPrueba";
            String descripAcMej = "AcMejPrueba";
            //var mockdb = new Mock<DataIntegradorEntities>();
            //mockdb.Setup(m => m.Accionable.Where(o => o.codPlan == codPlan && o.nombreObj == nombreObj && o.descripAcMej == descripAcMej));

            var controller = new AccionablesController();

            var result = controller.TablaAccionables(codPlan, nombreObj, descripAcMej, false);
            Assert.IsNotNull(result);
            controller.Dispose();
        }

        [TestMethod]
        public void EditDataMockTest()
        {
            Accionable acc = new Accionable() { codPlan = 666, nombreObj = "ObjPrueba", descripAcMej = "AcMejPrueba", descripcion = "descripcionPrueba", fechaInicio = Convert.ToDateTime("1995-09-29"), fechaFin = Convert.ToDateTime("2004-09-29") };
            var mockdb = new Mock<DataIntegradorEntities>();
            //mockdb.Setup(m => m.Entry(acc).State);
            mockdb.Setup(m => m.SaveChanges());
            var controller = new AccionablesController(mockdb.Object);

            var result = controller.Edit(acc);

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void DeleteAccionableMockTest()
        {
            int codPlan = 666;
            String nombObj = "ObjPrueba";
            String descripAcMej = "AcMejPrueba";
            String descripAccionable = "descripcionPrueba";
            Accionable acc = new Accionable() { codPlan = 666, nombreObj = "ObjPrueba", descripAcMej = "AcMejPrueba", descripcion = "descripcionPrueba", fechaInicio = Convert.ToDateTime("1995-09-29"), fechaFin = Convert.ToDateTime("2004-09-29") };
            var mockdb = new Mock<DataIntegradorEntities>();
            mockdb.Setup(m => m.Accionable.Remove(acc));
            mockdb.Setup(m => m.Accionable.Find(codPlan, nombObj, descripAcMej, descripAccionable));
            mockdb.Setup(m => m.SaveChanges());
            var controller = new AccionablesController(mockdb.Object);

            var result = controller.Edit(acc);

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

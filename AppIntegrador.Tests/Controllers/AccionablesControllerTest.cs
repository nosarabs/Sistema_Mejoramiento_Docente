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
        public void AccionablePorEvaluarTest()
        {
            var controller = new AccionablesController();
            var result = controller.AccionablePorEvaluar(1, "Objetivo de plan de mejora - Alexandra", "Accion de mejora de objetivo - Alexandra") as PartialViewResult;
            Assert.AreEqual(result.ViewName, "_AccionablePorEvaluar");
        }
        [TestMethod]
        public void AccionablePorEjecutarTestFail()
        {
            int codPlan = 666;
            String nombreObj = "ObjPrueba";
            String descripAcMej = "AcMejPrueba";

            var controller = new AccionablesController();

            var result = controller.AccionablesPorEjecutar(codPlan, nombreObj, descripAcMej, true);
            Assert.IsNull(result);
            controller.Dispose();
        }
        [TestMethod]
        public void AccionablePorEjecutarPacoTest()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();
            int codPlan = 1;
            String nombreObj = "ObjPrueba";
            String descripAcMej = "AcMejPrueba";
            mockDb.Setup(m => m.PlanDeMejora.Find(codPlan));

            AccionablesController controller = new AccionablesController(mockDb.Object);
            testSetup.SetupHttpContextPaco(controller);

            // Act
            var result = controller.AccionablesPorEjecutar(codPlan, nombreObj, descripAcMej, true);

            // Assert
            Assert.IsNull(result);
        }
        [TestMethod]
        public void AccionablePorEvaluarTinaTest()
        {
            TestSetup testSetup = new TestSetup();
            var mockDb = new Mock<DataIntegradorEntities>();
            int codPlan = 3;
            String nombreObj = "Objetivo 1";
            String descripAcMej = "Accion de Mejora 1";
            mockDb.Setup(m => m.PlanDeMejora.Find(codPlan));

            AccionablesController controller = new AccionablesController(mockDb.Object);
            testSetup.SetupHttpContextTina(controller);

            // Act
            var result = controller.AccionablePorEvaluar(codPlan, nombreObj, descripAcMej);

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void TablaAccionablesTrueTest()
        {
            var controller = new AccionablesController();
            var result = controller.TablaAccionables(1, "Objetivo de plan de mejora - Alexandra", "Accion de mejora de objetivo - Alexandra") as PartialViewResult;
            Assert.AreEqual("_Tabla", result.ViewName);
            controller.Dispose();
        }
        [TestMethod]
        public void TablaAccionablesFalseTest()
        {
            var controller = new AccionablesController();
            var result = controller.TablaAccionables(1, "Objetivo de plan de mejora - Alexandra", "Accion de mejora de objetivo - Alexandra", false) as PartialViewResult;
            Assert.AreEqual("_listarAccionables", result.ViewName);
            controller.Dispose();
        }

        [TestMethod]
        public void TablaAccionablesIsNotNullTest()
        {
            int codPlan = 666;
            String nombreObj = "ObjPrueba";
            String descripAcMej = "AcMejPrueba";

            var controller = new AccionablesController();

            var result = controller.TablaAccionables(codPlan, nombreObj, descripAcMej, false);
            Assert.IsNotNull(result);
            controller.Dispose();
        }

        [TestMethod]
        public void TablasAccionableEditTrue()
        {
            int codPlan = 666;
            String nombreObj = "ObjPrueba";
            String descripAcMej = "AcMejPrueba";
            
            var controller = new AccionablesController();

            var result = controller.TablaAccionables(codPlan, nombreObj, descripAcMej, true);
            Assert.IsNotNull(result);
            controller.Dispose();
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

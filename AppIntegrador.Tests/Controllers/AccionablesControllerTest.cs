using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador;
using AppIntegrador.Controllers;
using System.Web;
using Moq;
using AppIntegrador.Models;
using System.Web.SessionState;
using System.Reflection;
using System.Security.Principal;
using System.IO;
using System.Linq;

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
            var resultado = controller.Create(0, "nombObj", "descrAcMej", "FechaInicio", "FechaFin", true);
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

    }
}

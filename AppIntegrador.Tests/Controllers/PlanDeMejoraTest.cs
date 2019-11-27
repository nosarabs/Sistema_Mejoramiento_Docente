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
using System.Data.Entity;

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

    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador;
using AppIntegrador.Controllers;
using System.Web;

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
        public void CreateNuevo() 
        {
            PlanDeMejoraController controlador = new PlanDeMejoraController();
            ViewResult resultado = controlador.CreateNuevo() as ViewResult;
            Assert.IsNotNull(resultado);
        }

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
        public void TestIndexView()
        {
            //HttpContext context = System.Web.HttpContext.Current;
            
            //// Arrange
            //var controller = new PlanDeMejoraController();

            //// Act
            //ViewResult result = controller.Index("admin") as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }


    }
}

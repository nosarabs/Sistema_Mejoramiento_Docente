﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador;
using AppIntegrador.Controllers;
using System.Web;
using Moq;
using AppIntegrador.Models;


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
    }
}

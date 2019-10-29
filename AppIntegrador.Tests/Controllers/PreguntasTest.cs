﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador.Controllers;
using System.Web.Mvc;

namespace AppIntegrador.Tests.Controllers
{
    /// <summary>
    /// Summary description for PreguntasTest
    /// </summary>
    [TestClass]
    public class PreguntasTest
    {
        public PreguntasTest()
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

        // Historia RIP-CBX
        [TestMethod]
        public void PreguntasGeneralNoNulo()
        {
            PreguntasController preguntas = new PreguntasController();

            ViewResult result = preguntas.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarCrear()
        {
            var controller = new PreguntasController();
            var result = controller.Create() as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        // Historia RIP-CBX
        [TestMethod]
        public void ProbarViewbagGeneral()
        {
            // Arrange
            var controller = new PreguntasController();
            // Act
            ViewResult result = controller.Create() as ViewResult;
            // Assert
            Assert.AreEqual("Crear pregunta", result.ViewBag.Message);
        }
    }
}

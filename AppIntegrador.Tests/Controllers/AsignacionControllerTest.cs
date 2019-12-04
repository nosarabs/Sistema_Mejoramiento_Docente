using System;
using System.Text;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using Moq;
using System.Security.Principal;
using System.Web;
using System.Web.Routing;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.IO;
using System.Web.SessionState;
using System.Reflection;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    class AsignacionControllerTest
    {
        // Constructor
        public AsignacionControllerTest()
        { 

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

        [TestMethod]
        public void TestIndexNotNull()
        {
            AsignacionFormulariosController asignacionController = new AsignacionFormulariosController();
            ViewResult result = asignacionController.Index(null) as ViewResult;
            Assert.IsNotNull(result);
        }


    }
}

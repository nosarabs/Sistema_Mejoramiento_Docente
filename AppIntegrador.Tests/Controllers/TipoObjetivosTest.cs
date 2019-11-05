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
    [TestClass]
    public class TipoObjetivos
    {
        [TestMethod]
        public void IndexNotNullTest()
        {
            TipoObjetivosController toc = new TipoObjetivosController();
            var indexResult = toc.Index();
            Assert.IsNotNull(indexResult);
        }
        [TestMethod]
        public void IndexNameTest()
        {
            var toc = new TipoObjetivosController();
            var indexResult = toc.Index() as ViewResult;
            Assert.AreEqual("Index", indexResult.ViewName);
        }
        //[TestMethod]
        //public void IndexAtLeastThreeTest()
        //{
        //    var toc = new TipoObjetivosController();
        //    var indexResult = toc.Index() as ViewResult;
        //    Assert.IsTrue(indexResult. >= 3);
        //}
    }
}

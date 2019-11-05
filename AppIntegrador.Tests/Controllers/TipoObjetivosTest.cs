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

        [TestMethod]
        public void TestDetailsDataMock()
        {
            var mockdb = new Mock<DataIntegradorEntities>();
            String nombre = "Curso";
            TipoObjetivo to = new TipoObjetivo() { nombre = nombre };
            mockdb.Setup(m => m.TipoObjetivo.Find(nombre)).Returns(to);

            var controller = new TipoObjetivosController(mockdb.Object);
            var result = controller.Details(nombre) as ViewResult;
            Assert.AreEqual(result.Model, to);
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

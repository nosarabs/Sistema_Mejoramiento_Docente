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

//MOS-8 Como Usuario administrativo	quiero poder agregar tipos de objetivos para dar opciones a la hora de crear los objetivos
//Tarea 1: "1. Es necesario agregar un scaffold de las operaciones de CRUD de los tipos de objetivos
//Christian Asch
//Commits: c0d43bd, e4023d4

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
            toc.Dispose();
        }

        [TestMethod]
        public void DetailsNotNullTest()
        {
            String nombre = "Curso";
            var controller = new TipoObjetivosController();
            var result = controller.Details(nombre) as ViewResult;
            Assert.IsNotNull(result);
            controller.Dispose();
        }

        [TestMethod]
        public void DetailsNameTest()
        {
            String nombre = "Curso";
            TipoObjetivosController controller = new TipoObjetivosController();
            var result = controller.Details(nombre) as ViewResult;
            Assert.AreEqual(result.ViewName, "Detalles");
            controller.Dispose();
        }

        [TestMethod]
        public void DetailsNotFoundTest()
        {
            String nombre = "noexiste";
            TipoObjetivosController controller = new TipoObjetivosController();
            var result = controller.Details(nombre) as HttpNotFoundResult;
            Assert.AreEqual(result.StatusCode, 404);
            controller.Dispose();
        }

        [TestMethod]
        public void DetailsDataMockTest()
        {
            var mockdb = new Mock<DataIntegradorEntities>();
            String nombre = "Curso";
            TipoObjetivo to = new TipoObjetivo() { nombre = nombre };
            mockdb.Setup(m => m.TipoObjetivo.Find(nombre)).Returns(to);

            var controller = new TipoObjetivosController(mockdb.Object);
            var result = controller.Details(nombre) as ViewResult;
            Assert.AreEqual(result.Model, to);
            controller.Dispose();
        }

        [TestMethod]
        public void CreateNotNullTest()
        {
            var controller = new TipoObjetivosController();
            var result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
            controller.Dispose();
        }

        [TestMethod]
        public void CreateNameTest()
        {
            var controller = new TipoObjetivosController();
            var result = controller.Create() as ViewResult;
            Assert.AreEqual(result.ViewName, "Crear");
            controller.Dispose();
        }

        [TestMethod]
        public void CreateDataMockTest()
        {
            var mockdb = new Mock<DataIntegradorEntities>();
            String nombre = "Curso";
            TipoObjetivo to = new TipoObjetivo() { nombre = nombre };

            mockdb.Setup(m => m.TipoObjetivo.Add(to));
            mockdb.Setup(m => m.SaveChanges());

            var controller = new TipoObjetivosController(mockdb.Object);
            var result = controller.Create(to);
            Assert.IsNotNull(result);
            controller.Dispose();
        }

        [TestMethod]
        public void CreateIntegrationTest()
        {
            TipoObjetivo tipoObjetivo = new TipoObjetivo();
            tipoObjetivo.nombre = "ParaPruebas";
            var controller = new TipoObjetivosController();
            var result = controller.Create(tipoObjetivo);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditNotNullTest()
        {
            var controller = new TipoObjetivosController();
            String nombre = "Curso";
            var result = controller.Edit(nombre) as ViewResult;
            Assert.IsNotNull(result);
            controller.Dispose();
        }

        [TestMethod]
        public void EditNameTest()
        {
            var controller = new TipoObjetivosController();
            String nombre = "Curso";
            var result = controller.Edit(nombre) as ViewResult;
            Assert.AreEqual(result.ViewName, "Editar");
            controller.Dispose();
        }

        [TestMethod]
        public void EditDataMockTest()
        {
            var mockdb = new Mock<DataIntegradorEntities>();
            String nombre = "Curso";
            TipoObjetivo to = new TipoObjetivo() { nombre = nombre };
            mockdb.Setup(m => m.TipoObjetivo.Find(nombre)).Returns(to);

            var controller = new TipoObjetivosController(mockdb.Object);
            var result = controller.Edit(nombre) as ViewResult;
            Assert.AreEqual(result.Model, to);
            controller.Dispose();
        }

        [TestMethod]
        public void DeleteNotNullTest()
        {
            var controller = new TipoObjetivosController();
            String nombre = "Curso";
            var result = controller.Delete(nombre) as ViewResult;
            Assert.IsNotNull(result);
            controller.Dispose();
        }

        [TestMethod]
        public void DeleteNameTest()
        {
            var controller = new TipoObjetivosController();
            String nombre = "Curso";
            var result = controller.Delete(nombre) as ViewResult;
            Assert.AreEqual(result.ViewName, "Eliminar");
            controller.Dispose();
        }

        [TestMethod]
        public void DeleteDataMockTest()
        {
            var mockdb = new Mock<DataIntegradorEntities>();
            String nombre = "Curso";
            TipoObjetivo to = new TipoObjetivo() { nombre = nombre };
            mockdb.Setup(m => m.TipoObjetivo.Find(nombre)).Returns(to);

            var controller = new TipoObjetivosController(mockdb.Object);
            var result = controller.Delete(nombre) as ViewResult;
            Assert.AreEqual(result.Model, to);
            controller.Dispose();
        }

        [TestMethod]
        public void DeleteConfirmedDataMockTest()
        {
            var mockdb = new Mock<DataIntegradorEntities>();
            String nombre = "Curso";
            TipoObjetivo to = new TipoObjetivo() { nombre = nombre };
            mockdb.Setup(m => m.TipoObjetivo.Find(nombre)).Returns(to);

            var controller = new TipoObjetivosController(mockdb.Object);
            var result = controller.DeleteConfirmed(nombre, true);
            Assert.IsNotNull(result);
            controller.Dispose();
        }

        [TestMethod]
        public void DeleteConfirmedIntegrationTest()
        {
            String nombre = "ParaPruebas";

            var controller = new TipoObjetivosController();
            var result = controller.DeleteConfirmed(nombre, true);
            Assert.IsNotNull(result);
            controller.Dispose();
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

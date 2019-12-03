using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using AppIntegrador.Models;
using AppIntegrador.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.IO;
using System.Web.Routing;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class CSVcontrollerTest
    {
        // COD-70: Yo como administrador quiero almacenar los datos de un archivo CSV en el sistema
        // Tarea técnica: Cargar datos en blanco con el dato anteriomente registrado

     
        [TestMethod]
        public void TestCargarEstudiantes()
        {
            // Arrange
            //Creamos httpContextMock and serverMock:
            var httpContextMock = new Mock<HttpContextBase>();
            var serverMock = new Mock<HttpServerUtilityBase>();
            //Mock el path original
            serverMock.Setup(x => x.MapPath("./ArchivoCSV")).Returns(Path.GetFullPath("../../ArchivoCSVtest"));

            //Mockeamos el objeto HTTPContext.Server con el servidor ya mockeado:
            httpContextMock.Setup(x => x.Server).Returns(serverMock.Object);

            //Creo el controlador y su contexto
            CSVController controller = new CSVController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);

            //Insertamos el csv 
            string filePath = Path.GetFullPath("../../ArchivoCSVtest/listaEstudianteTest.csv");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            //Cargamos el mock
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.FileName).Returns("listaEstudianteTest.csv");
            uploadedFile.Setup(x => x.ContentType).Returns("listaEstudianteTest.csv");
            uploadedFile.Setup(x => x.InputStream).Returns(fileStream);
            HttpPostedFileBase[] httpPostedFileBases = { uploadedFile.Object };
            // Act
            ViewResult result = controller.Index(httpPostedFileBases[0]) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void TestCargarFuncionarios()
        {
            // Arrange
            //Creamos httpContextMock and serverMock:
            var httpContextMock = new Mock<HttpContextBase>();
            var serverMock = new Mock<HttpServerUtilityBase>();
            //Mock el path original
            serverMock.Setup(x => x.MapPath("./ArchivoCSV")).Returns(Path.GetFullPath("../../ArchivoCSVtest"));

            //Mockeamos el objeto HTTPContext.Server con el servidor ya mockeado:
            httpContextMock.Setup(x => x.Server).Returns(serverMock.Object);

            //Creo el controlador y su contexto
            CSVController controller = new CSVController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);

            //Insertamos el csv 
            string filePath = Path.GetFullPath("../../ArchivoCSVtest/listaFuncionarioTest.csv");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            //Cargamos el mock
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.FileName).Returns("listaFuncionarioTest.csv");
            uploadedFile.Setup(x => x.ContentType).Returns("listaFuncionarioTest.csv");
            uploadedFile.Setup(x => x.InputStream).Returns(fileStream);
            HttpPostedFileBase[] httpPostedFileBases = { uploadedFile.Object };
            // Act
            ViewResult result = controller.Funcionarios(httpPostedFileBases[0]) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCargarClase()
        {
            // Arrange
            //Creamos httpContextMock and serverMock:
            var httpContextMock = new Mock<HttpContextBase>();
            var serverMock = new Mock<HttpServerUtilityBase>();
            //Mock el path original
            serverMock.Setup(x => x.MapPath("./ArchivoCSV")).Returns(Path.GetFullPath("../../ArchivoCSVtest"));

            //Mockeamos el objeto HTTPContext.Server con el servidor ya mockeado:
            httpContextMock.Setup(x => x.Server).Returns(serverMock.Object);

            //Creo el controlador y su contexto
            CSVController controller = new CSVController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);

            //Insertamos el csv 
            string filePath = Path.GetFullPath("../../ArchivoCSVtest/listaClaseTest.csv");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            //Cargamos el mock
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.FileName).Returns("listaClaseTest.csv");
            uploadedFile.Setup(x => x.ContentType).Returns("listaClaseTest.csv");
            uploadedFile.Setup(x => x.InputStream).Returns(fileStream);
            HttpPostedFileBase[] httpPostedFileBases = { uploadedFile.Object };
            // Act
            ViewResult result = controller.Clase(httpPostedFileBases[0]) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCargarGuia()
        {
            // Arrange
            //Creamos httpContextMock and serverMock:
            var httpContextMock = new Mock<HttpContextBase>();
            var serverMock = new Mock<HttpServerUtilityBase>();
            //Mock el path original
            serverMock.Setup(x => x.MapPath("./ArchivoCSV")).Returns(Path.GetFullPath("../../ArchivoCSVtest"));

            //Mockeamos el objeto HTTPContext.Server con el servidor ya mockeado:
            httpContextMock.Setup(x => x.Server).Returns(serverMock.Object);

            //Creo el controlador y su contexto
            CSVController controller = new CSVController();
            controller.ControllerContext = new ControllerContext(httpContextMock.Object, new RouteData(), controller);

            //Insertamos el csv 
            string filePath = Path.GetFullPath("../../ArchivoCSVtest/guiaTest.csv");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            //Cargamos el mock
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.FileName).Returns("guiaTest.csv");
            uploadedFile.Setup(x => x.ContentType).Returns("guiaTest.csv");
            uploadedFile.Setup(x => x.InputStream).Returns(fileStream);
            HttpPostedFileBase[] httpPostedFileBases = { uploadedFile.Object };
            // Act
            ViewResult result = controller.GuiaHorarios(httpPostedFileBases[0]) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }






        /*
        [TestMethod]
        public void TestCarga()
        {
            // Arrange
            //Creo el controlador y su contexto
            CSVController controller = new CSVController();
            var file = @"../../Guias Horarios/guiaPrueba.csv";
            // Act
            var result = controller.cargarGuia(file);
            // Assert
            Assert.IsTrue(result);
        }
        */
    }
}

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

       // Cuando se cree el modulo de cargar archivos CSV se debe modificar el tests
        [TestMethod]
        public void TestMethod1()
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
            string filePath = Path.GetFullPath("../../ArchivoCSVtest/prueba.csv");
            FileStream fileStream = new FileStream(filePath, FileMode.Open);

            //Cargamos el mock
            Mock<HttpPostedFileBase> uploadedFile = new Mock<HttpPostedFileBase>();
            uploadedFile.Setup(x => x.FileName).Returns("pruebas.csv");
            uploadedFile.Setup(x => x.ContentType).Returns(".csv");
            uploadedFile.Setup(x => x.InputStream).Returns(fileStream);
            HttpPostedFileBase[] httpPostedFileBases = { uploadedFile.Object };


            // Act
            ViewResult result = controller.Index(httpPostedFileBases[0],1) as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestCarga()
        {
            // Arrange
            //Creo el controlador y su contexto
            CSVController controller = new CSVController();
            var file = @"../../ArchivoCSVtest/prueba.csv";
            // Act
            var result = controller.carga(file);
            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        //Este test se encarga de verificar que se valida la longitud del codigo de unidad academica
        public void TestValidaUnidad()
        {
            CSVController controller = new CSVController();
            ArchivoCSV prueba_malo = new ArchivoCSV();
            prueba_malo.CodigoUnidad = "999999999999999999999999999";
            var result = controller.validarEntradas(prueba_malo);
            Assert.IsFalse(result);
        }

        [TestMethod]
        //Este test se encarga de verificar que se valida que el valor de un grupo es numerico
        public void TestGrupoNumero()
        {
            CSVController controller = new CSVController();
            ArchivoCSV prueba_malo = new ArchivoCSV();
            prueba_malo.NumeroGrupo = "Texto";
            var result = controller.validarEntradas(prueba_malo);
            Assert.IsFalse(result);
        }

        [TestMethod]
        //Este test se encarga de verificar que se valida que el valor de un grupo es positivo
        public void TestGrupoPositivo()
        {
            CSVController controller = new CSVController();
            ArchivoCSV prueba_malo = new ArchivoCSV();
            prueba_malo.NumeroGrupo = "-10";
            var result = controller.validarEntradas(prueba_malo);
            Assert.IsFalse(result);
        }

        [TestMethod]
        //Este test se encarga de verificar que se valida que el valor de un semestre no es mayor a 3
        public void TestSemestreMayor()
        {
            CSVController controller = new CSVController();
            ArchivoCSV prueba_malo = new ArchivoCSV();
            prueba_malo.Semestre = "100";
            var result = controller.validarEntradas(prueba_malo);
            Assert.IsFalse(result);
        }

        [TestMethod]
        //Este test se encarga de verificar que se valida que el valor la sigla de un curso no es mayor a 50 caracteres
        public void TestValidaSigla()
        {
            CSVController controller = new CSVController();
            ArchivoCSV prueba_malo = new ArchivoCSV();
            prueba_malo.SiglaCurso = "123456371939573719563751637218362463628818191919343CI1111111";
            var result = controller.validarEntradas(prueba_malo);
            Assert.IsFalse(result);
        }
    }
}

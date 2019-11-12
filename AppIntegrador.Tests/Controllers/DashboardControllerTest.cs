using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using Moq;
using System.Data.Entity.Core.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class DashboardControllerTest
    {
        [TestMethod]
        public void ObtenerFormulariosTest()
        {
            //Arrange

            //Se crea el mock de la base de datos
            var mockDb = new Mock<DataIntegradorEntities>();

            //Se instancia el controlador y se le pasa como parámetro el mock
            DashboardController controller = new DashboardController(mockDb.Object);

            //Se inicializan los parámetros que se envian al DashboardController
            string codigoUA = null;
            string codigoCarrera = null;
            string codigoEnfasis = null;
            string siglaCurso = null;
            Nullable<byte> numeroGrupo = null;
            Nullable<byte> semestre = null;
            Nullable<int> anno = null;
            string correoProfesor = null;

            //Se crear un formulario como dummy data
            ObtenerFormulariosFiltros_Result formulario = new ObtenerFormulariosFiltros_Result
            {
                FCodigo = "00000001",
                FNombre = "Formulario de prueba",
                CSigla = "CI0128",
                GNumero = 1,
                GSemestre = 2,
                GAnno = 2,
                FechaInicio = DateTime.Parse("09/06/2019"),
                FechaFin = DateTime.Parse("11/06/2019")
            };

            //Se hace el mock del procedimiento almacenado que utiliza el método del controlador
            var mockedObjectResult = new Mock<IQueryable<ObtenerFormulariosFiltros_Result>>();
            var dummyData = new List<ObtenerFormulariosFiltros_Result> { formulario };
            mockedObjectResult.Setup(x => x.GetEnumerator()).Returns(dummyData.GetEnumerator());
            mockDb.Setup(x => x.ObtenerFormulariosFiltros(codigoUA, codigoCarrera, codigoEnfasis, siglaCurso, numeroGrupo, semestre, anno, correoProfesor)).Returns(mockedObjectResult.Object);

            //Act

            //Se hace el llamado al controlador y se obtiene el JSON
            String formulariosJson = controller.ObtenerFormularios(codigoUA, codigoCarrera, codigoEnfasis, siglaCurso, numeroGrupo, semestre, anno, correoProfesor);

            //Se deserializa el JSON
            var formularios = JsonConvert.DeserializeObject<List<ObtenerFormulariosFiltros_Result>>(formulariosJson, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });

            //Assert

            //Se comparan los miembros del formulario dummy con los del formulario retornado por el controlador
            Assert.AreEqual(dummyData[0].FCodigo, formularios[0].FCodigo);
            Assert.AreEqual(dummyData[0].FNombre, formularios[0].FNombre);
            Assert.AreEqual(dummyData[0].CSigla, formularios[0].CSigla);
            Assert.AreEqual(dummyData[0].GNumero, formularios[0].GNumero);
            Assert.AreEqual(dummyData[0].GSemestre, formularios[0].GSemestre);
            Assert.AreEqual(dummyData[0].GAnno, formularios[0].GAnno);
            Assert.AreEqual(dummyData[0].FechaInicio, formularios[0].FechaInicio);
            Assert.AreEqual(dummyData[0].FechaFin, formularios[0].FechaFin);
        }
    }
}

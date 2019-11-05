using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using AppIntegrador.Models;
using AppIntegrador.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            CSVController controller = new CSVController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }
    }
}

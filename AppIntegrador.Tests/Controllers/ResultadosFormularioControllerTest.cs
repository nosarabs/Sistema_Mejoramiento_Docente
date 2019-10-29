using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador;
using AppIntegrador.Controllers;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class ResultadosFormularioControllerTest
    {
        [TestMethod]
        public void TestTipoNotNull()
        {
            //Arrange
            ResultadosFormularioController controller = new ResultadosFormularioController();
            String codFormulario = "00000001";

            //Act
            String result = controller.GetTipoPregunta(codFormulario);

            //Assert
            Assert.IsNotNull(result);
        }

    }
}

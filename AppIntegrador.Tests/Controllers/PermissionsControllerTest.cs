using AppIntegrador;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppIntegrador.Tests.Controllers
{
    /* TAM 3.7 TaskId - 2 Prueba de unidad para cargar perfiles que tiene un usuario*/
    [TestClass]
    public class PermissionsControllerTest
    {
        [TestMethod]
        public void CargarPerfil()
        {
            PermissionsController controller = new PermissionsController();
            CurrentUser.Username = "admin@mail.com";
            JsonResult perfiles = controller.CargarPerfil();
            string test = "{\"data\":[\"Superusuario\"]}";
            string result = new JavaScriptSerializer().Serialize(perfiles.Data);

            Assert.AreEqual(test, result);
        }
    }
}

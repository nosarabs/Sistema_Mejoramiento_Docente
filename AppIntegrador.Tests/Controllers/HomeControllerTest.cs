using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using System.Threading.Tasks;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Login()
        {
            Usuario usuario = new Usuario();
            usuario.Username = "berta@mail.com";
            usuario.Password = "fsdfsfs";
            usuario.Activo = true;

            HomeController controller = new HomeController();

            Task<ActionResult> result = controller.Login(usuario);
            Assert.IsNotNull(result);
            /*
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);*/
        }
        /*
        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }*/
    }
}

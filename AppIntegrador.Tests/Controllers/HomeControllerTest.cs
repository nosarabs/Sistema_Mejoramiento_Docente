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
using System.Web.Security;
using Security.Authentication;
using Moq;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void LoginBlock()
        {
            DataIntegradorEntities db = new DataIntegradorEntities();
            Usuario usuario = new Usuario();
            usuario.Username = "berta@mail.com";
            usuario.Password = "fsdfsfs";
            usuario.Activo = true;

            HomeController controller = new HomeController();
            Task<ActionResult> result;

            result = controller.Login(usuario);

            //Assert.AreNotEqual("Index", result.Result.ViewName);
            /*

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);*/
        }

        /* TAM-1.1.6 Redirección Login */
        [TestMethod]
        public void LoginView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PasswordResetView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.PasswordReset() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LoginAdminSuccess()
        {
            // Arrange
            var formsAuthMock = new Mock<IAuth>();
            HomeController controller = new HomeController(formsAuthMock.Object);

            Usuario usuario = new Usuario();
            usuario.Username = "admin@mail.com";
            usuario.Password = "admin@mail.com";
            usuario.Activo = true;

            Task<ActionResult> result = controller.Login(usuario);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LoginAdminFailure()
        {
            // Arrange
            var formsAuthMock = new Mock<IAuth>();
            HomeController controller = new HomeController(formsAuthMock.Object);

            Usuario usuario = new Usuario();
            usuario.Username = "admin@mail.com";
            usuario.Password = "wrongOne";
            usuario.Activo = true;

            Task<ActionResult> result = controller.Login(usuario);

            Assert.IsNotNull(result);
        }
        /*Termina TAM-1.1.6 Redirección Login*/

        [TestMethod]
        public async Task CambiarContrasenna()
        {
            var formsAuthMock = new Mock<IAuth>();
            HomeController hc = new HomeController(formsAuthMock.Object);

            string contrasennaActual = "admin@mail.com";
            string contrasennaNueva = "test";
            string contrasennaConfirmar = "test";
            Usuario usuario = new Usuario();
            usuario.Username = "admin@mail.com";
            usuario.Password = "admin@mail.com";
            usuario.Activo = true;

            var login = await hc.Login(usuario);
            var result = await hc.CambiarContrasenna(contrasennaActual, contrasennaNueva, contrasennaConfirmar) as ViewResult;

            Assert.IsNotNull(result.ViewBag.NotifyTitle);
        }
    }
}



using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using Security.Authentication;
using Moq;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.Security.Principal;
using System.IO;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
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

            Usuario usuario = new Usuario
            {
                Username = "admin@mail.com",
                Password = "admin@mail.com",
                Activo = true
            };

            var result = controller.Login(usuario) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        /*Termina TAM-1.1.6 Redirección Login*/

        [TestMethod]
        public void LoginAdminFail()
        {
            // Arrange
            var formsAuthMock = new Mock<IAuth>();
            HomeController controller = new HomeController(formsAuthMock.Object);

            Usuario usuario = new Usuario
            {
                Username = "admin@mail.com",
                Password = "wrong",
                Activo = true
            };

            var result = controller.Login(usuario) as ViewResult;
            Assert.AreEqual("Login", result.ViewName);
        }
        /*Termina TAM-1.1.6 Redirección Login*/

        [TestMethod]
        public void LoginBlock()
        {
            // Arrange
            Usuario usuario = new Usuario
            {
                Username = "tina@mail.com",
                Password = "wrong",
                Activo = true
            };

            using (var context = new DataIntegradorEntities())
            {
                var user = context.Usuario.SingleOrDefault(u => u.Username == usuario.Username);
                if (user != null)
                {
                    user.Activo = true;
                    context.SaveChanges();
                }
            }

            var formsAuthMock = new Mock<IAuth>();
            HomeController controller = new HomeController(formsAuthMock.Object);



            //Se indica que el usuario ya ha fallado 2 veces el password
            //HttpContext.Current.Application["test"] = 2;
            //var apstate = new HttpApplicationState("test");
            //var HCresult = (int)HttpContext.Current.Application["test"];
            CurrentUser.setLoginFailures(2);
            controller.Login(usuario);     
            var result = controller.Login(usuario) as ViewResult;
            var testres = controller.ModelState["password"].Errors[0].ErrorMessage;
            string expectedresult = "Este usuario está bloqueado temporalmente.\nIntente de nuevo más tarde.";

            using (var context = new DataIntegradorEntities())
            {
                var user = context.Usuario.SingleOrDefault(u => u.Username == usuario.Username);
                if (user != null)
                {
                    user.Activo = true;
                    context.SaveChanges();
                }
            }

            Assert.AreEqual(expectedresult, testres);

        }


        [TestInitialize]
        public void Init()
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new HttpRequest("", "http://localhost/", "");

            // Step 2: Setup the HTTP Response
            var httpResponce = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            var httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer =
                new HttpSessionStateContainer("admin@mail.com",
                                               new SessionStateItemCollection(),
                                               new HttpStaticObjectsCollection(),
                                               10,
                                               true,
                                               HttpCookieMode.AutoDetect,
                                               SessionStateMode.InProc,
                                               false);
            httpContext.Items["AspSession"] =
                typeof(HttpSessionState)
                .GetConstructor(
                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            var fakeIdentity = new GenericIdentity("admin@mail.com");
            var principal = new GenericPrincipal(fakeIdentity, null);

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
            HttpContext.Current.User = principal;
        }

        [TestCleanup]
        public void Cleanup()
        {
            //Nos aseguramos que admin quede deslogeado despues de cada test.
            CurrentUser.deleteCurrentUser("admin@mail.com");
        }
    }
}



using AppIntegrador;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Security.Principal;
using System.IO;
using System.Web.Helpers;
using Security.Authentication;
using Moq;
using System.Web.SessionState;
using System.Reflection;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void UsersIndexNotNull()
        {
            UsersController controller = new UsersController();

            //HttpContext.Current.Session.Add("Username", "admin@mail.com");
            //HttpContext.Current.Session.Add("Profile", "Superusuario");
            //HttpContext.Current.Session.Add("Profile", "0000000001");
            //HttpContext.Current.Session.Add("EmphasisId", "0000000001");
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");

            Assert.IsNotNull(controller.Index());
        }

        [TestInitialize]
        public void TestSetup()
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
    }
}

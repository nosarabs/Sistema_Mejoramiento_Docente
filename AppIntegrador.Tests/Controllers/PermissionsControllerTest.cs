using AppIntegrador;
using System.Data.Entity;
using AppIntegrador.Controllers;
using AppIntegrador.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using EntityFramework.Testing;

namespace AppIntegrador.Tests.Controllers
{
    /* TAM 3.7 TaskId - 2 Prueba de unidad para cargar perfiles que tiene un usuario*/
    [TestClass]
    public class PermissionsControllerTest
    {
        [TestMethod]
        public void TestCargarPerfil()
        {
            PermissionsController controller = new PermissionsController();
            Init();
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");
            JsonResult perfiles = controller.CargarPerfil();
            string test = "{\"data\":[\"Superusuario\"]}";
            string result = new JavaScriptSerializer().Serialize(perfiles.Data);

            Assert.AreEqual(test, result);
        }

        [TestMethod]
        public void TestCargarUsuariosPermisos()
        {
            PermissionsController controller = new PermissionsController();
            Init();
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");
            JsonResult checkboxes = controller.CargarCheckboxes("8", CurrentUser.getUserProfile(), "0000000001", "0000000001");
            string test = "{\"persons\":[{\"PersonMail\":\"bakers@mail.com\",\"Checked\":false},{\"PersonMail\":\"mosqueteros@mail.com\",\"Checked\":false},{\"PersonMail\":\"rip@mail.com\",\"Checked\":false},{\"PersonMail\":\"tamales@mail.com\",\"Checked\":false},{\"PersonMail\":\"ed@mail.com\",\"Checked\":false},{\"PersonMail\":\"DA@mail.com\",\"Checked\":false},{\"PersonMail\":\"denisse@mail.com\",\"Checked\":false},{\"PersonMail\":\"christian.asch4@gmail.com\",\"Checked\":false},{\"PersonMail\":\"AC@mail.com\",\"Checked\":false},{\"PersonMail\":\"kirsten@mail.com\",\"Checked\":false},{\"PersonMail\":\"DE@mail.com\",\"Checked\":false},{\"PersonMail\":\"daniel@mail.com\",\"Checked\":false},{\"PersonMail\":\"tina@mail.com\",\"Checked\":false},{\"PersonMail\":\"ismael@mail.com\",\"Checked\":false},{\"PersonMail\":\"marcelo.jenkins@ucr.ac.cr\",\"Checked\":false},{\"PersonMail\":\"alexandra.martinez@ucr.ac.cr\",\"Checked\":false},{\"PersonMail\":\"andres@mail.com\",\"Checked\":false},{\"PersonMail\":\"al@mail.com\",\"Checked\":false},{\"PersonMail\":\"cristian.quesadalopez@ucr.ac.cr\",\"Checked\":false},{\"PersonMail\":\"ericrios24@gmail.com\",\"Checked\":false},{\"PersonMail\":\"berta@mail.com\",\"Checked\":false},{\"PersonMail\":\"DC@mail.com\",\"Checked\":false},{\"PersonMail\":\"josue@mail.com\",\"Checked\":false}],\"permissions\":[{\"PermissionName\":\"Ver usuarios\",\"PermissionCode\":101,\"Checked\":true},{\"PermissionName\":\"Crear usuarios\",\"PermissionCode\":102,\"Checked\":true},{\"PermissionName\":\"Ver detalles de usuarios\",\"PermissionCode\":103,\"Checked\":true},{\"PermissionName\":\"Editar usuarios\",\"PermissionCode\":104,\"Checked\":true},{\"PermissionName\":\"Borrar usuarios\",\"PermissionCode\":105,\"Checked\":true},{\"PermissionName\":\"Crear formularios\",\"PermissionCode\":201,\"Checked\":true},{\"PermissionName\":\"Ver formularios\",\"PermissionCode\":202,\"Checked\":true},{\"PermissionName\":\"Crear secciones\",\"PermissionCode\":203,\"Checked\":true},{\"PermissionName\":\"Ver secciones\",\"PermissionCode\":204,\"Checked\":true},{\"PermissionName\":\"Crear preguntas\",\"PermissionCode\":205,\"Checked\":true},{\"PermissionName\":\"Ver preguntas\",\"PermissionCode\":206,\"Checked\":true},{\"PermissionName\":\"Ver planes de mejora\",\"PermissionCode\":301,\"Checked\":true},{\"PermissionName\":\"Crear planes de mejora\",\"PermissionCode\":302,\"Checked\":true},{\"PermissionName\":\"Editar planes de mejora\",\"PermissionCode\":303,\"Checked\":true},{\"PermissionName\":\"Borrar planes de mejora\",\"PermissionCode\":304,\"Checked\":true},{\"PermissionName\":\"Crear objetivos\",\"PermissionCode\":305,\"Checked\":true},{\"PermissionName\":\"Ver objetivos\",\"PermissionCode\":306,\"Checked\":true},{\"PermissionName\":\"Editar objetivos\",\"PermissionCode\":307,\"Checked\":true},{\"PermissionName\":\"Borrar objetivos\",\"PermissionCode\":308,\"Checked\":true},{\"PermissionName\":\"Crear acciones de mejora\",\"PermissionCode\":309,\"Checked\":true},{\"PermissionName\":\"Ver acciones de mejora\",\"PermissionCode\":310,\"Checked\":true},{\"PermissionName\":\"Editar acciones de mejora\",\"PermissionCode\":311,\"Checked\":true},{\"PermissionName\":\"Borrar acciones de mejora\",\"PermissionCode\":312,\"Checked\":true},{\"PermissionName\":\"Ver respuestas de formularios\",\"PermissionCode\":401,\"Checked\":true}]}";
            string result = new JavaScriptSerializer().Serialize(checkboxes.Data);

            Assert.AreEqual(test, result);
        }

        [TestMethod]
        public void TestCargarCheckboxesNull()
        {
            PermissionsController controller = new PermissionsController();
            JsonResult checkboxes = controller.CargarCheckboxes(null, null, "0000000001", "0000000001");
            string result = new JavaScriptSerializer().Serialize(checkboxes.Data);
            string expected = "{\"persons\":\"\",\"permissions\":\"\"}";
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestEntrarCargarUsuariosSinPermiso()
        {
            
            CurrentUser.setCurrentUser("andres@mail.com", "Estudiante", "0000000001", "0000000001");
            var httpContext = new HttpContext(
                new HttpRequest("", "http://localhost:44334/Home/Login", ""),
                new HttpResponse(new StringWriter())
            );
            var tempData = new TempDataDictionary();
            //tempData["SessionVariable"] = "admin";
            PermissionsController controller = new PermissionsController()
            {
                TempData = tempData
            };
            RedirectToRouteResult result = controller.Index() as RedirectToRouteResult;
            System.Web.Routing.RouteValueDictionary dictionary = new System.Web.Routing.RouteValueDictionary();
            dictionary.Add("action", "Index");
            dictionary.Add("controller", "Home");
            RedirectToRouteResult expected = new RedirectToRouteResult(dictionary);
            Assert.AreEqual(controller.TempData["alertmessage"], "No tiene permisos para acceder a esta página.");
            Assert.AreEqual(result.RouteValues["action"], expected.RouteValues["action"]);
            Assert.AreEqual(result.RouteValues["controller"], expected.RouteValues["controller"]);
            CurrentUser.deleteCurrentUser("andres@mail.com");
        }

        [TestMethod]
        public void TestIndexNotNull()
        {
            PermissionsController controller = new PermissionsController();
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");
            Assert.IsNotNull(controller.Index());
        }

        [TestMethod]
        public void TestSeleccionarPerfilNotNull()
        {
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");
            PermissionsController controller = new PermissionsController();
            Assert.IsNotNull(controller.SeleccionarPerfil());
        }

        [TestMethod]
        public void TestGuardarSeleccion()
        {
            PermissionsController controller = new PermissionsController();
            CurrentUser.setCurrentUser("andres@mail.com", "Estudiante", "0000000001", "0000000001");
            controller.GuardarSeleccion("Profesor", "0000000001", "0000000001");
            Assert.AreEqual(CurrentUser.getUserProfile(), "Profesor");
            Assert.AreEqual(CurrentUser.getUserMajorId(), "0000000001");
            Assert.AreEqual(CurrentUser.getUserEmphasisId(), "0000000001");
        }

        [TestMethod]
        public void TestCargarEnfasisDeCarrera()
        {
            PermissionsController controller = new PermissionsController();
            JsonResult json = controller.CargarEnfasisDeCarrera("0000000001");
            string result = new JavaScriptSerializer().Serialize(json.Data);
            string expected = "{\"data\":[\"0000000000,Tronco Común\",\"0000000001,Ingeniería de Software\",\"0000000002,Ciencias de la Computación\",\"0000000003,Tecnologías de la Información\"]}";
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestCargarDatosDefault()
        {
            string correo = "admin@mail.com";
            var persona = new Persona()
            {
                Correo = correo,
                Identificacion = "123456781",
                Apellido1 = "Admin",
                Nombre1 = "Admin",
                TipoIdentificacion = "Cédula"
            };

            var usuario = new Usuario()
            {
                Activo = true
            };

            var carreras = new List<Carrera>
            {
                new Carrera() { Codigo = "0000000001", Nombre = "Bachillerato en Computación" }
            };

            var enfasis = new List<Enfasis>
            {
                new Enfasis() { CodCarrera = "0000000001", Codigo = "0000000000", Nombre = "Tronco Común" },
                new Enfasis() { CodCarrera = "0000000001", Codigo = "0000000001", Nombre = "Ingeniería de Software" }
            };

            var perfiles = new List<Perfil>
            {
                new Perfil() { Nombre = "Administrador" },
                new Perfil() { Nombre = "Director" }
            };

            var database = new Mock<DataIntegradorEntities>();

            database.Setup(m => m.Persona.Find(correo)).Returns(persona);
            database.Setup(m => m.Usuario.Find(correo)).Returns(usuario);

            database.Setup(m => m.Carrera).Returns(new Mock<DbSet<Carrera>>().SetupData(carreras, o => {
                return carreras.Single(x => x.Codigo == (string)o.First());
            }).Object);

            database.Setup(m => m.Enfasis).Returns(new Mock<DbSet<Enfasis>>().SetupData(enfasis, o => {
                return enfasis.Single(x => x.CodCarrera == (string)o.First() && x.Codigo == (string)o.First());
            }).Object);

            database.Setup(m => m.Perfil).Returns(new Mock<DbSet<Perfil>>().SetupData(perfiles, o => {
                return perfiles.Single(x => x.Nombre == (string)o.First());
            }).Object);

            Init();
            CurrentUser.setCurrentUser("admin@mail.com", "Administrador", "0000000001", "0000000000");

            PermissionsController controller = new PermissionsController(database.Object);
            JsonResult json = controller.CargarDatosDefault();
            string result = new JavaScriptSerializer().Serialize(json.Data);
            string expected = "{\"defaultProfile\":\"Administrador\",\"defaultMajor\":\"0000000001,Bachillerato en Computación\",\"defaultEmphasis\":\"0000000001,Ingeniería de Software\"}";
            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void TestGuardarPermisosSinPermiso()
        {
            CurrentUser.setCurrentUser("andres@mail.com", "Estudiante", "0000000001", "0000000001");
            var httpContext = new HttpContext(
                new HttpRequest("", "http://localhost:44334/Home/Login", ""),
                new HttpResponse(new StringWriter())
            );
            var tempData = new TempDataDictionary();
            PermissionsController controller = new PermissionsController()
            {
                TempData = tempData
            };
            PermissionsViewHolder model = new PermissionsViewHolder();
            RedirectToRouteResult result = controller.GuardarPermisos(model, false) as RedirectToRouteResult;
            System.Web.Routing.RouteValueDictionary dictionary = new System.Web.Routing.RouteValueDictionary();
            dictionary.Add("action", "../Home/Index");
            RedirectToRouteResult expected = new RedirectToRouteResult(dictionary);
            Assert.AreEqual(controller.TempData["alertmessage"], "No tiene permisos para acceder a esta página.");
            Assert.AreEqual(result.RouteValues["action"], expected.RouteValues["action"]);
            CurrentUser.deleteCurrentUser("andres@mail.com");
        }

        [TestInitialize]
        public void Init()
        {
            //No aseguramos que admin no haya quedado logeado por otros tests.
            CurrentUser.deleteCurrentUser("admin@mail.com");

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

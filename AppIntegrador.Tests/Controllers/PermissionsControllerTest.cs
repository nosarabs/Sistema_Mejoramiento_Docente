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
            string test = "{\"persons\":[{\"PersonMail\":\"rip@mail.com\",\"Checked\":false},{\"PersonMail\":\"denisse@mail.com\",\"Checked\":false},{\"PersonMail\":\"kirsten@mail.com\",\"Checked\":false},{\"PersonMail\":\"daniel@mail.com\",\"Checked\":false},{\"PersonMail\":\"tina@mail.com\",\"Checked\":false},{\"PersonMail\":\"ismael@mail.com\",\"Checked\":false},{\"PersonMail\":\"andres@mail.com\",\"Checked\":false},{\"PersonMail\":\"cristian.quesadalopez@ucr.ac.cr\",\"Checked\":false},{\"PersonMail\":\"ericrios24@gmail.com\",\"Checked\":false},{\"PersonMail\":\"berta@mail.com\",\"Checked\":false},{\"PersonMail\":\"josue@mail.com\",\"Checked\":false}],\"permissions\":[{\"PermissionName\":\"Ver usuarios\",\"PermissionCode\":101,\"Checked\":true},{\"PermissionName\":\"Crear usuarios\",\"PermissionCode\":102,\"Checked\":true},{\"PermissionName\":\"Ver detalles de usuarios\",\"PermissionCode\":103,\"Checked\":true},{\"PermissionName\":\"Editar usuarios\",\"PermissionCode\":104,\"Checked\":true},{\"PermissionName\":\"Borrar usuarios\",\"PermissionCode\":105,\"Checked\":true},{\"PermissionName\":\"Crear formularios\",\"PermissionCode\":201,\"Checked\":true},{\"PermissionName\":\"Ver formularios\",\"PermissionCode\":202,\"Checked\":true},{\"PermissionName\":\"Crear secciones\",\"PermissionCode\":203,\"Checked\":true},{\"PermissionName\":\"Ver secciones\",\"PermissionCode\":204,\"Checked\":true},{\"PermissionName\":\"Crear preguntas\",\"PermissionCode\":205,\"Checked\":true},{\"PermissionName\":\"Ver preguntas\",\"PermissionCode\":206,\"Checked\":true},{\"PermissionName\":\"Ver planes de mejora\",\"PermissionCode\":301,\"Checked\":true},{\"PermissionName\":\"Crear planes de mejora\",\"PermissionCode\":302,\"Checked\":true},{\"PermissionName\":\"Editar planes de mejora\",\"PermissionCode\":303,\"Checked\":true},{\"PermissionName\":\"Borrar planes de mejora\",\"PermissionCode\":304,\"Checked\":true},{\"PermissionName\":\"Crear objetivos\",\"PermissionCode\":305,\"Checked\":true},{\"PermissionName\":\"Ver objetivos\",\"PermissionCode\":306,\"Checked\":true},{\"PermissionName\":\"Editar objetivos\",\"PermissionCode\":307,\"Checked\":true},{\"PermissionName\":\"Borrar objetivos\",\"PermissionCode\":308,\"Checked\":true},{\"PermissionName\":\"Crear acciones de mejora\",\"PermissionCode\":309,\"Checked\":true},{\"PermissionName\":\"Ver acciones de mejora\",\"PermissionCode\":310,\"Checked\":true},{\"PermissionName\":\"Editar acciones de mejora\",\"PermissionCode\":311,\"Checked\":true},{\"PermissionName\":\"Borrar acciones de mejora\",\"PermissionCode\":312,\"Checked\":true},{\"PermissionName\":\"Ver respuestas de formularios\",\"PermissionCode\":401,\"Checked\":true}]}";
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
        public void EntrarCargarUsuariosSinPermiso()
        {

            CurrentUser.setCurrentUser("andres@mail.com", "Estudiante", "0000000001", "0000000001");
            var httpContext = new HttpContext(
                new HttpRequest("", "http://localhost:44334/Home/Login", ""),
                new HttpResponse(new StringWriter())
            );
            var tempData = new TempDataDictionary();
            tempData["SessionVariable"] = "admin";
            PermissionsController controller = new PermissionsController()
            {
                TempData = tempData
            };
            RedirectToRouteResult result = controller.Index() as RedirectToRouteResult;
            System.Web.Routing.RouteValueDictionary dictionary = new System.Web.Routing.RouteValueDictionary();
            dictionary.Add("action", "Index");
            dictionary.Add("controller", "Home");
            RedirectToRouteResult expected = new RedirectToRouteResult(dictionary);
            Assert.AreEqual(tempData["alertmessage"], "No tiene permisos para acceder a esta página.");
            Assert.AreEqual(result.RouteValues["action"], expected.RouteValues["action"]);
            Assert.AreEqual(result.RouteValues["controller"], expected.RouteValues["controller"]);
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

        [TestInitialize]
        public void Init()
        {
            HttpContext.Current = new HttpContext(
            new HttpRequest("", "http://localhost:44334/Home/Login", ""),
            new HttpResponse(new StringWriter())
            );

            // User is logged in
            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity("admin@mail.com"),
                new string[0]
                );

            // User is logged out
            HttpContext.Current.User = new GenericPrincipal(
                new GenericIdentity(String.Empty),
                new string[0]
                );
        }
    }
}

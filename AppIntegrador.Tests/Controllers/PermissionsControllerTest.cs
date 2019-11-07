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
        public void TestCargarPerfil()
        {
            PermissionsController controller = new PermissionsController();
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
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");
            JsonResult checkboxes = controller.CargarCheckboxes("8", CurrentUser.getUserProfile(), "0000000001", "0000000001");
            string test = "{\"persons\":[{\"PersonMail\":\"admin@mail.com\",\"Checked\":true},{\"PersonMail\":\"denisse@mail.com\",\"Checked\":false},{\"PersonMail\":\"kirsten@mail.com\",\"Checked\":false},{\"PersonMail\":\"daniel@mail.com\",\"Checked\":false},{\"PersonMail\":\"tina@mail.com\",\"Checked\":false},{\"PersonMail\":\"ismael@mail.com\",\"Checked\":false},{\"PersonMail\":\"andres@mail.com\",\"Checked\":false},{\"PersonMail\":\"ericrios24@gmail.com\",\"Checked\":false},{\"PersonMail\":\"berta@mail.com\",\"Checked\":false},{\"PersonMail\":\"josue@mail.com\",\"Checked\":false}],\"permissions\":[{\"PermissionName\":\"Ver usuarios\",\"PermissionCode\":101,\"Checked\":true},{\"PermissionName\":\"Crear usuarios\",\"PermissionCode\":102,\"Checked\":true},{\"PermissionName\":\"Ver detalles de usuarios\",\"PermissionCode\":103,\"Checked\":true},{\"PermissionName\":\"Editar usuarios\",\"PermissionCode\":104,\"Checked\":true},{\"PermissionName\":\"Borrar usuarios\",\"PermissionCode\":105,\"Checked\":true},{\"PermissionName\":\"Crear formularios\",\"PermissionCode\":201,\"Checked\":true},{\"PermissionName\":\"Ver formularios\",\"PermissionCode\":202,\"Checked\":true},{\"PermissionName\":\"Crear secciones\",\"PermissionCode\":203,\"Checked\":true},{\"PermissionName\":\"Ver secciones\",\"PermissionCode\":204,\"Checked\":true},{\"PermissionName\":\"Crear preguntas\",\"PermissionCode\":205,\"Checked\":true},{\"PermissionName\":\"Ver preguntas\",\"PermissionCode\":206,\"Checked\":true},{\"PermissionName\":\"Ver planes de mejora\",\"PermissionCode\":301,\"Checked\":true},{\"PermissionName\":\"Crear planes de mejora\",\"PermissionCode\":302,\"Checked\":true},{\"PermissionName\":\"Editar planes de mejora\",\"PermissionCode\":303,\"Checked\":true},{\"PermissionName\":\"Borrar planes de mejora\",\"PermissionCode\":304,\"Checked\":true},{\"PermissionName\":\"Crear objetivos\",\"PermissionCode\":305,\"Checked\":true},{\"PermissionName\":\"Ver objetivos\",\"PermissionCode\":306,\"Checked\":true},{\"PermissionName\":\"Editar objetivos\",\"PermissionCode\":307,\"Checked\":true},{\"PermissionName\":\"Borrar objetivos\",\"PermissionCode\":308,\"Checked\":true},{\"PermissionName\":\"Crear planes de mejora\",\"PermissionCode\":309,\"Checked\":true},{\"PermissionName\":\"Ver planes de mejora\",\"PermissionCode\":310,\"Checked\":true},{\"PermissionName\":\"Editar planes de mejora\",\"PermissionCode\":311,\"Checked\":true},{\"PermissionName\":\"Borrar planes de mejora\",\"PermissionCode\":312,\"Checked\":true},{\"PermissionName\":\"Ver respuestas de formularios\",\"PermissionCode\":401,\"Checked\":true}]}";
            string result = new JavaScriptSerializer().Serialize(checkboxes.Data);

            Assert.AreEqual(test, result);
        }
    }
}

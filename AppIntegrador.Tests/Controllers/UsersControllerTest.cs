using AppIntegrador.Controllers;
using AppIntegrador.Models;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Security.Principal;
using System.IO;
using Moq;
using System.Web.SessionState;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace AppIntegrador.Tests.Controllers
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void UsersIndexNotNull()
        {
            UsersController controller = new UsersController();
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UsersIndexResultName()
        {
            UsersController controller = new UsersController();
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");

            ViewResult result = controller.Index() as ViewResult;

            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void UserCreateNotNull()
        {
            UsersController controller = new UsersController();
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");

            ViewResult result = controller.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UsersCreateResultName()
        {
            UsersController controller = new UsersController();
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");

            ViewResult result = controller.Create() as ViewResult;

            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void DetailsNotNull()
        {
            UsersController controller = new UsersController();
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");

            ViewResult result = controller.Details("admin@", "mail.com") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditNotNull()
        {
            UsersController controller = new UsersController();
            CurrentUser.setCurrentUser("admin@mail.com", "Superusuario", "0000000001", "0000000001");

            ViewResult result = controller.Edit("admin@", "mail.com") as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateChangesSaved()
        {
            var personas = new List<Persona>
            {
                new Persona() { Correo = "fake1@mail.com", Identificacion = "123456781", Apellido1 = "Fake1", Nombre1 = "Fake", TipoIdentificacion = "Cédula" },
                new Persona() { Correo = "fake2@mail.com", Identificacion = "123456782", Apellido1 = "Fake2", Nombre1 = "Fake", TipoIdentificacion = "Cédula" },
                new Persona() { Correo = "fake3@mail.com", Identificacion = "123456783", Apellido1 = "Fake3", Nombre1 = "Fake", TipoIdentificacion = "Cédula" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Persona>>();

            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Provider).Returns(personas.Provider);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Expression).Returns(personas.Expression);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.ElementType).Returns(personas.ElementType);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.GetEnumerator()).Returns(personas.GetEnumerator());

            var myMockedObjectResult = new Mock<ObjectResult<int>>();

            Persona nuevaPersona = new Persona();
            nuevaPersona.Correo = "newusertest@mail.com";
            nuevaPersona.Nombre1 = "Test";
            nuevaPersona.Apellido1 = "Nuevo";
            nuevaPersona.TipoIdentificacion = "Cédula";
            nuevaPersona.Identificacion = "120540712";
            nuevaPersona.Estudiante = new Estudiante();

            ObjectParameter parameter = new ObjectParameter("result", typeof(bool));

            var database = new Mock<DataIntegradorEntities>();
            database.Setup(m => m.Persona).Returns(mockDbSet.Object);
            database.Setup(m => m.CheckID(It.IsAny<string>(), It.IsAny<ObjectParameter>())).Callback<string, ObjectParameter>((a, b) =>
            {
                b.Value = false;
            });

            UsersController controller = new UsersController(database.Object);

            var result = controller.Create(nuevaPersona) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void EditChangesSaved()
        {
            var personas = new List<Persona>
            {
                new Persona() { Correo = "fake1@mail.com", Identificacion = "123456781", Apellido1 = "Fake1", Nombre1 = "Fake", TipoIdentificacion = "Cédula" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Persona>>();

            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Provider).Returns(personas.Provider);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Expression).Returns(personas.Expression);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.ElementType).Returns(personas.ElementType);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.GetEnumerator()).Returns(personas.GetEnumerator());

            var usuarios = new List<Usuario>
            {
                new Usuario() { Activo = true }
            }.AsQueryable();

            var mockDbSetUsuario = new Mock<DbSet<Usuario>>();

            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(usuarios.Provider);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(usuarios.Expression);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(usuarios.ElementType);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(usuarios.GetEnumerator());

            var myMockedObjectResult = new Mock<ObjectResult<int>>();

            Persona persona = new Persona();
            persona.Correo = "newusertest@mail.com";
            persona.Nombre1 = "Test";
            persona.Apellido1 = "Nuevo";
            persona.TipoIdentificacion = "Cédula";
            persona.Identificacion = "120540712";
            persona.Estudiante = new Estudiante();
            Usuario usuario = new Usuario
            {
                Activo = true
            };
            UsuarioPersona usuarioPersona = new UsuarioPersona { Persona = persona, Usuario = usuario };

            ObjectParameter parameter = new ObjectParameter("result", typeof(bool));

            var database = new Mock<DataIntegradorEntities>();
            database.Setup(m => m.Persona).Returns(mockDbSet.Object);
            database.Setup(m => m.Usuario).Returns(mockDbSetUsuario.Object);
            database.Setup(m => m.ModificarCorreo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ObjectParameter>())).Callback<string, string, ObjectParameter>((a, b, c) =>
            {
                c.Value = false;
            });

            UsersController controller = new UsersController(database.Object);

            var result = controller.Edit(usuarioPersona) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditChangesSavedWrongCed()
        {
            var personas = new List<Persona>
            {
                new Persona() { Correo = "fake1@mail.com", Identificacion = "123456781", Apellido1 = "Fake1", Nombre1 = "Fake", TipoIdentificacion = "Cédula" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Persona>>();

            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Provider).Returns(personas.Provider);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Expression).Returns(personas.Expression);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.ElementType).Returns(personas.ElementType);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.GetEnumerator()).Returns(personas.GetEnumerator());

            var usuarios = new List<Usuario>
            {
                new Usuario() { Activo = true }
            }.AsQueryable();

            var mockDbSetUsuario = new Mock<DbSet<Usuario>>();

            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(usuarios.Provider);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(usuarios.Expression);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(usuarios.ElementType);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(usuarios.GetEnumerator());

            var myMockedObjectResult = new Mock<ObjectResult<int>>();

            Persona persona = new Persona();
            persona.Correo = "newusertest@mail.com";
            persona.Nombre1 = "Test";
            persona.Apellido1 = "Nuevo";
            persona.TipoIdentificacion = "Cédula";
            persona.Identificacion = "120540711232";
            persona.Estudiante = new Estudiante();
            Usuario usuario = new Usuario
            {
                Activo = true
            };
            UsuarioPersona usuarioPersona = new UsuarioPersona { Persona = persona, Usuario = usuario };

            ObjectParameter parameter = new ObjectParameter("result", typeof(bool));

            var database = new Mock<DataIntegradorEntities>();
            database.Setup(m => m.Persona).Returns(mockDbSet.Object);
            database.Setup(m => m.Usuario).Returns(mockDbSetUsuario.Object);
            database.Setup(m => m.ModificarCorreo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ObjectParameter>())).Callback<string, string, ObjectParameter>((a, b, c) =>
            {
                c.Value = false;
            });

            UsersController controller = new UsersController(database.Object);

            var result = controller.Edit(usuarioPersona) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditChangesSavedWrongPassport()
        {
            var personas = new List<Persona>
            {
                new Persona() { Correo = "fake1@mail.com", Identificacion = "123456781231", Apellido1 = "Fake1", Nombre1 = "Fake", TipoIdentificacion = "Cédula" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Persona>>();

            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Provider).Returns(personas.Provider);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Expression).Returns(personas.Expression);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.ElementType).Returns(personas.ElementType);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.GetEnumerator()).Returns(personas.GetEnumerator());

            var usuarios = new List<Usuario>
            {
                new Usuario() { Activo = true }
            }.AsQueryable();

            var mockDbSetUsuario = new Mock<DbSet<Usuario>>();

            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(usuarios.Provider);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(usuarios.Expression);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(usuarios.ElementType);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(usuarios.GetEnumerator());

            var myMockedObjectResult = new Mock<ObjectResult<int>>();

            Persona persona = new Persona();
            persona.Correo = "newusertest@mail.com";
            persona.Nombre1 = "Test";
            persona.Apellido1 = "Nuevo";
            persona.TipoIdentificacion = "Pasaporte";
            persona.Identificacion = "120540712443";
            persona.Estudiante = new Estudiante();
            Usuario usuario = new Usuario
            {
                Activo = true
            };
            UsuarioPersona usuarioPersona = new UsuarioPersona { Persona = persona, Usuario = usuario };

            ObjectParameter parameter = new ObjectParameter("result", typeof(bool));

            var database = new Mock<DataIntegradorEntities>();
            database.Setup(m => m.Persona).Returns(mockDbSet.Object);
            database.Setup(m => m.Usuario).Returns(mockDbSetUsuario.Object);
            database.Setup(m => m.ModificarCorreo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ObjectParameter>())).Callback<string, string, ObjectParameter>((a, b, c) =>
            {
                c.Value = false;
            });

            UsersController controller = new UsersController(database.Object);

            var result = controller.Edit(usuarioPersona) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void EditChangesSavedWrongResidence()
        {
            var personas = new List<Persona>
            {
                new Persona() { Correo = "fake1@mail.com", Identificacion = "123456781231", Apellido1 = "Fake1", Nombre1 = "Fake", TipoIdentificacion = "Cédula" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Persona>>();

            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Provider).Returns(personas.Provider);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Expression).Returns(personas.Expression);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.ElementType).Returns(personas.ElementType);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.GetEnumerator()).Returns(personas.GetEnumerator());

            var usuarios = new List<Usuario>
            {
                new Usuario() { Activo = true }
            }.AsQueryable();

            var mockDbSetUsuario = new Mock<DbSet<Usuario>>();

            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.Provider).Returns(usuarios.Provider);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.Expression).Returns(usuarios.Expression);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.ElementType).Returns(usuarios.ElementType);
            mockDbSetUsuario.As<IQueryable<Usuario>>().Setup(m => m.GetEnumerator()).Returns(usuarios.GetEnumerator());

            var myMockedObjectResult = new Mock<ObjectResult<int>>();

            Persona persona = new Persona();
            persona.Correo = "newusertest@mail.com";
            persona.Nombre1 = "Test";
            persona.Apellido1 = "Nuevo";
            persona.TipoIdentificacion = "Residencia";
            persona.Identificacion = "1205407";
            persona.Estudiante = new Estudiante();
            Usuario usuario = new Usuario
            {
                Activo = true
            };
            UsuarioPersona usuarioPersona = new UsuarioPersona { Persona = persona, Usuario = usuario };

            ObjectParameter parameter = new ObjectParameter("result", typeof(bool));

            var database = new Mock<DataIntegradorEntities>();
            database.Setup(m => m.Persona).Returns(mockDbSet.Object);
            database.Setup(m => m.Usuario).Returns(mockDbSetUsuario.Object);
            database.Setup(m => m.ModificarCorreo(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ObjectParameter>())).Callback<string, string, ObjectParameter>((a, b, c) =>
            {
                c.Value = false;
            });

            UsersController controller = new UsersController(database.Object);

            var result = controller.Edit(usuarioPersona) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteTest()
        {
            var personas = new List<Persona>
            {
                new Persona() { Correo = "fake1@mail.com", Identificacion = "123456781", Apellido1 = "Fake1", Nombre1 = "Fake", TipoIdentificacion = "Cédula" },
                new Persona() { Correo = "fake2@mail.com", Identificacion = "123456782", Apellido1 = "Fake2", Nombre1 = "Fake", TipoIdentificacion = "Cédula" },
                new Persona() { Correo = "fake3@mail.com", Identificacion = "123456783", Apellido1 = "Fake3", Nombre1 = "Fake", TipoIdentificacion = "Cédula" }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<Persona>>();

            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Provider).Returns(personas.Provider);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.Expression).Returns(personas.Expression);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.ElementType).Returns(personas.ElementType);
            mockDbSet.As<IQueryable<Persona>>().Setup(m => m.GetEnumerator()).Returns(personas.GetEnumerator());
            
            var database = new Mock<DataIntegradorEntities>();
            database.Setup(m => m.Persona).Returns(mockDbSet.Object);

            UsersController controller = new UsersController(database.Object);

            var result = controller.DeleteConfirmed("fake1", "@mail.com", true) as RedirectToRouteResult;

            Assert.AreEqual("Index", result.RouteValues["action"]);
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

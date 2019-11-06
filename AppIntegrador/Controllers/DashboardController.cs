using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;
using System.Data.Entity.Core.Objects;

namespace AppIntegrador.Controllers
{
    public class DashboardController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        //Berta Sánchez Jalet
        //COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
        //Tarea técnica: Crear funciones en el Controlador.
        //Cumplimiento: 8/10
        public String ObtenerPromedioProfesor(Usuario objUser)
        {
            String correo = HttpContext.User.Identity.Name;
            System.Diagnostics.Debug.WriteLine(correo);

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ObjectParameter resultPromedio = new ObjectParameter("promedio", typeof(float));

            db.PromedioProfesor(correo,resultPromedio);

            return serializer.Serialize(resultPromedio.Value);
        }

        //Berta Sánchez Jalet
        //COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
        //Tarea técnica: Crear funciones en el Controlador.
        //Cumplimiento: 8/10
        public String ObtenerPromedioCursos(Usuario objUser)
        {
            String correo = HttpContext.User.Identity.Name;

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ObjectParameter resultPromedio = new ObjectParameter("promedio", typeof(float));

            db.PromedioCursos(correo, resultPromedio);

            return serializer.Serialize(resultPromedio.Value);
        }
    }

    
}
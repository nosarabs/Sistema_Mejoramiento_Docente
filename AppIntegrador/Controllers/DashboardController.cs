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
    }

    //Berta Sánchez Jalet
    //COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
    //Tarea técnica: Crear funciones en el Controlador.
    //Cumplimiento: 7/10
    public String ObtenerPromedioProfesor(String codigoFormulario)
    {
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        ObjectParameter resultPromedio = new ObjectParameter("promedio", typeof(float));

        db.PromedioProfesor(codigoFormulario, siglaCurso, numeroGrupo, ano, semestre, fechaInicio, fechaFin, codigoSeccion, codigoPregunta, resultPromedio);

        return serializer.Serialize(resultPromedio.Value);
    }

    //Berta Sánchez Jalet
    //COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
    //Tarea técnica: Crear funciones en el Controlador.
    //Cumplimiento: 7/10
    public String ObtenerPromedioCursos(String codigoFormulario)
    {
        var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        ObjectParameter resultPromedio = new ObjectParameter("promedio", typeof(float));

        db.PromedioCursos(codigoFormulario, siglaCurso, numeroGrupo, ano, semestre, fechaInicio, fechaFin, codigoSeccion, codigoPregunta, resultPromedio);

        return serializer.Serialize(resultPromedio.Value);
    }
}
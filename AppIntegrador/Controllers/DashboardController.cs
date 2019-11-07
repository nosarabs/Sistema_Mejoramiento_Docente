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

        //Función que devuelve las unidades académicas con su respectivo código y nombre
        public String getUnidadesAcademicas()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
          
            // Se construye un objeto de tipo UnidadesAcadémicas con todas las unidades académicas
            var unidadesAcademicas = from uda in db.UnidadAcademica
                      orderby uda.Nombre
                      select new UnidadesAcademicas { codigo = uda.Codigo, nombre = uda.Nombre };
            
            return serializer.Serialize(unidadesAcademicas.ToList());
        }

        //Función que devuelve json con los énfasis de las carreras
        public String getCarreraEnfasis()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            // Se construye un objeto de tipo CarrerasEnfasis con los énfasis de cada carrera
            var carrerasEnfasis = from e in db.Enfasis
                      join c in db.Carrera on e.CodCarrera equals c.Codigo
                      orderby c.Nombre, e.Nombre
                      select new CarrerasEnfasis { codigoCarrera = c.Codigo, nombreCarrera = c.Nombre, codigoEnfasis = e.Codigo, nombreEnfasis = e.Nombre};

            // Se convierte a JSON la lista con las carrerasEnfasis
            return serializer.Serialize(carrerasEnfasis.ToList());
        }

        //Función que devuelve JSON con los grupos de un curso, con su respectivo número y período
        public String getCursoGrupo()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            // Se construye un objeto de tipo CursoGrupo con su información respectiva
            var cursoGrupo = from c in db.Curso
                                  join g in db.Grupo on c.Sigla equals g.SiglaCurso
                                  orderby c.Sigla, g.NumGrupo, g.Semestre, g.Anno
                                  select new CursoGrupo { siglaCurso = c.Sigla, nombreCurso = c.Nombre, numGrupo = g.NumGrupo, semestre = g.Semestre, anno = g.Anno};

            // Se convierte a JSON la lista con los grupos de los cursos
            return serializer.Serialize(cursoGrupo.ToList());
        }

        //Función que devuelve un JSON con el nombre completo de los profesores
        public String getProfesores()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            // Se construye un objeto de tipo Profesor con su nombre completo
            var profesores = from prof in db.Profesor
                             join per in db.Persona on prof.Correo equals per.Correo
                             orderby per.Apellido1, per.Apellido2, per.Nombre1, per.Nombre2
                             select new Profesores { correo = prof.Correo, nombre1 = per.Nombre1, nombre2 = per.Nombre2, apellido1 = per.Apellido1, apellido2 = per.Apellido2 };

            // Se convierte a JSON la lista con el nombre completo de los profesores
            return serializer.Serialize(profesores.ToList());
        }

        public String getFormularios()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            // Se construye un objeto de tipo Profesor con su nombre completo
            var profesores = from prof in db.Profesor
                             join per in db.Persona on prof.Correo equals per.Correo
                             orderby per.Apellido1, per.Apellido2, per.Nombre1, per.Nombre2
                             select new Profesores { correo = prof.Correo, nombre1 = per.Nombre1, nombre2 = per.Nombre2, apellido1 = per.Apellido1, apellido2 = per.Apellido2 };

            // Se convierte a JSON la lista con el nombre completo de los profesores
            return serializer.Serialize(profesores.ToList());
        }
    }
}
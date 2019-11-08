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

        //Retorna un string con la lista de formularios que pueden ser visualizados con base en los parámetros de los filtros.
        public String ObtenerFormularios(string codigoUA, string codigoCarrera, string codigoEnfasis, string siglaCurso, Nullable<byte> numeroGrupo, Nullable<byte> semestre, Nullable<int> anno, string correoProfesor)
        {

            /*Este método se pretende llamar con un ajax desde Javascript, debido a que los parámetros de tipo string que se pasan como null desde el Ajax
             aquí se reciben como string vacío "", se hace un checkeo de si los parámetros de tipo string están vacíos y en caso de ser así les asigna un null.
             Se utilizó la función IsNullOrEmpty por conveniencia, pero en realidad solo se necesita la revisión de si está vacío.*/

            string codUA = String.IsNullOrEmpty(codigoUA)? null : codigoUA;
            string codCarrera = String.IsNullOrEmpty(codigoCarrera) ? null : codigoCarrera;
            string codEnfasis = String.IsNullOrEmpty(codigoEnfasis) ? null : codigoEnfasis;
            string sigCurso = String.IsNullOrEmpty(siglaCurso) ? null : siglaCurso;
            string corrProfesor = String.IsNullOrEmpty(correoProfesor) ? null : correoProfesor;

            //Permite serializar objetos para enviarlos como JSONs.
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            //Llamado a la función de tabla que recupera los formularios según los parámetros de los filtros.
            var formularios = db.ObtenerFormulariosFiltros(codUA, codCarrera, codEnfasis, sigCurso, numeroGrupo, semestre, anno, corrProfesor);

            //Retorna la lista de formularios serializada.
            return serializer.Serialize(formularios.ToList());

        }
    }
}
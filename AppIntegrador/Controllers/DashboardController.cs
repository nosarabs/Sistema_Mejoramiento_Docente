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
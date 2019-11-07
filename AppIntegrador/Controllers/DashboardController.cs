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
        struct Resultado
        {
            public float promedio;
            public int cantidad;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        //Berta Sánchez Jalet
        //COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
        //Tarea técnica: Crear funciones en el Controlador.
        //Cumplimiento: 10/10
        public String ObtenerPromedioProfesor(String correo)
        {
                
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            ObjectParameter resultPromedio = new ObjectParameter("promedio", typeof(float));
            ObjectParameter resultCantidad = new ObjectParameter("cantidad", typeof(int));

            db.PromedioProfesor(correo, resultPromedio, resultCantidad);

            Resultado p;

            if(!(resultPromedio.Value is DBNull))
            {
                p.promedio = Convert.ToSingle(resultPromedio.Value);
                p.cantidad = Convert.ToInt32(resultCantidad.Value);
            } else
            {
                p.cantidad = 0;
                p.promedio = 0;
            }
            

            return serializer.Serialize(p);
        }

        //Berta Sánchez Jalet'Object cannot be cast from DBNull to other types.'

        //COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
        //Tarea técnica: Crear funciones en el Controlador.
        //Cumplimiento: 8/10
        public String ObtenerPromedioCursos(String correo)
        {

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ObjectParameter resultPromedio = new ObjectParameter("promedio", typeof(float));
            ObjectParameter resultCantidad = new ObjectParameter("cantidad", typeof(int));

            db.PromedioCursos(correo, resultPromedio, resultCantidad);

            Resultado c;

            if (!(resultPromedio.Value is DBNull))
            {
                c.promedio = Convert.ToSingle(resultPromedio.Value);
                c.cantidad = Convert.ToInt32(resultCantidad.Value);
            } else
            {
                c.cantidad = 0;
                c.promedio = 0;
            }

            return serializer.Serialize(c);
        }
        //Retorna un string con la lista de formularios que pueden ser visualizados con base en los parámetros de los filtros.
        public String ObtenerFormularios(string codigoUA, string codigoCarrera, string codigoEnfasis, string siglaCurso, Nullable<byte> numeroGrupo, Nullable<byte> semestre, Nullable<int> anno, string correoProfesor)
        {

            /*Este método se pretende llamar con un ajax desde Javascript, debido a que los parámetros de tipo string que se pasan como null desde el Ajax
             aquí se reciben como string vacío "", se hace un checkeo de si los parámetros de tipo string están vacíos y en caso de ser así les asigna un null.
             Se utilizó la función IsNullOrEmpty por conveniencia, pero en realidad solo se necesita la revisión de si está vacío.*/

            string codUA = String.IsNullOrEmpty(codigoUA) ? null : codigoUA;
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
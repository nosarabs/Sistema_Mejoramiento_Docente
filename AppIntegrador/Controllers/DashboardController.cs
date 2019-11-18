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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppIntegrador.Controllers
{
    public class DashboardController : Controller
    {
        private DataIntegradorEntities db;
        private FiltrosEntities fdb;

        public DashboardController()
        {
            db = new DataIntegradorEntities();
            fdb = new FiltrosEntities();
        }

        public DashboardController(DataIntegradorEntities db, FiltrosEntities fdb)
        {
            this.db = db;
            this.fdb = fdb;
        }

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
        public String ObtenerFormularios(List<UnidadesAcademicas> unidadesAcademicas, List<CarrerasEnfasis> carrerasEnfasis, List<CursoGrupo> grupos, List<Profesores> profesores)
        {

            //Se crean los parámetros que deben enviarse al procedimiento
            var uas = CrearTablaUA(unidadesAcademicas);
            var ces = CrearTablaCE(carrerasEnfasis);
            var gs = CrearTablaG(grupos);
            var ps = CrearTablaP(profesores);

            //Llamado a la función de tabla que recupera los formularios según los parámetros de los filtros.
            var formularios = fdb.ObtenerFormulariosFiltros(uas, ces, gs, ps);

           //Retorna la lista de formularios serializada.
           return JsonConvert.SerializeObject(formularios.ToList(), new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
           
        }

        //Crea un DataTable de unidades académicas a partir de una lista
        static DataTable CrearTablaUA(List<UnidadesAcademicas> unidadesAcademicas)
        {
            //Inicializa la variable como nula
            DataTable dt = null;

            //Verifica si la lista no es nula
            if (unidadesAcademicas != null)
            {
                /*Si la cantidad de elementos en la lista es mayor o igual a 1 y su primer elemento no es nulo crea la tabla.
                 Esta verificación se hace porque cuando se hace el request con ajax del método del controlador que utiliza esta
                 función, al pasar la lista como nula, el controlador recibe una lista con un único elemento nulo. Por lo tanto,
                 esta verificación es necesaria hasta tanto no se descubra como hacer que el controlador reciba la lista como nula
                 utilizando la solicitud con Ajax.*/
                if (unidadesAcademicas.Count >= 1 && unidadesAcademicas[0] != null)
                {
                    //Crea una nueva tabla
                    using (dt = new DataTable())
                    {
                        //Limpia la tabla
                        dt.Clear();

                        //Añade las columnas necesarias a la tabla
                        dt.Columns.Add("CodigoUA", typeof(string));

                        //Llena la tabla con el contenido de la lista
                        for (int i = 0; i < unidadesAcademicas.Count; ++i)
                        {
                            if (unidadesAcademicas[i] != null)
                            {
                                dt.Rows.Add(unidadesAcademicas[i].codigo);
                            }
                        }
                    }
                }
            }
            return dt;
        }

        //Crea un DataTable de carreras y énfasis a partir de una lista
        static DataTable CrearTablaCE(List<CarrerasEnfasis> carrerasEnfasis)
        {
            //Inicializa la variable como nula
            DataTable dt = null;

            //Verifica si la lista no es nula
            if (carrerasEnfasis != null)
            {
                /*Si la cantidad de elementos en la lista es mayor o igual a 1 y su primer elemento no es nulo crea la tabla.
                 Esta verificación se hace porque cuando se hace el request con ajax del método del controlador que utiliza esta
                 función, al pasar la lista como nula, el controlador recibe una lista con un único elemento nulo. Por lo tanto,
                 esta verificación es necesaria hasta tanto no se descubra como hacer que el controlador reciba la lista como nula
                 utilizando la solicitud con Ajax.*/
                if (carrerasEnfasis.Count >= 1 && carrerasEnfasis[0] != null)
                {
                    //Crea una nueva tabla
                    using (dt = new DataTable())
                    {
                        //Limpia la tabla
                        dt.Clear();

                        //Añade las columnas necesarias a la tabla
                        dt.Columns.Add("CodigoCarrera", typeof(string));
                        dt.Columns.Add("CodigoEnfasis", typeof(string));

                        //Llena la tabla con el contenido de la lista
                        for (int i = 0; i < carrerasEnfasis.Count; ++i)
                        {
                            if (carrerasEnfasis[i] != null)
                            {
                                dt.Rows.Add(carrerasEnfasis[i].codigoCarrera, carrerasEnfasis[i].codigoEnfasis);
                            }
                        }
                    }
                }
            }
            return dt;
        }

        //Crea un DataTable de grupos a partir de una lista
        static DataTable CrearTablaG(List<CursoGrupo> grupos)
        {
            //Inicializa la variable como nula
            DataTable dt = null;

            //Verifica si la lista no es nula
            if (grupos != null)
            {
                /*Si la cantidad de elementos en la lista es mayor o igual a 1 y su primer elemento no es nulo crea la tabla.
                 Esta verificación se hace porque cuando se hace el request con ajax del método del controlador que utiliza esta
                 función, al pasar la lista como nula, el controlador recibe una lista con un único elemento nulo. Por lo tanto,
                 esta verificación es necesaria hasta tanto no se descubra como hacer que el controlador reciba la lista como nula
                 utilizando la solicitud con Ajax.*/
                if (grupos.Count >= 1 && grupos[0] != null)
                {
                    //Crea una nueva tabla
                    using (dt = new DataTable())
                    {
                        //Limpia la tabla
                        dt.Clear();

                        //Añade las columnas necesarias a la tabla
                        dt.Columns.Add("SiglaCurso", typeof(string));
                        dt.Columns.Add("NumeroGrupo", typeof(byte));
                        dt.Columns.Add("Semestre", typeof(byte));
                        dt.Columns.Add("Anno", typeof(int));

                        //Llena la tabla con el contenido de la lista
                        for (int i = 0; i < grupos.Count; ++i)
                        {
                            if (grupos[i] != null)
                            {
                                dt.Rows.Add(grupos[i].siglaCurso, grupos[i].numGrupo, grupos[i].semestre, grupos[i].anno);
                            }
                        }
                    }
                }
            }
            return dt;
        }

        //Crea un DataTable de profesores a partir de una lista
        static DataTable CrearTablaP(List<Profesores> profesores)
        {
            //Inicializa la variable como nula
            DataTable dt = null;

            //Verifica si la lista no es nula
            if (profesores != null)
            {
                /*Si la cantidad de elementos en la lista es mayor o igual a 1 y su primer elemento no es nulo crea la tabla.
                 Esta verificación se hace porque cuando se hace el request con ajax del método del controlador que utiliza esta
                 función, al pasar la lista como nula, el controlador recibe una lista con un único elemento nulo. Por lo tanto,
                 esta verificación es necesaria hasta tanto no se descubra como hacer que el controlador reciba la lista como nula
                 utilizando la solicitud con Ajax.*/
                if (profesores.Count >= 1 && profesores[0] != null)
                {
                    //Crea una nueva tabla
                    using (dt = new DataTable())
                    {
                        //Limpia la tabla
                        dt.Clear();

                        //Añade las columnas necesarias a la tabla
                        dt.Columns.Add("CorreoProfesor", typeof(string));

                        //Llena la tabla con el contenido de la lista
                        for (int i = 0; i < profesores.Count; ++i)
                        {
                            if (profesores[i] != null)
                            {
                                dt.Rows.Add(profesores[i].correo);
                            }
                        }
                    }
                }
            }
            return dt;
        }
    }   
}
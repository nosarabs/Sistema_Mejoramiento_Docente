﻿using System;
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
using System.Data.SqlClient;

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

        //Berta Sánchez Jalet
        //COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
        //Tarea técnica: Crear funciones en el Controlador.
        //Cumplimiento: 8/10
        public String ObtenerPromedioProfesor(List<UAsFiltros> unidadesAcademicas, List<CarrerasEnfasisFiltros> carrerasEnfasis, List<GruposFiltros> grupos, List<ProfesoresFiltros> profesores)
        {

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var resultPromedio = new SqlParameter("@promedio", SqlDbType.Float);
            resultPromedio.Direction = ParameterDirection.Output;

            var resultCantidad = new SqlParameter("@cantidad", SqlDbType.Int);
            resultCantidad.Direction = ParameterDirection.Output;

            var uas = CrearTablaUA(unidadesAcademicas);
            var ces = CrearTablaCE(carrerasEnfasis);
            var gs = CrearTablaG(grupos);
            var ps = CrearTablaP(profesores);

            fdb.PromedioProfesor(uas, ces, gs, ps, resultPromedio, resultCantidad);

            Resultado p;

            if (!(resultPromedio.Value is DBNull))
            {
                p.promedio = Convert.ToSingle(resultPromedio.Value);
                p.cantidad = Convert.ToInt32(resultCantidad.Value);
            }
            else
            {
                p.cantidad = 0;
                p.promedio = 0;
            }


            return serializer.Serialize(p);
        }

        //Berta Sánchez Jalet
        //COD-67: Desplegar la información del puntaje de un profesor y un curso específico.
        //Tarea técnica: Crear funciones en el Controlador.
        //Cumplimiento: 8/10
        public String ObtenerPromedioCursos(List<UAsFiltros> unidadesAcademicas, List<CarrerasEnfasisFiltros> carrerasEnfasis, List<GruposFiltros> grupos, List<ProfesoresFiltros> profesores)
        {

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var resultPromedio = new SqlParameter("@promedio", SqlDbType.Float);
            resultPromedio.Direction = ParameterDirection.Output;

            var resultCantidad = new SqlParameter("@cantidad", SqlDbType.Int);
            resultCantidad.Direction = ParameterDirection.Output;

            var uas = CrearTablaUA(unidadesAcademicas);
            var ces = CrearTablaCE(carrerasEnfasis);
            var gs = CrearTablaG(grupos);
            var ps = CrearTablaP(profesores);

            fdb.PromedioCursos(uas, ces, gs, ps, resultPromedio, resultCantidad);

            Resultado c;

            if (!(resultPromedio.Value is DBNull))
            {
                c.promedio = Convert.ToSingle(resultPromedio.Value);
                c.cantidad = Convert.ToInt32(resultCantidad.Value);
            }
            else
            {
                c.cantidad = 0;
                c.promedio = 0;
            }

            return serializer.Serialize(c);
        }

        //Retorna un string con la lista de unidades académicas que aparecen en el filtro con base en los parámetros de los otros filtros.
        public string ObtenerUnidadesAcademicas(List<CarrerasEnfasisFiltros> carrerasEnfasis, List<GruposFiltros> grupos, List<ProfesoresFiltros> profesores)
        {

            //Se crean los parámetros que deben enviarse al procedimiento
            var ces = CrearTablaCE(carrerasEnfasis);
            var gs = CrearTablaG(grupos);
            var ps = CrearTablaP(profesores);

            //Llamado a la función de tabla que recupera las unidades académicas según los parámetros de los filtros.
            var formularios = fdb.ObtenerUAsFiltros(ces, gs, ps);

            //Retorna la lista de formularios serializada.
            return JsonConvert.SerializeObject(formularios.ToList());

        }

        //Retorna un string con la lista de carreras y énfasis que aparecen en el filtro con base en los parámetros de los otros filtros.
        public string ObtenerCarrerasEnfasis(List<UAsFiltros> unidadesAcademicas, List<GruposFiltros> grupos, List<ProfesoresFiltros> profesores)
        {

            //Se crean los parámetros que deben enviarse al procedimiento
            var uas = CrearTablaUA(unidadesAcademicas);
            var gs = CrearTablaG(grupos);
            var ps = CrearTablaP(profesores);

            //Llamado a la función de tabla que recupera las carreras y énfasis según los parámetros de los filtros.
            var carrerasEnfasis = fdb.ObtenerCarrerasEnfasisFiltros(uas, gs, ps);

            //Retorna la lista de formularios serializada.
            return JsonConvert.SerializeObject(carrerasEnfasis.ToList());

        }

        //Retorna un string con la lista de grupos que aparecen en el filtro con base en los parámetros de los otros filtros.
        public string ObtenerGrupos(List<UAsFiltros> unidadesAcademicas, List<CarrerasEnfasisFiltros> carrerasEnfasis, List<ProfesoresFiltros> profesores)
        {

            //Se crean los parámetros que deben enviarse al procedimiento
            var uas = CrearTablaUA(unidadesAcademicas);
            var ces = CrearTablaCE(carrerasEnfasis);
            var ps = CrearTablaP(profesores);

            //Llamado a la función de tabla que recupera los grupos según los parámetros de los filtros.
            var grupos = fdb.ObtenerGruposFiltros(uas, ces, ps);

            //Retorna la lista de formularios serializada.
            return JsonConvert.SerializeObject(grupos.ToList());

        }

        //Retorna un string con la lista de profesores que aparecen en el filtro con base en los parámetros de los otros filtros.
        public string ObtenerProfesores(List<UAsFiltros> unidadesAcademicas, List<CarrerasEnfasisFiltros> carrerasEnfasis, List<GruposFiltros> grupos)
        {
            //Se crean los parámetros que deben enviarse al procedimiento
            var uas = CrearTablaUA(unidadesAcademicas);
            var ces = CrearTablaCE(carrerasEnfasis);
            var gs = CrearTablaG(grupos);

            //Llamado a la función de tabla que recupera los profesores según los parámetros de los filtros.
            var profesores = fdb.ObtenerProfesoresFiltros(uas, ces, gs);

            //Retorna la lista de formularios serializada.
            return JsonConvert.SerializeObject(profesores.ToList());
        }

        //Retorna un string con la lista de formularios que pueden ser visualizados con base en los parámetros de los filtros.
        public string ObtenerFormularios(List<UAsFiltros> unidadesAcademicas, List<CarrerasEnfasisFiltros> carrerasEnfasis, List<GruposFiltros> grupos, List<ProfesoresFiltros> profesores)
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
        static DataTable CrearTablaUA(List<UAsFiltros> unidadesAcademicas)
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
                                dt.Rows.Add(unidadesAcademicas[i].CodigoUA);
                            }
                        }
                    }
                }
            }
            return dt;
        }

        //Crea un DataTable de carreras y énfasis a partir de una lista
        static DataTable CrearTablaCE(List<CarrerasEnfasisFiltros> carrerasEnfasis)
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
                                dt.Rows.Add(carrerasEnfasis[i].CodCarrera, carrerasEnfasis[i].CodEnfasis);
                            }
                        }
                    }
                }
            }
            return dt;
        }

        //Crea un DataTable de grupos a partir de una lista
        static DataTable CrearTablaG(List<GruposFiltros> grupos)
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
                                dt.Rows.Add(grupos[i].SiglaCurso, grupos[i].NumGrupo, grupos[i].Semestre, grupos[i].Anno);
                            }
                        }
                    }
                }
            }
            return dt;
        }

        //Crea un DataTable de profesores a partir de una lista
        static DataTable CrearTablaP(List<ProfesoresFiltros> profesores)
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
                                dt.Rows.Add(profesores[i].Correo);
                            }
                        }
                    }
                }
            }
            return dt;
        }
    }   
}
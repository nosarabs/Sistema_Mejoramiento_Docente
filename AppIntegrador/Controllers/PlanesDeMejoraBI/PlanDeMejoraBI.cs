﻿using AppIntegrador.Models;
using AppIntegrador.Models.Metadata.RelacionesPlanesFormularios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace AppIntegrador.Controllers.PlanesDeMejoraBI
{
    public class PlanDeMejoraBI
    {
        /*
            EFE:
                Le asigna al plan de mejora un código diferente, siento este 1000 para el primer plan
                y en caso contrario va asignando con un aumento de 1 al código del plan anterior
            REQ: 
                db: instancia de la conexión de la base de datos
                plan: instancia del nuevo plan de mejora que se quiere realizar
            MOD:
                El plan que se le envía como parametro
        */
        public void setCodigoAPlanDeMejora(DataIntegradorEntities db, PlanDeMejora plan)
        {
            // Primero analizamos tomamos la totalidad de los planes de mejora
            var totalPlanes = db.PlanDeMejora.ToList();
            var ultimoCodigo = -1;

            foreach (var item in totalPlanes) {
                ultimoCodigo = item.codigo;
            }
            plan.codigo = ultimoCodigo + 1;
            
            // Este metodo tambien deja el borrado en 0 ya que es un plan que se esta creando
            plan.borrado = false;
        }

        /*
         * EFE: Crea todas las tablas necesarias para el procedimiento de almacenamiento de plan de mejora
         * REQ: plan: plan de mejora con lo que se quiere crear
         * MOD: la base de datos si el procedimiento almacenado tiene exito
         */
        public void savePlan(PlanDeMejora plan) 
        {
            // Creacion de la tabla de planDeMejora y Asociacion de plan de mejora con formulario
            List<DataTable> planYAsocPlanFormulario = this.getPlanTableYAsocPlanFormularios(plan);
            DataTable planTable = planYAsocPlanFormulario[0];
            DataTable asocPlanFormTable = planYAsocPlanFormulario[1];

            // Creacion de la tabla de objetivos y Asociacion de objetivos con secciones de un formulario
            List<DataTable> objetivosYAsocObjetivosSecciones = this.getObjetivosTableYAsocObjetivosSecciones(plan);
            DataTable objetivosTable = objetivosYAsocObjetivosSecciones[0];
            DataTable asocObjetivosSeccionesTable = objetivosYAsocObjetivosSecciones[1];

            // Creacion de la tabla de planDeMejora y Asociacion de plan de mejora con formulario
            List<DataTable> accionesYAsocAccionesPreguntas = this.getAccionesDeMejoraTableYAsocAccionesPreguntas(plan);
            DataTable accionesDeMejoraTable = planYAsocPlanFormulario[0];
            DataTable asocAccionesPreguntasTable = planYAsocPlanFormulario[1];

            // Creacion de la tabla de planDeMejora y Asociacion de plan de mejora con formulario
            DataTable accionablesTable = this.getAccionablesTable(plan);

            this.enviarTablasAlmacenamiento(
                planTable,                      "tablaPlan",
                objetivosTable,                 "tablaObjetivos",
                accionesDeMejoraTable,          "tablaAcciones",
                accionablesTable,               "tablaAccionables",
                asocPlanFormTable,              "tablaAsocPlanFormularios",
                asocObjetivosSeccionesTable,    "tablaAsocObjetivosSecciones",
                asocAccionesPreguntasTable,     "tablaAsocAccionesPreguntas"
            );
        }

        /*
         * EFE: 
         *      Metodo encargado de enviar tablas al procedimito almacenado
         * REQ:
         *      tempTable: tabla con los datos 
         * MOD:
         *      Estado de la base de datos
         */
        public void enviarTablasAlmacenamiento(
                DataTable tablaPlan,        string tablaPlanName,
                DataTable tablaObjetivos,   string tablaObjetivosName,
                DataTable tablaAcciones,    string tableAccionesName,
                DataTable tablaAccionables, string tablaAccionablesName,

                DataTable tablaAsocPlanFormularios,     string AsocPlanFormulariosName,
                DataTable tablaAsocObjSecciones,        string AsocObjSeccionesName,
                DataTable tablaAsocAccionesPreguntas,   string AsocAccionesPreguntasName)
        {
            SqlConnection connection = new SqlConnection("data source=(localdb)\\MSSQLLocalDB;initial catalog=DataIntegrador;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;");
            connection.Open();
            SqlCommand cmd = new SqlCommand("AgregarPlanComplete", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            //Pass table Valued parameter to Store Procedure
            SqlParameter sqlParam = cmd.Parameters.AddWithValue(tablaPlanName, tablaPlan);
            sqlParam = cmd.Parameters.AddWithValue(tablaObjetivosName, tablaObjetivos);
            sqlParam = cmd.Parameters.AddWithValue(tableAccionesName, tablaAcciones);
            sqlParam = cmd.Parameters.AddWithValue(tablaAccionablesName, tablaAccionables);

            sqlParam = cmd.Parameters.AddWithValue(AsocPlanFormulariosName, tablaAsocPlanFormularios);
            sqlParam = cmd.Parameters.AddWithValue(AsocObjSeccionesName, tablaAsocObjSecciones);
            sqlParam = cmd.Parameters.AddWithValue(AsocAccionesPreguntasName, tablaAsocAccionesPreguntas);

            sqlParam.SqlDbType = SqlDbType.Structured;

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        /**
         * EFE:
         *      Metodo que se encarga de crear una tabla con los datos del plan de mejora que ingresamos
         *      Tambien crea la tabla de relacion de plan de mejora con formulario
         * REQ:
         *      plan: plan de mejora que se coloca dentro de la lista
         * MOD:
         *      ----
         */
        public List<DataTable> getPlanTableYAsocPlanFormularios(PlanDeMejora plan)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            // Creando las columnas
            dt2.Columns.Add("codigoPlan");
            dt2.Columns.Add("codigoForm");

            IEnumerable<Formulario> formularios = plan.Formulario;
            if (formularios != null) 
            {
                foreach (var form in formularios) 
                {
                    dt2.Rows.Add(plan.codigo, form.Codigo);
                }
            }

            // Creando las columnas
            dt.Columns.Add("codigo");
            dt.Columns.Add("nombre");
            dt.Columns.Add("fechaInicio");
            dt.Columns.Add("fechaFin");
            dt.Columns.Add("borrado");

            //Agregando las filas
            dt.Rows.Add(plan.codigo, plan.nombre, plan.fechaInicio, plan.fechaFin, plan.borrado);

            List<DataTable> lista = new List<DataTable>();
            lista.Add(dt);
            lista.Add(dt2);
            return lista;
        }

        /**
         * EFE:
         *      Metodo que se encarga de crear una tabla con los datos del objetivo que ingresamos
         *      Tambien crea una tabla de las asociaciones entre el plan y los formularios
         * REQ:
         *      plan: plan de mejora que se coloca dentro de la lista
         * MOD:
         *      ----
         */
        public List<DataTable> getObjetivosTableYAsocObjetivosSecciones(PlanDeMejora plan)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            //Crando las columnas
            dt2.Columns.Add("codigoPlan");
            dt2.Columns.Add("nombreObjetivo");
            dt2.Columns.Add("codigoSeccion");

            // Creando las columnas
            dt.Columns.Add("codigoPlan");
            dt.Columns.Add("nombre");
            dt.Columns.Add("descripcion");
            dt.Columns.Add("fechaInicio");
            dt.Columns.Add("fechaFin");
            dt.Columns.Add("nombreTipoObj");
            dt.Columns.Add("codPlantilla");
            dt.Columns.Add("borrado");

            // Lista de los objetivos del plan
            ICollection<Objetivo> objetivos = plan.Objetivo;

            if (objetivos != null) 
            {
                //Ahora se agregan las tuplas a la tabla correspondiente
                foreach (var obj in objetivos)
                {
                    dt.Rows.Add(obj.codPlan, obj.nombre, obj.descripcion, obj.fechaInicio, obj.fechaFin, obj.nombTipoObj, obj.codPlantilla, obj.borrado);
                    
                    // Realizando la tupla de la asociacion 
                    ICollection<Seccion> secciones = obj.Seccion;
                    if (secciones !=  null) 
                    {
                        foreach (var sec in secciones) 
                        {
                            dt2.Rows.Add(plan.codigo, obj.nombre, sec.Codigo);
                        }
                    }
                }
            }

            List<DataTable> lista = new List<DataTable>();
            lista.Add(dt);
            lista.Add(dt2);
            return lista;
        }

        /**
         * EFE:
         *      Metodo que se encarga de crear una tabla con los datos de las acciones de mejora del plan
         *      Tambien crea una tabla de las asociaciones de los objetivos con secciones de un formulario
         * REQ:
         *      plan: plan de mejora que se coloca dentro de la lista
         * MOD:
         *      ----
         */
        public List<DataTable> getAccionesDeMejoraTableYAsocAccionesPreguntas(PlanDeMejora plan)
        {
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            // Creando las columnas
            dt2.Columns.Add("codigoPlan");
            dt2.Columns.Add("nombreObjetivo");
            dt2.Columns.Add("descripcionAccion");
            dt2.Columns.Add("codigoPregunta");

            // Creando las columnas
            dt.Columns.Add("codigoPlan");
            dt.Columns.Add("nombreObj");
            dt.Columns.Add("descripcion");
            dt.Columns.Add("fechaInicio");
            dt.Columns.Add("fechaFin");
            dt.Columns.Add("codPlantilla");
            dt.Columns.Add("borrado");

            // Lista de los objetivos del plan
            ICollection<Objetivo> objetivos = plan.Objetivo;

            if (objetivos != null)
            {
                //Ahora se agregan las tuplas a la tabla correspondiente
                foreach (var obj in objetivos)
                {
                    ICollection<AccionDeMejora> acciones = obj.AccionDeMejora;
                    if (acciones != null) 
                    {
                        foreach (var acc in acciones) 
                        {
                            dt.Rows.Add(acc.codPlan, acc.nombreObj, acc.descripcion, acc.fechaInicio, acc.fechaFin, acc.codPlan, acc.borrado);

                            // Realizando la tupla de la asociacion 
                            ICollection<Pregunta> preguntas = acc.Pregunta;
                            if (preguntas != null)
                            {
                                foreach (var preg in preguntas)
                                {
                                    dt2.Rows.Add(plan.codigo, obj.nombre, acc.descripcion, preg.Codigo);
                                }
                            }
                        }
                    }
                }
            }

            List<DataTable> lista = new List<DataTable>();
            lista.Add(dt);
            lista.Add(dt2);
            return lista;
        }

        /**
         * EFE:
         *      Metodo que se encarga de crear una tabla con los datos de los accionables del plan de mejora
         *      Tambien crea la tabla de aociación de acciones de mejora con preguntas.
         * REQ:
         *      plan: plan de mejora que se coloca dentro de la lista
         * MOD:
         *      ----
         */
        public DataTable getAccionablesTable(PlanDeMejora plan)
        {
            DataTable dt = new DataTable();

            // Creando las columnas
            dt.Columns.Add("codigoPlan");
            dt.Columns.Add("nombreObj");
            dt.Columns.Add("descripcionAccion");
            dt.Columns.Add("descripcion");
            dt.Columns.Add("fechaInicio");
            dt.Columns.Add("fechaFin");
            dt.Columns.Add("tipo");

            // Lista de los objetivos del plan
            ICollection<Objetivo> objetivos = plan.Objetivo;

            if (objetivos != null)
            {
                //Ahora se agregan las tuplas a la tabla correspondiente
                foreach (var obj in objetivos)
                {
                    ICollection<AccionDeMejora> acciones = obj.AccionDeMejora;
                    if (acciones != null)
                    {
                        foreach (var acc in acciones)
                        {
                            ICollection<Accionable> accionables = acc.Accionable;
                            if (accionables != null)
                            {
                                foreach (var accio in accionables) 
                                {
                                    dt.Rows.Add(accio.codPlan, accio.nombreObj, accio.descripAcMej, accio.descripcion, accio.fechaInicio, accio.fechaFin, accio.tipo);
                                }
                            }
                        }
                    }
                }
            }
            return dt;
        }
    }
}
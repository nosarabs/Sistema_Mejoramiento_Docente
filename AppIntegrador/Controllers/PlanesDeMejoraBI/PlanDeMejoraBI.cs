using AppIntegrador.Models;
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
        * EFE:
        *       Se encarga de agregar las tuplas de la segunda tabla a la primera
        * REQ:
        *       tableOne: tabla a la que se le agregan las tuplas
        *       tableTwo: tabla a la que se le copian las tuplas
        * MOD:
        *       tableOne
        */
        public void copyTable(DataTable tableOne, DataTable tableTwo) 
        {
            foreach (var tuple in tableTwo.Rows)
            {
                tableOne.Rows.Add(tuple);
            }
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
                DataTable tempTable, string tableParamName, 
                DataTable tablaAsocPlanFormularios, string AsocPlanFormulariosName,
                DataTable tablaAsocObjSecciones, string AsocObjSeccionesName,
                DataTable tablaAsocAccionesPreguntas, string AsocAccionesPreguntasName)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["DataIntegradorEntities"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("AgregarPlanComplete", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            //Pass table Valued parameter to Store Procedure
            SqlParameter sqlParam = cmd.Parameters.AddWithValue(tableParamName, tempTable);
            sqlParam = cmd.Parameters.AddWithValue(AsocPlanFormulariosName, tablaAsocPlanFormularios);
            sqlParam = cmd.Parameters.AddWithValue(AsocObjSeccionesName, tablaAsocObjSecciones);
            sqlParam = cmd.Parameters.AddWithValue(AsocAccionesPreguntasName, tablaAsocAccionesPreguntas);

            sqlParam.SqlDbType = SqlDbType.Structured;

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        /**
         * EFE:
         *      Metodo que se encarga de crear una tabla con los datos del plane de mejora que ingresamos
         * REQ:
         *      plan: plan de mejora que se coloca dentro de la lista
         * MOD:
         *      ----
         */
        // Funcion que se encarga de devolver la info del plan como una tabla
        public DataTable getPlanTable(PlanDeMejora plan)
        {
            DataTable dt = new DataTable();

            // Creando las columnas
            dt.Columns.Add("codigo");
            dt.Columns.Add("nombre");
            dt.Columns.Add("fechaInicio");
            dt.Columns.Add("fechaFin");
            dt.Columns.Add("borrado");

            //Agregando las filas
            dt.Rows.Add(plan.codigo, plan.nombre, plan.fechaInicio, plan.fechaFin, plan.borrado);

            return dt;
        }

        /**
         * EFE:
         *      Metodo que se encarga de crear una tabla con los datos de la asocioacion del plan con los formularios
         * REQ:
         *             plan: plan de mejora que se coloca dentro de la lista
         *      formularios: lista de codigo de los formuarios
         * MOD:
         *      ----
         */
        // Funcion que se encarga de devolver la info del plan como una tabla
        public DataTable getTablaAsociacionPlanFormularios(PlanDeMejora plan, List<string> formularios) 
        {
            DataTable dt = new DataTable();

            // Creando las columnas
            dt.Columns.Add("codigoPlan");
            dt.Columns.Add("codigoForm");

            //Agregando las filas
            if (formularios != null)
            {
                foreach (var item in formularios)
                {
                    dt.Rows.Add(plan.codigo, item);
                }
            }
            return dt;
        }

        /*
         * EFE:
         *      Creacion de la tabla de asociacion entre el objetivo y las secciones
         * REQ:
         * nombreObjetivos: lista de los objetivos del plan de mejora
         *       secciones: lista de secciones que queremos asociar a los diversos ojetivos
         *       
         *       Ambas listas,(secciones y nombreObjetivos), deben de ser del mismo tamaño.
         *       Para este metodo se crea la tabla de asociacion completa entre los objetivos y las secciones.
         * MOD:
         *      ---------
         */
        public DataTable getTablaAsociacionObjetivoSeccion(PlanDeMejora plan, List<string> nombreObjetivos, List<List<string>> secciones)
        {
            //Primero de crea una lista vacia de las asociaciones
            DataTable dt = new DataTable();
            // Creando las columnas
            dt.Columns.Add("codigoPlan");
            dt.Columns.Add("nombreObjetivo");
            dt.Columns.Add("codigoSeccion");

            if (nombreObjetivos != null) 
            {
                int indexObjetivos = -1;
                foreach (var nombreObjetivo in nombreObjetivos) 
                {
                    indexObjetivos++;
                    if (secciones != null)
                    {
                        var listaSeccionesDeObjetivo = secciones[indexObjetivos];
                        var tieneSecciones = false;
                        foreach (var nombreSeccion in listaSeccionesDeObjetivo) 
                        {
                            dt.Rows.Add(plan.codigo, nombreObjetivo, nombreSeccion);
                            tieneSecciones = true;
                        }
                        if (!tieneSecciones) {
                            dt.Rows.Add(plan.codigo, nombreObjetivo, null);
                        }
                    }
                    else 
                    {
                        // Para el caso en el que no hay una matriz de secciones
                        dt.Rows.Add(plan.codigo, nombreObjetivo, null);
                    }
                }
            }
            return dt;
        }

        /*
         * EFE:
         *      Creacion de la tabla de asociacion entre la acción de mejora y las preguntas
         * REQ:
         *    objetivos: lista de objetivos a los que se le asocian cada accion.
         *     acciones: lista de acciones de mejora.
         *    preguntas: lista de preguntas que se asocian a cada accion.
         *    
         *    Para este caso se hace unicamente la asociacion de las acciones y las respectivas preguntas por ununico objetivo,
         *    
         * MOD:
         *      ---------
         */
        public DataTable getTablaAsociacionAccionPregunta(PlanDeMejora plan, string nombreObjetivo, List<string> descripcionAcciones, List<List<string>> codigosPreguntas)
        {
            //Primero de crea una lista vacia de las asociaciones
            DataTable dt = new DataTable();
            // Creando las columnas
            dt.Columns.Add("codigoPlan");
            dt.Columns.Add("nombreObjetivo");
            dt.Columns.Add("descripcionAccion");
            dt.Columns.Add("codigoPregunta");

            if (descripcionAcciones != null)
            {
                int index = -1;
                foreach (var descripcionAccion in descripcionAcciones)
                {
                    index++;
                    if (codigosPreguntas != null)
                    {
                        var listaCodigosPreguntas = codigosPreguntas[index];
                        var almaceno = false;
                        foreach (var nombreSeccion in listaSeccionesDeObjetivo)
                        {
                            dt.Rows.Add(plan.codigo, nombreObjetivo, nombreSeccion);
                            almaceno = true;
                        }
                        if (!almaceno)
                        {
                            dt.Rows.Add(plan.codigo, nombreObjetivo, null);
                        }
                    }
                    else
                    {
                        // Para el caso en el que no hay una matriz de secciones
                        dt.Rows.Add(plan.codigo, nombreObjetivo, null);
                    }
                }
            }
            return dt;
        }
    }
}
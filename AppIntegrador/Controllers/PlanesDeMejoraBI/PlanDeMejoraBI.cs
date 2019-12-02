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
         *      Metodo encargado de enviar tablas al procedimito almacenado
         * REQ:
         *      tempTable: tabla con los datos 
         * MOD:
         *      Estado de la base de datos
         */
        public void enviarTablasAlmacenamiento(DataTable tempTable, string tableParamName, DataTable tablaAsocPlanFormularios, string AsocPlanFormualriosName)
        {
            String connectionString = ConfigurationManager.ConnectionStrings["DataIntegradorEntities"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("AgregarPlanComplete", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            //Pass table Valued parameter to Store Procedure
            SqlParameter sqlParam = cmd.Parameters.AddWithValue(tableParamName, tempTable);
            sqlParam = cmd.Parameters.AddWithValue(AsocPlanFormualriosName, tablaAsocPlanFormularios);

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
            foreach (var item in formularios) {
                dt.Rows.Add(plan.codigo, item);
            }
            
            return dt;
        }




        /*
         * EFE:
         *      Creacion de la tabla de asociacion entre el objetivo y las secciones
         * REQ:
         *     objetivo: objetivo al que le vamos a asociar las secciones
         *    secciones: lista de secciones que queremos asociar a un objetivo
         * MOD:
         *      ---------
         */
        /*public List<ObjetivoSeccion> getTablaAsociacionObjetivoSeccion(Objetivo objetivo, List<Seccion> secciones)
        {
            //Primero de crea una lista vacia de las asociaciones
            List<ObjetivoSeccion> asociacion = new List<ObjetivoSeccion>();
            foreach (var item in secciones)
            {
                var temp = new ObjetivoSeccion();

                temp.codPlan = objetivo.codPlan;
                temp.nombreObjetivo = objetivo.nombre;
                temp.codSeccion = item.Codigo;

                asociacion.Add(temp);
            }
            return asociacion;
        }*/

        /*
         * EFE:
         *      Creacion de la tabla de asociacion entre la acción de mejora y las preguntas
         * REQ:
         *     objetivo: objetivo al que le vamos a asociar las secciones
         *    secciones: lista de secciones que queremos asociar a un objetivo
         * MOD:
         *      ---------
         */
        /*public List<AccionPregunta> getTablaAsociacionObjetivoSeccion(AccionDeMejora accion, List<Pregunta> preguntas)
        {
            //Primero de crea una lista vacia de las asociaciones
            List<AccionPregunta> asociacion = new List<AccionPregunta>();
            foreach (var item in preguntas)
            {
                var temp = new AccionPregunta();
                
                temp.codPlanAccionDeMejora = accion.codPlan;
                temp.nombreObjetivo = accion.nombreObj;
                temp.descripcion = accion.descripcion;
                temp.codPregunta = item.Codigo;
                
                asociacion.Add(temp);
            }
            return asociacion;
        }*/
    }
}
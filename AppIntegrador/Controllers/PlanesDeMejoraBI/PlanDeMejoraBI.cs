using AppIntegrador.Models;
using AppIntegrador.Models.Metadata.RelacionesPlanesFormularios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
            // Buscando el código del último plan agregado
            var codigoDePlanAnterior = totalPlanes.Count == 0 ? 0 : totalPlanes.Last().codigo;
            // Aumentamos el codigo del plan de 1 en 1
            plan.codigo = codigoDePlanAnterior + 1;
            // Este metodo tambien deja el borrado en 0 ya que es un plan que se esta creando
            plan.borrado = false;
        }

        /*
         * EFE:
         *      Creacion de la tabla de asociacion entre el plan de mejora y el/los formularios
         * REQ:
         *           plan: instancia del plan de mejora que se quiere crear
         *    formularios: lista de objetivos del plan de mejora
         * MOD:
         *      ---------
         */
        public List<PlanFormulario> getTablaAsociacionPlanFormularios(PlanDeMejora plan, List<Formulario> formularios) 
        {
            //Primero de crea una lista vacia de las asociaciones
            List<PlanFormulario> asociacion = new List<PlanFormulario>();
            foreach (var item in formularios) 
            {
                var temp = new PlanFormulario();

                temp.codPlan = plan.codigo;
                temp.codFormulario = item.Codigo;

                asociacion.Add(temp);
            }
            return asociacion;
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
        public List<ObjetivoSeccion> getTablaAsociacionObjetivoSeccion(Objetivo objetivo, List<Seccion> secciones)
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
        }

        /*
         * EFE:
         *      Creacion de la tabla de asociacion entre la acción de mejora y las preguntas
         * REQ:
         *     objetivo: objetivo al que le vamos a asociar las secciones
         *    secciones: lista de secciones que queremos asociar a un objetivo
         * MOD:
         *      ---------
         */
        public List<AccionPregunta> getTablaAsociacionObjetivoSeccion(AccionDeMejora accion, List<Pregunta> preguntas)
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
        }


        /*
         EFE:
                Se encarga de buscar en la base de datos los formualrios y hacer una lista de las instancias de los mismos.
         REQ:
                         db: Instancia de la base de datos
                formularios: Lista de string de los identificadores de los formularios
         MOD:
                --------
         */
        public List<Formulario> getFormulariosFromDB(List<string> formularios)
        {
            List<Formulario> resultado = new List<Formulario>();
            foreach (var item in formularios)
            { 
                
            }
            return resultado;
        }
    }
}
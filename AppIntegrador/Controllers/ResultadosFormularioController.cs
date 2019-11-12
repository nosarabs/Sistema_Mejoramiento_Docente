using System;
using System.Globalization;
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
    public class ResultadosFormularioController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: ResultadosFormulario
        public ActionResult Formulario(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano, string fechaInicio, string fechaFin)
        {

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var modelo = new ResultadosFormulario
            {
                CodigoFormulario = serializer.Serialize(codigoFormulario),
                SiglaCurso = serializer.Serialize(siglaCurso),
                NumeroGrupo = serializer.Serialize(numeroGrupo),
                Semestre = serializer.Serialize(semestre),
                Ano = serializer.Serialize(ano),
                FechaInicio = serializer.Serialize(fechaInicio),
                FechaFin = serializer.Serialize(fechaFin),
                Preguntas = serializer.Serialize(ObtenerPreguntas(codigoFormulario))
                Secciones = ObtenerSecciones(codigoFormulario)
            };
            return View(modelo);
        }

        /*  ID: COD-65: Yo como administrador quiero ver las secciones que componen un formulario
            específico.
            Tarea: Crear un método que recupere las secciones asociadas a un formulario
        */
        public IEnumerable<Secciones> ObtenerSecciones(String codigoFormulario)
        {
            //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            // Extrae las secciones del formulario específico, dado por el código del formulario
            var secciones = from f in db.Formulario
                            join fs in db.Formulario_tiene_seccion on f.Codigo equals fs.FCodigo
                            join s in db.Seccion on fs.SCodigo equals s.Codigo
                            where f.Codigo == codigoFormulario
                            orderby fs.Orden
                            select new Secciones { codigoSeccion = fs.SCodigo, textoSeccion = s.Nombre };

            var listaSecciones = secciones.ToList();

            return listaSecciones;
            // ESTO ES PARA CUANDO SE INTENTE LLAMAR AL MÉTODO A PARTIR DEL FILTRO
            //return serializer.Serialize(secciones.ToList());
        }

        // GET: PreguntasFormulario
        public List<Preguntas> ObtenerPreguntas(String codigoFormulario)
        {
            var preguntas =     from f in db.Formulario
                                join fs in db.Formulario_tiene_seccion on f.Codigo equals fs.FCodigo
                                join s in db.Seccion on fs.SCodigo equals s.Codigo
                                join sp in db.Seccion_tiene_pregunta on s.Codigo equals sp.SCodigo
                                join p in db.Pregunta on sp.PCodigo equals p.Codigo
                                where f.Codigo == codigoFormulario
                                orderby fs.Orden, sp.Orden
                                select new Preguntas { codigoSeccion = sp.SCodigo, codigoPregunta = p.Codigo, textoPregunta = p.Enunciado };

            var listaPreguntas = preguntas.ToList();

            for (int i = 0; i < preguntas.Count(); ++i)
            {
                listaPreguntas[i].tipoPregunta = GetTipoPregunta(listaPreguntas[i].codigoPregunta);
            }

            return listaPreguntas;
        }

        public string ObtenerEtiquetasEscala(string codigoPregunta)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            List<string> ejeX = new List<string>();

            var minimo = (from f in db.Escalar
                          where f.Codigo == codigoPregunta
                          select f.Minimo).First();

            var maximo = (from f in db.Escalar
                          where f.Codigo == codigoPregunta
                          select f.Maximo).First();
            var incremento = (from f in db.Escalar
                              where f.Codigo == codigoPregunta
                              select f.Incremento).First();


            // Iteracion sobre una lista nueva
            for (int index = minimo; index <= maximo; index += incremento)
            {
                // Agrega la opcion posible a la lista
                ejeX.Add(index.ToString());
            }
            return serializer.Serialize(ejeX);
        }

        public String ObtenerRespuestasEscala(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano, System.DateTime fechaInicio, System.DateTime fechaFin, String codigoSeccion, String codigoPregunta)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var minimo = (from f in db.Escalar
                          where f.Codigo == codigoPregunta
                          select f.Minimo).First();

            var maximo = (from f in db.Escalar
                          where f.Codigo == codigoPregunta
                          select f.Maximo).First();
            var incremento = (from f in db.Escalar
                              where f.Codigo == codigoPregunta
                              select f.Incremento).First();

            List<int> ejeY = new List<int>();

            var numOpcion = 0;
            // Iteracion sobre una lista nueva
            for (int index = minimo; index <= maximo; index += incremento)
            {
                
                var contadorRespuestas = (from f in db.Opciones_seleccionadas_respuesta_con_opciones
                                          where f.OpcionSeleccionada == numOpcion
                                          && f.FCodigo == codigoFormulario
                                          && f.CSigla == siglaCurso
                                          && f.GNumero == numeroGrupo
                                          && f.GSemestre == semestre
                                          && f.GAnno == ano
                                          && f.SCodigo == codigoSeccion
                                          && f.PCodigo == codigoPregunta
                                          && f.Fecha >= fechaInicio
                                          && f.Fecha <= fechaFin
                                          select f.OpcionSeleccionada).Count();
                ejeY.Add(contadorRespuestas);
                ++numOpcion;
            }
            return serializer.Serialize(ejeY);
        }

        public String ObtenerRespuestasTextoAbierto(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano, System.DateTime fechaInicio, System.DateTime fechaFin, String codigoSeccion, String codigoPregunta)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var respuestas = from rrl in db.Responde_respuesta_libre
                             where rrl.FCodigo == codigoFormulario
                             && rrl.CSigla == siglaCurso
                             && rrl.GNumero == numeroGrupo
                             && rrl.GSemestre == semestre
                             && rrl.GAnno == ano
                             && rrl.SCodigo == codigoSeccion
                             && rrl.PCodigo == codigoPregunta
                             && rrl.Fecha >= fechaInicio
                             && rrl.Fecha <= fechaFin
                             select new SelectListItem { Value = rrl.Observacion };

            return serializer.Serialize(respuestas.ToList());
        }

        public String GetTipoPregunta(String codigoPregunta)
        {
            String tipo = "";

            List<Pregunta> preguntas = db.Pregunta.Where(x => x.Codigo.Equals(codigoPregunta)).ToList();

            if(preguntas != null)
            {
                Pregunta pregunta = preguntas.First();

                switch(pregunta.Tipo)
                {
                    case "U":
                        tipo = "seleccion_unica";
                        break;
                    case "M":
                        tipo = "seleccion_multiple";
                        break;
                    case "L":
                        tipo = "texto_abierto";
                        break;
                    case "S":
                        tipo = "seleccion_cerrada";
                        break;
                    case "E":
                        tipo = "escala";
                        break;
                    default:
                        break;
                }
            }

            return tipo;
        }

        public String ObtenterOpcionesPreguntasSeleccion(String codigoPregunta)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var opciones = from ods in db.Opciones_de_seleccion
                           where ods.Codigo.Equals(codigoPregunta)
                           orderby ods.Orden
                           select ods.Texto;
            return serializer.Serialize(opciones);
        }

        public String ObtenerOpcionesSeleccionadasPreguntasSeleccion(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano, System.DateTime fechaInicio, System.DateTime fechaFin, String codigoSeccion, String codigoPregunta, int numOpciones)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<int> respuestas = new List<int>();


            for (int i = 0; i < numOpciones; ++i)
            {
                respuestas.Add(
                    (from osrco in db.Opciones_seleccionadas_respuesta_con_opciones
                     where osrco.OpcionSeleccionada == i
                        && osrco.FCodigo == codigoFormulario
                        && osrco.CSigla == siglaCurso
                        && osrco.GNumero == numeroGrupo
                        && osrco.GSemestre == semestre
                        && osrco.GAnno == ano
                        && osrco.SCodigo == codigoSeccion
                        && osrco.PCodigo == codigoPregunta
                        && osrco.Fecha >= fechaInicio
                        && osrco.Fecha <= fechaFin
                     select osrco).Count());
            }
                                          
            return serializer.Serialize(respuestas);
        }

        public String getJustificacionPregunta(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano, System.DateTime fechaInicio, System.DateTime fechaFin, String codigoSeccion, String codigoPregunta)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<int> justificaciones = new List<int>();

            var respuestas =    from rrco in db.Responde_respuesta_con_opciones
                                where rrco.FCodigo == codigoFormulario
                                && rrco.CSigla == siglaCurso
                                && rrco.GNumero == numeroGrupo
                                && rrco.GSemestre == semestre
                                && rrco.GAnno == ano
                                && rrco.SCodigo == codigoSeccion
                                && rrco.PCodigo == codigoPregunta
                                && rrco.Fecha >= fechaInicio
                                && rrco.Fecha <= fechaFin
                                select new SelectListItem { Value = rrco.Justificacion };

            return serializer.Serialize(respuestas.ToList());
        }

        public String obtenerDesviacionEstandar(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano, System.DateTime fechaInicio, System.DateTime fechaFin, String codigoSeccion, String codigoPregunta) {

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ObjectParameter resultado = new ObjectParameter("Desviacion", typeof(float));
            db.DesviacionEstandarEscalar(codigoFormulario, siglaCurso, numeroGrupo, ano, semestre, fechaInicio, fechaFin, codigoSeccion, codigoPregunta, resultado);

            return serializer.Serialize(resultado.Value);

        }


        public String getMedianaRespuestaEscalar(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano, System.DateTime fechaInicio, System.DateTime fechaFin, String codigoSeccion, String codigoPregunta)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ObjectParameter resultadoMediana = new ObjectParameter("mediana", typeof(float));
            db.Mediana(codigoFormulario, siglaCurso, numeroGrupo, ano, semestre, fechaInicio, fechaFin, codigoSeccion, codigoPregunta, resultadoMediana);

            return serializer.Serialize(resultadoMediana.Value);
        }

        //Denisse Alfaro P. Josue Zeledon R.
        //COD-4: Visualizar el promedio para las respuestas de las preguntas de escala numérica. 
        //Tarea técnica: Al seleccionar una pregunta de escala numerica en la vista, invocar al controlador para que este llame a la funcion de la base de datos. 
        //Cumplimiento: 7/10
        public String getPromedio(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano, System.DateTime fechaInicio, System.DateTime fechaFin, String codigoSeccion, String codigoPregunta)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            ObjectParameter resultPromedio = new ObjectParameter("promedio", typeof(float));
            db.PromedioRespuestasPreguntaEscalaNumerica(codigoFormulario, siglaCurso, numeroGrupo, ano, semestre, fechaInicio, fechaFin, codigoSeccion, codigoPregunta, resultPromedio);

            return serializer.Serialize(resultPromedio.Value);
        }

    }
}
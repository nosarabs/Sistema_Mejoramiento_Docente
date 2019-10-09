using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class ResultadosFormularioController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: ResultadosFormulario
        public ActionResult Formulario(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 año)
        {

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var modelo = new ResultadosFormulario
            {
                CodigoFormulario = serializer.Serialize(codigoFormulario),
                SiglaCurso = serializer.Serialize(siglaCurso),
                NumeroGrupo = serializer.Serialize(numeroGrupo),
                Semestre = serializer.Serialize(semestre),
                Año = serializer.Serialize(año),
                Preguntas = serializer.Serialize(ObtenerPreguntas(codigoFormulario))
            };
            return View(modelo);
        }

        // GET: PreguntasFormulario
        [HttpGet]
        public IEnumerable<SelectListItem> ObtenerPreguntas(String codigoFormulario)
        {
            var preguntas = from f in db.Formulario
                            join fs in db.Formulario_tiene_seccion on f.Codigo equals fs.FCodigo
                            join s in db.Seccion on fs.SCodigo equals s.Codigo
                            join sp in db.Seccion_tiene_pregunta on s.Codigo equals sp.SCodigo
                            join p in db.Pregunta on sp.PCodigo equals p.Codigo
                            where f.Codigo == codigoFormulario
                            orderby fs.Orden, sp.Orden
                            select new SelectListItem { Value = p.Codigo, Text = p.Enunciado };

            return preguntas.ToList();
        }

        // GET: RespuestasFormulario
        [HttpGet]
        public IEnumerable<SelectListItem> ObtenerRespuestas(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 año, String codigoPregunta)
        {
            var respuestas = from f in db.Opciones_seleccionadas_respuesta_con_opciones
                             where f.FCodigo == codigoFormulario && f.CSigla == siglaCurso && f.GNumero == numeroGrupo && f.GSemestre == semestre && f.GAnno == año && f.PCodigo == codigoPregunta
                             select new SelectListItem { Value = f.OpcionSeleccionada.ToString() };

            return respuestas.ToList();
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
            for (int index = minimo; index <= maximo; index+=incremento)
            {
                // Agrega la opcion posible a la lista
                ejeX.Add(index.ToString());
            }
            return serializer.Serialize(ejeX);
        }
  
        public string ObtenerRespuestasEscala(string codigoPregunta)
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

            // Iteracion sobre una lista nueva
            for (int index = minimo; index <= maximo; index += incremento)
            {
                var contadorRespuestas = (from f in db.Opciones_seleccionadas_respuesta_con_opciones
                                          where f.OpcionSeleccionada == index && f.PCodigo == codigoPregunta
                                          select f.OpcionSeleccionada).Count();
                ejeY.Add(contadorRespuestas);
            }
            return serializer.Serialize(ejeY);
        }

        [HttpGet]
        public string GetTipoPregunta(string codigoPregunta)
        {
            //Console.WriteLine("Entra a getTipoPregunta");
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //return serializer.Serialize("escala");
            //JObject json = JObject.Parse("escala");
            //return "escala";



            List<string> tipo =new List<string>();
            
            //string tipo;

            if ((from pcrl in db.Pregunta_con_respuesta_libre
                 where pcrl.Codigo == codigoPregunta
                 select pcrl).Count() != 0)
                tipo.Add("texto_abierto");
                //tipo ="texto_abierto";
                else
                    if ((from e in db.Escalar
                         where e.Codigo == codigoPregunta
                         select e).Count() != 0)
                        tipo.Add("escala");
                        //tipo="escala";
                    else
                        if ((from snnr in db.Si_no_nr
                             where snnr.Codigo == codigoPregunta
                             select snnr).Count() != 0)
                            tipo.Add("seleccion_cerrada");
                            //tipo="seleccion_cerrada";
                        else
                            if ((from pcods in db.Pregunta_con_opciones_de_seleccion
                                    where pcods.Codigo == codigoPregunta & pcods.Tipo == "M"
                                    select pcods).Count() != 0)
                                tipo.Add("seleccion_multiple");
                                //tipo="seleccion_multiple";
                            else
                                if ((from pcods in db.Pregunta_con_opciones_de_seleccion
                                 where pcods.Codigo == codigoPregunta & pcods.Tipo == "U"
                                 select pcods).Count() != 0)
                                tipo.Add("seleccion_unica");

            return serializer.Serialize(tipo);
            //return tipo;
        }
    

        /*
         Método que recupera las opciones de repuestas a una pregunta de selección múltiple para un formulario
        */
        public string ObtenerRespuestasSeleccionMultiple(string codigoPregunta)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //List<string> opciones = new List<string>();
            var opciones = from ods in db.Opciones_de_seleccion
                           join pcods in db.Pregunta_con_opciones_de_seleccion on ods.Codigo equals pcods.Codigo
                           where (pcods.Tipo.Equals('M')) && (pcods.Codigo == codigoPregunta)
                           orderby ods.Orden
                           select ods.Texto;

            return serializer.Serialize(opciones);
        }
    }
}
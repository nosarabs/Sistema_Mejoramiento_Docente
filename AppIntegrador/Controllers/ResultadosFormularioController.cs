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
        public ActionResult Formulario(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano)
        {

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); 

            var modelo = new ResultadosFormulario
            {
                CodigoFormulario = serializer.Serialize(codigoFormulario),
                SiglaCurso = serializer.Serialize(siglaCurso),
                NumeroGrupo = serializer.Serialize(numeroGrupo),
                Semestre = serializer.Serialize(semestre),
                Ano = serializer.Serialize(ano),
                Preguntas = serializer.Serialize(ObtenerPreguntas(codigoFormulario))
            };
            return View(modelo);
        }

        // GET: PreguntasFormulario
        [HttpGet]
        public IEnumerable<SelectListItem> ObtenerPreguntas (String codigoFormulario)
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
        public IEnumerable<SelectListItem> ObtenerRespuestas(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano, String codigoPregunta)
        {
            var respuestas = from f in db.Opciones_seleccionadas_respuesta_con_opciones
                             where f.FCodigo == codigoFormulario && f.CSigla == siglaCurso && f.GNumero == numeroGrupo && f.GSemestre == semestre && f.GAnno == ano && f.PCodigo == codigoPregunta
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
  
        public String ObtenerRespuestasEscala(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano, String codigoPregunta)
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
                                          where f.OpcionSeleccionada == index
                                          && f.FCodigo == codigoFormulario
                                          && f.CSigla == siglaCurso
                                          && f.GNumero == numeroGrupo
                                          && f.GSemestre == semestre
                                          && f.GAnno == ano
                                          && f.PCodigo == codigoPregunta
                                          select f.OpcionSeleccionada).Count();
                ejeY.Add(contadorRespuestas);
            }
            return serializer.Serialize(ejeY);
        }

        [HttpGet]
        public String ObtenerRespuestasTextoAbierto(String codigoFormulario, String siglaCurso, Byte numeroGrupo, Byte semestre, Int32 ano, String codigoPregunta)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var respuestas = from rrl in db.Responde_respuesta_libre
                             where rrl.FCodigo == codigoFormulario
                             && rrl.CSigla == siglaCurso
                             && rrl.GNumero == numeroGrupo
                             && rrl.GSemestre == semestre
                             && rrl.GAnno == ano
                             && rrl.PCodigo == codigoPregunta
                             select new SelectListItem { Value = rrl.Observacion };

            return serializer.Serialize(respuestas.ToList());
        }
    }
}
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
            var modelo = new ResultadosFormulario
            {
                CodigoFormulario = codigoFormulario,
                SiglaCurso = siglaCurso,
                NumeroGrupo = numeroGrupo,
                Semestre = semestre,
                Año = año,
                Preguntas = ObtenerPreguntas(codigoFormulario)
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
    }
}
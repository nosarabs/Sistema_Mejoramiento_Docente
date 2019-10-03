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
        public ActionResult Formulario(String codigo)
        {
            ViewBag.codigo = codigo;
            return View();
        }

        // GET: PreguntasFormulario
        /*[HttpGet]
        public IEnumerable<SelectListItem> ObtenerPreguntas ()
        {
            return 
        }*/
    }
}
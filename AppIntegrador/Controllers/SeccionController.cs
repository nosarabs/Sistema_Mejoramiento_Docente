using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace AppIntegrador.Controllers
{
    public class SeccionController : Controller
    {
        private DataIntegradorEntities db = null;
        public CrearSeccionModel crearSeccion = new CrearSeccionModel();

        public SeccionController()
        {
            this.db = new DataIntegradorEntities();
        }

        public SeccionController(DataIntegradorEntities db)
        {
            this.db = db;
        }

        // GET: Seccion
        public ActionResult Index(string input0, string input1, string input2)
        {
            var seccion = db.Seccion;

            ViewBag.filtro = "Ninguno";
            if (input0 == null && input1 == null && input2 == null)
            {
                ViewBag.filtro = "Ninguno";
                return View(seccion.ToList());
            }
            // si se selecionó el código  
            if (input1.Length > 0)
            {
                ViewBag.filtro = "Por código: " + input1;
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(seccion.Where(x => x.Codigo.Contains(input1)).ToList());
            }
            // si se selecionó el enunciado 
            else if (input2.Length > 0)
            {
                ViewBag.filtro = "Nombre: " + input2;
                return View(seccion.Where(x => x.Nombre.Contains(input2)).ToList());
            }
            else
            {
                ViewBag.filtro = "Ninguno";
                return View(seccion.ToList());
            }
        }

        // GET: Seccion/Create
        public ActionResult Create()
        {
            crearSeccion.pregunta = db.Pregunta;
            return View(crearSeccion);
        }

        // POST: Seccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre")] Seccion seccion, List<Pregunta_con_opciones_de_seleccion> preguntas)
        {
            crearSeccion.pregunta = db.Pregunta;
            if (ModelState.IsValid && seccion.Codigo.Length > 0 && seccion.Nombre.Length > 0)
            {
                if ( InsertSeccionTienePregunta(seccion, preguntas) )
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    // Notifique que ocurrió un error
                    ModelState.AddModelError("Seccion.Codigo", "Código ya en uso.");
                    return View(crearSeccion);
                }
            }

            return View(crearSeccion);
        }

        // Historia RIP-BKS1
        // Se copió la función para filtrar preguntas.
        [HttpGet]
        public ActionResult Create(string input0, string input1, string input2, string input3)
        {
            crearSeccion.pregunta = db.Pregunta;
            ViewBag.filtro = "Ninguno";
            if (input0 == null && input1 == null && input2 == null && input3 == null)
            {
                crearSeccion.pregunta = db.Pregunta.ToList();
                return View("Create", crearSeccion);
            }
            //if a user choose the radio button option as Subject  
            if (input1.Length > 0)
            {
                ViewBag.filtro = "Por código: " + input1;
                crearSeccion.pregunta = db.Pregunta.Where(x => x.Codigo.Contains(input1)).ToList();
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
            }
            else if (input2.Length > 0)
            {
                ViewBag.filtro = "Enunciado: " + input2;
                crearSeccion.pregunta = db.Pregunta.Where(x => x.Enunciado.Contains(input2)).ToList();
            }
            else if (input3.Length > 0)
            {
                var aux = "";
                switch(input3)
                {
                    case "U":
                        aux = "Selección Única";
                        break;
                    case "M":
                        aux = "Selección Múltiple";
                        break;
                    case "L":
                        aux = "Respuesta libre";
                        break;
                    case "S":
                        aux = "Sí/No/NR";
                        break;
                    case "E":
                        aux = "Escalar";
                        break;
                    default:
                        break;
                }
                ViewBag.filtro = "Tipo: " + aux;
                crearSeccion.pregunta = db.Pregunta.Where(x => x.Tipo.Contains(input3)).ToList();
            }
            else
            {
                ViewBag.filtro = "Ninguno";
                crearSeccion.pregunta = db.Pregunta.ToList();
            }
            return View("Create", crearSeccion);
        }

       
        private bool InsertSeccionTienePregunta(Seccion seccion, List<Pregunta_con_opciones_de_seleccion> preguntas)
        {
            try
            {
                if (db.AgregarSeccion(seccion.Codigo, seccion.Nombre) == 0)
                {
                    return false;
                }
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException)
            {
                return false;
            }

            if (preguntas != null)
            {
                for (int index = 0; index < preguntas.Count; ++index)
                {
                    db.AsociarPreguntaConSeccion(seccion.Codigo, preguntas[index].Codigo);
                }
            }
            return true;
        }

        [HttpGet]
        public ActionResult VistaPrevia(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        [HttpPost]
        public ActionResult ActualizarBancoSecciones()
        {
            return PartialView("~/Views/Seccion/_SeccionPartial.cshtml", db.Seccion);
        }
        [HttpPost]
        public ActionResult ActualizarCrearSeccion()
        {
            crearSeccion = new CrearSeccionModel();
            return PartialView("~/Views/Seccion/_CreateSeccionPartial.cshtml", crearSeccion);
        }

        public bool AgregarPreguntasASeccion(string codSeccion, List<string> codPreguntas)
        {
            if (codPreguntas != null)
            {
                for (int index = 0; index < codPreguntas.Count; ++index)
                {
                    db.AsociarPreguntaConSeccion(codSeccion, codPreguntas[index]);
                }
            }
            return true;
        }

        [HttpPost]
        public ActionResult AgregarPreguntas(string codSeccion, List<string> codPreguntas)
        {
            return Json(new { insertadoExitoso = AgregarPreguntasASeccion(codSeccion, codPreguntas) });
        }
    }
}

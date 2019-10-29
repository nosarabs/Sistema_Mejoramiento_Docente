using System.Collections.Generic;
using System.Web.Mvc;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class PreguntasController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: Preguntas/Create
        public ActionResult Create()
        {
            ViewBag.message = "Crear pregunta";
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pregunta pregunta, List<Opciones_de_seleccion> Opciones)
        {
            if (ModelState.IsValid && pregunta.Codigo.Length > 0 && pregunta.Enunciado.Length > 0)
            {
                bool validOptions = Opciones != null;
                if (validOptions)
                {
                    validOptions = false;
                    foreach (Opciones_de_seleccion opcion in Opciones)
                    {
                        if (opcion.Texto != null && opcion.Texto.Length > 0)
                        {
                            validOptions = true;
                        }
                    }
                }

                if (!validOptions)
                {
                    ModelState.AddModelError("", "Una pregunta de selección única necesita al menos una opción");
                    return View(pregunta);
                }
                try
                {
                    if (db.AgregarPreguntaConOpcion(pregunta.Codigo, "U", pregunta.Enunciado, pregunta.Pregunta_con_opciones.TituloCampoObservacion) == 0)
                    {
                        ModelState.AddModelError("Codigo", "Código ya en uso.");
                        return View(pregunta);
                    }
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    ModelState.AddModelError("Codigo", "Código ya en uso.");
                    return View(pregunta);
                }

                foreach (Opciones_de_seleccion opcion in Opciones)
                {
                    db.AgregarOpcion(pregunta.Codigo, (byte)opcion.Orden, opcion.Texto);
                }

                ViewBag.Message = "Exitoso";
                return View();
            }

            return View();
        }

        // Retorna la vista "parcial" de pregunta con opciones (.cshtml)
        public ActionResult PreguntaConOpciones()
        {
            ViewBag.message = "Pregunta con opciones";
            return View("Pregunta con opciones");

        }

        [HttpGet]
        public ActionResult OpcionesDeSeleccion(int i)
        {
            if(i < 0)
            {
                return null;
            }
            ViewBag.i = i;
            return View("Opciones");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }





}






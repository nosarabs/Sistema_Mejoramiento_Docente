using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class PreguntasController : Controller
    {
        private DataIntegradorEntities db;
        private DataIntegradorEntities @object;

        public PreguntasController()
        {
            this.db = new DataIntegradorEntities();
        }

        public PreguntasController(DataIntegradorEntities db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult Banco(string input0, string input1, string input2, string input3)
        {
            var pregunta = db.Pregunta;

            ViewBag.filtro = "Ninguno";
            if (input0 == null && input1 == null && input2 == null && input3 == null)
            {
                ViewBag.filtro = "Ninguno";
                return View(pregunta.ToList());
            }
            // si se selecionó el código  
            if (input1.Length > 0)
            {
                ViewBag.filtro = "Por código: " + input1;
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(pregunta.Where(x => x.Codigo.Contains(input1)).ToList());
            }
            // si se selecionó el enunciado 
            else if (input2.Length > 0)
            {
                ViewBag.filtro = "Enunciado: " + input2;
                return View(pregunta.Where(x => x.Enunciado.Contains(input2)).ToList());
            }
            // si se seleccionó el tipo
            else if (input3.Length > 0)
            {
                var aux = "";
                switch (input3)
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
                return View(pregunta.Where(x => x.Tipo.Contains(input3)).ToList());
            }
            else
            {
                ViewBag.filtro = "Ninguno";
                return View(pregunta.ToList());
            }
        }
        [HttpPost]
        public ActionResult ActualizarBancoPreguntas(string input0 = null, string input1 = null, string input2 = null, string input3 = null)
        {
            var pregunta = db.Pregunta;

            ViewBag.filtro = "Ninguno";
            if (input0 == null && input1 == null && input2 == null && input3 == null)
            {
                ViewBag.filtro = "Ninguno";
                return PartialView("~/Views/PreguntaConOpcionesDeSeleccion/_IndexPartial.cshtml", pregunta.ToList());
            }
            // si se selecionó el código  
            if (input1.Length > 0)
            {
                ViewBag.filtro = "Por código: " + input1;
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return PartialView("~/Views/PreguntaConOpcionesDeSeleccion/_IndexPartial.cshtml", pregunta.Where(x => x.Codigo.Contains(input1)).ToList());
            }
            // si se selecionó el enunciado 
            else if (input2.Length > 0)
            {
                ViewBag.filtro = "Enunciado: " + input2;
                return PartialView("~/Views/PreguntaConOpcionesDeSeleccion/_IndexPartial.cshtml", pregunta.Where(x => x.Enunciado.Contains(input2)).ToList());
            }
            // si se seleccionó el tipo
            else if (input3.Length > 0)
            {
                var aux = "";
                switch (input3)
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
                return PartialView("~/Views/PreguntaConOpcionesDeSeleccion/_IndexPartial.cshtml", pregunta.Where(x => x.Tipo.Contains(input3)).ToList());
            }
            else
            {
                ViewBag.filtro = "Ninguno";
                return PartialView("~/Views/PreguntaConOpcionesDeSeleccion/_IndexPartial.cshtml", pregunta.ToList());
            }
        }

        // GET: Preguntas/Create
        public ActionResult Create()
        {
            ViewBag.message = "Crear pregunta";
            return View("Create");
        }

        public ActionResult GuardarRespuestaLibre(Pregunta pregunta)
        {
            // asegurarse que exista la preguna
            if (pregunta != null)
            {
                try
                {
                    // se trata de guardar la pregunta de respuesta libre 
                    if (db.AgregarPreguntaRespuestaLibre(pregunta.Codigo, "L", pregunta.Enunciado) == 0)
                    {
                        // si se presentó un problema, se devuelve el codigo de error
                        ModelState.AddModelError("Codigo", "Código ya en uso.");
                        return View(pregunta);
                    }
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    // si se presentó un problema, se devuelve el codigo de error
                    ModelState.AddModelError("Codigo", "Código ya en uso.");
                    return View(pregunta);
                }
            }

            ViewBag.message = "Crear pregunta";
            return View("Create");
        }

        public ActionResult GuardarPreguntaSiNo(Pregunta pregunta)
        {
            // asegurarse que exista la preguna
            if (pregunta != null)
            {
                try
                {
                    // se trata de guardar la pregunta de con opciones de si/no/nr
                    if (db.AgregarPreguntaConOpcion(pregunta.Codigo, "S", pregunta.Enunciado, pregunta.Pregunta_con_opciones.TituloCampoObservacion) == 0)
                    {
                        // si se presentó un problema, se devuelve el codigo de error
                        ModelState.AddModelError("Codigo", "Código ya en uso.");
                        return View(pregunta);
                    }
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    // si se presentó un problema, se devuelve el codigo de error
                    ModelState.AddModelError("Codigo", "Código ya en uso.");
                    return View(pregunta);
                }
            }

            ViewBag.message = "Crear pregunta";
            return View("Create");
        }

        public ActionResult GuardarPreguntaEscalar(Pregunta pregunta, int min, int max)
        {
            // asegurarse que exista la preguna
            if (pregunta != null)
            {
                if (max > min)
                {
                    try
                    {
                        // se trata de guardar la pregunta de con opciones de
                        if (db.AgregarPreguntaEscalar(pregunta.Codigo, "E", pregunta.Enunciado, pregunta.Pregunta_con_opciones.TituloCampoObservacion, 1, min, max) == 0)
                        {
                            // si se presentó un problema, se devuelve el codigo de error
                            ModelState.AddModelError("Codigo", "Código ya en uso.");
                            return View(pregunta);
                        }
                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        // si se presentó un problema, se devuelve el codigo de error
                        ModelState.AddModelError("Codigo", "Código ya en uso.");
                        return View(pregunta);
                    }
                }
                else
                {
                    // si está intentando poner un rango inválido
                    ModelState.AddModelError("min", "El mínimo debe ser menor al máximo");
                    return View(pregunta);
                }
            }

            ViewBag.message = "Crear pregunta";
            return View("Create");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pregunta pregunta, List<Opciones_de_seleccion> Opciones, int min = 0, int max = 0)
        {
            // Se fija que la pregunta no sea nula y que tenga opciones, a menos que sea escalar o libre, que no requieren opciones
            if (pregunta == null || (Opciones == null && pregunta.Tipo == "U" && pregunta.Tipo == "M"))
            {
                ModelState.AddModelError("", "Datos incompletos");
                return View("Create");
            }

            if (ModelState.IsValid && pregunta.Codigo.Length > 0 && pregunta.Enunciado.Length > 0)
            {
                // GuardarPreguntaSiNo las preguntas dependiendo del tipo
                // ToDo: realizar un método para cada tipo de pregunta
                switch (pregunta.Tipo)
                {
                    case "U": break;
                    case "M": break;
                    case "L": return GuardarRespuestaLibre(pregunta);
                    case "S": return GuardarPreguntaSiNo(pregunta);
                    case "E": return GuardarPreguntaEscalar(pregunta, min, max);
                }
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
                    if (db.AgregarPreguntaConOpcion(pregunta.Codigo, pregunta.Tipo, pregunta.Enunciado, pregunta.Pregunta_con_opciones.TituloCampoObservacion) == 0)
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

                ModelState.Clear();
                ViewBag.Message = "Exitoso";
                return View("Create");
            }
            else
            {
                ModelState.AddModelError("", "Datos incompletos");
                return View("Create");
            }
        }

        // Retorna la vista "parcial" de Respuesta libre (.cshtml)
        public ActionResult RespuestaLibre()
        {
            ViewBag.message = "Respuesta Libre";
            return View("RespuestaLibre");
        }

        // Retorna la vista "parcial" de Pregunta Escalar (.cshtml)
        public ActionResult PreguntaEscalar()
        {
            ViewBag.message = "Pregunta Escalar";
            return View("PreguntaEscalar");
        }

        // Retorna la vista "parcial" de pregunta Si/No/NR (.cshtml)
        public ActionResult PreguntaSiNo()
        {
            ViewBag.message = "Pregunta Si/No/NR";
            return View("PreguntaSiNo");
        }

        // Retorna la vista "parcial" de pregunta con opciones (.cshtml)
        public ActionResult PreguntaConOpciones()
        {
            ViewBag.message = "Pregunta con opciones";
            return View("PreguntaConOpciones");

        }

        [HttpGet]
        public ActionResult OpcionesDeSeleccion(int i, char Tipo)
        {
            if (i < 0)
            {
                return null;
            }
            ViewBag.Tipo = Tipo;
            ViewBag.i = i;
            return View("OpcionesDeSeleccion");
        }

        [HttpGet]
        public ActionResult VistaPrevia(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View(pregunta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult Estilos()
        {
            ViewBag.message = "Estilos UCR";
            return View("Estilos");
        }

    }




}






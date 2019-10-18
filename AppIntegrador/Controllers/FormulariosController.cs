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
    public class FormulariosController : Controller
    {
        
        private DataIntegradorEntities db = new DataIntegradorEntities();
        public CrearFormularioModel crearFormulario = new CrearFormularioModel();


        // GET: Formularios
        private class SeccionYCodigo
        {
            // It is important to declare them public so they get returned
            public string Codigo { get; set; }
            public string Nombre { get; set; }
        }
        private class Opcion
        {
            public string Texto { get; set; }
            public int Orden { get; set; }
        }

        public ActionResult LlenarFormulario(string id)
        {
            Formulario formularioDB = db.Formulario.Find(id);
            if (formularioDB == null)
            {
                return RedirectToAction("Index");
            }

            List<SeccionConPreguntas> secciones = new List<SeccionConPreguntas>();

            // Sacar el nombre de cada formulario y el código en el orden definido.
            SqlParameter codForm = new SqlParameter("CodForm", id);
            List<SeccionYCodigo> nombresSecciones = db.Database.SqlQuery<SeccionYCodigo>("EXEC ObtenerSeccionesDeFormulario @CodForm", codForm).ToList();

            // Agrego cada seccion a la lista de secciones
            foreach (var seccion in nombresSecciones)
            {
                SqlParameter sectionCode = new SqlParameter("sectionCode", seccion.Codigo);

                // Obtiene los códigos de todas las preguntas relacionadas a la sección
                List<string> Codigos = db.Database.SqlQuery<string>("EXEC ObtenerPreguntasDeSeccion @sectionCode", sectionCode).ToList();
                // Lista con cada tipo de pregunta
                TodasLasPreguntas todasLasPreguntas = new TodasLasPreguntas();
                // Lista que contiene cada pregunta con sus opciones
                List<PreguntaConOpciones> preguntasConOpciones = new List<PreguntaConOpciones>();

                // Agrego cada pregunta a la lista de preguntas
                foreach (string codigo in Codigos)
                {
                    PreguntaConOpciones pregunta = new PreguntaConOpciones();

                    SqlParameter questionCode = new SqlParameter("questionCode", codigo);
                    SqlParameter questionCode2 = new SqlParameter("questionCode", codigo);

                    // Obtiene el enunciado de una pregunta
                    pregunta.Enunciado = db.Database.SqlQuery<string>("SELECT p.Enunciado FROM Pregunta p WHERE p.Codigo = @questionCode", questionCode).First();

                    // Obtiene las opciones de una pregunta
                    List<Opcion> opciones = db.Database.SqlQuery<Opcion>("EXEC ObtenerOpcionesDePregunta @questionCode", questionCode2).ToList();
                    pregunta.Opciones = opciones.Select(Opcion => Opcion.Texto);

                    // Añade la pregunta con sus opciones a la lista
                    preguntasConOpciones.Add(pregunta);
                }

                todasLasPreguntas.PreguntasConOpciones = (IEnumerable<PreguntaConOpciones>)preguntasConOpciones;

                SeccionConPreguntas seccionCompleta = new SeccionConPreguntas
                {
                    Nombre = seccion.Nombre,
                    Preguntas = todasLasPreguntas
                };

                secciones.Add(seccionCompleta);
            } // Foreach section in nombresSecciones

            LlenarFormulario formularioCompleto = new LlenarFormulario
            {
                Nombre = formularioDB.Nombre,
                Secciones = secciones
            };

            return View(formularioCompleto);
        }


        // GET: Formularios
        public ActionResult Index(string inp1, string inp2)
        {
            if (inp2 == null && inp1 == null)
            {
                return View(db.Formulario.ToList());
            }
            //if a user choose the radio button option as Subject  
            if (inp2 == null)
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.Formulario.Where(x => x.Codigo.Contains(inp1)).ToList());
            }
            else if (inp1 == null)
            {
                return View(db.Formulario.Where(x => x.Nombre.Contains(inp2)).ToList());
            }
            else
            {
                return View(db.Formulario.ToList());
            }
        }


        // GET: Formularios/Details/5
        public ActionResult Details(string id)
        {
            crearFormulario.seccion = db.Seccion;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formulario.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }
            return View(formulario);
        }

        // GET: Formularios/Create
        public ActionResult Create()
        {
            crearFormulario.seccion = db.Seccion;
            return View(crearFormulario);
        }

        // POST: Formularios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        public ActionResult Create([Bind(Include = "Codigo,Nombre")] Formulario formulario, List<Seccion> secciones)
        {
            crearFormulario.seccion = db.Seccion;
            if (ModelState.IsValid && formulario.Codigo.Length > 0 && formulario.Nombre.Length > 0)
            {
                if(InsertFormularioTieneSeccion(formulario, secciones))
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    // Notifique que ocurrió un error
                    ModelState.AddModelError("Codigo", "Código ya en uso.");
                    return View(crearFormulario);
                }
            }

            return View(crearFormulario);
        }

        // GET: Formularios/Edit/5
        public ActionResult Edit(string id)
        {
            crearFormulario.seccion = db.Seccion;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formulario.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }
            return View(formulario);
        }

        // Historia RIP-CF5
        // Se copió la función para filtrar preguntas.
        [HttpGet]
        public ActionResult Create(string inp1, string inp2)
        {
            crearFormulario.seccion = db.Seccion;
            if (inp2 == null && inp1 == null)
            {
                crearFormulario.seccion = db.Seccion.ToList();

            }
            //if a user choose the radio button option as Subject  
            else if (inp2 == null)
            {
                crearFormulario.seccion = db.Seccion.Where(x => x.Codigo.Contains(inp1)).ToList();
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
            }
            else if (inp1 == null)
            {
                crearFormulario.seccion = db.Seccion.Where(x => x.Nombre.Contains(inp2)).ToList();
            }
            else
            {
                crearFormulario.seccion = db.Seccion.ToList();
            }
            return View("Create", crearFormulario);
        }

        // POST: Formularios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre")] Formulario formulario)
        {
            crearFormulario.seccion = db.Seccion;
            if (ModelState.IsValid)
            {
                db.Entry(formulario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formulario);
        }

        // GET: Formularios/Delete/5
        public ActionResult Delete(string id)
        {
            crearFormulario.seccion = db.Seccion;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formulario.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }
            return View(formulario);
        }

        // POST: Formularios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            crearFormulario.seccion = db.Seccion;
            Formulario formulario = db.Formulario.Find(id);
            db.Formulario.Remove(formulario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            crearFormulario.seccion = db.Seccion;
            if (disposing)
            {
                db.Dispose();
            }
            crearFormulario.seccion = db.Seccion;
            base.Dispose(disposing);
        }


        

        private bool InsertFormularioTieneSeccion(Formulario formulario, List<Seccion> secciones)
        {
            try
            {
                if (db.AgregarFormulario(formulario.Codigo, formulario.Nombre) == 0)
                {
                    return false;
                }
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException)
            {
                return false;
            }

            if (secciones != null)
            {
                for (int index = 0; index < secciones.Count; ++index)
                {
                    db.AsociarSeccionConFormulario(formulario.Codigo, secciones[index].Codigo, index);
                }
            }
            return true;
        }
    }
}

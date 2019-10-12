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

namespace AppIntegrador.Controllers
{
    public class FormulariosController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

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
                return HttpNotFound();
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
        public ActionResult Index()
        {
            return View(db.Formulario.ToList());
        }



        // GET: Formularios/Details/5
        public ActionResult Details(string id)
        {
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
            var crearFormulario = new CrearFormularioModel();
            crearFormulario.seccion = db.Seccion.ToList();
            return View(crearFormulario);
        }

        // POST: Formularios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre")] Formulario formulario)
        {
            try
            {

                if (ModelState.IsValid && formulario.Codigo.Length > 0 && formulario.Nombre.Length > 0)
                {
                    db.Formulario.Add(formulario);
                    db.SaveChanges();
                    return RedirectToAction("Create");
                }
            }
            catch (Exception exception)
            {
                if (exception is System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    ModelState.AddModelError("Codigo", "Código ya en uso.");
                    return View(formulario);
                }
            }

            return View();
        }

        // GET: Formularios/Edit/5
        public ActionResult Edit(string id)
        {
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

        // POST: Formularios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre")] Formulario formulario)
        {
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
            Formulario formulario = db.Formulario.Find(id);
            db.Formulario.Remove(formulario);
            db.SaveChanges();
            return RedirectToAction("Index");
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

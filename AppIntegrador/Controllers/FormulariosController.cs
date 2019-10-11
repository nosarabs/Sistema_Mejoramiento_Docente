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

        // GET: Formularios
        public ActionResult Index()
        {
            return View(db.Formulario.ToList());
        }

        public ActionResult LlenarFormulario(string id)
        {
            Formulario formulario = db.Formulario.Find(id);

            if (formulario == null)
            {
                return HttpNotFound();
            }

            return View(formulario);
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
            return View();
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

        public class SeccionConOrden
        {
            // It is important to declare them public so they get returned
            public string Nombre { get; set; }
            public int Orden { get; set; }
        }

        public class Opcion
        { 
            public string Texto { get; set; }
            public int Orden { get; set; }
        }

        public class PreguntaConEnunciadoYOpciones
        {
            public string Enunciado { get; set; }
            public List<Opcion> Opciones { get; set; }
        }
       
        [HttpGet]
        public JsonResult GetSections(string id)
        {
            SqlParameter codForm = new SqlParameter("CodForm", id);
            List<SeccionConOrden> secciones = db.Database.SqlQuery<SeccionConOrden>("EXEC SeccionesDeFormulario @CodForm", codForm).ToList();

            return Json(secciones, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetQuestions(string id)
        {
            SqlParameter sectionCode = new SqlParameter("sectionCode", id);

            // Obtiene los códigos de todas las preguntas relacionadas a la sección
            List<string> Codigos = db.Database.SqlQuery<string>("EXEC ObtenerPreguntasDeSeccion @sectionCode", sectionCode).ToList();

            List<PreguntaConEnunciadoYOpciones> preguntasConOpciones = new List<PreguntaConEnunciadoYOpciones>();
   
            foreach (string codigo in Codigos)
            {
                PreguntaConEnunciadoYOpciones pregunta = new PreguntaConEnunciadoYOpciones();

                SqlParameter questionCode = new SqlParameter("questionCode", codigo);

                // Obtiene el enunciado de una pregunta
                pregunta.Enunciado = db.Database.SqlQuery<string>("SELECT p.Enunciado FROM Pregunta p WHERE p.Codigo = @questionCode", questionCode).ToString();
                
                // Obtiene las opciones de una pregunta
                pregunta.Opciones = db.Database.SqlQuery<Opcion>("EXEC ObtenerOpcionesDePregunta @questionCode", questionCode).ToList();

                // Añade la pregunta con sus opciones a la lista
                preguntasConOpciones.Add(pregunta);
            }

            return Json(preguntasConOpciones, JsonRequestBehavior.AllowGet);
        }
    }
}

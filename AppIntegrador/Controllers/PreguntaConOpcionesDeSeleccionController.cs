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
    public class PreguntaConOpcionesDeSeleccionController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        public class IncompleteOption
        {
            public string texto { get; set; }
            public int orden { get; set; }
        }

        public List<IncompleteOption> Opciones = new List<IncompleteOption>();

        public ActionResult SaveOptions(List<IncompleteOption> values)
        {
            string codigo = values[0].texto;
            string enunciado = values[1].texto;
            string justificacion = values[2].texto;

            Pregunta pregunta = new Pregunta();
            pregunta.Codigo = codigo;
            pregunta.Enunciado = enunciado;

            db.Pregunta.Add(pregunta);
            db.SaveChanges();

            Pregunta_con_opciones pregunta2 = new Pregunta_con_opciones();
            pregunta2.Codigo = codigo;
            pregunta2.TituloCampoObservacion = justificacion;
            db.Pregunta_con_opciones.Add(pregunta2);
            db.SaveChanges();

            Pregunta_con_opciones_de_seleccion pregunta3 = new Pregunta_con_opciones_de_seleccion();
            pregunta3.Codigo = codigo;
            pregunta3.Tipo = "U";
            db.Pregunta_con_opciones_de_seleccion.Add(pregunta3);
            db.SaveChanges();

            for (int i = 3; i < values.Count; ++i)
            {
                IncompleteOption opcion = values[i];
                // Asigno el codigo a cada opcion de la pregunta
                Opciones_de_seleccion newOption = new Opciones_de_seleccion
                {
                    Codigo = codigo,
                    Texto = opcion.texto,
                    Orden = opcion.orden
                };

                db.Opciones_de_seleccion.Add(newOption);
                db.SaveChanges();
            }
            

            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Pregunta_con_opciones_de_seleccion()
        {
            return View();
        }

        [HttpGet]
        public ActionResult OpcionesDeSeleccion(int? i)
        {
            ViewBag.i = i;
            return PartialView();
        }

        // GET: PreguntaConOpcionesDeSeleccion
        public ActionResult Index()
        {
            var pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion;
            return View(pregunta_con_opciones_de_seleccion.ToList());
        }

        // GET: PreguntaConOpcionesDeSeleccion/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta_con_opciones_de_seleccion pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion.Find(id);
            if (pregunta_con_opciones_de_seleccion == null)
            {
                return HttpNotFound();
            }
            return View(pregunta_con_opciones_de_seleccion);
        }

        // GET: PreguntaConOpcionesDeSeleccion/Create
        // Metodo usado para el render partial
        public ActionResult OpcionesDeSeleccion()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: PreguntaConOpcionesDeSeleccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pregunta_con_opciones_de_seleccion pregunta, List<Opciones_de_seleccion> opcionesUnused)
        {
            // Para esta fase del proyecto solo se soportan preguntas de selección única
            pregunta.Tipo = "U";
            if (ModelState.IsValid && pregunta.Codigo.Length > 0 && pregunta.Pregunta_con_opciones.Pregunta.Enunciado.Length > 0)
            {
                ModelState.AddModelError("Codigo", "");
                try
                {
                    // Obtenga el codigo brindado para esa pregunta y asigneselo a la superclases pregunta
                    pregunta.Pregunta_con_opciones.Pregunta.Codigo = pregunta.Codigo;
                    // Agregue esa pregunta a la tabla de preguntas
                    db.Pregunta.Add(pregunta.Pregunta_con_opciones.Pregunta);
                    // Agregue la pregunta con opciones perse a la table=a
                    db.Pregunta_con_opciones_de_seleccion.Add(pregunta);
                    db.SaveChanges();

                    //List<Opciones_de_seleccion> options = new List<Opciones_de_seleccion>();
                    //string codigoPregunta = pregunta.Codigo;
                    //for (int i = 0; i < Opciones.Count; i++)
                    //{
                    //    IncompleteOption opcion = Opciones[i];
                    //    // Asigno el codigo a cada opcion de la pregunta
                    //    Opciones_de_seleccion newOption = new Opciones_de_seleccion
                    //    {
                    //        Codigo = codigoPregunta,
                    //        Texto = opcion.texto,
                    //        Orden = opcion.orden
                    //    };

                    //    options.Add(newOption);
                    //}

                    //// Guardo todas las opciones de una
                    //db.Opciones_de_seleccion.AddRange(options);
                    //db.SaveChanges();

                    return RedirectToAction("Create");
                }
                catch (Exception exception)
                {
                    if (exception is System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        ModelState.AddModelError("Codigo", "Código ya en uso.");
                        return View(pregunta);
                    }
                }
            }

            return View();
        }

        // GET: PreguntaConOpcionesDeSeleccion/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta_con_opciones_de_seleccion pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion.Find(id);
            if (pregunta_con_opciones_de_seleccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Codigo = new SelectList(db.Pregunta_con_opciones, "Codigo", "TituloCampoObservacion", pregunta_con_opciones_de_seleccion.Codigo);
            return View(pregunta_con_opciones_de_seleccion);
        }

        // POST: PreguntaConOpcionesDeSeleccion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Tipo")] Pregunta_con_opciones_de_seleccion pregunta_con_opciones_de_seleccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pregunta_con_opciones_de_seleccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Codigo = new SelectList(db.Pregunta_con_opciones, "Codigo", "TituloCampoObservacion", pregunta_con_opciones_de_seleccion.Codigo);
            return View(pregunta_con_opciones_de_seleccion);
        }

        // GET: PreguntaConOpcionesDeSeleccion/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta_con_opciones_de_seleccion pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion.Find(id);
            if (pregunta_con_opciones_de_seleccion == null)
            {
                return HttpNotFound();
            }
            return View(pregunta_con_opciones_de_seleccion);
        }

        // POST: PreguntaConOpcionesDeSeleccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Pregunta_con_opciones_de_seleccion pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion.Find(id);
            db.Pregunta_con_opciones_de_seleccion.Remove(pregunta_con_opciones_de_seleccion);
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

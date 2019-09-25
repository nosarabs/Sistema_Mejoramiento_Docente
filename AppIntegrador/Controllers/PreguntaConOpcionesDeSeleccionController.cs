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
        public ActionResult Create()
        {
            ViewBag.Codigo = new SelectList(db.Pregunta_con_opciones, "Codigo", "TituloCampoObservacion");
            return View();
        }

        // POST: PreguntaConOpcionesDeSeleccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pregunta_con_opciones_de_seleccion pregunta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Obtenga el codigo brindado para esa pregunta y asigneselo a la superclases pregunta
                    pregunta.Pregunta_con_opciones.Pregunta.Codigo = pregunta.Codigo;
                    // Agregue esa pregunta a la tabla de preguntas
                    db.Preguntas.Add(pregunta.Pregunta_con_opciones.Pregunta);
                    // Agregue la pregunta con opciones perse a la table=a
                    db.Pregunta_con_opciones_de_seleccion.Add(pregunta);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception exception)
                {
                    // return Content("<script language='javascript' type='text/javascript'>alert('El código ya está en uso');</script>");
                    return View(pregunta);
                }
            }

            ViewBag.Codigo = new SelectList(db.Pregunta_con_opciones,
                "Codigo", "TituloCampoObservacion", pregunta.Codigo);
            return View(pregunta);
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

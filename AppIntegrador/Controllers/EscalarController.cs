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
    public class EscalarController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: Escalar
        public ActionResult Index()
        {
            var escalars = db.Escalars.Include(e => e.Pregunta_con_opciones);
            return View(escalars.ToList());
        }

        // GET: Escalar/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escalar escalar = db.Escalars.Find(id);
            if (escalar == null)
            {
                return HttpNotFound();
            }
            return View(escalar);
        }

        // GET: Escalar/Create
        public ActionResult Create()
        {
            ViewBag.Codigo = new SelectList(db.Pregunta_con_opciones, "Codigo", "TituloCampoObservacion");
            return View();
        }

        // POST: Escalar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Incremento,Minimo,Maximo")] Escalar escalar)
        {
            if (ModelState.IsValid)
            {
                db.Escalars.Add(escalar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Codigo = new SelectList(db.Pregunta_con_opciones, "Codigo", "TituloCampoObservacion", escalar.Codigo);
            return View(escalar);
        }

        // GET: Escalar/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escalar escalar = db.Escalars.Find(id);
            if (escalar == null)
            {
                return HttpNotFound();
            }
            ViewBag.Codigo = new SelectList(db.Pregunta_con_opciones, "Codigo", "TituloCampoObservacion", escalar.Codigo);
            return View(escalar);
        }

        // POST: Escalar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Incremento,Minimo,Maximo")] Escalar escalar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(escalar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Codigo = new SelectList(db.Pregunta_con_opciones, "Codigo", "TituloCampoObservacion", escalar.Codigo);
            return View(escalar);
        }

        // GET: Escalar/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Escalar escalar = db.Escalars.Find(id);
            if (escalar == null)
            {
                return HttpNotFound();
            }
            return View(escalar);
        }

        // POST: Escalar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Escalar escalar = db.Escalars.Find(id);
            db.Escalars.Remove(escalar);
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

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
    public class Visualizacion_Respuestas_EscalarController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: Visualizacion_Respuestas_Escalar
        public ActionResult Index()
        {
            return View(db.Visualizacion_Respuestas_Escalar.ToList());
        }

        // GET: Visualizacion_Respuestas_Escalar/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visualizacion_Respuestas_Escalar visualizacion_Respuestas_Escalar = db.Visualizacion_Respuestas_Escalar.Find(id);
            if (visualizacion_Respuestas_Escalar == null)
            {
                return HttpNotFound();
            }
            return View(visualizacion_Respuestas_Escalar);
        }

        // GET: Visualizacion_Respuestas_Escalar/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Visualizacion_Respuestas_Escalar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Incremento,Minimo,Maximo,FCodigo,Username,CSigla,GNumero,GAnno,GSemestre,Fecha,PCodigo,OpcionSeleccionada")] Visualizacion_Respuestas_Escalar visualizacion_Respuestas_Escalar)
        {
            if (ModelState.IsValid)
            {
                db.Visualizacion_Respuestas_Escalar.Add(visualizacion_Respuestas_Escalar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(visualizacion_Respuestas_Escalar);
        }

        // GET: Visualizacion_Respuestas_Escalar/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visualizacion_Respuestas_Escalar visualizacion_Respuestas_Escalar = db.Visualizacion_Respuestas_Escalar.Find(id);
            if (visualizacion_Respuestas_Escalar == null)
            {
                return HttpNotFound();
            }
            return View(visualizacion_Respuestas_Escalar);
        }

        // POST: Visualizacion_Respuestas_Escalar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Incremento,Minimo,Maximo,FCodigo,Username,CSigla,GNumero,GAnno,GSemestre,Fecha,PCodigo,OpcionSeleccionada")] Visualizacion_Respuestas_Escalar visualizacion_Respuestas_Escalar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visualizacion_Respuestas_Escalar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(visualizacion_Respuestas_Escalar);
        }

        // GET: Visualizacion_Respuestas_Escalar/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visualizacion_Respuestas_Escalar visualizacion_Respuestas_Escalar = db.Visualizacion_Respuestas_Escalar.Find(id);
            if (visualizacion_Respuestas_Escalar == null)
            {
                return HttpNotFound();
            }
            return View(visualizacion_Respuestas_Escalar);
        }

        // POST: Visualizacion_Respuestas_Escalar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Visualizacion_Respuestas_Escalar visualizacion_Respuestas_Escalar = db.Visualizacion_Respuestas_Escalar.Find(id);
            db.Visualizacion_Respuestas_Escalar.Remove(visualizacion_Respuestas_Escalar);
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

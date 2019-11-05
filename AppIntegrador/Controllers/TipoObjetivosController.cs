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
    public class TipoObjetivosController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        public TipoObjetivosController()
        {
        }
        //Para pruebas
        public TipoObjetivosController(DataIntegradorEntities db)
        {
            this.db = db;
        }


        // GET: TipoObjetivos
        public ActionResult Index()
        {
            return View("Index", db.TipoObjetivo.ToList());
        }

        // GET: TipoObjetivos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoObjetivo tipoObjetivo = db.TipoObjetivo.Find(id);
            if (tipoObjetivo == null)
            {
                return HttpNotFound();
            }
            return View(tipoObjetivo);
        }

        // GET: TipoObjetivos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoObjetivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre")] TipoObjetivo tipoObjetivo)
        {
            if (ModelState.IsValid)
            {
                db.TipoObjetivo.Add(tipoObjetivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoObjetivo);
        }
        //Para pruebas
        public ActionResult Create([Bind(Include = "nombre")] TipoObjetivo tipoObjetivo, DataIntegradorEntities db)
        {
            if (ModelState.IsValid)
            {
                db.TipoObjetivo.Add(tipoObjetivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoObjetivo);
        }



        // GET: TipoObjetivos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoObjetivo tipoObjetivo = db.TipoObjetivo.Find(id);
            if (tipoObjetivo == null)
            {
                return HttpNotFound();
            }
            return View(tipoObjetivo);
        }

        // POST: TipoObjetivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nombre")] TipoObjetivo tipoObjetivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoObjetivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoObjetivo);
        }

        // GET: TipoObjetivos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoObjetivo tipoObjetivo = db.TipoObjetivo.Find(id);
            if (tipoObjetivo == null)
            {
                return HttpNotFound();
            }
            return View(tipoObjetivo);
        }

        // POST: TipoObjetivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TipoObjetivo tipoObjetivo = db.TipoObjetivo.Find(id);
            db.TipoObjetivo.Remove(tipoObjetivo);
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

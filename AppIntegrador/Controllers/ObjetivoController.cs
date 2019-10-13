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
    public class ObjetivoController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: Objetivo
        public ActionResult Index()
        {
            var objetivo = db.Objetivo.Include(o => o.PlantillaObjetivo).Include(o => o.TipoObjetivo);
            return View(objetivo.ToList());
        }

        // GET: Objetivo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivo.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // GET: Objetivo/Create
        public ActionResult Create()
        {
            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre");
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre");
            return View();
        }

        // POST: Objetivo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codPlan,nombre,descripcion,fechaInicio,fechaFin,nombTipoObj,codPlantilla")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Objetivo.Add(objetivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre", objetivo.codPlantilla);
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre", objetivo.nombTipoObj);
            return View(objetivo);
        }

        // GET: Objetivo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivo.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre", objetivo.codPlantilla);
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre", objetivo.nombTipoObj);
            return View(objetivo);
        }

        // POST: Objetivo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codPlan,nombre,descripcion,fechaInicio,fechaFin,nombTipoObj,codPlantilla")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objetivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre", objetivo.codPlantilla);
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre", objetivo.nombTipoObj);
            return View(objetivo);
        }

        // GET: Objetivo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivo.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // POST: Objetivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Objetivo objetivo = db.Objetivo.Find(id);
            db.Objetivo.Remove(objetivo);
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

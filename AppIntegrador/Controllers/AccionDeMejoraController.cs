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
    public class AccionDeMejoraController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: AccionDeMejora
        public ActionResult Index()
        {
            var accionDeMejora = db.AccionDeMejora.Include(a => a.Objetivo).Include(a => a.PlantillaAccionDeMejora);
            return View(accionDeMejora.ToList());
        }

        // GET: AccionDeMejora/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccionDeMejora accionDeMejora = db.AccionDeMejora.Find(id);
            if (accionDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(accionDeMejora);
        }

        // GET: AccionDeMejora/Create
        public ActionResult Create()
        {
            ViewBag.codPlan = new SelectList(db.Objetivo, "codPlan", "descripcion");
            ViewBag.codPlantilla = new SelectList(db.PlantillaAccionDeMejora, "codigo", "descripcion");
            return View();
        }

        // POST: AccionDeMejora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codPlan,nombreObj,descripcion,fechaInicio,fechaFin,codPlantilla")] AccionDeMejora accionDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.AccionDeMejora.Add(accionDeMejora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codPlan = new SelectList(db.Objetivo, "codPlan", "descripcion", accionDeMejora.codPlan);
            ViewBag.codPlantilla = new SelectList(db.PlantillaAccionDeMejora, "codigo", "descripcion", accionDeMejora.codPlantilla);
            return View(accionDeMejora);
        }

        // GET: AccionDeMejora/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccionDeMejora accionDeMejora = db.AccionDeMejora.Find(id);
            if (accionDeMejora == null)
            {
                return HttpNotFound();
            }
            ViewBag.codPlan = new SelectList(db.Objetivo, "codPlan", "descripcion", accionDeMejora.codPlan);
            ViewBag.codPlantilla = new SelectList(db.PlantillaAccionDeMejora, "codigo", "descripcion", accionDeMejora.codPlantilla);
            return View(accionDeMejora);
        }

        // POST: AccionDeMejora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codPlan,nombreObj,descripcion,fechaInicio,fechaFin,codPlantilla")] AccionDeMejora accionDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accionDeMejora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codPlan = new SelectList(db.Objetivo, "codPlan", "descripcion", accionDeMejora.codPlan);
            ViewBag.codPlantilla = new SelectList(db.PlantillaAccionDeMejora, "codigo", "descripcion", accionDeMejora.codPlantilla);
            return View(accionDeMejora);
        }

        // GET: AccionDeMejora/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccionDeMejora accionDeMejora = db.AccionDeMejora.Find(id);
            if (accionDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(accionDeMejora);
        }

        // POST: AccionDeMejora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccionDeMejora accionDeMejora = db.AccionDeMejora.Find(id);
            db.AccionDeMejora.Remove(accionDeMejora);
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

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
    public class AccionablesController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: Accionables
        public ActionResult Index()
        {
            var accionable = db.Accionable.Include(a => a.AccionDeMejora);
            return View(accionable.ToList());
        }

        // GET: Accionables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accionable accionable = db.Accionable.Find(id);
            if (accionable == null)
            {
                return HttpNotFound();
            }
            return View(accionable);
        }

        // GET: Accionables/Create
        public ActionResult Create()
        {
            ViewBag.codPlan = new SelectList(db.AccionDeMejora, "codPlan", "nombreObj");
            return View();
        }

        // POST: Accionables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codPlan,nombreObj,descripAcMej,descripcion,fechaInicio,fechaFin,progreso")] Accionable accionable)
        {
            if (ModelState.IsValid)
            {
                db.Accionable.Add(accionable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codPlan = new SelectList(db.AccionDeMejora, "codPlan", "nombreObj", accionable.codPlan);
            return View(accionable);
        }

        // GET: Accionables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accionable accionable = db.Accionable.Find(id);
            if (accionable == null)
            {
                return HttpNotFound();
            }
            ViewBag.codPlan = new SelectList(db.AccionDeMejora, "codPlan", "nombreObj", accionable.codPlan);
            return View(accionable);
        }

        // POST: Accionables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codPlan,nombreObj,descripAcMej,descripcion,fechaInicio,fechaFin,progreso")] Accionable accionable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accionable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codPlan = new SelectList(db.AccionDeMejora, "codPlan", "nombreObj", accionable.codPlan);
            return View(accionable);
        }

        // GET: Accionables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accionable accionable = db.Accionable.Find(id);
            if (accionable == null)
            {
                return HttpNotFound();
            }
            return View(accionable);
        }

        // POST: Accionables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accionable accionable = db.Accionable.Find(id);
            db.Accionable.Remove(accionable);
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

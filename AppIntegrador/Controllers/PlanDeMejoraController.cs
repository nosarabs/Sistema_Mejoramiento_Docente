using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using AppIntegrador.Models;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Controllers
{
    public class PlanDeMejoraController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: PlanDeMejora
        public ActionResult Index()
        {
            var planDeMejora = db.PlanDeMejoras.Include(p => p.Profesor).Include(p => p.Profesor1);
            return View(planDeMejora.ToList());
        }

        // GET: PlanDeMejora/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeMejora planDeMejora = db.PlanDeMejoras.Find(id);
            if (planDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(planDeMejora);
        }

        // GET: PlanDeMejora/Create
        public ActionResult Create()
        {
            ViewBag.CedProf = new SelectList(db.Profesors, "Cedula", "Cedula");
            ViewBag.CedProfAsig = new SelectList(db.Profesors, "Cedula", "Cedula");
            return View();
        }

        // POST: PlanDeMejora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CedProf,Codigo,Nombre,FechaInicio,FechaFin,CodigoF,CedProfAsig")] PlanDeMejora planDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.PlanDeMejoras.Add(planDeMejora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CedProf = new SelectList(db.Profesors, "Cedula", "Cedula", planDeMejora.CedProf);
            ViewBag.CedProfAsig = new SelectList(db.Profesors, "Cedula", "Cedula", planDeMejora.CedProfAsig);
            return View(planDeMejora);
        }

        // GET: PlanDeMejora/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeMejora planDeMejora = db.PlanDeMejoras.Find(id);
            if (planDeMejora == null)
            {
                return HttpNotFound();
            }
            ViewBag.CedProf = new SelectList(db.Profesors, "Cedula", "Cedula", planDeMejora.CedProf);
            ViewBag.CedProfAsig = new SelectList(db.Profesors, "Cedula", "Cedula", planDeMejora.CedProfAsig);
            return View(planDeMejora);
        }

        // POST: PlanDeMejora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CedProf,Codigo,Nombre,FechaInicio,FechaFin,CodigoF,CedProfAsig")] PlanDeMejora planDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planDeMejora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CedProf = new SelectList(db.Profesors, "Cedula", "Cedula", planDeMejora.CedProf);
            ViewBag.CedProfAsig = new SelectList(db.Profesors, "Cedula", "Cedula", planDeMejora.CedProfAsig);
            return View(planDeMejora);
        }

        // GET: PlanDeMejora/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeMejora planDeMejora = db.PlanDeMejoras.Find(id);
            if (planDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(planDeMejora);
        }

        // POST: PlanDeMejora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PlanDeMejora planDeMejora = db.PlanDeMejoras.Find(id);
            db.PlanDeMejoras.Remove(planDeMejora);
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

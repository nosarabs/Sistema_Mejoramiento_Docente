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
    public class PlanMejorasController : Controller
    {
        private MosqueterosEntities db = new MosqueterosEntities();

        // GET: PlanMejoras
        public ActionResult Index()
        {
            return View(db.PlanMejora.ToList());
        }

        // GET: PlanMejoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMejora planMejora = db.PlanMejora.Find(id);
            if (planMejora == null)
            {
                return HttpNotFound();
            }
            return View(planMejora);
        }

        // GET: PlanMejoras/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlanMejoras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre,FechaInicio,FechaFin")] PlanMejora planMejora)
        {
            if (ModelState.IsValid)
            {
                db.PlanMejora.Add(planMejora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(planMejora);
        }

        // GET: PlanMejoras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMejora planMejora = db.PlanMejora.Find(id);
            if (planMejora == null)
            {
                return HttpNotFound();
            }
            return View(planMejora);
        }

        // POST: PlanMejoras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre,FechaInicio,FechaFin")] PlanMejora planMejora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planMejora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planMejora);
        }

        // GET: PlanMejoras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMejora planMejora = db.PlanMejora.Find(id);
            if (planMejora == null)
            {
                return HttpNotFound();
            }
            return View(planMejora);
        }

        // POST: PlanMejoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanMejora planMejora = db.PlanMejora.Find(id);
            db.PlanMejora.Remove(planMejora);
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

        [ChildActionOnly]
        public ActionResult GetObjetivosIndex(string path)
        {
            return new FilePathResult(path, "text/cshtml");
        }
    }
}

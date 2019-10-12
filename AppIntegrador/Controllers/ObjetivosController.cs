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
    public class ObjetivosController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: Objetivos
        public ActionResult Index()
        {
            //var objetivo = db.Objetivo.Include(o => o.PlanDeMejora).Include(o => o.TipoObjetivo1);
            //return View(objetivo.ToList());

            return View();
        }

        // GET: Objetivos/Details/5
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

        // GET: Objetivos/Create
        public ActionResult Create()
        {
            ViewBag.CedulaProf = new SelectList(db.PlanDeMejora, "CedProf", "Nombre");
            ViewBag.TipoObjetivo = new SelectList(db.TipoObjetivo, "Nombre", "Descripcion");
            return View();
        }

        // POST: Objetivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre,Descripcion,TipoObjetivo,CedulaProf,CodigoPlan")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Objetivo.Add(objetivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.CedulaProf = new SelectList(db.PlanDeMejora, "CedProf", "Nombre", objetivo.CedulaProf);
            ViewBag.TipoObjetivo = new SelectList(db.TipoObjetivo, "Nombre", "Descripcion", objetivo.TipoObjetivo);
            return View(objetivo);
        }

        // GET: Objetivos/Edit/5
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
            //ViewBag.CedulaProf = new SelectList(db.PlanDeMejora, "CedProf", "Nombre", objetivo.CedulaProf);
            ViewBag.TipoObjetivo = new SelectList(db.TipoObjetivo, "Nombre", "Descripcion", objetivo.TipoObjetivo);
            return View(objetivo);
        }

        // POST: Objetivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre,Descripcion,TipoObjetivo,CedulaProf,CodigoPlan")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objetivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CedulaProf = new SelectList(db.PlanDeMejora, "CedProf", "Nombre", objetivo.CedulaProf);
            ViewBag.TipoObjetivo = new SelectList(db.TipoObjetivo, "Nombre", "Descripcion", objetivo.TipoObjetivo);
            return View(objetivo);
        }

        // GET: Objetivos/Delete/5
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

        // POST: Objetivos/Delete/5
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

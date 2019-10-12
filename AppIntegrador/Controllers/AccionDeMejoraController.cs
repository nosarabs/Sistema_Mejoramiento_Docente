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
            var accionDeMejora = db.AccionDeMejora.Include(a => a.Objetivo);
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
            ViewBag.CodigoObj = new SelectList(db.Objetivo, "Codigo", "Nombre");
            return View();
        }

        // POST: AccionDeMejora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Descripcion,FechaInicio,FechaFin,CodigoObj")] AccionDeMejora accionDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.AccionDeMejora.Add(accionDeMejora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //@TODO: 
            //ViewBag.CodigoObj = new SelectList(db.Objetivo, "Codigo", "Nombre", accionDeMejora.CodigoObj);
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
            //ViewBag.CodigoObj = new SelectList(db.Objetivo, "Codigo", "Nombre", accionDeMejora.CodigoObj);
            return View(accionDeMejora);
        }

        // POST: AccionDeMejora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Descripcion,FechaInicio,FechaFin,CodigoObj")] AccionDeMejora accionDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accionDeMejora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.CodigoObj = new SelectList(db.Objetivo, "Codigo", "Nombre", accionDeMejora.CodigoObj);
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

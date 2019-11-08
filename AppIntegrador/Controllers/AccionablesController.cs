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
        public ActionResult Create(int codPlan, string nombObj, string descripAcMej, string fechaInicioAccionDeMejora, string fechaFinAccionDeMejora)
        {

            ViewBag.IdPlan = codPlan;
            ViewBag.nomObj = nombObj;
            ViewBag.descripAcMej = descripAcMej;
            ViewBag.progreso = 0;
            ViewBag.fechaInicioAccionDeMejora = fechaInicioAccionDeMejora;
            ViewBag.fechaFinAccionDeMejora = fechaFinAccionDeMejora;

            Session["codPlan"] = codPlan;
            Session["nombreObj"] = nombObj;
            Session["descripAcMej"] = descripAcMej;
            Session["progreso"] = 0;
            Session["fechaInicioAccionDeMejora"] = fechaInicioAccionDeMejora;
            Session["fechaFinAccionDeMejora"] = fechaFinAccionDeMejora;

            Models.Metadata.AccionableMetadata accionable = new Models.Metadata.AccionableMetadata();
            return PartialView("_Create", accionable);
        }

        // POST: Accionables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public EmptyResult Create([Bind(Include = "codPlan,nombreObj,descripAcMej,descripcion,fechaInicio,fechaFin,progreso")] Accionable accionable)
        {
            bool error = false;

            if (accionable.fechaInicio != null && accionable.fechaFin != null)
            {
                if ((DateTime.Compare(accionable.fechaInicio.Value, accionable.fechaFin.Value) > 0))
                {
                    error = true;
                }
            }
            if (!error && ModelState.IsValid)
            {
                db.Accionable.Add(accionable);
                db.SaveChanges();
                return new EmptyResult();
            }

            ViewBag.codPlan = new SelectList(db.AccionDeMejora, "codPlan", "nombreObj", accionable.codPlan);
            return new EmptyResult();

        }

        public ActionResult TablaAccionables(int codPlan, string nombObj, string descripAcMej, bool edit = true)
        {
            ViewBag.IdPlan = codPlan;
            ViewBag.nomObj = nombObj;
            ViewBag.descripAcMej = descripAcMej;

            IEnumerable<AppIntegrador.Models.Accionable> accionables = db.Accionable.Where(o => o.codPlan == codPlan && o.nombreObj == nombObj && o.descripAcMej == descripAcMej);
            if( edit == false)
            {
                return PartialView("_listarAccionables", accionables);
            }
            return PartialView("_Tabla", accionables);
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

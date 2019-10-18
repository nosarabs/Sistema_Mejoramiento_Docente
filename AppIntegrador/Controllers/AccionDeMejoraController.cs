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
            var objetivo = db.AccionDeMejora.Include(o => o.PlantillaAccionDeMejora).Include(o => o.Objetivo);
            return View(objetivo.ToList());
        }

        // GET: AccionDeMejora/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccionDeMejora/Create
        public ActionResult Create()
        {
            ViewBag.codPlan = new SelectList(db.PlanDeMejora, "codigo", "nombre");
            ViewBag.nombreObj = new SelectList(db.Objetivo, "nombre", "nombre");
            return View("_createAccionDeMejora");
        }

        // POST: AccionDeMejora/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codPlan,nombreObj,descripcion,fechaInicio,fechaFin,codPlantilla")] AccionDeMejora accionDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.AccionDeMejora.Add(accionDeMejora);
                db.SaveChanges();
                return RedirectToAction("Index", "PlanDeMejora");
            }

            ViewBag.codPlan = new SelectList(db.PlanDeMejora, "codigo", "nombre");
            ViewBag.nombreObj = new SelectList(db.Objetivo, "nombre", "nombre");
            return View(accionDeMejora);

        }

        // GET: AccionDeMejora/Edit/5
        // Corresponde a MOS 1.3 (2)
        public ActionResult Edit(int plan, string obj, string descripcion)
        {
            if (plan == null || obj == null || descripcion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccionDeMejora accionDeMejora = db.AccionDeMejora.Find(plan, obj, descripcion);
            if (accionDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(accionDeMejora);
        }

        // POST: AccionDeMejora/Edit/5
        // Corresponde a MOS 1.3 (2)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codPlan,nombreObj,descripcion,fechaInicio,fechaFin,,codPlantilla")] PlanDeMejora planDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planDeMejora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planDeMejora);
        }

        // GET: AccionDeMejora/Delete/5
        // Corresponde a MOS 1.3 (2)
        public ActionResult Delete(int? plan, string nombObj, string descripcion)
        {
            if (plan == null || nombObj == null || descripcion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccionDeMejora accion = db.AccionDeMejora.Find(plan, nombObj, descripcion);
            if (accion == null)
            {
                return HttpNotFound();
            }
            return View(accion);
        }

        // POST: AccionDeMejora/Delete/5
        // Corresponde a MOS 1.3 (2)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? plan, string nombObj, string descripcion)
        {
            AccionDeMejora accion = db.AccionDeMejora.Find(plan, nombObj, descripcion);
            db.AccionDeMejora.Remove(accion);
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

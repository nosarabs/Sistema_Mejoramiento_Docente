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
    public class PlanDeMejoraController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // -------------------------- READ - Planes de mejora ---------------
        public ActionResult Index()
        {
            var totalPlanesDeMejora = this.db.PlanDeMejora.ToList();
            return View(totalPlanesDeMejora);
        }
        // GET: PlanDeMejora/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            if (planDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(planDeMejora);
        }
        // -------------------------- END OF READ --------------------------






        // -------------------------- CREATE - Planes de mejora ---------------
        public ActionResult Create()
        {
            return View("_crearPlanDeMejora");
        }
        // Para emplear este create se requiere todo el cuerpo de un plan de mejora
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre,fechaInicio,fechaFin")] PlanDeMejora planDeMejora)
        {
            if (ModelState.IsValid)
            {
                var plans = this.db.PlanDeMejora.ToList();
                var codigoTemporal = plans.Count == 0 ? 1000 : plans.Last().codigo;
                planDeMejora.codigo = codigoTemporal + 1;
                db.PlanDeMejora.Add(planDeMejora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("_crearPlanDeMejora");
        }
        // -------------------------- END OF CREATE -----------------------------






        // -------------------------- UPDATE - Planes de Mejora -----------------
        // GET: PlanDeMejora/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            if (planDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(planDeMejora);
        }

        // POST: PlanDeMejora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,nombre,fechaInicio,fechaFin")] PlanDeMejora planDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planDeMejora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planDeMejora);
        }
        // Method that edits one "PlanDeMejora"
        public ActionResult ModificarPlan(int codigo, string nombre, DateTime fechaInicio, DateTime fechaFin)
        {
            if (codigo != null && nombre != null && fechaInicio != null && fechaFin != null)
            {
                var planTemp = new PlanDeMejora();
                planTemp.codigo = codigo;
                planTemp.nombre = nombre;
                planTemp.fechaInicio = fechaInicio;
                planTemp.fechaFin = fechaFin;
                this.Edit(planTemp);
            }
            return RedirectToAction("Index");
        }
        // -------------------------- END OF UPDATE -----------------------------







        // -------------------------- DELETE - Planes de Mejora --------------------
        // GET: PlanDeMejora/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            if (planDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(planDeMejora);
        }

        // POST: PlanDeMejora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            db.PlanDeMejora.Remove(planDeMejora);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Method that deletes one "PlanDeMejora"
        public ActionResult BorrarPlan(int codigoPlan)
        {
            this.DeleteConfirmed(codigoPlan);
            return RedirectToAction("Index");
        }
        // -------------------------- END OF DELETE --------------------------------






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

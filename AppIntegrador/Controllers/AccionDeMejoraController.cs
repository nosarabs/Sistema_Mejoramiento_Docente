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
                return RedirectToAction("Index");
            }

            ViewBag.codPlan = new SelectList(db.PlanDeMejora, "codigo", "nombre");
            ViewBag.nombreObj = new SelectList(db.Objetivo, "nombre", "nombre");
            return View(accionDeMejora);

        }

        // GET: AccionDeMejora/Edit/5
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccionDeMejora/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

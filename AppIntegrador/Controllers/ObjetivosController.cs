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
            var objetivo = db.Objetivo.Include(o => o.PlantillaObjetivo).Include(o => o.TipoObjetivo);
            return View(objetivo.ToList());
        }
        public ActionResult accionesObjetivo(string id, string nomb)
        {
            var idPlan = -1;
            Int32.TryParse(id, out idPlan);
            IEnumerable<AppIntegrador.Models.AccionDeMejora> acciones = db.AccionDeMejora.Where(o => o.codPlan == idPlan && o.nombreObj == nomb);
            return PartialView("~/Views/AccionDeMejora/Index.cshtml", acciones);
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
        public ActionResult Create(int id)
        {
            ViewBag.id = id;
            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre");
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre");
            return View("_crearObjetivo");
        }

        // POST: Objetivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codPlan,nombre,descripcion,fechaInicio,fechaFin,nombTipoObj,codPlantilla")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Objetivo.Add(objetivo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre", objetivo.codPlantilla);
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre", objetivo.nombTipoObj);
            return View(objetivo);
        }

        // GET: Objetivos/Edit/5
        public ActionResult Edit(int? plan, string title)
        {
            if (plan == null || title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivo.Find(plan, title);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre", objetivo.codPlantilla);
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre", objetivo.nombTipoObj);
            return View(objetivo);
        }

        // POST: Objetivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codPlan,nombre,descripcion,fechaInicio,fechaFin,nombTipoObj,codPlantilla")] Objetivo objetivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objetivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre", objetivo.codPlantilla);
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre", objetivo.nombTipoObj);
            return View(objetivo);
        }

        // GET: Objetivos/Delete/5
        public ActionResult Delete(int? plan, string title)
        {
            if (plan == null || title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivo.Find(plan, title);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // POST: Objetivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int plan, string title)
        {
            Objetivo objetivo = db.Objetivo.Find(plan, title);
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

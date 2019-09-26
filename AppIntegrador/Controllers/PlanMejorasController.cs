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
        private proyEntities db = new proyEntities();

        // GET: PlanMejoras
        public ActionResult Index()
        {
            var planMejoras = db.PlanMejoras.Include(p => p.Formulario);
            return View(planMejoras.ToList());
        }

        // GET: PlanMejoras/Objetivos/5
        public ActionResult Objetivos(int? user_id, int? codigo)
        {
            if (user_id == null && codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMejora planMejora = db.PlanMejoras.Find(user_id, codigo);
            if (planMejora == null)
            {
                return HttpNotFound();
            }
            var objetivos = planMejora.Objetivoes;
            return View(objetivos);
        }

        // GET: PlanMejoras/Details/5
        public ActionResult Details(int? user_id, int? codigo)
        {
            if (user_id == null || codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMejora planMejora = db.PlanMejoras.Find(user_id, codigo);
            if (planMejora == null)
            {
                return HttpNotFound();
            }
            return View(planMejora);
        }

        // GET: PlanMejoras/Create
        public ActionResult Create()
        {
            ViewBag.CodigoF = new SelectList(db.Formularios, "Codigo", "Codigo");
            return View();
        }

        // POST: PlanMejoras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Codigo,Nombre,FechaInicio,FechaFin,CodigoF,UserIDA")] PlanMejora planMejora)
        {
            if (ModelState.IsValid)
            {
                db.PlanMejoras.Add(planMejora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoF = new SelectList(db.Formularios, "Codigo", "Codigo", planMejora.CodigoF);
            return View(planMejora);
        }

        // GET: PlanMejoras/Edit/5
        public ActionResult Edit(int? user_id, int? codigo)
        {
            if (user_id == null || codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMejora planMejora = db.PlanMejoras.Find(user_id, codigo);
            if (planMejora == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoF = new SelectList(db.Formularios, "Codigo", "Codigo", planMejora.CodigoF);
            return View(planMejora);
        }

        // POST: PlanMejoras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Codigo,Nombre,FechaInicio,FechaFin,CodigoF,UserIDA")] PlanMejora planMejora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planMejora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoF = new SelectList(db.Formularios, "Codigo", "Codigo", planMejora.CodigoF);
            return View(planMejora);
        }

        // GET: PlanMejoras/Delete/5
        public ActionResult Delete(int? user_id, int? codigo)
        {
            if (user_id == null && codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanMejora planMejora = db.PlanMejoras.Find(user_id, codigo);
            if (planMejora == null)
            {
                return HttpNotFound();
            }
            return View(planMejora);
        }

        // POST: PlanMejoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int user_id, int codigo)
        {
            PlanMejora planMejora = db.PlanMejoras.Find(user_id, codigo);
            db.PlanMejoras.Remove(planMejora);
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

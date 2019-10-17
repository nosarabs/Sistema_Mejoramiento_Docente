﻿using System;
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

        // GET: PlanDeMejora
        public ActionResult Index()
        {
            return View(db.PlanDeMejora.ToList());
        }

        /*
            Modificado por: Johan Córdoba
            Historia a la que pertenece: MOS-1.2 "agregar, modificar, borrar y consultar los objetivos de un plan de mejora"
            Para no tener que crear la vista parcial dento de la carpeta de planes de mejora cambié el controlador.
            Ahora este redirige a la vista de objetivos y la que está en planes de mejora "_objetivosPlan" ya no es necesaria
        */
        public ActionResult objetivosPlan(int id) {
            IEnumerable<AppIntegrador.Models.Objetivo> objetivosDePlan = db.Objetivo.Where(o => o.codPlan == id);
            return PartialView("~/Views/Objetivos/Index.cshtml", objetivosDePlan);
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

        // GET: PlanDeMejora/Create
        public ActionResult Create()
        {
            return View("_crear");
        }

        // POST: PlanDeMejora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo,nombre,fechaInicio,fechaFin")] PlanDeMejora planDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.PlanDeMejora.Add(planDeMejora);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(planDeMejora);
        }

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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //////////////////////////////////////////////BI///////////////////////////////////////////////////

        // Method that creates the plan and sets the next "codigo" automatically
        public ActionResult CrearPlanDeMejora(string nombre, DateTime fechaInicio, DateTime fechaFin)
        {
            if (nombre != null && fechaInicio != null && fechaFin != null)
            {
                var planTemp = new PlanDeMejora();
                var plans = this.db.PlanDeMejora.ToList();
                var codigoTemporal = plans.Count == 0 ? -1 : plans.Last().codigo;
                planTemp.codigo = codigoTemporal + 1;
                planTemp.nombre = nombre;
                planTemp.fechaInicio = fechaInicio;
                planTemp.fechaFin = fechaFin;
                this.Create(planTemp);
            }
            return RedirectToAction("Index");
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

        // Method that deletes one "PlanDeMejora"
        public ActionResult BorrarPlan(int codigoPlan)
        {
            this.DeleteConfirmed(codigoPlan);
            return RedirectToAction("Index");
        }
    }
}

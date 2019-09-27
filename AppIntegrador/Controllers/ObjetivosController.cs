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
        private proyEntities db = new proyEntities();

        // GET: Objetivos
        public ActionResult Index()
        {
            var objetivoes = db.Objetivoes.Include(o => o.Tipo_Objetivo);
            return View(objetivoes.ToList());
        }

        // GET: Objetivos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivoes.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // GET: Objetivos/Create
        public ActionResult Create()
        {
            ViewBag.returnUrl = Request.UrlReferrer;
            ViewBag.Tipo_O = new SelectList(db.Tipo_Objetivo, "Nombre", "Nombre");
            return View();
        }

        public ActionResult CrearAsociado(int? user_id, int? codigo)
        {
            if (user_id == null && codigo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.user_id = user_id;
            ViewBag.codigo = codigo;
            ViewBag.Tipo_O = new SelectList(db.Tipo_Objetivo, "Nombre", "Nombre");
            return View();
        }

        // POST: Objetivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre,Descripcion,Tipo_O")] Objetivo objetivo, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                db.Objetivoes.Add(objetivo);
                db.SaveChanges();
<<<<<<< HEAD
                return RedirectToAction("Index");
=======
                return Redirect(returnUrl);
>>>>>>> lucho
            }

            ViewBag.Tipo_O = new SelectList(db.Tipo_Objetivo, "Nombre", "Nombre", objetivo.Tipo_O);
            return View(objetivo);
        }

        // GET: Objetivos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivoes.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.returnUrl = Request.UrlReferrer;
            ViewBag.Tipo_O = new SelectList(db.Tipo_Objetivo, "Nombre", "Nombre", objetivo.Tipo_O);
            return View(objetivo);
        }

        // POST: Objetivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre,Descripcion,Tipo_O")] Objetivo objetivo, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(objetivo).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect(returnUrl);
            }
            ViewBag.Tipo_O = new SelectList(db.Tipo_Objetivo, "Nombre", "Nombre", objetivo.Tipo_O);
            return View(objetivo);
        }

        // GET: Objetivos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivoes.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.returnUrl = Request.UrlReferrer;
            return View(objetivo);
        }

        // POST: Objetivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string returnUrl)
        {
            Objetivo objetivo = db.Objetivoes.Find(id);
            db.Objetivoes.Remove(objetivo);
            db.SaveChanges();
            return Redirect(returnUrl);
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

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
    public class SeccionController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: Seccion
        public ActionResult Index(string inp1, string inp2)
        {
            if (inp2 == null && inp1 == null)
            {
                return View(db.Seccion.ToList());
            }
            //if a user choose the radio button option as Subject  
            if (inp2 == null)
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.Seccion.Where(x => x.Codigo.Contains(inp1)).ToList());
            }
            else if (inp1 == null )
            {
                return View(db.Seccion.Where(x => x.Nombre.Contains(inp2)).ToList());
            }
            else
            {
                return View(db.Seccion.ToList());
            }
        }

        // GET: Seccion/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        // GET: Seccion/Create
        public ActionResult Create()
        {

            var crearSeccion = new CrearSeccionModel();
            //crearSeccion.Seccion = db.Seccion();
            crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion.ToList();            
            return View(crearSeccion);
        }

        // POST: Seccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre")] Seccion seccion)
        {
            if (ModelState.IsValid)
            {
                db.Seccion.Add(seccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seccion);
        }

        // GET: Seccion/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        // POST: Seccion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre")] Seccion seccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(seccion);
        }

        // GET: Seccion/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seccion seccion = db.Seccion.Find(id);
            if (seccion == null)
            {
                return HttpNotFound();
            }
            return View(seccion);
        }

        // POST: Seccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Seccion seccion = db.Seccion.Find(id);
            db.Seccion.Remove(seccion);
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

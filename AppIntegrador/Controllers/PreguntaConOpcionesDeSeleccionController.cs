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
    public class PreguntaConOpcionesDeSeleccionController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        [HttpGet]
        public ActionResult Pregunta_con_opciones_de_seleccion()
        {
            return View();
        }

        [HttpGet]
        public ActionResult OpcionesDeSeleccion(int? i)
        {
            ViewBag.i = i;
            return PartialView();
        }


        // GET: PreguntaConOpcionesDeSeleccion
        public ActionResult Index()
        {
            var pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion;
            return View(pregunta_con_opciones_de_seleccion.ToList());
        }

        // GET: PreguntaConOpcionesDeSeleccion/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta_con_opciones_de_seleccion pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion.Find(id);
            if (pregunta_con_opciones_de_seleccion == null)
            {
                return HttpNotFound();
            }
            return View(pregunta_con_opciones_de_seleccion);
        }

        // GET: PreguntaConOpcionesDeSeleccion/Create
        // Metodo usado para el render partial
        public ActionResult OpcionesDeSeleccion()
        {
            return View();
        }

        public ActionResult Create()
       {
            return View();
        }

        // POST: PreguntaConOpcionesDeSeleccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pregunta_con_opciones_de_seleccion pregunta, List<Opciones_de_seleccion> opciones)
        {
            // Para esta fase del proyecto solo se soportan preguntas de selección única
            pregunta.Tipo = "U";
            if (ModelState.IsValid && pregunta.Codigo.Length > 0 && pregunta.Pregunta_con_opciones.Pregunta.Enunciado.Length > 0)
            {
                ModelState.AddModelError("Codigo", "");
                try
                {
                    // Obtenga el codigo brindado para esa pregunta y asigneselo a la superclases pregunta
                    pregunta.Pregunta_con_opciones.Pregunta.Codigo = pregunta.Codigo;
                    // Agregue esa pregunta a la tabla de preguntas
                    db.Preguntas.Add(pregunta.Pregunta_con_opciones.Pregunta);
                    // Agregue la pregunta con opciones perse a la table=a
                    db.Pregunta_con_opciones_de_seleccion.Add(pregunta);
                    db.SaveChanges();

                    string codigoPregunta = pregunta.Codigo;
                    foreach (Opciones_de_seleccion opcion in opciones)
                    {
                        // Asigno el codigo a cada opcion de la pregunta
                        opcion.Codigo = codigoPregunta;
                    }

                    // Guardo todas las opciones de una
                    db.Opciones_de_seleccion.AddRange(opciones);
                    db.SaveChanges();

                    return RedirectToAction("Create");
                }
                catch(Exception exception)
                {
                    if (exception is System.Data.Entity.Infrastructure.DbUpdateException)
                    {
                        ModelState.AddModelError("Codigo", "Código ya en uso.");
                        return View(pregunta);
                    }
                }
            }

            return View();
        }

        // GET: PreguntaConOpcionesDeSeleccion/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta_con_opciones_de_seleccion pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion.Find(id);
            if (pregunta_con_opciones_de_seleccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Codigo = new SelectList(db.Pregunta_con_opciones, "Codigo", "TituloCampoObservacion", pregunta_con_opciones_de_seleccion.Codigo);
            return View(pregunta_con_opciones_de_seleccion);
        }

        // POST: PreguntaConOpcionesDeSeleccion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Tipo")] Pregunta_con_opciones_de_seleccion pregunta_con_opciones_de_seleccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pregunta_con_opciones_de_seleccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Codigo = new SelectList(db.Pregunta_con_opciones, "Codigo", "TituloCampoObservacion", pregunta_con_opciones_de_seleccion.Codigo);
            return View(pregunta_con_opciones_de_seleccion);
        }

        // GET: PreguntaConOpcionesDeSeleccion/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta_con_opciones_de_seleccion pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion.Find(id);
            if (pregunta_con_opciones_de_seleccion == null)
            {
                return HttpNotFound();
            }
            return View(pregunta_con_opciones_de_seleccion);
        }

        // POST: PreguntaConOpcionesDeSeleccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Pregunta_con_opciones_de_seleccion pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion.Find(id);
            db.Pregunta_con_opciones_de_seleccion.Remove(pregunta_con_opciones_de_seleccion);
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

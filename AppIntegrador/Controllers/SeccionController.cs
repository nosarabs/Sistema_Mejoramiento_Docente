﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace AppIntegrador.Controllers
{
    public class SeccionController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();
        public CrearSeccionModel crearSeccion = new CrearSeccionModel();

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
            crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion;
            return View(crearSeccion);
        }

        // POST: Seccion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Nombre")] Seccion seccion, List<Pregunta_con_opciones_de_seleccion> preguntas)
        {
            crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion;
            if (ModelState.IsValid && seccion.Codigo.Length > 0 && seccion.Nombre.Length > 0)
            {
                if ( InsertSeccionTienePregunta(seccion, preguntas) )
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    // Notifique que ocurrió un error
                    ModelState.AddModelError("Codigo", "Código ya en uso.");
                    return View(crearSeccion);
                }
            }

            return View(crearSeccion);
        }

        // Historia RIP-BKS1
        // Se copió la función para filtrar preguntas.
        [HttpGet]
        public ActionResult Create(string inp1, string inp2, string inp3)
        {
            crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion;
            if (inp2 == null && inp1 == null && inp3 == null)
            {
                crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion.ToList();

            }
            //if a user choose the radio button option as Subject  
            else if (inp2 == null && inp3 == null)
            {
                crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion.Where(x => x.Codigo.Contains(inp1)).ToList();
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
            }
            else if (inp1 == null && inp3 == null)
            {
                crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion.Where(x => x.Tipo.Contains(inp2)).ToList();
            }
            else if (inp1 == null && inp2 == null)
            {
                crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion.Where(x => x.Pregunta_con_opciones.Pregunta.Enunciado.Contains(inp3)).ToList();
            }
            else
            {
                crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion.ToList();
            }
            return View("Create", crearSeccion);
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
        private bool InsertSeccionTienePregunta(Seccion seccion, List<Pregunta_con_opciones_de_seleccion> preguntas)
        {
            if (db.AgregarSeccion(seccion.Codigo, seccion.Nombre) == 0)
            {
                return false;
            }

            if (preguntas != null)
            {
                for (int index = 0; index < preguntas.Count; ++index)
                {
                    db.AsociarPreguntaConSeccion(seccion.Codigo, preguntas[index].Codigo, index);
                }
            }
            return true;
        }

    }
}

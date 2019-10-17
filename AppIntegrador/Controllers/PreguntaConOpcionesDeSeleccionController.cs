﻿
using System;
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
        //public ActionResult Index()
        //{
        //    var pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion;
        //    return View(pregunta_con_opciones_de_seleccion.ToList());
        //}

        //the first parameter is the option that we choose and the second parameter will use the textbox value  
        public ActionResult Index(string input0, string input1, string input2, string input3)
        {
            var pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion;

            ViewBag.filtro = "Ninguno";
            if (input0 == null && input1 == null && input2 == null && input3 == null)
            {
                ViewBag.filtro = "Ninguno";
                return View(pregunta_con_opciones_de_seleccion.ToList());
            }
            // si se selecionó el código  
            if (input1.Length > 0)
            {
                ViewBag.filtro = "Por código: " + input1;
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(pregunta_con_opciones_de_seleccion.Where(x => x.Codigo.Contains(input1)).ToList());
            }
            // si se selecionó el enunciado 
            else if ( input2.Length > 0)
            {
                ViewBag.filtro = "Enunciado: " + input2;
                return View(pregunta_con_opciones_de_seleccion.Where(x => x.Pregunta_con_opciones.Pregunta.Enunciado.Contains(input2)).ToList());
            }
            // si se seleccionó el tipo
            else if (input3.Length > 0)
            {
                var aux = input3 == "U" ? "Selección Única" : "Seleción Múltiple";
                ViewBag.filtro = "Tipo: " + aux;
                return View(pregunta_con_opciones_de_seleccion.Where(x => x.Tipo.Contains(input3)).ToList());
            }
            else
            {
                ViewBag.filtro = "Ninguno";
                return View(pregunta_con_opciones_de_seleccion.ToList());
            }
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
        public ActionResult Create(Pregunta_con_opciones_de_seleccion pregunta, List<Opciones_de_seleccion> Opciones)
        {
            // Para esta fase del proyecto solo se soportan preguntas de selección única
            pregunta.Tipo = "U";
            if (ModelState.IsValid && pregunta.Codigo.Length > 0 && pregunta.Pregunta_con_opciones.Pregunta.Enunciado.Length > 0)
            {
                bool validOptions = Opciones != null;
                if (validOptions)
                {
                    validOptions = false;
                    foreach (Opciones_de_seleccion opcion in Opciones)
                    {
                        if (opcion.Texto != null && opcion.Texto != "")
                        {
                            validOptions = true;
                        }
                    }
                }

                if(!validOptions)
                {
                    ModelState.AddModelError("", "Una pregunta de selección única necesita al menos una opción");
                    return View(pregunta);
                }
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LoginIntegrador"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        try
                        {
                            cmd.Connection = con;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = "dbo.AgregarPreguntaConOpcion";
                            cmd.Parameters.Add(new SqlParameter("@cod", pregunta.Codigo));
                            cmd.Parameters.Add(new SqlParameter("@type", 'U'));
                            cmd.Parameters.Add(new SqlParameter("@enunciado", pregunta.Pregunta_con_opciones.Pregunta.Enunciado));
                            cmd.Parameters.Add(new SqlParameter("@justificacion", pregunta.Pregunta_con_opciones.TituloCampoObservacion));

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        catch (System.Data.SqlClient.SqlException)
                        {
                            ModelState.AddModelError("Codigo", "Código ya en uso.");
                            return View(pregunta);
                        }
                    }

                    foreach (Opciones_de_seleccion opcion in Opciones)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = "dbo.AgregarOpcion";
                            cmd.Parameters.Add(new SqlParameter("@cod", pregunta.Codigo));
                            cmd.Parameters.Add(new SqlParameter("@orden", opcion.Orden));
                            cmd.Parameters.Add(new SqlParameter("@texto", opcion.Texto));

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                }

                    ViewBag.Message = "Exitoso";
                    return View();
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


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
        public ActionResult Index(string option, string search)
        {
            var pregunta_con_opciones_de_seleccion = db.Pregunta_con_opciones_de_seleccion;

            //if a user choose the radio button option as Subject  
            if (option == "Tipo" && search.Length > 0)
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(pregunta_con_opciones_de_seleccion.Where(x => x.Tipo.Contains(search)).ToList());
            }
            else if (option == "Codigo" && search.Length > 0)
            {
                return View(pregunta_con_opciones_de_seleccion.Where(x => x.Codigo.Contains(search)).ToList());
            }
            else if (option == "Enunciado" && search.Length > 0)
            {
                return View(pregunta_con_opciones_de_seleccion.Where(x => x.Pregunta_con_opciones.Pregunta.Enunciado.Contains(search)).ToList());
            }
            else
            {
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
                try
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LoginIntegrador"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = "dbo.AgregarPreguntaConOpcion";
                            cmd.Parameters.Add(new SqlParameter("@cod", pregunta.Codigo));
                            cmd.Parameters.Add(new SqlParameter("@type", 'U'));
                            cmd.Parameters.Add(new SqlParameter("@texto", pregunta.Pregunta_con_opciones.Pregunta.Enunciado));
                            cmd.Parameters.Add(new SqlParameter("@justificacion", pregunta.Pregunta_con_opciones.TituloCampoObservacion));

                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
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
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    ModelState.AddModelError("Codigo", "Código ya en uso.");
                    return View(pregunta);
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

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
    public class SeccionController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();
        public CrearSeccionModel crearSeccion = new CrearSeccionModel();

        // GET: Seccion
        public ActionResult Index(string input0, string input1, string input2)
        {
            var seccion = db.Seccion;

            ViewBag.filtro = "Ninguno";
            if (input0 == null && input1 == null && input2 == null)
            {
                ViewBag.filtro = "Ninguno";
                return View(seccion.ToList());
            }
            // si se selecionó el código  
            if (input1.Length > 0)
            {
                ViewBag.filtro = "Por código: " + input1;
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(seccion.Where(x => x.Codigo.Contains(input1)).ToList());
            }
            // si se selecionó el enunciado 
            else if (input2.Length > 0)
            {
                ViewBag.filtro = "Nombre: " + input2;
                return View(seccion.Where(x => x.Nombre.Contains(input2)).ToList());
            }
            else
            {
                ViewBag.filtro = "Ninguno";
                return View(seccion.ToList());
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
                    ModelState.AddModelError("Seccion.Codigo", "Código ya en uso.");
                    return View(crearSeccion);
                }
            }

            return View(crearSeccion);
        }

        // Historia RIP-BKS1
        // Se copió la función para filtrar preguntas.
        [HttpGet]
        public ActionResult Create(string input0, string input1, string input2, string input3)
        {
            crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion;
            ViewBag.filtro = "Ninguno";
            if (input0 == null && input1 == null && input2 == null && input3 == null)
            {
                crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion.ToList();
                return View("Create", crearSeccion);
            }
            //if a user choose the radio button option as Subject  
            if (input1.Length > 0)
            {
                ViewBag.filtro = "Por código: " + input1;
                crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion.Where(x => x.Codigo.Contains(input1)).ToList();
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
            }
            else if (input2.Length > 0)
            {
                ViewBag.filtro = "Enunciado: " + input2;
                crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion.Where(x => x.Pregunta_con_opciones.Pregunta.Enunciado.Contains(input2)).ToList();
            }
            else if (input3.Length > 0)
            {
                var aux = input3 == "U" ? "Selección Única" : "Seleción Múltiple";
                ViewBag.filtro = "Tipo: " + aux;
                crearSeccion.pregunta_Con_Opciones_De_Seleccion = db.Pregunta_con_opciones_de_seleccion.Where(x => x.Tipo.Contains(input3)).ToList();
            }
            else
            {
                ViewBag.filtro = "Ninguno";
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
            try
            {
                if (db.AgregarSeccion(seccion.Codigo, seccion.Nombre) == 0)
                {
                    return false;
                }
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException)
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

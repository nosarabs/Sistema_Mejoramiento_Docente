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
    public class FormulariosController : Controller
    {
        
        private DataIntegradorEntities db = new DataIntegradorEntities();
        public CrearFormularioModel crearFormulario = new CrearFormularioModel();


        // GET: Formularios
        private class SeccionYCodigo
        {
            // It is important to declare them public so they get returned
            public string Codigo { get; set; }
            public string Nombre { get; set; }
        }
        private class Opcion
        {
            public string Texto { get; set; }
            public int Orden { get; set; }
        }

        public ActionResult LlenarFormulario(string id)
        {
            Formulario formularioDB = db.Formulario.Find(id);
            if (formularioDB == null)
            {
                return RedirectToAction("Index");
            }
            LlenarFormulario formulario = new LlenarFormulario { Nombre = id, Secciones = new List<SeccionConPreguntas>() };
            List<ObtenerSeccionesDeFormulario_Result> seccionesDeFormulario = db.ObtenerSeccionesDeFormulario(id).ToList();
            foreach(var seccion in seccionesDeFormulario)
            {
                List<ObtenerPreguntasDeSeccion_Result> preguntas = db.ObtenerPreguntasDeSeccion(seccion.Codigo).ToList();
                SeccionConPreguntas nuevaSeccion = new SeccionConPreguntas { CodigoSeccion = seccion.Codigo, Nombre = seccion.Nombre, Preguntas = new TodasLasPreguntas() };
                nuevaSeccion.Preguntas.Preguntas = new List<PreguntaConCodigoSeccion>();
                foreach(var pregunta in preguntas)
                {
                    nuevaSeccion.Preguntas.Preguntas.Add(new PreguntaConCodigoSeccion
                    {
                        Pregunta = new Pregunta { Codigo = pregunta.Codigo, Enunciado = pregunta.Enunciado, Tipo = pregunta.Tipo },
                        CodigoSeccion = nuevaSeccion.CodigoSeccion
                    });
                    ObtenerInformacionDePreguntas(nuevaSeccion.Preguntas.Preguntas);
                }
                formulario.Secciones.Add(nuevaSeccion);
            }

            return View(formulario);
        }

        public void ObtenerInformacionDePreguntas(IEnumerable<PreguntaConCodigoSeccion> preguntas)
        {
            if (preguntas != null)
            {
                foreach (PreguntaConCodigoSeccion pregunta in preguntas)
                {
                    if (pregunta.Pregunta.Tipo == "U" || pregunta.Pregunta.Tipo == "M" || pregunta.Pregunta.Tipo == "E" || pregunta.Pregunta.Tipo == "S")
                    {
                        pregunta.Pregunta.Pregunta_con_opciones = db.Pregunta_con_opciones.Where(x => x.Codigo.Equals(pregunta.Pregunta.Codigo)).ToList().FirstOrDefault();
                        if (pregunta.Pregunta.Tipo == "U" || pregunta.Pregunta.Tipo == "M")
                        {
                            pregunta.Pregunta.Pregunta_con_opciones.Pregunta_con_opciones_de_seleccion.Opciones_de_seleccion =
                                db.Opciones_de_seleccion.Where(x => x.Codigo.Equals(pregunta.Pregunta.Codigo)).ToList();
                        }
                        else if (pregunta.Pregunta.Tipo == "E")
                        {
                            pregunta.Pregunta.Pregunta_con_opciones.Escalar = db.Escalar.Where(x => x.Codigo.Equals(pregunta.Pregunta.Pregunta_con_opciones.Escalar.Codigo)).ToList().FirstOrDefault();
                        }
                    }
                }
            }
        }


        // GET: Formularios
        public ActionResult Index(string input0, string input1, string input2)
        {
            var formulario = db.Formulario;

            ViewBag.filtro = "Ninguno";
            if (input0 == null && input1 == null && input2 == null)
            {
                ViewBag.filtro = "Ninguno";
                return View("Index", formulario.ToList());
            }
            // si se selecionó el código  
            if (input1.Length > 0)
            {
                ViewBag.filtro = "Por código: " + input1;
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View("Index", formulario.Where(x => x.Codigo.Contains(input1)).ToList());
            }
            // si se selecionó el enunciado 
            else if (input2.Length > 0)
            {
                ViewBag.filtro = "Nombre: " + input2;
                return View("Index", formulario.Where(x => x.Nombre.Contains(input2)).ToList());
            }
            else
            {
                ViewBag.filtro = "Ninguno";
                return View("Index", formulario.ToList());
            }
        }

        // GET: Formularios/Details/5
        public ActionResult Details(string id)
        {
            crearFormulario.seccion = db.Seccion;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formulario.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }
            return View(formulario);
        }

        // GET: Formularios/Create
        public ActionResult Create()
        {
            crearFormulario.seccion = db.Seccion;
            return View("Create", crearFormulario);
        }

        // POST: Formularios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]        
        public ActionResult Create([Bind(Include = "Codigo,Nombre")] Formulario formulario, List<Seccion> secciones)
        {
            crearFormulario.seccion = db.Seccion;
            if (ModelState.IsValid && formulario.Codigo.Length > 0 && formulario.Nombre.Length > 0)
            {
                if(InsertFormularioTieneSeccion(formulario, secciones))
                {
                    ViewBag.Message = "Exitoso";
                    return RedirectToAction("Create");
                }
                else
                {
                    // Notifique que ocurrió un error
                    ModelState.AddModelError("Formulario.Codigo", "Código ya en uso.");
                    return View(crearFormulario);
                }
            }

            return View("Create", crearFormulario);
        }

        [HttpPost]
        public ActionResult GuardarRespuestas(PreguntaConCodigoSeccion objUser)
        {
            DateTime today = DateTime.Today;

            Console.WriteLine(today);

            //Console.WriteLine(HttpContext.Current.User.Identity.Name);

           return View();
        }

        // GET: Formularios/Edit/5
        public ActionResult Edit(string id)
        {
            crearFormulario.seccion = db.Seccion;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formulario.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }
            return View(formulario);
        }

        // Historia RIP-CF5
        // Se copió la función para filtrar preguntas.
        [HttpGet]
        public ActionResult Create(string input0, string input1, string input2)
        {
            crearFormulario.seccion = db.Seccion;

            ViewBag.filtro = "Ninguno";
            if (input0 == null && input1 == null && input2 == null)
            {
                crearFormulario.seccion = db.Seccion.ToList();
                return View("Create", crearFormulario);
            }
            //if a user choose the radio button option as Subject  
            if (input1.Length > 0)
            {
                ViewBag.filtro = "Por código: " + input1;
                crearFormulario.seccion = db.Seccion.Where(x => x.Codigo.Contains(input1)).ToList();
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
            }
            else if (input2.Length > 0)
            {
                ViewBag.filtro = "Nombre: " + input2;
                crearFormulario.seccion = db.Seccion.Where(x => x.Nombre.Contains(input2)).ToList();
            }
            else
            {
                ViewBag.filtro = "Ninguno";
                crearFormulario.seccion = db.Seccion.ToList();
            }
            return View("Create", crearFormulario);
        }

        // POST: Formularios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Nombre")] Formulario formulario)
        {
            crearFormulario.seccion = db.Seccion;
            if (ModelState.IsValid)
            {
                db.Entry(formulario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(formulario);
        }

        // GET: Formularios/Delete/5
        public ActionResult Delete(string id)
        {
            crearFormulario.seccion = db.Seccion;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Formulario formulario = db.Formulario.Find(id);
            if (formulario == null)
            {
                return HttpNotFound();
            }
            return View(formulario);
        }

        // POST: Formularios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            crearFormulario.seccion = db.Seccion;
            Formulario formulario = db.Formulario.Find(id);
            db.Formulario.Remove(formulario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            crearFormulario.seccion = db.Seccion;
            if (disposing)
            {
                db.Dispose();
            }
            crearFormulario.seccion = db.Seccion;
            base.Dispose(disposing);
        }
        

        private bool InsertFormularioTieneSeccion(Formulario formulario, List<Seccion> secciones)
        {
            try
            {
                if (db.AgregarFormulario(formulario.Codigo, formulario.Nombre) == 0)
                {
                    return false;
                }
            }
            catch (System.Data.Entity.Core.EntityCommandExecutionException)
            {
                return false;
            }

            if (secciones != null)
            {
                for (int index = 0; index < secciones.Count; ++index)
                {
                    db.AsociarSeccionConFormulario(formulario.Codigo, secciones[index].Codigo, index);
                }
            }
            return true;
        }
    }
}

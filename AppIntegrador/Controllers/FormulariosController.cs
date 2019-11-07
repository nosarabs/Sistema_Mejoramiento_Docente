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
using System.Data.Entity.Core.Objects;
using System.Threading.Tasks;

namespace AppIntegrador.Controllers
{
    public class FormulariosController : Controller
    {

        private DataIntegradorEntities db;
        public CrearFormularioModel crearFormulario = new CrearFormularioModel();

        public FormulariosController()
        {
            db = new DataIntegradorEntities();
        }

        public FormulariosController(DataIntegradorEntities db)
        {
            this.db = db;
        }

        public ActionResult LlenarFormulario(string id)
        {
            Formulario formularioDB = db.Formulario.Find(id);
            if (formularioDB == null)
            {
                return RedirectToAction("Index");
            }
            LlenarFormulario formulario = new LlenarFormulario { Formulario = formularioDB, Secciones = new List<SeccionConPreguntas>() };
            ObjectResult<ObtenerSeccionesDeFormulario_Result> seccionesDeFormulario = db.ObtenerSeccionesDeFormulario(id);

            /* Antes se utilizaba Linq para obtener estas respuestas, pero Linq no acepta parámetros nulos por lo que se cambió por un procedimiento almacenado
            var respuestasList Respuestas_a_formulario.Where(x => x.Correo.Equals(HttpContext.User.Identity.Name)
                                                   && x.CSigla.Equals("CI0128") && x.FCodigo.Equals(formularioDB.Codigo) && x.GNumero == 1
                                                    && x.GAnno == 2019 && x.GSemestre == 2);
            */

            var respuestasObtenidas = db.ObtenerRespuestasAFormulario(formularioDB.Codigo, HttpContext.User.Identity.Name, "CI0128", 1, 2019, 2);

            Respuestas_a_formulario respuestas = new Respuestas_a_formulario();

            if (respuestasObtenidas != null)
            {
                var respuestasList = respuestasObtenidas.FirstOrDefault();

                if (respuestasList != null)
                {
                    respuestas.FCodigo = respuestasList.FCodigo;
                    respuestas.Correo = respuestasList.Correo;
                    respuestas.CSigla = respuestasList.CSigla;
                    respuestas.Fecha = respuestasList.Fecha;
                    respuestas.Finalizado = respuestas.Finalizado;
                    respuestas.GAnno = respuestasList.GAnno;
                    respuestas.GNumero = respuestasList.GNumero;
                    respuestas.GSemestre = respuestasList.GSemestre;
                }
            }

            if (seccionesDeFormulario != null)
            {
                foreach (var seccion in seccionesDeFormulario.ToList())
                {
                    List<ObtenerPreguntasDeSeccion_Result> preguntas = db.ObtenerPreguntasDeSeccion(seccion.Codigo).ToList();
                    SeccionConPreguntas nuevaSeccion = new SeccionConPreguntas { CodigoSeccion = seccion.Codigo, Nombre = seccion.Nombre, Preguntas = new List<PreguntaConNumeroSeccion>(), Orden = seccion.Orden };
                    foreach (var pregunta in preguntas)
                    {
                        nuevaSeccion.Preguntas.Add(new PreguntaConNumeroSeccion
                        {
                            Pregunta = new Pregunta { Codigo = pregunta.Codigo, Enunciado = pregunta.Enunciado, Tipo = pregunta.Tipo },
                            OrdenSeccion = nuevaSeccion.Orden,
                            OrdenPregunta = pregunta.Orden
                        });
                        ObtenerInformacionDePreguntas(nuevaSeccion.Preguntas, nuevaSeccion.CodigoSeccion, respuestas);
                    }
                    formulario.Secciones.Add(nuevaSeccion);
                }

            }

            return View(formulario);
        }


        public ActionResult DesplegarFormulario(string id)
        {
            Formulario formularioDB = db.Formulario.Find(id);
            LlenarFormulario formulario = new LlenarFormulario { Formulario = formularioDB, Secciones = new List<SeccionConPreguntas>() };
            ObjectResult<ObtenerSeccionesDeFormulario_Result> seccionesDeFormulario = db.ObtenerSeccionesDeFormulario(id);

            /* Antes se utilizaba Linq para obtener estas respuestas, pero Linq no acepta parámetros nulos por lo que se cambió por un procedimiento almacenado
            var respuestasList Respuestas_a_formulario.Where(x => x.Correo.Equals(HttpContext.User.Identity.Name)
                                                   && x.CSigla.Equals("CI0128") && x.FCodigo.Equals(formularioDB.Codigo) && x.GNumero == 1
                                                    && x.GAnno == 2019 && x.GSemestre == 2);
            */



            if (seccionesDeFormulario != null)
            {
                foreach (var seccion in seccionesDeFormulario.ToList())
                {
                    List<ObtenerPreguntasDeSeccion_Result> preguntas = db.ObtenerPreguntasDeSeccion(seccion.Codigo).ToList();
                    SeccionConPreguntas nuevaSeccion = new SeccionConPreguntas { CodigoSeccion = seccion.Codigo, Nombre = seccion.Nombre, Preguntas = new List<PreguntaConNumeroSeccion>(), Orden = seccion.Orden };
                    foreach (var pregunta in preguntas)
                    {
                        nuevaSeccion.Preguntas.Add(new PreguntaConNumeroSeccion
                        {
                            Pregunta = new Pregunta { Codigo = pregunta.Codigo, Enunciado = pregunta.Enunciado, Tipo = pregunta.Tipo },
                            OrdenSeccion = nuevaSeccion.Orden,
                            OrdenPregunta = pregunta.Orden
                        });
                        ObtenerInformacionDePreguntas(nuevaSeccion.Preguntas, nuevaSeccion.CodigoSeccion, null);
                    }
                    formulario.Secciones.Add(nuevaSeccion);
                }
            }
            return PartialView("SeccionConPreguntas", formulario.Secciones);
        }
        // Retorna la vista "parcial" de pregunta Si/No/NR (.cshtml)
        public ActionResult RespuestaLibre()
        {
            ViewBag.message = "RespuestaLibre";
            return View("RespuestaLibre");
        }

        // Se espera que respuestas ya venga con el código del formulario.
        [HttpPost]
        public ActionResult GuardarRespuestas(Respuestas_a_formulario respuestas, List<SeccionConPreguntas> secciones)
        {
            if (respuestas == null || secciones == null)
            {
                return RedirectToAction("Index");
            }

            respuestas.Fecha = DateTime.Today;
            respuestas.Correo = HttpContext.User.Identity.Name;

            // La parte de grupo por ahora va hardcodeada, porque por ahora es la implementación de llenar el formulario nada más
            respuestas.CSigla = "CI0128";
            respuestas.GNumero = 1;
            respuestas.GAnno = 2019;
            respuestas.GSemestre = 2;

            db.EliminarRespuestasDeFormulario(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero, respuestas.GAnno, respuestas.GSemestre);

            // Llamar a procedimiento que agrega Respuestas_a_formulario
            db.GuardarRespuestaAFormulario(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero, respuestas.GAnno, respuestas.GSemestre, respuestas.Fecha);

            // Luego, por cada sección guarde las respuestas de cada una de sus preguntas
            foreach (SeccionConPreguntas seccion in secciones)
            {
                if (seccion.Preguntas != null)
                {
                    foreach (PreguntaConNumeroSeccion pregunta in seccion.Preguntas)
                    {
                        GuardarRespuestaAPregunta(pregunta, seccion.CodigoSeccion, respuestas);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public void GuardarRespuestaAPregunta(PreguntaConNumeroSeccion pregunta, string CodigoSeccion, Respuestas_a_formulario respuestas)
        {
            if (pregunta != null && !string.IsNullOrEmpty(CodigoSeccion) && respuestas != null)
            {

                if (pregunta.Pregunta.Tipo == "L")
                {
                    db.GuardarRespuestaAPreguntaLibre(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero, respuestas.GAnno, respuestas.GSemestre,
                                                            respuestas.Fecha, pregunta.Pregunta.Codigo, CodigoSeccion, pregunta.RespuestaLibreOJustificacion);
                }
                else
                {
                    // Se crea la tupla que indica que el formulario fue llenado. Es el intento de llenado de un formulario, se ocupa antes de agregar las opciones seleccionadas
                    db.GuardarRespuestaAPreguntaConOpciones(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero, respuestas.GAnno,
                                                        respuestas.GSemestre, respuestas.Fecha, pregunta.Pregunta.Codigo, CodigoSeccion, pregunta.RespuestaLibreOJustificacion);

                    // Se recorren cada una de las opciones que fueron seleccionadas para la pregunta. En el caso de selección múltiple, serán varias.
                    // En todos los demás casos solo se ejecuta una vez.
                    if (pregunta.Opciones != null && pregunta.Opciones.Any())
                    {
                        foreach (var opcion in pregunta.Opciones)
                        {
                            db.GuardarOpcionesSeleccionadas(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero, respuestas.GAnno,
                                                            respuestas.GSemestre, respuestas.Fecha, pregunta.Pregunta.Codigo, CodigoSeccion, (byte)opcion);
                        }
                    }
                }
            }
        }

        public void ObtenerInformacionDePreguntas(IEnumerable<PreguntaConNumeroSeccion> preguntas, string codSeccion, Respuestas_a_formulario respuestas)
        {
            if (preguntas != null)
            {
                foreach (PreguntaConNumeroSeccion pregunta in preguntas)
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

                        if (respuestas != null)
                        {

                            var respuestaGuardada = db.Responde_respuesta_con_opciones.Where(x => x.Correo.Equals(respuestas.Correo) && x.CSigla.Equals(respuestas.CSigla)
                                                                        && x.GNumero == respuestas.GNumero && x.GSemestre == respuestas.GSemestre
                                                                        && x.GAnno == respuestas.GAnno && x.FCodigo.Equals(respuestas.FCodigo)
                                                                        && x.SCodigo.Equals(codSeccion) && x.PCodigo.Equals(pregunta.Pregunta.Codigo));
                            if (respuestaGuardada != null && respuestaGuardada.ToList().Any())
                            {
                                pregunta.RespuestaLibreOJustificacion = respuestaGuardada.FirstOrDefault().Justificacion;
                                var opcionesGuardadas = db.Opciones_seleccionadas_respuesta_con_opciones.Where(x => x.Correo.Equals(respuestas.Correo) && x.CSigla.Equals(respuestas.CSigla)
                                                                        && x.GNumero == respuestas.GNumero && x.GSemestre == respuestas.GSemestre
                                                                        && x.GAnno == respuestas.GAnno && x.FCodigo.Equals(respuestas.FCodigo)
                                                                        && x.SCodigo.Equals(codSeccion) && x.PCodigo.Equals(pregunta.Pregunta.Codigo));
                                pregunta.Opciones = new List<int>();
                                if (opcionesGuardadas != null)
                                {
                                    foreach (var opcion in opcionesGuardadas)
                                    {
                                        pregunta.Opciones.Add(opcion.OpcionSeleccionada);
                                    }
                                }
                            }
                        }
                    }
                    else if (pregunta.Pregunta.Tipo == "L" && respuestas != null)
                    {
                        var respuestaGuardada = db.Responde_respuesta_libre.Where(x => x.Correo.Equals(respuestas.Correo) && x.CSigla.Equals(respuestas.CSigla)
                                                                    && x.GNumero == respuestas.GNumero && x.GSemestre == respuestas.GSemestre
                                                                    && x.GAnno == respuestas.GAnno && x.FCodigo.Equals(respuestas.FCodigo)
                                                                    && x.SCodigo.Equals(codSeccion) && x.PCodigo.Equals(pregunta.Pregunta.Codigo));
                        if (respuestaGuardada != null)
                        {
                            pregunta.RespuestaLibreOJustificacion = respuestaGuardada.FirstOrDefault().Observacion;
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
            crearFormulario.crearSeccionModel = new CrearSeccionModel();
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
                if (InsertFormulario(formulario))
                {
                    ViewBag.Message = "Exitoso";
                    return RedirectToAction("Index");
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
        //        [HttpPost]
        public ActionResult AplicarFiltro(string input0, string input1, string input2)
        {
            crearFormulario.seccion = db.Seccion;

            ViewBag.filtro = "Ninguno";
            if (input0 == null && input1 == null && input2 == null)
            {
                crearFormulario.seccion = db.Seccion.ToList();
                return PartialView("~/Views/Seccion/_SeccionPartial.cshtml", crearFormulario.seccion);
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
            return PartialView("~/Views/Seccion/_SeccionPartial.cshtml", crearFormulario.seccion);
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


        public class SeccionesFormulario
        {
            public string codigo { get; set; }
            public string nombre { get; set; }
            public List<String> seccionesAsociadas { get; set; }
        }
        [HttpPost]
        /**
         * Este método valida si ya el formulario fue creado, de no ser así
         * lo crea y le asocia las secciones recibidas por parámetros
         * 
         */
        public ActionResult AsociarSesionesAFormulario(SeccionesFormulario formulario)
        {
            Formulario form = new Formulario();
            form.Codigo = formulario.codigo;
            form.Nombre = formulario.nombre;

            if (ModelState.IsValid && formulario != null && formulario.codigo != null && formulario.nombre != null && formulario.codigo.Length > 0 && formulario.nombre.Length > 0)
            {
                if (InsertFormularioTieneSeccion(form, formulario.seccionesAsociadas))
                {
                    ViewBag.Message = "Exitoso";
                }
                else
                {
                    // Notifique que ocurrió un error
                    ModelState.AddModelError("Formulario.Codigo", "Código ya en uso.");
                }
            }


            return DesplegarFormulario(form.Codigo);
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

        private bool InsertFormularioTieneSeccion(Formulario formulario, List<String> secciones)
        {
            if (formulario == null || secciones == null)
            {
                return false;
            }

            for (int index = 0; index < secciones.Count; ++index)
            {
                db.AsociarSeccionConFormulario(formulario.Codigo, secciones[index], index);
            }
            return true;
        }

        private bool InsertFormulario(Formulario formulario)
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
            return true;
        }

        [HttpPost]
        public ActionResult AgregarFormulario(Formulario formulario)
        {
            return Json(new { guardadoExitoso = InsertFormulario(formulario) });
        }
    }
}
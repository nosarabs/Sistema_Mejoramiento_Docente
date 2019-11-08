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


        public PartialViewResult CargarSeccionesFormulario(string Id)
        {
            IEnumerable<ObtenerSeccionesDeFormulario_Result> seccionesSeleccionadas = db.ObtenerSeccionesDeFormulario("CI0128G1").ToList();

            return PartialView("_SeccionesActualesPartial", seccionesSeleccionadas);
        }

        public ActionResult LlenarFormulario(string id)
        {
            if(HttpContext == null)
            {
                return Redirect("~/");
            }
            Formulario formularioDB = db.Formulario.Find(id);
            if (formularioDB == null)
            {
                return RedirectToAction("Index");
            }
            LlenarFormulario formulario = new LlenarFormulario { Formulario = formularioDB, Secciones = new List<SeccionConPreguntas>() };
            ObjectResult<ObtenerSeccionesDeFormulario_Result> seccionesDeFormulario = db.ObtenerSeccionesDeFormulario(id);

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

            ObtenerSeccionesConPreguntas(formulario, seccionesDeFormulario, respuestas);

            return View(formulario);
        }

        public ActionResult DesplegarFormulario(string id)
        {
            Formulario formularioDB = db.Formulario.Find(id);
            LlenarFormulario formulario = new LlenarFormulario { Formulario = formularioDB, Secciones = new List<SeccionConPreguntas>() };
            ObjectResult<ObtenerSeccionesDeFormulario_Result> seccionesDeFormulario = db.ObtenerSeccionesDeFormulario(id);

            ObtenerSeccionesConPreguntas(formulario, seccionesDeFormulario, null);

            return PartialView("SeccionConPreguntas", formulario.Secciones);
        }

        public void ObtenerSeccionesConPreguntas(LlenarFormulario formulario, ObjectResult<ObtenerSeccionesDeFormulario_Result> seccionesDeFormulario, 
            Respuestas_a_formulario respuestas)
        {
            if (formulario != null && seccionesDeFormulario != null)
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
        }

        // Retorna la vista "parcial" de pregunta Respuesta libre (.cshtml)
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
                        pregunta.Pregunta.Pregunta_con_opciones = db.Pregunta_con_opciones.Find(pregunta.Pregunta.Codigo);
                        if (pregunta.Pregunta.Tipo == "U" || pregunta.Pregunta.Tipo == "M")
                        {
                            var resultadoObtenerOpciones = db.ObtenerOpcionesDePregunta(pregunta.Pregunta.Codigo);
                            
                            if(resultadoObtenerOpciones != null)
                            {
                                pregunta.Pregunta.Pregunta_con_opciones.Pregunta_con_opciones_de_seleccion.Opciones_de_seleccion = new List<Opciones_de_seleccion>();
                                foreach (var opcion in resultadoObtenerOpciones.ToList())
                                {
                                    pregunta.Pregunta.Pregunta_con_opciones.Pregunta_con_opciones_de_seleccion.Opciones_de_seleccion.Add
                                        (new Opciones_de_seleccion { Codigo = pregunta.Pregunta.Codigo, Orden = opcion.Orden, Texto = opcion.Texto });
                                }
                            }
                        }
                        else if (pregunta.Pregunta.Tipo == "E")
                        {
                            pregunta.Pregunta.Pregunta_con_opciones.Escalar = db.Escalar.Find(pregunta.Pregunta.Codigo);
                        }

                        if (respuestas != null)
                        {
                            var resultadoRespuestaGuardada = db.ObtenerRespuestasAPreguntaConOpciones(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero, respuestas.GSemestre, respuestas.GAnno,
                                                                                     codSeccion, pregunta.Pregunta.Codigo);
                            if (resultadoRespuestaGuardada != null)
                            {
                                var respuestaGuardada = resultadoRespuestaGuardada.ToList();
                                if (respuestaGuardada.Any())
                                {
                                    pregunta.RespuestaLibreOJustificacion = respuestaGuardada.FirstOrDefault().Justificacion;

                                    var opcionesGuardadas = db.ObtenerOpcionesSeleccionadas(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero, respuestas.GSemestre, respuestas.GAnno,
                                                                                         codSeccion, pregunta.Pregunta.Codigo);
                                    pregunta.Opciones = new List<int>();
                                    if (opcionesGuardadas != null)
                                    {
                                        foreach (var opcion in opcionesGuardadas.ToList())
                                        {
                                            pregunta.Opciones.Add(opcion.OpcionSeleccionada);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (pregunta.Pregunta.Tipo == "L" && respuestas != null)
                    {
                        var resultadoRespuestaGuardada = db.Responde_respuesta_libre.Where(x => x.Correo.Equals(respuestas.Correo) && x.CSigla.Equals(respuestas.CSigla)
                                                                    && x.GNumero == respuestas.GNumero && x.GSemestre == respuestas.GSemestre
                                                                    && x.GAnno == respuestas.GAnno && x.FCodigo.Equals(respuestas.FCodigo)
                                                                    && x.SCodigo.Equals(codSeccion) && x.PCodigo.Equals(pregunta.Pregunta.Codigo));
                        
                        if (resultadoRespuestaGuardada != null)
                        {
                            var respuestaGuardada = resultadoRespuestaGuardada.ToList();
                            if(respuestaGuardada.Any())
                            {
                                pregunta.RespuestaLibreOJustificacion = respuestaGuardada.FirstOrDefault().Observacion;
                            }
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
        public ActionResult Create([Bind(Include = "Codigo,Nombre")] Formulario formulario, List<Seccion> secciones, int? formularioCreado)
        {
            if(formularioCreado == 1)
            {
                return RedirectToAction("Index");
            }
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
                    ViewBag.Message = "Fallido";
                    crearFormulario.Formulario = formulario;
                    crearFormulario.crearSeccionModel = new CrearSeccionModel();
                    return View(crearFormulario);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AgregarSeccion(Seccion seccion)
        {
            return Json(new { guardadoExitoso = seccion != null && InsertSeccion(seccion) });
        }

        [HttpPost]
        public ActionResult ActualizarBancoSecciones()
        {
            crearFormulario.seccion = db.Seccion;
            return PartialView("~/Views/Seccion/_SeccionPartial.cshtml", crearFormulario.seccion);
        }
        [HttpPost]
        public ActionResult ActualizarCrearSeccion()
        {
            crearFormulario.crearSeccionModel = new CrearSeccionModel();
            return PartialView("~/Views/Seccion/_CreateSeccionPartial.cshtml", crearFormulario.crearSeccionModel);
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
        /**
         * Este método valida si ya el formulario fue creado, de no ser así
         * lo crea y le asocia las secciones recibidas por parámetros
         * 
         */
        [HttpPost]
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

        private bool InsertSeccion(Seccion seccion)
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
            return true;
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
            return Json(new { guardadoExitoso = formulario != null && InsertFormulario(formulario) });
        }


    }
}
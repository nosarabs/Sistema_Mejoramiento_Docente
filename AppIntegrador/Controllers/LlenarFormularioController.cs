using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Controllers
{
    public class LlenarFormularioController : Controller
    {
        private DataIntegradorEntities db;

        // Fechas en formato dd/MM
        const string InicioVerano = "01/01/";
        const string FinVerano = "07/03/";

        const string InicioPrimerSemestre = "08/03/";
        const string FinPrimerSemestre = "31/07/";

        const string InicioSegundoSemestre = "01/08/";
        const string FinSegundoSemestre = "31/12/";

        byte SemestreActual;

        readonly DateTime FechaActual;

        readonly DateTime FechaInicioVerano;
        readonly DateTime FechaFinVerano;

        readonly DateTime FechaInicioSemestre1;
        readonly DateTime FechaFinSemestre1;

        readonly DateTime FechaInicioSemestre2;
        readonly DateTime FechaFinSemestre2;

        public LlenarFormularioController()
        {
            db = new DataIntegradorEntities();

            FechaActual = DateTime.Now;

            FechaInicioVerano = DateTime.ParseExact(s: InicioVerano + (FechaActual.Year - 1).ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            FechaFinVerano = DateTime.ParseExact(s: FinVerano + (FechaActual.Year - 1).ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            FechaInicioSemestre1 = DateTime.ParseExact(s: InicioPrimerSemestre + FechaActual.Year.ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            FechaFinSemestre1 = DateTime.ParseExact(s: FinPrimerSemestre + FechaActual.Year.ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            FechaInicioSemestre2 = DateTime.ParseExact(s: InicioSegundoSemestre + FechaActual.Year.ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            FechaFinSemestre2 = DateTime.ParseExact(s: FinSegundoSemestre + FechaActual.Year.ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            SemestreActual = ObtenerSemestreActual();
        }

        public LlenarFormularioController(DataIntegradorEntities db)
        {
            this.db = db;
        }

        public ActionResult LlenarFormulario(string id)
        {
            if (HttpContext == null)
            {
                return Redirect("~/");
            }
            Formulario formularioDB = db.Formulario.Find(id);
            if (formularioDB == null)
            {
                return RedirectToAction("MisFormularios");
            }
            LlenarFormulario formulario = CrearFormulario(id, formularioDB);

            return View(formulario);
        }

        public LlenarFormulario CrearFormulario(string id, Formulario formularioDB)
        {
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
            SeccionController seccionController = new SeccionController(this.db);
            seccionController.ObtenerSeccionesConPreguntas(formulario, seccionesDeFormulario, respuestas, false);
            return formulario;
        }

        [HttpGet]
        public ActionResult VistaPrevia(string id)
        {
            if (HttpContext == null)
            {
                return Redirect("~/");
            }
            Formulario formularioDB = db.Formulario.Find(id);
            if (formularioDB == null)
            {
                return RedirectToAction("../Formularios/Index");
            }
            LlenarFormulario formulario = CrearFormulario(id, formularioDB);

            return View(formulario);
        }

        // Se espera que respuestas ya venga con el código del formulario.
        [HttpPost]
        public ActionResult GuardarRespuestas(Respuestas_a_formulario respuestas, List<SeccionConPreguntas> secciones)
        {
            if (respuestas == null || secciones == null)
            {
                return RedirectToAction("MisFormularios");
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
            db.GuardarRespuestaAFormulario(respuestas.FCodigo, respuestas.Correo, respuestas.CSigla, respuestas.GNumero, respuestas.GAnno, respuestas.GSemestre, respuestas.Fecha, respuestas.Finalizado);

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

            return RedirectToAction("MisFormularios");
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

        public byte ObtenerSemestreActual()
        {
            // Verano
            if (FechaInicioVerano < FechaActual && FechaActual < FechaFinVerano)
            {
                return 3;
            }
            // Primer semestre
            else if (FechaInicioSemestre1 < FechaActual && FechaActual < FechaFinSemestre1)
            {
                return 1;
            }
            // Segundo semestre
            else
            {
                return 2;
            }
        }

        // GET: LlenarFormulario
        public ActionResult MisFormularios()
        {
            MisFormulariosModel modelo = new MisFormulariosModel();

            List<Periodo_activa_por> periodosSemestre = ObtenerFormulariosSemestre();
            List<Periodo_activa_por> periodosPasados = ObtenerFormulariosDisponibles(ObtenerFechaInicioSemestre(), null);

            foreach (var periodo in periodosSemestre)
            {
                FormularioAsignado formulario = new FormularioAsignado(periodo, HttpContext.User.Identity.Name);
                modelo.FormulariosSemestre.Add(formulario);
            }

            foreach (var periodo in periodosPasados)
            {
                FormularioAsignado formulario = new FormularioAsignado(periodo, HttpContext.User.Identity.Name);
                modelo.FormulariosPasados.Add(formulario);
            }

            return View(modelo);
        }

        private List<Periodo_activa_por> ObtenerFormulariosSemestre()
        {
            List<Periodo_activa_por> formularios = new List<Periodo_activa_por>();

            String correo = HttpContext.User.Identity.Name;

            var formsDB = db.ObtenerFormulariosPorSemestre(correo, DateTime.Now.Year, SemestreActual);

            foreach (var form in formsDB)
            {
                Periodo_activa_por periodo = new Periodo_activa_por
                {
                    CSigla = form.SiglaCurso,
                    FCodigo = form.Codigo,
                    GAnno = form.Anno,
                    GNumero = form.NumGrupo,
                    GSemestre = form.Semestre,
                    FechaInicio = form.FechaInicio,
                    FechaFin = form.FechaFin,
                };

                formularios.Add(periodo);
            }

            return formularios;
        }

        private List<Periodo_activa_por> ObtenerFormulariosDisponibles(DateTime? inicio, DateTime? fin)
        {
            List<Periodo_activa_por> formularios = new List<Periodo_activa_por>();

            String correo = HttpContext.User.Identity.Name;

            var formsDB = db.ObtenerFormulariosDeEstudiante(correo, inicio, fin).ToList();

            foreach (var form in formsDB)
            {
                Periodo_activa_por periodo = new Periodo_activa_por
                {
                    CSigla = form.SiglaCurso,
                    FCodigo = form.Codigo,
                    GAnno = form.Anno,
                    GNumero = form.NumGrupo,
                    GSemestre = form.Semestre,
                    FechaInicio = form.FechaInicio,
                    FechaFin = form.FechaFin,
                };

                formularios.Add(periodo);
            }

            return formularios;
        }

        private DateTime ObtenerFechaInicioSemestre()
        {
            // Verano
            if (FechaInicioVerano < FechaActual && FechaActual < FechaFinVerano)
            {
                return FechaInicioVerano;
            }
            // Primer semestre
            else if(FechaInicioSemestre1 < FechaActual && FechaActual < FechaFinSemestre1)
            {
                return FechaInicioSemestre1;
            }
            // Segundo semestre
            else
            {
                return FechaInicioSemestre2;
            }
        }

        private DateTime ObtenerFechaFinSemestre()
        {
            // Verano
            if (FechaInicioVerano < FechaActual && FechaActual < FechaFinVerano)
            {
                return FechaFinVerano;
            }
            // Primer semestre
            else if (FechaInicioSemestre1 < FechaActual && FechaActual < FechaFinSemestre1)
            {
                return FechaFinSemestre1;
            }
            // Segundo semestre
            else
            {
                return FechaFinSemestre2;
            }
        }

    }
}
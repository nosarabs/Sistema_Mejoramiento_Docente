using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class AsignacionFormulariosController : Controller
    {
        private DataIntegradorEntities db;

        private DashboardController dashboard = new DashboardController();

        public AsignacionFormulariosController()
        {
            db = new DataIntegradorEntities();
        }
        // GET: Asignacion
        [HttpGet]
        public ActionResult Index(string id)
        {
            if (HttpContext == null)
            {
                return Redirect("~/");
            }
            Formulario formularioDB = db.Formulario.Find(id);
            if (formularioDB == null)
            {
                return View();
            }

            ViewBag.Nombre = formularioDB.Nombre;
            ViewBag.Codigo = formularioDB.Codigo;
            return View();
        }


        [HttpPost]
        public JsonResult Asignar(string codigoFormulario, string codigoUASeleccionada, string codigoCarreraEnfasisSeleccionada, string grupoSeleccionado, string correoProfesorSeleccionado, string fechaInicioSeleccionado, string fechaFinSeleccionado)
        {
            string codigoCarrera = null;
            string codigoEnfasis = null;
            string siglaCursoGrupo = null;
            Nullable<byte> numeroGrupo = null;
            Nullable<byte> semestreGrupo = null;
            Nullable<int> anno = null;
            Nullable<DateTime> fechaInicio = null;
            Nullable<DateTime> fechaFin = null; 

            if (codigoUASeleccionada == "null" && codigoCarreraEnfasisSeleccionada == "null" && grupoSeleccionado == "null" && correoProfesorSeleccionado == "null" && fechaInicioSeleccionado == "" && fechaFinSeleccionado == "")
            {
                return Json(new { errorcito = false, tipoError = 1 });
            }
            if (fechaInicioSeleccionado.Length > 0 && fechaFinSeleccionado.Length > 0)
            {
                fechaInicio = Convert.ToDateTime(fechaInicioSeleccionado);
                fechaFin = Convert.ToDateTime(fechaFinSeleccionado);
            }
            else
            {
                return Json(new { errorcito = false, tipoError = 2});
            }

            if (DateTime.Compare((DateTime)fechaInicio, (DateTime)fechaFin) > 0)
            {
                return Json(new { errorcito = false, tipoError = 3 });
            }

            if (correoProfesorSeleccionado == "null")
                correoProfesorSeleccionado = null;
            if (codigoUASeleccionada == "null")
                codigoUASeleccionada = null;

            if (codigoCarreraEnfasisSeleccionada != "null")
            {
                string[] codigosSeparados = codigoCarreraEnfasisSeleccionada.Split('/');
                codigoCarrera = codigosSeparados[0];
                codigoEnfasis = codigosSeparados[1];
            }

            if (grupoSeleccionado != "null")
            {
                string[] grupoLlaves = grupoSeleccionado.Split('/');
                siglaCursoGrupo = grupoLlaves[0];
                numeroGrupo = byte.Parse(grupoLlaves[1]);
                semestreGrupo = byte.Parse(grupoLlaves[2]);
                anno = int.Parse(grupoLlaves[3]);
            }

            var grupos = db.ObtenerGruposAsociados(codigoUASeleccionada, codigoCarrera, codigoEnfasis, siglaCursoGrupo, numeroGrupo , semestreGrupo, anno, correoProfesorSeleccionado);
     
            var gruposAsociadosLista = grupos.ToList();
            
            if (gruposAsociadosLista.Count < 0)
            {
                return Json(new { errorcito = false, tipoError = 4 });
            }

            for (int index = 0; index < gruposAsociadosLista.Count; ++index)
            {
                var grupoActual = gruposAsociadosLista[index];
                db.AsignarFormulario(codigoFormulario, grupoActual.SiglaCurso, grupoActual.NumGrupo, grupoActual.Anno, grupoActual.Semestre, fechaInicio, fechaFin);
            }

            // RIP-AF3
            // Como es la asignacion por curso, se asume que viene null los demas 
            // para que acá sean implementados

            // llamado al procedimiento almacenado 
            // db.ObtenerGruposAsociados();
            //List<UAsFiltros> tempUA;

            //for(int index = 0; index < unidadesAcademicas.Count; ++index)
            //{
            //    if (unidadesAcademicas[index].Equals())
            //}
            return Json(new { errorcito = true });
        }

        // Modificado por: Jostin Álvarez
        // Historia a la que pertenece: RIP-AFC "Yo como administrativo quiero enviar un correo a los estudiantes para que llenen formularios cuando se los asigno"
        // Envía un correo cada estudiando que está asignado a un cuestionario pidiéndole que lo llene.
        private void EnviarCorreoSobreAsignaciónCuestionario(Formulario formulario)
        {
            List<string> involucrados = new List<string>();

            // Se obtienen todos los estudiantes a los que les tiene que llegar el correo.
            List<ObtenerEstudiantesAsociadosAFormulario_Result> estudiantes = db.ObtenerEstudiantesAsociadosAFormulario(formulario.Codigo).ToList();
            foreach (ObtenerEstudiantesAsociadosAFormulario_Result estudiante in estudiantes)
            {
                involucrados.Add(estudiante.Correo);
            }
            Utilities.EmailNotification emailNotification = new Utilities.EmailNotification();

            string asunto = "Evaluación del formulario " + formulario.Nombre;

            string texto = "Estimado/a estudiante\n";
            texto += "La Escuela de Ciencias de la Computación e Informática le solicita ";
            texto += "que conteste el formulario " + formulario.Codigo + ": " + formulario.Nombre;
            texto += "<br>Favor no responder directamente a este correo";

            string textoAlt = "<body><p>" + texto + "</p></body>";

            // Se envía el correo formado a todos los estudiantes involucrados
            _ = emailNotification.SendNotification(involucrados, asunto, texto, textoAlt);
        }
    }
}

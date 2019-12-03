using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AppIntegrador.Models;
using AppIntegrador.Utilities;

namespace AppIntegrador.Controllers
{
    public class AsignacionFormulariosController : Controller
    {
        // Referencia a la base de datos
        private DataIntegradorEntities db;
        // Referencia al Dashboard controller
        private DashboardController dashboard = new DashboardController();
        private readonly IPerm permissionManager;

        // Controlador por defecto
        public AsignacionFormulariosController()
        {
            db = new DataIntegradorEntities();
            permissionManager = new PermissionManager();
        }

        // Metodo principal de la vista: Index
        [HttpGet]
        public ActionResult Index(string id)
        {
            if (!permissionManager.IsAuthorized(Permission.CREAR_FORMULARIO))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }

            if (HttpContext == null)
            {
                return Redirect("~/");
            }
            // Asocia el id del formulario correspondiente
            Formulario formularioDB = db.Formulario.Find(id);
            if (formularioDB == null)
            {
                return View();
            }
            // Almacena el codigo y el nombre del formulario
            ViewBag.Nombre = formularioDB.Nombre;
            ViewBag.Codigo = formularioDB.Codigo;
            return View();
        }


        /**
         * RIP AF1,AF2,AF3,AF4
         * Asignación de formularios: recibe los datos desde la vista 
         * necesarios para la asignar un formulario a uno o más grupos
         */
        [HttpPost]
        public JsonResult Asignar(string codigoFormulario, string codigoUASeleccionada, string codigoCarreraEnfasisSeleccionada, string grupoSeleccionado, string correoProfesorSeleccionado, string fechaInicioSeleccionado, string fechaFinSeleccionado)
        {
            if (!permissionManager.IsAuthorized(Permission.CREAR_FORMULARIO))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return Json(new { error = false, tipoError = 5 });
            }
            // Parámetros que se enviaran al procedimiento almacenado
            string codigoCarrera = null;
            string codigoEnfasis = null;
            string siglaCursoGrupo = null;
            Nullable<byte> numeroGrupo = null;
            Nullable<byte> semestreGrupo = null;
            Nullable<int> anno = null;
            Nullable<DateTime> fechaInicio = null;
            Nullable<DateTime> fechaFin = null;

            // Sino se seleccionan datos, existe un error
            if (codigoUASeleccionada == "null" && codigoCarreraEnfasisSeleccionada == "null" && grupoSeleccionado == "null" && correoProfesorSeleccionado == "null" || (fechaInicioSeleccionado == "" && fechaFinSeleccionado == ""))
            {
                return Json(new { error = false, tipoError = 1 });
            }

            if (fechaInicioSeleccionado.Length > 0 && fechaFinSeleccionado.Length > 0)
            {
                fechaInicio = Convert.ToDateTime(fechaInicioSeleccionado);
                fechaFin = Convert.ToDateTime(fechaFinSeleccionado);
            }
            else
            {
                return Json(new { error = false, tipoError = 2 });
            }

            if (DateTime.Compare((DateTime)fechaInicio, (DateTime)fechaFin) > 0)
            {
                return Json(new { error = false, tipoError = 3 });
            }

            // Parsea los strings
            DividirCarreraEnfasis(ref codigoCarreraEnfasisSeleccionada, ref codigoCarrera, ref codigoEnfasis);
            DividirGrupo(ref grupoSeleccionado, ref siglaCursoGrupo, ref numeroGrupo, ref semestreGrupo, ref anno);
            // Conviertes las cadenas "null" en nul
            convertNullStringToNull(ref correoProfesorSeleccionado);
            convertNullStringToNull(ref codigoUASeleccionada);

            // Hace el llamado al procedimiento almacenado que devuelve una tabla con todos los grupos necesarios
            var grupos = db.ObtenerGruposAsociados(codigoUASeleccionada, codigoCarrera, codigoEnfasis, siglaCursoGrupo, numeroGrupo, semestreGrupo, anno, correoProfesorSeleccionado);
            List<ObtenerGruposAsociados_Result> gruposAsociadosLista = null;
            // Se convierte la tabla a lista
            try
            {
                gruposAsociadosLista = grupos.ToList();
                // Si no existen grupos asociados, pues no se pudo asignar
                if (gruposAsociadosLista.Count <= 0)
                {
                    return Json(new { error = false, tipoError = 4 });
                }
            }
            catch
            {
                return Json(new { error = false, tipoError = 4 });
            }
            // Itera por los grupos obtenidos, asignandoles el formulario
            for (int index = 0; index < gruposAsociadosLista.Count; ++index)
            {
                var grupoActual = gruposAsociadosLista[index];
                // Procedimiento que almacena en las relaciones Activa_Por, Activa_Por_Periodo
                db.AsignarFormulario(codigoFormulario, grupoActual.SiglaCurso, grupoActual.NumGrupo, grupoActual.Anno, grupoActual.Semestre, fechaInicio, fechaFin);
            }
            return Json(new { error = true });
        }

        private bool convertNullStringToNull(ref string convertedString)
        {
            if (convertedString == "null")
            {
                convertedString = null;
            }
            return false;
        }
        private bool DividirCarreraEnfasis(ref string codigoCarreraEnfasisSeleccionada, ref string codigoCarrera, ref string codigoEnfasis)
        {

            if (codigoCarreraEnfasisSeleccionada != "null")
            {
                string[] codigosSeparados = codigoCarreraEnfasisSeleccionada.Split('/');
                codigoCarrera = codigosSeparados[0];
                codigoEnfasis = codigosSeparados[1];
                return true;
            }
            return false;
        }

        private bool DividirGrupo(ref string grupoSeleccionado, ref string siglaCursoGrupo, ref Nullable<byte> numeroGrupo, ref Nullable<byte> semestreGrupo, ref Nullable<int> anno)
        {
            if (grupoSeleccionado != "null")
            {
                string[] grupoLlaves = grupoSeleccionado.Split('/');
                siglaCursoGrupo = grupoLlaves[0];
                numeroGrupo = byte.Parse(grupoLlaves[1]);
                semestreGrupo = byte.Parse(grupoLlaves[2]);
                anno = int.Parse(grupoLlaves[3]);
                return true;
            }
            return false;
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

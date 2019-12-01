using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            return View();
        }

        [HttpPost]
        public ActionResult Asignar(string filtroUA, string filtroCarreraEnfasis, string filtroCursoGrupo, string filtroProfesores)
        {
            // RIP-AF3
            // Como es la asignacion por curso, se asume que viene null los demas 
            // para que acá sean implementados

            // llamado al procedimiento almacenado 
            return View("Index");
        }

        // Modificado por: Jostin Álvarez
        // Historia a la que pertenece: RIP-AFC "Yo como administrativo quiero enviar un correo a los estudiantes para que llenen formularios cuando se los asigno"
        // Envía un correo cada estudiando que está asignado a un cuestionario pidiéndole que lo llene.
        private void EnviarCorreoSobreAsignaciónCuestionario(Formulario formulario)
        {
            List<string> involucrados = new List<string>();

            // Se obtienen todos los estudiantes a los que les tiene que llegar el correo.
            List<Estudiante> estudiantes = db.ObtenerEstudiantesAsociadosAFormulario();
            foreach (Estudiante estudiante in estudiantes)
            {
                involucrados.Add(estudiante.Correo);
            }
            Utilities.EmailNotification emailNotification = new Utilities.EmailNotification();

            string asunto = "Evaluación del formulario " + formulario.Nombre;

            string texto = "Estimado/a estudiante\n";
            texto += "La Escuela de " + formulario.fechaInicio.ToString();
            texto += "<br>Favor no responder directamente a este correo";

            string textoAlt = "<body><p>" + texto + "</p></body>";

            // Se envía el correo formado a todos los estudiantes involucrados
            _ = emailNotification.SendNotification(involucrados, asunto, texto, textoAlt);
        }
    }
}

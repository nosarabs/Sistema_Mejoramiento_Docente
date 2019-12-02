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

        private DashboardController dashboard =  new DashboardController();

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
        public ActionResult Asignar(List<UAsFiltros> unidadesAcademicas, string seleccionUA, List<CarrerasEnfasisFiltros> carrerasEnfasis, string seleccionCarrera, List<GruposFiltros> grupos, string seleccionGrupo, List<ProfesoresFiltros> profesores, string seleccionProfesor)
        {
            // RIP-AF3
            // Como es la asignacion por curso, se asume que viene null los demas 
            // para que acá sean implementados

            // llamado al procedimiento almacenado 

            //List<UAsFiltros> tempUA;

            //for(int index = 0; index < unidadesAcademicas.Count; ++index)
            //{
            //    if (unidadesAcademicas[index].Equals())
            //}

            //dashboard.ObtenerGrupos()
            return View("Index");
        }
    }
}
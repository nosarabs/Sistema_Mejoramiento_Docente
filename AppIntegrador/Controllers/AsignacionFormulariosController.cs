using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Controllers
{
    public class AsignacionFormulariosController : Controller
    {
        private DashboardController dashboard =  new DashboardController();
        // GET: Asignacion
        [HttpGet]
        public ActionResult Index()
        {
            var x = 1;
            return View();
        }

        [HttpPost]
        public ActionResult Asignar(string filtroUA, string filtroCarreraEnfasis, string filtroCursoGrupo, string filtroProfesores)
        {
            // RIP-AF3
            // Como es la asignacion por curso, se asume que viene null los demas 
            // para que acá sean implementados
            
            // llamado al procedimiento almacenado 
            return View();
        }
    }
}
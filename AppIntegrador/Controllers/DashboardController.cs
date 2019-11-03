using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;
using System.Data.Entity.Core.Objects;

namespace AppIntegrador.Controllers
{
    public class DashboardController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        public List<FiltroFormulariosModel> FiltrarFormularios(String codigoUA, String codigoCarrera, String codigoEnfasis, String siglaCurso, Byte? numeroGrupo, Byte? semestre, Int32? ano, String correoProfesor)
        {
            
        }
    }
}
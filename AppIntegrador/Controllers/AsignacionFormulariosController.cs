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
        public ActionResult Index()
        {
            return View();
        }
    }
}
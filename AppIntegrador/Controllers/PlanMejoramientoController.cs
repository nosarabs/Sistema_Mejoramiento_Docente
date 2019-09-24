using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Controllers
{
    public class PlanMejoramientoController : Controller
    {
        // GET: PlanMejoramiento
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult planMejoramiento()
        {
            ViewBag.Message = "Planes de mejoramiento.";

            return View();
        }

        public ActionResult estadisticas()
        {
            ViewBag.Message = "Estadísticas de planes de mejoramiento.";

            return View();
        }
    }
}
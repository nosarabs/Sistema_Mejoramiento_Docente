using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class DatosCSVController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();
        // GET: DatosCSV
        public ActionResult Index()
        {
            return View();
        }

        public static List<DatosCSV> ProcesarCSV(string path)
        {
            var model = new DatosCSV();
            return System.IO.File.ReadAllLines(path)
                .Skip(1)
                .Where(row => row.Length > 0)
                .Select(model.ParseRow).ToList();
        }
    }
}
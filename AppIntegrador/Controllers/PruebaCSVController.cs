using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LINQtoCSV;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class PruebaCSVController : Controller
    {
        // GET: PruebaCSV
        public ActionResult Index()
        {
            return View();
        }

        public IEnumerable<DatosCSV> LeerCSV()
        {
            CsvFileDescription inputFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };

            CsvContext cc = new CsvContext();

            IEnumerable<DatosCSV> products =
                cc.Read<DatosCSV>("datos.csv", inputFileDescription);

            // Data is now available via variable products.

            /*var productsByName =
                from p in products
                orderby p.Nombre
                select new { p.Nombre, p.SiglaCurso, p.Anno, p.Semestre, p.Grupo };*/

            return products;
        }
    }
}
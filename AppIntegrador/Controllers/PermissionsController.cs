using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Controllers
{
    public class PermissionsController : Controller
    {
        private Entities db = new Entities();
        // GET: Permissions
        public ActionResult Index()
        {
            return View(new PermissionsViewHolder());
        }

        [HttpPost]
        public ActionResult Index(PermissionsViewHolder Data)
        {
            //TO-DO: Guardar aquí la selección de perfil, permisos, usuarios, carrera 
            //y énfasis en la base de datos.
            return View(Data);
        }

        [HttpGet]
        public JsonResult CargarEnfasisDeCarrera(string value)
        {
            List<string> carreras = new List<string>();
            using (var context = new Entities())
            {
                var listaCarreras = from Carrera in db.EnfasisXCarrera(value)
                                    select Carrera;
                foreach (var codigoEnfasis in listaCarreras)
                    carreras.Add(codigoEnfasis + "," + (db.Enfasis.Find(value, codigoEnfasis)).Nombre);

            }
            return Json(new { data = carreras }, JsonRequestBehavior.AllowGet);
        }

    }
    
}
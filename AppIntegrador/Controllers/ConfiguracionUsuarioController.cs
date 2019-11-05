using AppIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppIntegrador.Controllers
{
    public class ConfiguracionUsuarioController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();
        // GET: ConfiguracionUsuario
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public JsonResult CargarPerfil()
        {
            string username = CurrentUser.Username;
            List<string> perfiles = new List<string>();
            using (var context = new DataIntegradorEntities())
            {
                var listaPerfiles = from Perfil in db.PerfilesXUsuario(username)
                                    select Perfil;
                foreach (var nombrePerfil in listaPerfiles)
                    perfiles.Add(nombrePerfil.NombrePefil);
            }
            return Json(new { data = perfiles }, JsonRequestBehavior.AllowGet);
        }
    }
}
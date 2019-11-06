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

        //Función que devuelve las unidades académicas con su respectivo código y nombre
        public String getUnidadesAcademicas()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
          
            List<UnidadesAcademicas> unidadesAcademicas = new List<UnidadesAcademicas>();

            var uas = from uda in db.UnidadAcademica
                      orderby uda.Nombre
                      select new UnidadesAcademicas { codigo = uda.Codigo, nombre = uda.Nombre };
            
            return serializer.Serialize(uas.ToList());
            //return unidadesAcademicas;
        }
    }
}
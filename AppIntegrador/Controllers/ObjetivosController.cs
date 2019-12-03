using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using AppIntegrador.Models;
using System.IO;
using AppIntegrador.Utilities;

using AppIntegrador.Controllers.PlanesDeMejoraBI;

namespace AppIntegrador.Controllers
{
    public class ObjetivosController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();
        private readonly IPerm permissionManager = new PermissionManager();

        // GET: Objetivos
        public ActionResult Index()
        {
            if (!permissionManager.IsAuthorized(Permission.VER_OBJETIVOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            var objetivo = db.Objetivo.Include(o => o.PlantillaObjetivo).Include(o => o.TipoObjetivo);
            return View(objetivo.ToList());
        }

        public ActionResult accionesObjetivo(string id, string nomb)
        {
            var idPlan = -1;
            Int32.TryParse(id, out idPlan);
            IEnumerable<AppIntegrador.Models.AccionDeMejora> acciones = db.AccionDeMejora.Where(o => o.codPlan == idPlan && o.nombreObj == nomb);
            return PartialView("~/Views/AccionDeMejora/Index.cshtml", acciones);
        }


        // GET: Objetivos/Details/5
        public ActionResult Details(int? id)
        {
            if (!permissionManager.IsAuthorized(Permission.VER_OBJETIVOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivo.Find(id);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // GET: Objetivos/Create
        public ActionResult Create(int id)
        {
            if (!permissionManager.IsAuthorized(Permission.CREAR_OBJETIVOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            ViewBag.id = id;
            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre");
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre");
            return View("_crearObjetivo");
        }

        [HttpPost]
        public ActionResult AnadirObjetivos(List<Objetivo> objetivos)
        {
            string result = PlanesDeMejoraUtil.RenderViewToString(PartialView("_TablaObjetivos", objetivos), this.ControllerContext);
            return Json(new { error = true, message = result });
        }

        [HttpPost]
        public ActionResult ObtenerSecciones(List<String> FormularioSeleccionado)
        {
            List<Formulario_tiene_seccion> parejas = new List<Formulario_tiene_seccion>();
            if(FormularioSeleccionado != null && FormularioSeleccionado.Count > 0)
            {
                foreach (var codigo in FormularioSeleccionado)
                {
                    foreach (var pareja in db.Formulario_tiene_seccion.Where(x => x.FCodigo.Equals(codigo) ) )
                    {
                        parejas.Add(pareja);
                    }
                }
            }
            string result = PlanesDeMejoraUtil.RenderViewToString(PartialView("_AnadirSecciones", parejas), this.ControllerContext);
            return Json(new { error = true, message = result });
        }

        // POST: Objetivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codPlan,nombre,descripcion,fechaInicio,fechaFin,nombTipoObj,codPlantilla")] Objetivo objetivo)
        {
            if (!permissionManager.IsAuthorized(Permission.CREAR_OBJETIVOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            bool error = false;

            if (objetivo.fechaInicio != null && objetivo.fechaFin != null) {
                if ((DateTime.Compare(objetivo.fechaInicio.Value, objetivo.fechaFin.Value) > 0))
                {
                    error = true;
                }
            }
            if( !error)
            {
                if (ModelState.IsValid)
                {
                    db.Objetivo.Add(objetivo);
                    db.SaveChanges();
                    return RedirectToAction("Index", "PlanDeMejora");
                }
            }
            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre", objetivo.codPlantilla);
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre", objetivo.nombTipoObj);
            return RedirectToAction("Index","PlanDeMejora");
        }

        // GET: Objetivos/Edit/5
        public ActionResult Edit(int? plan, string title)
        {
            if (!permissionManager.IsAuthorized(Permission.EDITAR_OBJETIVOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            if (plan == null || title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivo.Find(plan, title);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre", objetivo.codPlantilla);
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre", objetivo.nombTipoObj);
            return View(objetivo);
        }

        // POST: Objetivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codPlan,nombre,descripcion,fechaInicio,fechaFin,nombTipoObj,codPlantilla")] Objetivo objetivo)
        {
            if (!permissionManager.IsAuthorized(Permission.EDITAR_OBJETIVOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            bool error = false;

            if (objetivo.fechaInicio != null && objetivo.fechaFin != null)
            {
                if ((DateTime.Compare(objetivo.fechaInicio.Value, objetivo.fechaFin.Value) > 0))
                {
                    error = true;
                }
            }
            if ( !error)
            { 
                if (ModelState.IsValid)
                {
                    db.Entry(objetivo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "PlanDeMejora");
                }
            }
            ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre", objetivo.codPlantilla);
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre", objetivo.nombTipoObj);
            return View("Index", "PlanDeMejora");
        }

        // GET: Objetivos/Delete/5
        public ActionResult Delete(int? plan, string title)
        {
            if (!permissionManager.IsAuthorized(Permission.BORRAR_OBJETIVOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            if (plan == null || title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objetivo objetivo = db.Objetivo.Find(plan, title);
            if (objetivo == null)
            {
                return HttpNotFound();
            }
            return View(objetivo);
        }

        // POST: Objetivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int plan, string title)
        {
            if (!permissionManager.IsAuthorized(Permission.BORRAR_OBJETIVOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            Objetivo objetivo = db.Objetivo.Find(plan, title);
            db.Objetivo.Remove(objetivo);
            db.SaveChanges();
            return RedirectToAction("Index", "PlanDeMejora");
        }

        //se usa
        [HttpGet]
        public PartialViewResult listaDeObjetivos(int id)
        {
            ViewBag.IdPlan = id;
            IEnumerable<AppIntegrador.Models.Objetivo> objetivos = db.Objetivo.Where(o => o.codPlan == id);
            //return PartialView("_listarObjetivos", objetivos);
            return PartialView("_TablaObjetivosLista", objetivos);
        }

        public string TablaSeccionesAsociadas(int id, string objt)
        {

            IEnumerable<string> CodigosSecciones = db.ObtenerSeccionesAsociadasAObjetivo(id, objt);
            List<AppIntegrador.Models.Seccion> Secciones = new List<Seccion>();

            foreach (var cod in CodigosSecciones)
            {
                Secciones.Add(db.Seccion.Find(cod));
            }

            return PlanesDeMejoraUtil.RenderViewToString(PartialView("_ListaSecciones", Secciones), this.ControllerContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

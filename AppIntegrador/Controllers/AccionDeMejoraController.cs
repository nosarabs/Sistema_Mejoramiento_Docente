using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Controllers.PlanesDeMejoraBI;
using AppIntegrador.Models;
using AppIntegrador.Utilities;

namespace AppIntegrador.Controllers
{
    public class AccionDeMejoraController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();
        private readonly IPerm permissionManager = new PermissionManager();

        // GET: AccionDeMejora
        public ActionResult Index()
        {
            if (!permissionManager.IsAuthorized(Permission.VER_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            var objetivo = db.AccionDeMejora.Include(o => o.PlantillaAccionDeMejora).Include(o => o.Objetivo);
            return View("Index", objetivo.ToList());
        }

        // GET: AccionDeMejora/Details/5
        public ActionResult Details(int id)
        {
            if (!permissionManager.IsAuthorized(Permission.VER_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            return View();
        }

        // GET: AccionDeMejora/Create
        public ActionResult Create()
        {
            if (!permissionManager.IsAuthorized(Permission.CREAR_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            ViewBag.codPlan = new SelectList(db.PlanDeMejora, "codigo", "nombre");
            ViewBag.nombreObj = new SelectList(db.Objetivo, "nombre", "nombre");
            return View("_createAccionDeMejora");
        }

        [HttpPost]
        public ActionResult AnadirAccionesDeMejora(List<AccionDeMejora> accionesDeMejora)
        {
            List<AccionDeMejora> misAccionesDeMejora = accionesDeMejora;
            if (misAccionesDeMejora == null)
            {
                misAccionesDeMejora = new List<AccionDeMejora>();
            }
            return Json(new { error = true, message = PlanesDeMejoraUtil.RenderViewToString(PartialView("_TablaAccionMejora", misAccionesDeMejora), this.ControllerContext) });
        }

        [HttpPost]
        public ActionResult ObtenerPreguntas(List<String> SeccionSeleccionado)
        {
            List<Seccion_tiene_pregunta> parejas = new List<Seccion_tiene_pregunta>();
            if (SeccionSeleccionado != null && SeccionSeleccionado.Count > 0)
            {
                foreach (var codigo in SeccionSeleccionado)
                {
                    foreach (var pareja in db.Seccion_tiene_pregunta.Where(x => x.SCodigo.Equals(codigo)))
                    {
                        parejas.Add(pareja);
                    }
                }
            }
            string result = PlanesDeMejoraUtil.RenderViewToString(PartialView("_AnadirPreguntas", parejas), this.ControllerContext);
            return Json(new { error = true, message = result });
        }

        // POST: AccionDeMejora/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codPlan,nombreObj,descripcion,fechaInicio,fechaFin,codPlantilla")] AccionDeMejora accionDeMejora)
        {
            if (!permissionManager.IsAuthorized(Permission.CREAR_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            bool error = false;

            if (accionDeMejora.fechaInicio != null && accionDeMejora.fechaFin != null)
            {
                if ((DateTime.Compare(accionDeMejora.fechaInicio.Value, accionDeMejora.fechaFin.Value) > 0))
                {
                    error = true;
                }
            }
            if (!error)
            {
                if (ModelState.IsValid)
                {
                    db.AccionDeMejora.Add(accionDeMejora);
                    db.SaveChanges();
                    return RedirectToAction("Index", "PlanDeMejora");
                }
            }
            ViewBag.codPlan = new SelectList(db.PlanDeMejora, "codigo", "nombre");
            ViewBag.nombreObj = new SelectList(db.Objetivo, "nombre", "nombre");
            return RedirectToAction("Index", "PlanDeMejora");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public EmptyResult CrearAccionDeMejora([Bind(Include = "codPlan,nombreObj,descripcion,fechaInicio,fechaFin,codPlantilla")] AccionDeMejora accionDeMejora)
        {
            bool error = false;

            if (accionDeMejora.fechaInicio != null && accionDeMejora.fechaFin != null)
            {
                if ((DateTime.Compare(accionDeMejora.fechaInicio.Value, accionDeMejora.fechaFin.Value) > 0))
                {
                    error = true;
                }
            }
            if (!error)
            {
                if (ModelState.IsValid)
                {
                    db.AccionDeMejora.Add(accionDeMejora);
                    db.SaveChanges(); IEnumerable<AppIntegrador.Models.AccionDeMejora> acciones = db.AccionDeMejora.Where(o => o.codPlan == accionDeMejora.codPlan && o.nombreObj == accionDeMejora.nombreObj);

                    return new EmptyResult();
                }
            }
            return new EmptyResult();
        }

        // GET: AccionDeMejora/Edit/5
        // Corresponde a MOS 1.3 (2)
        public ActionResult Edit(int plan, string obj, string descripcion)
        {
            if (!permissionManager.IsAuthorized(Permission.EDITAR_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            if (plan == null || obj == null || descripcion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccionDeMejora accionDeMejora = db.AccionDeMejora.Find(plan, obj, descripcion);
            if (accionDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(accionDeMejora);
        }

        // POST: AccionDeMejora/Edit/5
        // Corresponde a MOS 1.3 (2)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codPlan,nombreObj,descripcion,fechaInicio,fechaFin,,codPlantilla")] AccionDeMejora accionDeMejora)
        {
            if (!permissionManager.IsAuthorized(Permission.EDITAR_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            bool error = false;

            if (accionDeMejora.fechaInicio != null && accionDeMejora.fechaFin != null)
            {
                if ((DateTime.Compare(accionDeMejora.fechaInicio.Value, accionDeMejora.fechaFin.Value) > 0))
                {
                    error = true;
                }
            }
            if (!error)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(accionDeMejora).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "PlanDeMejora");
                }
            }
            return RedirectToAction("Index", "PlanDeMejora"); 
        }

        // GET: AccionDeMejora/Delete/5
        // Corresponde a MOS 1.3 (2)
        public ActionResult Delete(int? plan, string nombObj, string descripcion)
        {
            if (!permissionManager.IsAuthorized(Permission.BORRAR_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            if (plan == null || nombObj == null || descripcion == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccionDeMejora accion = db.AccionDeMejora.Find(plan, nombObj, descripcion);
            if (accion == null)
            {
                return HttpNotFound();
            }
            return View(accion);
        }

        // POST: AccionDeMejora/Delete/5
        // Corresponde a MOS 1.3 (2)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? plan, string nombObj, string descripcion)
        {
            if (!permissionManager.IsAuthorized(Permission.BORRAR_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            AccionDeMejora accion = db.AccionDeMejora.Find(plan, nombObj, descripcion);
            db.AccionDeMejora.Remove(accion);
            db.SaveChanges();
            return RedirectToAction("Index", "PlanDeMejora");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public PartialViewResult divAccion(int plan, string nombObj, string fechaInicioObjetivo, string fechaFinObjetivo) {
            ViewBag.IdPlan = plan;
            ViewBag.nomObj = nombObj;
            ViewBag.fechaInicioObjetivo = fechaInicioObjetivo;
            ViewBag.fechaFinObjetivo = fechaFinObjetivo;
            Session["idPlan"] = plan;
            Session["nombreObj"] = nombObj;
            Session["fechaInicioObjetivo"] = fechaInicioObjetivo;
            Session["fechaFinObjetivo"] = fechaFinObjetivo;

            AppIntegrador.Models.Metadata.AccionDeMejoraMetadata accion = new AppIntegrador.Models.Metadata.AccionDeMejoraMetadata();
            return PartialView("_crearAccionDeMejora", accion);
        }

        public ActionResult AccionesDeObjetivo(int plan, string nombObj, bool edit = true)
        {
            if (!permissionManager.IsAuthorized(Permission.VER_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            ViewBag.idPlan = plan;
            ViewBag.nombreObj = nombObj;

            IEnumerable<AppIntegrador.Models.AccionDeMejora> acciones = db.AccionDeMejora.Where(o => o.codPlan == plan && o.nombreObj == nombObj);
            if (edit == false)
            {
                return PartialView("_listarAcciones", acciones);
            }
            return PartialView("_accionesDeUnObjetivo", acciones);
        }

        public string TablaPreguntasAsociadas(int id, string objt, string des)
        {

            IEnumerable<string> CodigosPreguntas = db.ObtenerPreguntasDeAccionDeMejora(id, objt, des);
            List<AppIntegrador.Models.Pregunta> preguntas = new List<Pregunta>();

            foreach (var cod in CodigosPreguntas)
            {
                preguntas.Add(db.Pregunta.Find(cod));
            }

            return PlanesDeMejoraUtil.RenderViewToString(PartialView("_ListaPreguntas", preguntas), this.ControllerContext);
        }
    }
}

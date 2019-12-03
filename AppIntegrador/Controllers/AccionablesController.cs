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
    public class AccionablesController : Controller
    {
        private DataIntegradorEntities db;
        private readonly IPerm permissionManager;

        public AccionablesController()
        {
            db = new DataIntegradorEntities();
            permissionManager = new PermissionManager();
        }

        public AccionablesController(DataIntegradorEntities db)
        {
            this.db = db;
            permissionManager = new PermissionManager();
        }

        // GET: Accionables
        public ActionResult Index()
        {
            if (!permissionManager.IsAuthorized(Permission.VER_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            var accionable = db.Accionable.Include(a => a.AccionDeMejora);
            return View("Index", accionable.ToList());
        }

        // Hay que refactorizar este metodo para que no utilice Session
        // GET: Accionables/Create
        public ActionResult Create(int codPlan, string nombObj, string descripAcMej, string fechaInicioAccionDeMejora, string fechaFinAccionDeMejora, int peso, bool unitTesting = false)
        {
            if (!permissionManager.IsAuthorized(Permission.CREAR_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            ViewBag.IdPlan = codPlan;
            ViewBag.nomObj = nombObj;
            ViewBag.descripAcMej = descripAcMej;
            ViewBag.progreso = 0;
            ViewBag.fechaInicioAccionDeMejora = fechaInicioAccionDeMejora;
            ViewBag.fechaFinAccionDeMejora = fechaFinAccionDeMejora;
            ViewBag.peso = peso;
            if (!unitTesting)
            {
                Session["codPlan"] = codPlan;
                Session["nombreObj"] = nombObj;
                Session["descripAcMej"] = descripAcMej;
                Session["progreso"] = 0;
                Session["fechaInicioAccionDeMejora"] = fechaInicioAccionDeMejora;
                Session["fechaFinAccionDeMejora"] = fechaFinAccionDeMejora;
                Session["peso"] = peso;
            }

            Models.Metadata.AccionableMetadata accionable = new Models.Metadata.AccionableMetadata();
            return PartialView("_Create", accionable);
        }
        [HttpPost]
        public ActionResult AnadirAccionables(List<Accionable> accionables)
        {
            List<Accionable> misAccionables = accionables;
            if (misAccionables == null)
            {
                misAccionables = new List<Accionable>();
            }
            return Json(new { error = true, message = PlanesDeMejoraUtil.RenderViewToString(PartialView("_TablaAccionables", misAccionables), this.ControllerContext) });
        }

        // POST: Accionables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public EmptyResult Create([Bind(Include = "codPlan,nombreObj,descripAcMej,descripcion,fechaInicio,fechaFin,progreso,peso")] Accionable accionable)
        {
            bool error = false;

            if (accionable.fechaInicio != null && accionable.fechaFin != null)
            {
                if ((DateTime.Compare(accionable.fechaInicio.Value, accionable.fechaFin.Value) > 0))
                {
                    error = true;
                }
            }
            if (!error && ModelState.IsValid)
            {
                db.Accionable.Add(accionable);
                db.SaveChanges();
                return new EmptyResult();
            }

            ViewBag.codPlan = new SelectList(db.AccionDeMejora, "codPlan", "nombreObj", accionable.codPlan);
            return new EmptyResult();

        }
        //Requiere refactorización para eliminar el .Where de aquí
        public ActionResult TablaAccionables(int codPlan, string nombObj, string descripAcMej, bool edit = true)
        {
            if (!permissionManager.IsAuthorized(Permission.VER_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            ViewBag.IdPlan = codPlan;
            ViewBag.nomObj = nombObj;
            ViewBag.descripAcMej = descripAcMej;

            IEnumerable<AppIntegrador.Models.Accionable> accionables = db.Accionable.Where(o => o.codPlan == codPlan && o.nombreObj == nombObj && o.descripAcMej == descripAcMej);
            if( edit == false)
            {
                return PartialView("_listarAccionables", accionables);
            }
            return PartialView("_Tabla", accionables);
        }

        // POST: Accionables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codPlan,nombreObj,descripAcMej,descripcion,fechaInicio,fechaFin,progreso")] Accionable accionable)
        {
            if (!permissionManager.IsAuthorized(Permission.EDITAR_ACCIONES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
            if (ModelState.IsValid)
            {
                db.Entry(accionable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.codPlan = new SelectList(db.AccionDeMejora, "codPlan", "nombreObj", accionable.codPlan);
            return View(accionable);
        }

        // POST: Accionables/Delete/5
        [HttpPost, ActionName("DeleteAccionable")]
        [ValidateAntiForgeryToken]
        public PartialViewResult DeleteAccionable(int codPlan, string nombObj, string descripAcMej, string descripAccionable)
        {
            Accionable accionable = db.Accionable.Find(codPlan, nombObj, descripAcMej, descripAccionable);
            db.Accionable.Remove(accionable);
            db.SaveChanges();
            IEnumerable<AppIntegrador.Models.Accionable> listaAccionables = db.Accionable.Where(o => o.codPlan == codPlan && o.nombreObj == nombObj && o.descripAcMej == descripAcMej);
            return PartialView("_Tabla", listaAccionables);
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

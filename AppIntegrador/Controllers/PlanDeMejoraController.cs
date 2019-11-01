using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class PlanDeMejoraController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: PlanDeMejora
        public ActionResult Index()
        {
            HttpContext context = System.Web.HttpContext.Current;
            ObjectParameter count = new ObjectParameter("count", 999);
            ViewBag.cantidad = count.Value;
            ViewBag.nombre = context.User.Identity.Name;
            return View(db.PlanDeMejora.ToList());
        }

        //Para pruebas
        //public ActionResult Index(String nombre)
        //{
        //    ObjectParameter count = new ObjectParameter("count", 999);
        //    ViewBag.cantidad = count.Value;
        //    ViewBag.nombre = nombre;
        //    return View(db.PlanDeMejora.ToList());
        //}

        public ActionResult Index2(int idPlanDeMejora)
        {
            return PartialView("~/Views/PlanDeMejora/Index.cshtml", new ViewDataDictionary { { "idPlan", idPlanDeMejora } });
        }

        /*
            Modificado por: Johan Córdoba
            Historia a la que pertenece: MOS-1.2 "agregar, modificar, borrar y consultar los objetivos de un plan de mejora"
            Para no tener que crear la vista parcial dento de la carpeta de planes de mejora cambié el controlador.
            Ahora este redirige a la vista de objetivos y la que está en planes de mejora "_objetivosPlan" ya no es necesaria
        */
        public ActionResult objetivosPlan(string id)
        {
            var idPlan = -1;
            Int32.TryParse(id, out idPlan);
            ViewBag.idPlan = idPlan;
            IEnumerable<AppIntegrador.Models.Objetivo> objetivosDePlan = db.Objetivo.Where(o => o.codPlan == idPlan);
            return PartialView("~/Views/Objetivos/Index.cshtml", objetivosDePlan);
        }

        public ActionResult accionesObjetivo(string id, string nomb)
        {
            var idPlan = -1;
            if (Int32.TryParse(id, out idPlan))
            {
                Session["id"] = idPlan;
                Session["name"] = nomb;
                IEnumerable<AppIntegrador.Models.AccionDeMejora> acciones = db.AccionDeMejora.Where(o => o.codPlan == idPlan && o.nombreObj == nomb);
                return PartialView("~/Views/AccionDeMejora/Index.cshtml", acciones);
            }
            return null;
        }


        // GET: PlanDeMejora/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            if (planDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(planDeMejora);
        }

        // GET: PlanDeMejora/Create
        public ActionResult Create()
        {
            AppIntegrador.Models.Metadata.PlanDeMejoraMetadata plan = new AppIntegrador.Models.Metadata.PlanDeMejoraMetadata();
            return View("_crearPlanDeMejora", plan);
        }


        //Añadido por: Johan Córdoba
        //Historia a la que pertenece: MOS-25 "como usuario quiero tener una interfaz que muestre de forma clara las jerarquías entre las distintas partes del subsistema de creación de planes de mejora"
        //Retorna la nueva vista de planes de mejora adaptada a las solicitudes del PO.
        public ActionResult CreateNuevo()
        {
            AppIntegrador.Models.Metadata.PlanDeMejoraMetadata plan = new AppIntegrador.Models.Metadata.PlanDeMejoraMetadata();
            return View("CrearPlanDeMejora", plan);
        }

        // POST: PlanDeMejora/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Create([Bind(Include = "codigo,nombre,fechaInicio,fechaFin")] PlanDeMejora planDeMejora)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.PlanDeMejora.Add(planDeMejora);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var errors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in errors.ValidationErrors)
                        {
                            // get the error message 
                            string errorMessage = validationError.ErrorMessage;
                        }
                    }
                }
            }

            //return View(planDeMejora);
        }

        // GET: PlanDeMejora/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            if (planDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(planDeMejora);
        }

        // POST: PlanDeMejora/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo,nombre,fechaInicio,fechaFin")] PlanDeMejora planDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planDeMejora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        public ActionResult EditarPlanDeMejora(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            if (planDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(planDeMejora);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPlanDeMejora([Bind(Include = "codigo,nombre,fechaInicio,fechaFin")] PlanDeMejora planDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planDeMejora).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPlanDeMejora2([Bind(Include = "codigo,nombre,fechaInicio,fechaFin")] PlanDeMejora planDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planDeMejora).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("EditarPlanDeMejora2", planDeMejora);
        }

        // GET: PlanDeMejora/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            if (planDeMejora == null)
            {
                return HttpNotFound();
            }
            return View(planDeMejora);
        }

        // POST: PlanDeMejora/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            db.PlanDeMejora.Remove(planDeMejora);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //////////////////////////////////////////////BI///////////////////////////////////////////////////

        // Method that creates the plan and sets the next "codigo" automatically
        public ActionResult CrearPlanDeMejora(string nombre, DateTime fechaInicio, DateTime fechaFin)
        {
            int id = -1;
            var planTemp = new PlanDeMejora();
            if (nombre != null && fechaInicio != null && fechaFin != null)
            {
                if( DateTime.Compare(fechaInicio, fechaFin) < 0)
                {
                    
                    var plans = this.db.PlanDeMejora.ToList();
                    var codigoTemporal = plans.Count == 0 ? -1 : plans.Last().codigo;
                    planTemp.codigo = codigoTemporal + 1;
                    id = planTemp.codigo;
                    planTemp.nombre = nombre;
                    planTemp.fechaInicio = fechaInicio;
                    planTemp.fechaFin = fechaFin;
                    this.Create(planTemp);
                }
                else
                {
                    //return RedirectToAction("Index", ViewBag.Fecha = -1);
                }
            }
            //return RedirectToAction("Index");
            return View("EditarPlanDeMejora2", planTemp);
        }

        // Method that edits one "PlanDeMejora"
        public ActionResult ModificarPlan(int codigo, string nombre, DateTime fechaInicio, DateTime fechaFin)
        {
            if (codigo != null && nombre != null && fechaInicio != null && fechaFin != null)
            {
                var planTemp = new PlanDeMejora();
                planTemp.codigo = codigo;
                planTemp.nombre = nombre;
                planTemp.fechaInicio = fechaInicio;
                planTemp.fechaFin = fechaFin;
                this.Edit(planTemp);
            }
            return RedirectToAction("Index");
        }

        // Method that deletes one "PlanDeMejora"
        public ActionResult BorrarPlan(int codigoPlan)
        {
            this.DeleteConfirmed(codigoPlan);
            return RedirectToAction("Index");
        }
    }
}

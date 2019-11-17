﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class PlanDeMejoraController : Controller
    {
        private DataIntegradorEntities db;

        public PlanDeMejoraController()
        {
            db = new DataIntegradorEntities();
        }

        public PlanDeMejoraController(DataIntegradorEntities db)
        {
            this.db = db;
        }


        // GET: PlanDeMejora
        [HttpGet]
        public ActionResult Index()
        {
            HttpContext context = System.Web.HttpContext.Current;
            ObjectParameter count = new ObjectParameter("count", 999);
            ViewBag.cantidad = count.Value;
            ViewBag.nombre = context.User.Identity.Name;
            return View("Index", db.PlanDeMejora.ToList());
        }

        /*
            Permite realizar pruebas sobre el método index
        */
        public ActionResult Index(String nombre)
        {
            ObjectParameter count = new ObjectParameter("count", 999);
            ViewBag.cantidad = count.Value;
            ViewBag.nombre = nombre;
            return View(db.PlanDeMejora.ToList());
        }

        /*
            Modificado por: Johan Córdoba
            Historia a la que pertenece: MOS-1.2 "agregar, modificar, borrar y consultar los objetivos de un plan de mejora"
            Para no tener que crear la vista parcial dento de la carpeta de planes de mejora cambié el controlador.
            Ahora este redirige a la vista de objetivos y la que está en planes de mejora "_objetivosPlan" ya no es necesaria
        */
        public ActionResult Create()
        {
            AppIntegrador.Models.Metadata.PlanDeMejoraMetadata plan = new AppIntegrador.Models.Metadata.PlanDeMejoraMetadata();
            return View("_crearPlanDeMejora", plan);
        }


        //Añadido por: Johan Córdoba
        //Historia a la que pertenece: MOS-25 "como usuario quiero tener una interfaz que muestre de forma clara las jerarquías entre las distintas partes del subsistema de creación de planes de mejora"
        //Retorna la nueva vista de creación de planes de mejora adaptada a las solicitudes del PO.
        public ActionResult CreateNuevo()
        {
            AppIntegrador.Models.Metadata.PlanDeMejoraMetadata plan = new AppIntegrador.Models.Metadata.PlanDeMejoraMetadata();
            ViewBag.profesores = new SelectList(db.Profesor, "correo", "correo");
            return View("CrearPlanDeMejora", plan);
        }

        //Modificado por: Johan Córdoba
        //Historia a la que pertenece: MOS-25 "como usuario quiero tener una interfaz que muestre de forma clara las jerarquías entre las distintas partes del subsistema de creación de planes de mejora"
        //crea un plan de mejora este método solo es llamado internamente por lo que retorna un void
        //pero esto puede cambiarse para que retorne true o false y validad la creación
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

        //Agregado por: Johan Córdoba
        //Historia a la que pertenece: MOS-25 "como usuario quiero tener una interfaz que muestre de forma clara las jerarquías entre las distintas partes del subsistema de creación de planes de mejora"
        //permite editar los datos de un plan de mejora 
        //retorna la vista de editar para que puedan ser añadidos los objetivos, acciones y acionables al mismo
        public ActionResult EditarPlanDeMejora(int id)
        {
            ViewBag.IdPlan = id;
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            ViewBag.Editar = true;
            ViewBag.profesores = new SelectList(db.Profesor, "correo", "correo");
            return View("EditarPlanDeMejora2", planDeMejora);
        }

        //Modificado por: Johan Córdoba
        //Historia a la que pertenece: MOS-25 "como usuario quiero tener una interfaz que muestre de forma clara las jerarquías entre las distintas partes del subsistema de creación de planes de mejora"
        //permite editar los datos de un plan de mejora 
        //retorna la misma vista de editar para que puedan ser añadidos los objetivos, acciones y acionables al mismo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPlanDeMejora2([Bind(Include = "codigo,nombre,fechaInicio,fechaFin")] PlanDeMejora planDeMejora)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planDeMejora).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.profesores = new SelectList(db.Profesor, "correo", "correo");
            ViewBag.IdPlan = planDeMejora.codigo;
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

        //Modificado por: Christian Asch
        //Historia a la que pertenece: MOS-1.4.2 "Como usuario administrativo quiero que se notifique a los involucrados sobre el inicio de un plan, objetivo o acción de mejora para que los involucrados puedan estar informados"
        //Envía un correo cada profesor que está asignado al plan avisándole que ha sido asignado.
        private void EnviarCorreoSobreCreacionPlan(PlanDeMejora plan)
        {
            List<string> involucrados = new List<string>();
            foreach(Profesor profesor in plan.Profesor)
            {
                involucrados.Add(profesor.Correo);
            }
            Utilities.EmailNotification emailNotification = new Utilities.EmailNotification();

            string asunto = "Creación de un nuevo plan de mejora";

            string texto = "Usted ha sido involucrado en el plan de mejora llamado: " + plan.nombre + "<br>Con código: " + plan.codigo;
            texto += "<br>Este plan iniciará el " + plan.fechaInicio.ToString();
            texto += "<br>Favor no responder directamente a este correo";
            string textoAlt = "<body><p>" + texto + "</p></body>";


            _ = emailNotification.SendNotification(involucrados, asunto, texto, textoAlt);
        }

        //Modificado por: Johan Córdoba
        //Historia a la que pertenece: MOS-25 "como usuario quiero tener una interfaz que muestre de forma clara las jerarquías entre las distintas partes del subsistema de creación de planes de mejora"
        //crea un plan de mejora con un id que se determina automáticamente
        //retorna el view que permite editar un plan de mejora completo para añadir objetivos, acciones y accionables
        public ActionResult CrearPlanDeMejora(string nombre, DateTime fechaInicio, DateTime fechaFin, List<String> Profesor)
        {
            int id = -1;
            var planTemp = new PlanDeMejora();
            if (nombre != null && fechaInicio != null && fechaFin != null)
            {
                if (DateTime.Compare(fechaInicio, fechaFin) < 0)
                {

                    var plans = this.db.PlanDeMejora.ToList();
                    var codigoTemporal = plans.Count == 0 ? -1 : plans.Last().codigo;
                    planTemp.codigo = codigoTemporal + 1;
                    id = planTemp.codigo;
                    planTemp.nombre = nombre;
                    planTemp.fechaInicio = fechaInicio;
                    planTemp.fechaFin = fechaFin;

                    if(Profesor != null)
                    {
                        foreach(String correo in Profesor)
                        {
                            var prof = db.Profesor.Find(correo);
                            planTemp.Profesor.Add(prof);
                        }
                        EnviarCorreoSobreCreacionPlan(planTemp);
                    }
                    this.Create(planTemp);

                    ViewBag.IdPlan = id;
                    ViewBag.editar = false;
                    ViewBag.profesores = new SelectList(db.Profesor, "correo", "correo");
                    return View("EditarPlanDeMejora2", planTemp);
                }
            }
            //return RedirectToAction("Index");
            ViewBag.IdPlan = id;
            ViewBag.profesores = new SelectList(db.Profesor, "correo", "correo");
            return View("EditarPlanDeMejora2", planTemp);
        }

        // Method that deletes one "PlanDeMejora"
        public ActionResult BorrarPlan(int codigoPlan)
        {
            this.DeleteConfirmed(codigoPlan);
            return RedirectToAction("Index");
        }

        //Añadido por: Johan Córdoba
        //Historia a la que pertenece: MOS-25 "como usuario quiero tener una interfaz que muestre de forma clara las jerarquías entre las distintas partes del subsistema de creación de planes de mejora"
        //Retorna un partial view que permite crear un objetivo en el plan con el id recibido
        [ChildActionOnly]
        public PartialViewResult divObjetivo(int id)
        {
            AppIntegrador.Models.Metadata.ObjetivoMetadata objetivo = new AppIntegrador.Models.Metadata.ObjetivoMetadata();
            ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre");
            ViewBag.IdPlan = id;
            return PartialView("_crearObjetivo", objetivo);
        }


        //Modificado por: Johan Córdoba
        //Historia a la que pertenece: MOS-25 "como usuario quiero tener una interfaz que muestre de forma clara las jerarquías entre las distintas partes del subsistema de creación de planes de mejora"
        //Si no hay fechas vacías o que no tengan sentido crea un objetivo
        //De momento retorna un EmtyResult pero esto se puede modificar para las validaciones
        [HttpPost]
        [ValidateAntiForgeryToken]
        public EmptyResult CrearObjetivo([Bind(Include = "codPlan,nombre,descripcion,fechaInicio,fechaFin,nombTipoObj,codPlantilla")] Objetivo objetivo)
        {
            bool error = false;

            if (objetivo.fechaInicio != null && objetivo.fechaFin != null)
            {
                if ((DateTime.Compare(objetivo.fechaInicio.Value, objetivo.fechaFin.Value) > 0))
                {
                    error = true;
                }
            }
            if (!error)
            {
                if (ModelState.IsValid)
                {
                    db.Objetivo.Add(objetivo);
                    db.SaveChanges();
                    IEnumerable<AppIntegrador.Models.Objetivo> objetivos = db.Objetivo.Where(o => o.codPlan == objetivo.codPlan);
                    //return PartialView("_objetivosDelPlan", objetivos);
                    ViewBag.NuevoObj = 1;
                    return new EmptyResult();
                }
            }
            //ViewBag.codPlantilla = new SelectList(db.PlantillaObjetivo, "codigo", "nombre", objetivo.codPlantilla);
            //ViewBag.nombTipoObj = new SelectList(db.TipoObjetivo, "nombre", "nombre", objetivo.nombTipoObj);
            //return RedirectToAction("Index", "PlanDeMejora");
            return new EmptyResult();
        }




        //Añadido por: Johan Córdoba
        //Historia a la que pertenece: MOS-25 "como usuario quiero tener una interfaz que muestre de forma clara las jerarquías entre las distintas partes del subsistema de creación de planes de mejora"
        //Permite actualizar la vista parcial de objetivos al crear uno nuevo
        public ActionResult RefrescarObjetivos(int Id)
        {
            ViewBag.IdPlan = Id;
            IEnumerable<AppIntegrador.Models.Objetivo> objetivosDePlan = db.Objetivo.Where(o => o.codPlan == Id);
            return PartialView("_objetivosDelPlan", objetivosDePlan);
        }

        //Añadido por: Johan Córdoba
        //Historia a la que pertenece: MOS-27 "tener una página que liste los planes de mejora"
        //Retorna la vista DetallesPlanDeMejora que muestra todos los detalles de un plan incluyendo sus objetivos acciones y accionables.
        [HttpGet]
        public ActionResult Detalles(int id)
        {
            ViewBag.IdPlan = id;
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            return View("DetallesPlanDeMejora", planDeMejora);
        }
    }
}

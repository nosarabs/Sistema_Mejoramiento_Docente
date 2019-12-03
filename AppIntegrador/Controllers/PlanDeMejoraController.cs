using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AppIntegrador.Controllers.PlanesDeMejoraBI;
using AppIntegrador.Models;
using AppIntegrador.Utilities;

namespace AppIntegrador.Controllers
{
    public class PlanDeMejoraController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();
        private readonly IPerm permissionManager;

        public PlanDeMejoraController()
        {
            this.db = new DataIntegradorEntities();
            permissionManager = new PermissionManager();
        }

        public PlanDeMejoraController(DataIntegradorEntities mdb)
        {
            this.db = mdb;
            permissionManager = new PermissionManager();
        }

        // GET: PlanDeMejora
        [HttpGet]
        public ActionResult Index(List<PlanDeMejora> planes = null)
        {
            if (!permissionManager.IsAuthorized(Permission.VER_PLANES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            if (planes == null || planes.Count == 0)
            {
                planes = db.PlanDeMejora.ToList();
            }

            HttpContext context = System.Web.HttpContext.Current;
            ObjectParameter count = new ObjectParameter("count", 999);
            ViewBag.cantidad = count.Value;
            ViewBag.nombre = context.User.Identity.Name;

            return View("Index", planes);
        }

        public ActionResult Buscar(String nombrePlan)
        {
            if (!permissionManager.IsAuthorized(Permission.VER_PLANES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            var planes = db.PlanDeMejora.Where(x => x.nombre.Contains(nombrePlan)).ToList();
            return Index(planes);
        }

        /*
            Permite realizar pruebas sobre el método index
        */
        public ActionResult Index(String nombre)
        {
            if (!permissionManager.IsAuthorized(Permission.VER_PLANES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            ObjectParameter count = new ObjectParameter("count", 999);
            ViewBag.cantidad = count.Value;
            ViewBag.nombre = nombre;
            return View("Index", db.PlanDeMejora.ToList());
        }

        /*
            Modificado por: Johan Córdoba, Christian Asch
            Historia a la que pertenece: MOS-1.2 "agregar, modificar, borrar y consultar los objetivos de un plan de mejora"
            Para no tener que crear la vista parcial dento de la carpeta de planes de mejora cambié el controlador.
            Ahora este redirige a la vista de objetivos y la que está en planes de mejora "_objetivosPlan" ya no es necesaria
        */
        public ActionResult Crear(PlanDeMejora plan = null)
        {
            if (!permissionManager.IsAuthorized(Permission.CREAR_PLANES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            if (plan == null)
            {
                plan = new PlanDeMejora();
            }
            List<String> ProfesoresNombreLista = new List<String>();
            List<String> tiposDeObjetivo = new List<String>();
            foreach(var tipo in db.TipoObjetivo)
            {
                tiposDeObjetivo.Add(tipo.nombre);
            }

            ViewBag.ProfesoresLista = db.Profesor.ToList();
            String name = "NombreCompleto";
            ObjectParameter name_op;
            foreach (var profe in ViewBag.ProfesoresLista)
            {
                name_op = new ObjectParameter(name, "");
                db.GetTeacherName(profe.Correo, name_op);
                ProfesoresNombreLista.Add(name_op.Value.ToString());
            }
            ViewBag.ProfesoresNombreLista = ProfesoresNombreLista;
            ViewBag.FormulariosLista = db.Formulario.ToList();
            ViewBag.tiposDeObjetivo = tiposDeObjetivo.Select(x =>
                                  new SelectListItem()
                                  {
                                      Text = x
                                  });
            ;
            return View("Crear", plan);
        }

        [HttpPost]
        public ActionResult Crear([Bind(Include = "nombre,fechaInicio,fechaFin")]PlanDeMejora plan, List<String> ProfeSeleccionado = null, List<String> FormularioSeleccionado = null, List<Objetivo> Objetivo = null, Dictionary<String, String> SeccionConObjetivo = null, Dictionary<String, String> PreguntaConAccion = null)
        {
            if (!permissionManager.IsAuthorized(Permission.CREAR_PLANES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            // Objeto de ayuda business intelligence planes de mejora
            PlanDeMejoraBI planesHelper = new PlanDeMejoraBI();

            // Asignacion del codigo al nuevo plan de mejora
            planesHelper.setCodigoAPlanDeMejora(this.db, plan);

            //Agregando los objetivos al plan
            plan.Objetivo = Objetivo;

            //Agregando las secciones a los objetivos
            planesHelper.insertSeccionesEnObjetivos(plan.Objetivo, SeccionConObjetivo, db);

            //Agregando las preguntas a las acciones
            planesHelper.insertPreguntasEnAcciones(plan.Objetivo, PreguntaConAccion, db);

            //Agrgando los formularios al plan de mejora
            planesHelper.insertFormularios(plan, FormularioSeleccionado, db);

            //Agregando los profesores seleccionados al plan de mejora
            planesHelper.insertProfesores(plan, ProfeSeleccionado, db);

            // Almacenamiento del plan por medio de un procedimiento almacenado
            planesHelper.savePlan(plan);
            db.SaveChanges();

            PlanDeMejora planTemporal = db.PlanDeMejora.Find(plan.codigo);
            if (planTemporal != null && ProfeSeleccionado != null) 
            {
                if (ProfeSeleccionado.Count > 0) 
                {
                    this.EnviarCorreoSobreCreacionPlan(planTemporal, ProfeSeleccionado);
                }
            }

            return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public ActionResult AnadirProfes(List<String> ProfeSeleccionado)
        {
            if (!permissionManager.IsAuthorized(Permission.VER_PLANES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            List<Profesor> profesores = new List<Profesor>();
            List<String> ProfesoresNombreLista = new List<String>();
            ObjectParameter name_op;
            if (ProfeSeleccionado != null)
            {
                foreach (var correo in ProfeSeleccionado)
                {
                    var profesor = db.Profesor.Find(correo);
                    profesores.Add(profesor);

                    name_op = new ObjectParameter("NombreCompleto", "");
                    db.GetTeacherName(correo, name_op);
                    ProfesoresNombreLista.Add(name_op.Value.ToString());
                }
            }
            ViewBag.ProfesoresLista = profesores;
            ViewBag.ProfesoresNombreLista = ProfesoresNombreLista;
            return PartialView("_TablaProfesores");
        }

        [HttpPost]
        public ActionResult AnadirFormularios(List<String> FormularioSeleccionado)
        {
            if (!permissionManager.IsAuthorized(Permission.VER_PLANES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            List<Formulario> formularios = new List<Formulario>();
            if (FormularioSeleccionado != null)
            {
                foreach (var codigo in FormularioSeleccionado)
                {
                    var formulario = db.Formulario.Find(codigo);
                    formularios.Add(formulario);
                }
            }
            return PartialView("_TablaFormularios", formularios);
        }

        //Agregado por: Johan Córdoba
        //Historia a la que pertenece: MOS-25 "como usuario quiero tener una interfaz que muestre de forma clara las jerarquías entre las distintas partes del subsistema de creación de planes de mejora"
        //permite editar los datos de un plan de mejora 
        //retorna la vista de editar para que puedan ser añadidos los objetivos, acciones y acionables al mismo
        public ActionResult EditarPlanDeMejora(int id)
        {
            if (!permissionManager.IsAuthorized(Permission.EDITAR_PLANES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            ViewBag.IdPlan = id;
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(id);
            ViewBag.Editar = true;
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
            if (!permissionManager.IsAuthorized(Permission.EDITAR_PLANES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            if (ModelState.IsValid)
            {
                db.Entry(planDeMejora).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.profesores = new SelectList(db.Profesor, "correo", "correo");
            ViewBag.IdPlan = planDeMejora.codigo;
            return View("EditarPlanDeMejora2", planDeMejora);
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
        private void EnviarCorreoSobreCreacionPlan(PlanDeMejora plan, List<string> correos)
        {
            Utilities.EmailNotification emailNotification = new Utilities.EmailNotification();

            string asunto = "Creación de un nuevo plan de mejora";

            string texto = "Usted ha sido involucrado en el plan de mejora llamado: " + plan.nombre + "<br>Con código: " + plan.codigo;
            texto += "<br>Este plan iniciará el " + plan.fechaInicio.ToString();
            texto += "<br>Favor no responder directamente a este correo";
            string textoAlt = "<body><p>" + texto + "</p></body>";


            _ = emailNotification.SendNotification(correos, asunto, texto, textoAlt);
        }

        // Method that deletes one "PlanDeMejora"
        public ActionResult BorrarPlan(int codigoPlan)
        {
            if (!permissionManager.IsAuthorized(Permission.BORRAR_PLANES_MEJORA))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página.";
                return RedirectToAction("../Home/Index");
            }
            PlanDeMejora planDeMejora = db.PlanDeMejora.Find(codigoPlan);
            //db.PlanDeMejora.Remove(planDeMejora);
            db.BorrarPlan(codigoPlan);
            db.SaveChanges();
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
            if (!permissionManager.IsAuthorized(Permission.VER_OBJETIVOS))
            {
                TempData["alertmessage"] = "No tiene permisos para acceder a esta página";
                return RedirectToAction("../Home/Index");
            }
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

            List<AppIntegrador.Models.Persona> profesoresNombreLista = new List<Persona>();

            List<string> profesoresLista = db.ObtenerCorreosDeProfesoresDelPlan(id).ToList();
            
            foreach (var profe in profesoresLista)
            {
                profesoresNombreLista.Add(db.Persona.Find(profe));
            }

            ViewBag.ProfesoresNombreLista = profesoresNombreLista;

            List< AppIntegrador.Models.Formulario> formulariosNombreLista = new List<Formulario>();
            List<string> formulariosLista = db.ObtenerFormulariosAsociados(id).ToList();

            foreach (var form in formulariosLista)
            {
                formulariosNombreLista.Add(db.Formulario.Find(form));
            }

            ViewBag.Profesores = profesoresNombreLista;
            ViewBag.Formularios = formulariosNombreLista;
            return View("DetallesPlanDeMejora", planDeMejora);
        }
    }
}
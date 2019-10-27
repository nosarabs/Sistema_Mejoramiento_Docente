using System.Web.Mvc;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class PreguntasController : Controller
    {
        private DataIntegradorEntities db = new DataIntegradorEntities();

        // GET: Preguntas/Create
        public ActionResult Create()
        {
            ViewBag.Codigo = new SelectList(db.Pregunta_con_opciones, "Codigo", "TituloCampoObservacion");
            ViewBag.Codigo = new SelectList(db.Pregunta_con_respuesta_libre, "Codigo", "Codigo");
            return View();
        }


        // POST: Preguntas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pregunta pregunta)
        {
            if (ModelState.IsValid)
            {
                db.Pregunta.Add(pregunta);
                db.SaveChanges();
                return View();
            }

            return View(pregunta);
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

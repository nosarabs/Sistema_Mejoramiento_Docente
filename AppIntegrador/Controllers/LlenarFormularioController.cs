using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class LlenarFormularioController : Controller
    {
        private DataIntegradorEntities db;

        public LlenarFormularioController()
        {
            db = new DataIntegradorEntities();
        }

        // GET: LlenarFormulario
        public ActionResult MisFormularios()
        {
            List<Periodo_activa_por> periodos = ObtenerFormulariosDisponibles();

            List<FormularioAsignado> formularios = new List<FormularioAsignado>();

            foreach(var periodo in periodos)
            {
                FormularioAsignado formulario = new FormularioAsignado(periodo);
                formularios.Add(formulario);
            }

            return View(formularios);
        }

        private List<Periodo_activa_por> ObtenerFormulariosDisponibles()
        {
            List<Periodo_activa_por> formularios = new List<Periodo_activa_por>();

            String correo = HttpContext.User.Identity.Name;

            var formsDB = db.ObtenerFormulariosDeEstudiante(correo, null, null).ToList();

            foreach(var form in formsDB)
            {
                Periodo_activa_por periodo = new Periodo_activa_por
                {
                    CSigla = form.CSigla,
                    FCodigo = form.FCodigo,
                    GAnno = form.GAnno,
                    GNumero = form.GNumero,
                    GSemestre = form.GSemestre,
                    FechaInicio = form.FechaInicio,
                    FechaFin = form.FechaFin,
                };

                formularios.Add(periodo);
            }

            return formularios;
        }
    }
}
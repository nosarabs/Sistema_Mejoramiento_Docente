using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;

namespace AppIntegrador.Controllers
{
    public class LlenarFormularioController : Controller
    {
        private DataIntegradorEntities db;

        // Fechas en formato dd/MM
        const string InicioVerano = "01/01/";
        const string FinVerano = "07/03/";

        const string InicioPrimerSemestre = "08/03/";
        const string FinPrimerSemestre = "31/07/";

        const string InicioSegundoSemestre = "01/08/";
        const string FinSegundoSemestre = "31/12/";

        byte SemestreActual;

        readonly DateTime FechaActual;

        readonly DateTime FechaInicioVerano;
        readonly DateTime FechaFinVerano;

        readonly DateTime FechaInicioSemestre1;
        readonly DateTime FechaFinSemestre1;

        readonly DateTime FechaInicioSemestre2;
        readonly DateTime FechaFinSemestre2;

        public LlenarFormularioController()
        {
            db = new DataIntegradorEntities();

            FechaActual = DateTime.Now;

            FechaInicioVerano = DateTime.ParseExact(s: InicioVerano + (FechaActual.Year - 1).ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            FechaFinVerano = DateTime.ParseExact(s: FinVerano + (FechaActual.Year - 1).ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            FechaInicioSemestre1 = DateTime.ParseExact(s: InicioPrimerSemestre + FechaActual.Year.ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            FechaFinSemestre1 = DateTime.ParseExact(s: FinPrimerSemestre + FechaActual.Year.ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            FechaInicioSemestre2 = DateTime.ParseExact(s: InicioSegundoSemestre + FechaActual.Year.ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            FechaFinSemestre2 = DateTime.ParseExact(s: FinSegundoSemestre + FechaActual.Year.ToString(CultureInfo.InvariantCulture), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            SemestreActual = ObtenerSemestreActual();
        }

        public byte ObtenerSemestreActual()
        {
            // Verano
            if (FechaInicioVerano < FechaActual && FechaActual < FechaFinVerano)
            {
                return 3;
            }
            // Primer semestre
            else if (FechaInicioSemestre1 < FechaActual && FechaActual < FechaFinSemestre1)
            {
                return 1;
            }
            // Segundo semestre
            else
            {
                return 2;
            }
        }

        // GET: LlenarFormulario
        public ActionResult MisFormularios()
        {
            MisFormulariosModel modelo = new MisFormulariosModel();

            List<Periodo_activa_por> periodosSemestre = ObtenerFormulariosSemestre();
            List<Periodo_activa_por> periodosPasados = ObtenerFormulariosDisponibles(ObtenerFechaInicioSemestre(), null);

            foreach (var periodo in periodosSemestre)
            {
                FormularioAsignado formulario = new FormularioAsignado(periodo);
                modelo.FormulariosSemestre.Add(formulario);
            }

            foreach (var periodo in periodosPasados)
            {
                FormularioAsignado formulario = new FormularioAsignado(periodo);
                modelo.FormulariosPasados.Add(formulario);
            }

            return View(modelo);
        }

        private List<Periodo_activa_por> ObtenerFormulariosSemestre()
        {
            List<Periodo_activa_por> formularios = new List<Periodo_activa_por>();

            String correo = HttpContext.User.Identity.Name;

            var formsDB = db.ObtenerFormulariosPorSemestre(correo, DateTime.Now.Year, SemestreActual);

            foreach (var form in formsDB)
            {
                Periodo_activa_por periodo = new Periodo_activa_por
                {
                    CSigla = form.SiglaCurso,
                    FCodigo = form.Codigo,
                    GAnno = form.Anno,
                    GNumero = form.NumGrupo,
                    GSemestre = form.Semestre,
                    FechaInicio = form.FechaInicio,
                    FechaFin = form.FechaFin,
                };

                formularios.Add(periodo);
            }

            return formularios;
        }

        private List<Periodo_activa_por> ObtenerFormulariosDisponibles(DateTime? inicio, DateTime? fin)
        {
            List<Periodo_activa_por> formularios = new List<Periodo_activa_por>();

            String correo = HttpContext.User.Identity.Name;

            var formsDB = db.ObtenerFormulariosDeEstudiante(correo, inicio, fin).ToList();

            foreach(var form in formsDB)
            {
                Periodo_activa_por periodo = new Periodo_activa_por
                {
                    CSigla = form.SiglaCurso,
                    FCodigo = form.Codigo,
                    GAnno = form.Anno,
                    GNumero = form.NumGrupo,
                    GSemestre = form.Semestre,
                    FechaInicio = form.FechaInicio,
                    FechaFin = form.FechaFin,
                };

                formularios.Add(periodo);
            }

            return formularios;
        }

        private DateTime ObtenerFechaInicioSemestre()
        {
            // Verano
            if (FechaInicioVerano < FechaActual && FechaActual < FechaFinVerano)
            {
                return FechaInicioVerano;
            }
            // Primer semestre
            else if(FechaInicioSemestre1 < FechaActual && FechaActual < FechaFinSemestre1)
            {
                return FechaInicioSemestre1;
            }
            // Segundo semestre
            else
            {
                return FechaInicioSemestre2;
            }
        }

        private DateTime ObtenerFechaFinSemestre()
        {
            // Verano
            if (FechaInicioVerano < FechaActual && FechaActual < FechaFinVerano)
            {
                return FechaFinVerano;
            }
            // Primer semestre
            else if (FechaInicioSemestre1 < FechaActual && FechaActual < FechaFinSemestre1)
            {
                return FechaFinSemestre1;
            }
            // Segundo semestre
            else
            {
                return FechaFinSemestre2;
            }
        }

    }
}
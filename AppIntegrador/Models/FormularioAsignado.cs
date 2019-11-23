using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public class FormularioAsignado
    {
        public Periodo_activa_por Periodo { get; set; }
        public String Nombre { get; set; }

        public FormularioAsignado()
        {
            this.Periodo = null;
            this.Nombre = "Formulario inválido";
        }

        public FormularioAsignado(Periodo_activa_por periodo)
        {
            // Guardar objeto de periodo
            this.Periodo = periodo;

            // Basado en el objeto, guardar nombre del formulario
            using (DataIntegradorEntities db = new DataIntegradorEntities())
            {
                this.Nombre = db.Formulario.Find(this.Periodo.FCodigo).Nombre;
                db.Dispose();
            }
        }
    }
}
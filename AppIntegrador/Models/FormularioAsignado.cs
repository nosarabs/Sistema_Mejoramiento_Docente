using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public class FormularioAsignado
    {
        public Periodo_activa_por Periodo { get; }
        public string Nombre { get; }
        public string NombreCurso { get;}
        public int Estado { get; }

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

                this.NombreCurso = db.Curso.Find(this.Periodo.CSigla).Nombre;

                this.Estado = 3;
                db.Dispose();
            }
        }
    }
}
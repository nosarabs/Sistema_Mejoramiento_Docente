using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public class FormularioAsignado
    {
        public Formulario_activo Formulario { get; }
        public string Nombre { get; }
        public string NombreCurso { get;}
        public int Estado { get; }

        public FormularioAsignado()
        {
            this.Formulario = null;
            this.Nombre = "Formulario inválido";
        }

        public FormularioAsignado(Formulario_activo Formulario)
        {
            // Guardar objeto de periodo
            this.Formulario = Formulario;

            // Basado en el objeto, guardar nombre del formulario
            using (DataIntegradorEntities db = new DataIntegradorEntities())
            {
                this.Nombre = db.Formulario.Find(this.Formulario.FCodigo).Nombre;

                this.NombreCurso = db.Curso.Find(this.Formulario.CSigla).Nombre;

                var fecha = DateTime.Now;

                if(this.Formulario.FechaFin < fecha)
                {
                    // Finalizado
                    this.Estado = 1;
                }
                else if(this.Formulario.FechaInicio > fecha)
                {
                    // No disponible
                    this.Estado = 2;
                }
                else
                {
                    // Disponible
                    this.Estado = 3;
                }
                db.Dispose();
            }
        }
    }
}
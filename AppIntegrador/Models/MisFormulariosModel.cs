using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public class MisFormulariosModel
    {
        public List<FormularioAsignado> FormulariosSemestre { get; }
        public List<FormularioAsignado> FormulariosPasados { get; }

        public MisFormulariosModel()
        {
            FormulariosSemestre = new List<FormularioAsignado>();
            FormulariosPasados = new List<FormularioAsignado>();
        }
    }
}
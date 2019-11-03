using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public partial class PreguntaConNumeroSeccion
    {
        public int OrdenSeccion { get; set; }
        public int OrdenPregunta { get; set; }
        public Pregunta Pregunta { get; set; }
    }
}
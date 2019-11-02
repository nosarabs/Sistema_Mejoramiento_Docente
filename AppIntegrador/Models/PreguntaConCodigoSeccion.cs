using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public partial class PreguntaConCodigoSeccion
    {
        public string CodigoSeccion { get; set; }
        public Pregunta Pregunta { get; set; }
    }
}
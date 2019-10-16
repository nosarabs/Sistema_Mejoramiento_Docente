using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public class LlenarFormulario
    {
        public string Nombre { get; set; }
        public IEnumerable<SeccionConPreguntas> Secciones { get; set; }
    }
}
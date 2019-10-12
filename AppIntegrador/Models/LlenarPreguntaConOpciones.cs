using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public partial class LlenarPreguntaConOpciones
    {
        public string Enunciado { get; set; }
        public IEnumerable<string> Opciones { get; set; }
    }
}
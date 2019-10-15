using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public class TodasLasPreguntas
    {
        public IEnumerable<PreguntaConOpciones> PreguntasConOpciones { get; set; }
        // Luego van más atributos aquí con los otros modelos de las preguntas
    }
}
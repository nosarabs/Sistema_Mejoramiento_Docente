﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public partial class PreguntaConOpciones
    {
        public string CodigoSeccion { get; set; }
        public string CodigoPregunta { get; set; }
        public string Enunciado { get; set; }
        public IEnumerable<string> Opciones { get; set; }
    }
}
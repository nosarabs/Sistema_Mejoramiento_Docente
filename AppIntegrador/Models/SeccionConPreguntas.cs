﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public partial class SeccionConPreguntas
    {
        public string Nombre { get; set; }
        public TodasLasPreguntas Preguntas { get; set; }
    }
}
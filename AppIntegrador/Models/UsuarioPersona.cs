﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models
{
    public class UsuarioPersona
    {
        public Usuario Usuario { get; set; }

        public Persona Persona { get; set; }
    }
}
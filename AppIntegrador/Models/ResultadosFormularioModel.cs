using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIntegrador.Models;
namespace AppIntegrador.Models
{
    public class ResultadosFormulario
    {
        public String CodigoFormulario { get; set; }
        public String SiglaCurso { get; set; }
        public String NumeroGrupo { get; set; }
        public String Semestre { get; set; }
        public String Año { get; set; }
        public String Preguntas { get; set; }
    }
}
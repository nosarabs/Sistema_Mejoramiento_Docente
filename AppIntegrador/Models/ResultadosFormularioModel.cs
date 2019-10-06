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
        public Byte NumeroGrupo { get; set; }
        public Byte Semestre { get; set; }
        public Int32 Año { get; set; }
        public IEnumerable<SelectListItem> Preguntas { get; set; }
    }
}
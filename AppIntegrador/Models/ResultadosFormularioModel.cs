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
        public IEnumerable<SelectListItem> Preguntas { get; set; }
    }
}
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
    public class Dashboard
    {
    }

    public class UnidadesAcademicas
    {
        public String codigo { get; set; }
        public String nombre { get; set; }
    }

    public class CarrerasEnfasis
    {
        public String codigoCarrera { get; set; }
        public String nombreCarrera { get; set; }
        public String codigoEnfasis { get; set; }
        public String nombreEnfasis { get; set; }
    }

    public class CursoGrupo
    {
        public String siglaCurso { get; set; }
        public String nombreCurso { get; set; }
        public int numGrupo { get; set; }
        public int semestre { get; set; }
        public int anno { get; set; }

    }

    public class Profesores
    {
        public String nombre { get; set; }
        public String apellido1 { get; set; }
        public String apellido2 { get; set; }
    }

    public class Formularios
    {
        public String codigo { get; set; }
        public String nombre { get; set; }
    }
}
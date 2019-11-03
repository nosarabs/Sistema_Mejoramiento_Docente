using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LINQtoCSV;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using AppIntegrador.Models;

namespace AppIntegrador.Models
{
    public class ArchivoCSV
    {
        public String CodigoUnidad { get; set; }
        public String NombreFacultad { get; set; }
        public String CodigoCarrera { get; set; }
        public String NombreCarrera { get; set; }
        public String CodigoEnfasis { get; set; }
        public String NombreEnfasis { get; set; }
        public String SiglaCurso { get; set; }
        public String NombreCurso { get; set; }
        public String NumeroGrupo { get; set; }
        public String Anno { get; set; }
        public String Semestre { get; set; }
        public String CorreoProfesor { get; set; }
        public String IdProfesor { get; set; }
        public String NombreProfesor { get; set; }
        public String ApellidoProfesor { get; set; }
        public String TipoIdProfesor { get; set; }
        public String CorreoEstudiante { get; set; }
        public String IdEstudiante { get; set; }
        public String NombreEstudiante { get; set; }
        public String ApellidoEstudiante { get; set; }
        public String TipoIdEstudiante { get; set; }
    }
}
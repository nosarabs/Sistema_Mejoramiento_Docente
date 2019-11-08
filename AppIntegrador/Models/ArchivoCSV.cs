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
        public string CodigoUnidad { get; set; }
        public string NombreFacultad { get; set; }
        public string CodigoCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public string CodigoEnfasis { get; set; }
        public string NombreEnfasis { get; set; }
        public string SiglaCurso { get; set; }
        public string NombreCurso { get; set; }
        public string NumeroGrupo { get; set; }
        public string Anno { get; set; }
        public string Semestre { get; set; }
        public string CorreoProfesor { get; set; }
        public string IdProfesor { get; set; }
        public string NombreProfesor { get; set; }
        public string ApellidoProfesor { get; set; }
        public string TipoIdProfesor { get; set; }
        public string CorreoEstudiante { get; set; }
        public string IdEstudiante { get; set; }
        public string NombreEstudiante { get; set; }
        public string ApellidoEstudiante { get; set; }
        public string TipoIdEstudiante { get; set; }
    }
}
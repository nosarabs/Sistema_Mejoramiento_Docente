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
        public string CodigoUnidadCarrera { get; set; }
        public string CodigoCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public string CodigoCarreraEnfasis { get; set; }
        public string CodigoEnfasis { get; set; }
        public string NombreEnfasis { get; set; }
        public string CodigoCarreraCurso { get; set; }
        public string CodigoEnfasisCurso { get; set; }
        public string SiglaCurso { get; set; }
        public string NombreCurso { get; set; }
        public string SiglaCursoGrupo { get; set; }
        public string NumeroGrupo { get; set; }
        public string Semestre { get; set; }
        public string Anno { get; set; }
        public string CorreoPersona { get; set; }
        public string IdPersona { get; set; }
        public string TipoIdPersona { get; set; }
        public string NombrePersona { get; set; }
        public string ApellidoPersona { get; set; }
        public string CorreoProfesor { get; set; }
        public string CorreoEstudiante { get; set; }
        public string SiglaCursoImparte { get; set; }
        public string NumeroGrupoImparte { get; set; }
        public string SemestreGrupoImparte { get; set; }
        public string AnnoGrupoImparte { get; set; }
        public string SiglaCursoMatricula { get; set; }
        public string NumeroGrupoMatricula { get; set; }
        public string SemestreMatricula { get; set; }
        public string AnnoMatricula { get; set; }
        public string CorreoMatricula { get; set; }
    }
}
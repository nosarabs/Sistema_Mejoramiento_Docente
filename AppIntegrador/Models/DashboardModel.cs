﻿using System;
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

    //Clase para representar las Unidades Académicas
    public class UnidadesAcademicas
    {
        public String codigo { get; set; }
        public String nombre { get; set; }
    }

    //Clase para representar las Carreras con sus respectivos énfasis
    public class CarrerasEnfasis
    {
        public String codigoCarrera { get; set; }
        public String nombreCarrera { get; set; }
        public String codigoEnfasis { get; set; }
        public String nombreEnfasis { get; set; }
    }

    //Clase para representar los grupos asociados a un curso, los cuales están asociados a un período (semestre y año)
    public class CursoGrupo
    {
        public String siglaCurso { get; set; }
        public String nombreCurso { get; set; }
        public Nullable<byte> numGrupo { get; set; }
        public Nullable<byte> semestre { get; set; }
        public Nullable<int> anno { get; set; }

    }
}
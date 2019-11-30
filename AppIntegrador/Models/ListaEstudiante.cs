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
using System.ComponentModel.DataAnnotations;

namespace AppIntegrador.Models
{
    public class ListaEstudiante
    {
        [Required (AllowEmptyStrings = false , ErrorMessage = "Campo de correo vacío")]
        [EmailAddress   (ErrorMessage = "El correo no es valido")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CorreoPersona { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo de ID vacío")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IdPersona { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo de TipoID vacío")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TipoIdPersona { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo de Nombre vacío")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string NombrePersona { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo de Apellido vacío")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ApellidoPersona { get; set; }
        [Required]
        public string Borrado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo de CorreoEst vacío")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CorreoEstudiante { get; set; }
        public string CorreoEstudianteEmpadronado { get; set; }
        public string CodigoCarreraEmpadronado { get; set; }
        public string CodigoEnfasisEmpadronado { get; set; }
    }
}
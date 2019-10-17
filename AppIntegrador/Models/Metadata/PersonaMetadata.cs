using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models.Metadata
{
    public class PersonaMetadata
    {
        [Display(Name = "Correo Electrónico")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [Display(Name = "Correo Alternativo")]
        [DataType(DataType.EmailAddress)]
        public string CorreoAlt { get; set; }
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }
        [Display(Name = "Primer Nombre")]
        public string Nombre1 { get; set; }
        [Display(Name = "Segundo Nombre")]
        public string Nombre2 { get; set; }
        [Display(Name = "Primer Apellido")]
        public string Apellido1 { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string Apellido2 { get; set; }
        [Display(Name = "Tipo de Identificación")]
        public string TipoIdentificacion { get; set; }
    }
}
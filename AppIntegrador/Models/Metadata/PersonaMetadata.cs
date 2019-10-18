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
        [StringLength(50, ErrorMessage = "El correo no puede exceder los 50 caracteres. ")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [Display(Name = "Correo Alternativo")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "El correo no puede exceder los 50 caracteres. ")]
        public string CorreoAlt { get; set; }
        [StringLength(30, ErrorMessage = "La identificación no puede exceder los 30 caracteres. ")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }
        [Display(Name = "Primer Nombre")]
        [StringLength(15, ErrorMessage = "Este campo no puede exceder los 15 caracteres. ")]
        public string Nombre1 { get; set; }
        [Display(Name = "Segundo Nombre")]
        [StringLength(15, ErrorMessage = "Este campo no puede exceder los 15 caracteres. ")]
        public string Nombre2 { get; set; }
        [Display(Name = "Primer Apellido")]
        [StringLength(15, ErrorMessage = "Este campo no puede exceder los 15 caracteres. ")]
        public string Apellido1 { get; set; }
        [Display(Name = "Segundo Apellido")]
        [StringLength(15, ErrorMessage = "Este campo no puede exceder los 15 caracteres. ")]
        public string Apellido2 { get; set; }
        [Display(Name = "Tipo de Identificación")]
        public string TipoIdentificacion { get; set; }
    }
}
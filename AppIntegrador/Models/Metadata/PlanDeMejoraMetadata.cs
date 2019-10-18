using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppIntegrador.Models.Metadata
{
    public class PlanDeMejoraMetadata
    {
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Este campo es necesario")]
        [Range(0, int.MaxValue, ErrorMessage = "Este valor debe ser un número entero no negativo")]
        public int codigo { get; set; }

        [Display(Name = "Nombre")]
        [DataType(DataType.Text)]
        [MaxLength(30, ErrorMessage = "La longitud máxima de este campo es de 30 caracteres")]
        [Required]
        public string nombre { get; set; }

        [Display(Name = "Inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public Nullable<System.DateTime> fechaInicio { get; set; }

        [Display(Name = "Fin")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public Nullable<System.DateTime> fechaFin { get; set; }

    }
}
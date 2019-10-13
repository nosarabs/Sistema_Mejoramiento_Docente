using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppIntegrador.Models.Metadata
{
    public class PlanDeMejoraMetadata
    {
        [Display(Name = "Código del plan")]
        [Required(ErrorMessage = "Este campo es necesario")]
        [Range(0, int.MaxValue, ErrorMessage = "Este valor debe ser un número entero no negativo")]
        public int codigo { get; set; }

        [Display(Name = "Nombre del plan")]
        [DataType(DataType.Text)]
        [MaxLength(30, ErrorMessage = "La longitud máxima de este campo es de 30 caracteres")]
        public string nombre { get; set; }

        [Display(Name = "Fecha de inicio del plan")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> fechaInicio { get; set; }

        [Display(Name = "Fecha final del plan")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> fechaFin { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppIntegrador.Models.Metadata
{
    public class ObjetivoMetadata
    {
        [Display(Name = "Código del Plan")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int codPlan { get; set; }

        [Display(Name = "Nombre del objetivo")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string nombre { get; set; }

        [Display(Name = "Descripción del objetivo")]
        public string descripcion { get; set; }

        [Display(Name = "Fecha de inicio")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> fechaInicio { get; set; }

        [Display(Name = "Fecha final")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public Nullable<System.DateTime> fechaFin { get; set; }

        [Display(Name = "Tipo de objetivo")]
        public string nombTipoObj { get; set; }

        [Display(Name = "Código de la plantilla utilizada")]
        public Nullable<int> codPlantilla { get; set; }
    }
}
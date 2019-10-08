using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppIntegrador.Models.Metadata
{
    public class AccionDeMejoraMetadata
    {
        [Display(Name = "Codigo de la acción de mejora")]
        [Required(ErrorMessage = "Este campo es necesario")]
        [Range(0, int.MaxValue, ErrorMessage = "Este valor debe ser un entero no negativo")]
        public int Codigo { get; set; }
        [MaxLength(500, ErrorMessage = "Este campo acepta un máximo de 500 caracteres")]
        public string Descripcion { get; set; }
        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FechaInicio { get; set; }
        [Display(Name = "Fecha de cierre")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FechaFin { get; set; }
        [Display(Name = "Código del objetivo")]
        [Range(0, int.MaxValue, ErrorMessage = "Este valor debe ser un entero no negativo")]
        public int CodigoObj { get; set; }

        public virtual Objetivo Objetivo { get; set; }
    }
}
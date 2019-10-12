using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AppIntegrador.Models.Metadata
{
    public class PlanDeMejoraMetadata
    {
        [Display(Name = "Cédula del profesor")]
        [MaxLength(10, ErrorMessage ="La longitud máxima de este campo es de 10 caracteres.")]
        public string CedProf { get; set; }
        [Display(Name = "Código del plan")]
        [Required(ErrorMessage = "Este campo es necesario")]
        [Range(0, int.MaxValue, ErrorMessage = "Este valor debe ser un número entero no negativo")]
        public int Codigo { get; set; }
        [Display(Name = "Nombre del plan")]
        [DataType(DataType.Text)]
        [MaxLength(30, ErrorMessage = "La longitud máxima de este campo es de 30 caracteres")]
        public string Nombre { get; set; }
        [Display(Name = "Fecha de inicio del plan")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FechaInicio { get; set; }
        [Display(Name = "Fecha final del plan")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> FechaFin { get; set; }
        [Display(Name = "Código del formulario asignado al plan")]
        [MaxLength(8, ErrorMessage = "La longitud máxima de este campo es de 8 caracteres")]
        public string CodigoF { get; set; }
        [Display(Name = "Cédula del profesor asignado")]
        [MaxLength(10, ErrorMessage = "La longitud máxima de este campo es de 10 caracteres.")]
        public string CedProfAsig { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Objetivo> Objetivo { get; set; }
        public virtual Profesor Profesor { get; set; }
        public virtual Profesor Profesor1 { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppIntegrador.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Objetivo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Objetivo()
        {
            this.AccionDeMejora = new HashSet<AccionDeMejora>();
        }
    
        public int codPlan { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Nullable<System.DateTime> fechaInicio { get; set; }
        public Nullable<System.DateTime> fechaFin { get; set; }
        public string nombTipoObj { get; set; }
        public Nullable<int> codPlantilla { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccionDeMejora> AccionDeMejora { get; set; }
        public virtual PlantillaObjetivo PlantillaObjetivo { get; set; }
        public virtual TipoObjetivo TipoObjetivo { get; set; }
    }
}

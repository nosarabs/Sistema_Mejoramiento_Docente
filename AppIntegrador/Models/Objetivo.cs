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
    
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string TipoObjetivo { get; set; }
        public string CorreoProf { get; set; }
        public int CodigoPlan { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccionDeMejora> AccionDeMejora { get; set; }
        public virtual PlanDeMejora PlanDeMejora { get; set; }
        public virtual TipoObjetivo TipoObjetivo1 { get; set; }
    }
}
